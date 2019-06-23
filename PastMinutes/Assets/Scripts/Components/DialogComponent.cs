using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogTree;

public class DialogComponent : MonoBehaviour
{
    public DialogTree.NodeSaver dialogTree;

    private StartDialogNode start;
    private DialogNode currentPos;
    //private DialogNode standardDialogNode;
    //private UIDialogItem[] standardDialog;
    private bool visited;

    // Start is called before the first frame update
    void Start()
    {
        if(dialogTree == null)
        {
            Debug.Log("Please add Dialog to dialogComponent of " + gameObject.name);
            enabled = false;
        }
        start = dialogTree.nodes.Find(n => n.node.GetType().Equals(typeof(StartDialogNode))).node as StartDialogNode;
        //standardDialogNode = start.outPoint[2].connectedNode;
        //List<UIDialogItem> standard = standardDialogNode.GetDialog();
        //standard.ForEach(d => d.option = UIDialogItem.DialogOption.standardNode);
        //standardDialog = standard.ToArray();

    }

    public UIDialogItem[] GetCurrentDialog()
    {
        List<UIDialogItem> res = currentPos.GetDialog();
        //res.AddRange(standardDialog);
        return res.ToArray();
    }

    public UIDialogItem[] GetNextDialog(int index)
    {
        currentPos = currentPos.outPoint[index].connectedNode;
        List<UIDialogItem> res = currentPos.GetDialog();       
        //res.AddRange(standardDialog);
        return res.ToArray();
    }

    public void BeginnConversation()
    {
        
        if (visited)
        {
            currentPos = start.outPoint[1].connectedNode;
        }
        else
        {
            visited = true;
            currentPos = start.outPoint[0].connectedNode;
        }
    }
}
