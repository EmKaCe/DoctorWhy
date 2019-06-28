using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDialogComponent : MonoBehaviour
{

    ScrollRect scrollRect;
    UnityAction<int, string[]> dialogInteractionListener;
    UnityAction<int, string[]> dialogOptionsListener;
    UnityAction<int, string[]> dialogEndListener;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI npcAnswer;
    public Button dialogOption;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = gameObject.GetComponentInChildren<ScrollRect>();
        scrollRect.gameObject.transform.parent.gameObject.SetActive(false);
        EventManager.StartListening(EventSystem.BeginConversation(), dialogInteractionListener);
        
    }

    private void Awake()
    {
        dialogInteractionListener = new UnityAction<int, string[]>(ActivateDialogView);
        dialogOptionsListener = new UnityAction<int, string[]>(SetDialog);
        dialogEndListener = new UnityAction<int, string[]>(DeactivateDialogView);
    }

    public void ActivateDialogView(int id, string[] charName)
    {
        scrollRect.gameObject.transform.parent.gameObject.SetActive(true);
        characterName.text = charName[0];
        EventManager.StartListening(EventSystem.SendDialogOptions(), dialogOptionsListener);
        EventManager.TriggerEvent(EventSystem.GetDialogOptions(), -1, charName);
        EventManager.StartListening(EventSystem.EndConversation(), dialogEndListener);
    }

    public void DeactivateDialogView(int i, string[] empty)
    {
        Debug.Log("how to end him rightly");
        EventManager.StopListening(EventSystem.SendDialogOptions(), dialogOptionsListener);
        EventManager.StopListening(EventSystem.EndConversation(), dialogEndListener);     
        scrollRect.gameObject.transform.parent.gameObject.SetActive(false);
        
    }

    /// <summary>
    /// Called when DialogComponent sends new dialog options
    /// first element of dialogs is npc answer
    /// </summary>
    /// <param name="index"></param>
    /// <param name="dialogs"></param>
    private void SetDialog(int index, string[] dialogs)
    {
        if(dialogs.Length > 0)
        {
            DialogTree.UIDialogItem[] items = DialogTree.UIDialogItem.ToUIDialogItem(dialogs);
            npcAnswer.text = items[0].dialog;
            for (int i = 1; i < items.Length; i++)
            {
                Button b = Instantiate(dialogOption, scrollRect.content);
                //needed cause delegate would otherwise use the actual i to find the position, which causes an exception
                int pos = items[i].index;
                b.onClick.AddListener(delegate { GetNextDialog(pos); });
                b.GetComponentInChildren<Text>().text = items[i].dialog;
            }
        }
        
    }

    public void GetNextDialog(int index)
    {
        foreach(Transform child in scrollRect.content.transform)
        {
            Destroy(child.gameObject);
        }
        EventManager.TriggerEvent(EventSystem.GetDialogOptions(), index, new string[] { });
    }
}
