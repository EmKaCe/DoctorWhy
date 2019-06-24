using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogTree;

[RequireComponent(typeof(EntityComponent), typeof(CircleCollider2D))]
public class DialogComponent : MonoBehaviour
{
    public DialogTree.NodeSaver dialogTree;
    public int index;
    public bool talking;
    private StartDialogNode start;
    private DialogNode currentPos;
    //private DialogNode standardDialogNode;
    //private UIDialogItem[] standardDialog;
    private bool visited;
    public List<Prerequisite> prerequisites;

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

    }

    private void Update()
    {
            if (Input.GetKeyDown(KeyCode.U))
            {
            Debug.Log("Test");
                BeginConversation();
                GetCurrentDialog();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GetNextDialog(index);
            }
        
    }

    public UIDialogItem[] GetCurrentDialog()
    {
        List<UIDialogItem> res = currentPos.GetDialog(dialogTree, currentPos);
        //res.AddRange(standardDialog);
        foreach (UIDialogItem re in res)
        {
            Debug.Log(re.index + ": " + re.dialog);
        }
        return res.ToArray();
    }

    public UIDialogItem[] GetNextDialog(int index)
    {
        currentPos = dialogTree.connections.Find(c => c.endNode.Equals(currentPos) && c.outIndex == index).startNode;
        //currentPos = currentPos.outPoint[index].connectedNode;
        //No further dialog
        if(currentPos == null || currentPos.GetType().Equals(typeof(ExitNode)))
        {
            EndConversation();
        }
        List<UIDialogItem> res = currentPos.GetDialog(dialogTree, currentPos);
        //res.AddRange(standardDialog);
        foreach (UIDialogItem re in res)
        {
            Debug.Log(re.index + ": " + re.dialog);
        }
        return res.ToArray();
    }

    public void BeginConversation()
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
}
