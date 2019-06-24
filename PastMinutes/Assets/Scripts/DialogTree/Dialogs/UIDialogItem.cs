using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DialogTree
{
    public class UIDialogItem
    {

        public enum DialogOption
        {
            baseNode,
            standardNode
        }

        public string dialog;
        public int index;
       // public DialogOption option;

        public UIDialogItem(string dialog, int index)
        {
            this.dialog = dialog;
            this.index = index;
        }
    }
}
