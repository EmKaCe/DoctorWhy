using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public static string[] ToStringArray(UIDialogItem[] items)
        {
            string[] res = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                res[i] = items[i].index + ":" + items[i].dialog;
            }
            return res;
        }

        public static UIDialogItem[] ToUIDialogItem(string[] dialogs)
        {
            List<UIDialogItem> res = new List<UIDialogItem>();
            res.AddRange(dialogs.Select(d => new UIDialogItem(d.Split(':')[1], int.Parse(d.Split(':')[0]))));
           // res.ForEach(i => Debug.Log("Method: " + i.index + ": " + i.dialog));
            return res.ToArray();
        }
    }
}
