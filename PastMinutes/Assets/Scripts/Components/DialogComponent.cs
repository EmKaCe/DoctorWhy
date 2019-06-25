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

    UnityAction<int, string[]> conversationStartListener;
    UnityAction<int, string[]> conversationProgressListener;

    private void Awake()
    {
        conversationStartListener = new UnityAction<int, string[]>(BeginConversation);
        conversationProgressListener = new UnityAction<int, string[]>(GetDialog);
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
        Debug.Log(start);
        List<DialogTree.StandardNodeSave> l = dialogTree.nodes.FindAll(n => n.node.GetType().Equals(typeof(PrerequisiteNode)));
        foreach(DialogTree.StandardNodeSave s in l)
        {
            prerequisites.Add((s.node as PrerequisiteNode).prerequisite);
        }
        //standardDialogNode = start.outPoint[2].connectedNode;
        //List<UIDialogItem> standard = standardDialogNode.GetDialog();
        //standard.ForEach(d => d.option = UIDialogItem.DialogOption.standardNode);
        //standardDialog = standard.ToArray();

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
        foreach (UIDialogItem re in res)
        {
            Debug.Log(re.index + ": " + re.dialog);
        }
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
        string[] res = new string[dialog.Length];
        for(int i = 0; i < dialog.Length; i++)
        {
            res[i] = dialog[i].index + ":" + dialog[i].dialog;
        }
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
        //res.AddRange(standardDialog);
        foreach (UIDialogItem re in res)
        {
            Debug.Log(re.index + ": " + re.dialog);
        }
        return res.ToArray();
    }

    public void BeginConversation(int nothing, string[] empty)
    {
        talking = true;
        Debug.Log("Begin dialog");
        EventManager.TriggerEvent(EventSystem.BeginConversation(), gameObject.GetComponentInParent<EntityComponent>().entityID, new string[] { });
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
    }

    public void EndConversation()
    {
        talking = false;
        Debug.Log("End dialog");
        EventManager.TriggerEvent(EventSystem.EndConversation(), gameObject.GetComponentInParent<EntityComponent>().entityID, new string[] { });
    }

    public override void StartInteraction()
    {
        Debug.Log("Trigger works");
        EventManager.StartListening(EventSystem.StartConversation(), conversationStartListener);
        //EventManager.StartListening(EventSystem.conver)
    }

    public override void StopInteraction()
    {
        EventManager.StopListening(EventSystem.StartConversation(), conversationStartListener);
        if (talking)
        {
            EndConversation();
        }
    }

    public void OnDisable()
    {
        EventManager.StopListening(EventSystem.StartConversation(), conversationStartListener);
    }
}
