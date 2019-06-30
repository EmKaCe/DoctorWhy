using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogTree;
using UnityEngine.Events;

[RequireComponent(typeof(EntityComponent), typeof(CircleCollider2D))]
public class DialogComponent : InteractionComponent
{
    public DialogTree.NodeSaver dialogTree;
    public bool talking;
    private StartDialogNode start;
    private DialogNode currentPos;
    //private DialogNode standardDialogNode;
    //private UIDialogItem[] standardDialog;
    private bool visited;
    public List<Prerequisite> prerequisites;

    //UnityAction<int, string[]> conversationStartListener;
    UnityAction<int, string[]> conversationProgressListener;
    UnityAction<int, string[]> triggerDialogListener;


    protected override void Awake()
    {
        base.Awake();
        conversationProgressListener = new UnityAction<int, string[]>(GetDialog);
        triggerDialogListener = new UnityAction<int, string[]>(TriggerDialog);
    }

    // Start is called before the first frame update
    void Start()
    {
        talking = false;
        if(dialogTree == null)
        {
            Debug.Log("Please add Dialog to dialogComponent of " + gameObject.name);
            enabled = false;
        }
        start = dialogTree.nodes.Find(n => n.node.GetType().Equals(typeof(StartDialogNode))).node as StartDialogNode;
        List<DialogTree.StandardNodeSave> l = dialogTree.nodes.FindAll(n => n.node.GetType().Equals(typeof(PrerequisiteNode)));
        foreach(DialogTree.StandardNodeSave s in l)
        {
            prerequisites.Add((s.node as PrerequisiteNode).prerequisite);
        }
        //standardDialogNode = start.outPoint[2].connectedNode;
        //List<UIDialogItem> standard = standardDialogNode.GetDialog();
        //standard.ForEach(d => d.option = UIDialogItem.DialogOption.standardNode);
        //standardDialog = standard.ToArray();
        EventManager.StartListening(EventSystem.TriggerDialog(), triggerDialogListener);
    }

    public UIDialogItem[] GetCurrentDialog()
    {
        if (currentPos == null || currentPos.GetType().Equals(typeof(ExitNode)))
        {
            EndConversation();
        }
        if (currentPos.GetType().Equals(typeof(NPCActionNode)))
        {
            (currentPos as NPCActionNode).action.Act();
            return GetNextDialog(0);
        }
        List<UIDialogItem> res = currentPos.GetDialog(dialogTree, currentPos);
        //res.AddRange(standardDialog);
        //foreach (UIDialogItem re in res)
        //{
        //    Debug.Log(re.index + ": " + re.dialog);
        //}
        return res.ToArray();
    }

    public void GetDialog(int index, string[] empty)
    {
        UIDialogItem[] dialog;
        if(index >= 0)
        {
            dialog = GetNextDialog(index);
        }
        else
        {
            dialog = GetCurrentDialog();
        }
        string[] res = UIDialogItem.ToStringArray(dialog);
        EventManager.TriggerEvent(EventSystem.SendDialogOptions(), index, res);
    }

    public UIDialogItem[] GetNextDialog(int index)
    {
        if(dialogTree.connections.Find(c => c.endNode.Equals(currentPos) && c.outIndex == index) == null)
        {
            EndConversation();
            return new UIDialogItem[] { };
        }
        else
        {
            currentPos = dialogTree.connections.Find(c => c.endNode.Equals(currentPos) && c.outIndex == index).startNode;
        }      
        //currentPos = currentPos.outPoint[index].connectedNode;
        //No further dialog
        if (currentPos == null || currentPos.GetType().Equals(typeof(ExitNode)))
        {
            EndConversation();
        }
        if (currentPos.GetType().Equals(typeof(NPCActionNode)))
        {
            (currentPos as NPCActionNode).action.Act();
            return GetNextDialog(0);
        }
        List<UIDialogItem> res = currentPos.GetDialog(dialogTree, currentPos);
        return res.ToArray();
    }

    public void BeginConversation()
    {
        talking = true;
        if (visited)
        {
            currentPos = dialogTree.connections.Find(c => c.endNode.Equals(start) && c.outIndex == 1).startNode;
            //currentPos = start.outPoint[1].connectedNode;
        }
        else
        {
            visited = true;
            currentPos = dialogTree.connections.Find(c => c.endNode.Equals(start) && c.outIndex == 0).startNode;
            //currentPos = start.outPoint[0].connectedNode;
        }
        EventManager.StartListening(EventSystem.GetDialogOptions(), conversationProgressListener);
        EventManager.TriggerEvent(EventSystem.BeginConversation(), gameObject.GetComponentInParent<EntityComponent>().entityID, new string[] { gameObject.name });
        
        
    }

    public void EndConversation()
    {
        talking = false;
        EventManager.TriggerEvent(EventSystem.EndConversation(), gameObject.GetComponentInParent<EntityComponent>().entityID, new string[] { });
        EventManager.StopListening(EventSystem.GetDialogOptions(), conversationProgressListener);
    }

    public override void StopInteraction()
    {
        base.StopInteraction();
        if (talking)
        {
            EndConversation();
        }
    }

    public override void Action(int entityId, string[] values)
    {
        EventManager.TriggerEvent(EventSystem.StowGun(), entityId, new string[] { });
        BeginConversation();
    }

    public override void Quit(int entityId, string[] values)
    {
        //do nothing
    }

    public override void ComponentHasParent()
    {
        
    }

    public void TriggerDialog(int entityID, string[] id)
    {
        int.TryParse(id[0], out int ownID);
        if(ownID == gameObject.GetComponent<EntityComponent>().entityID)
        {
            Action(entityID, id);
        }
    }
}
