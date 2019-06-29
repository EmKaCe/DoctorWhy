using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDialogs : MonoBehaviour
{
    ScrollRect scrollRect;
    UnityAction<int, string[]> dialogInteractionListener;
    UnityAction<int, string[]> dialogOptionsListener;
    UnityAction<int, string[]> dialogEndListener;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI npcAnswer;
    public Button dialogOption;
    public Button UIReplyBack;
    public Button UIReplyForward;
    public GridLayoutGroup repliesGrid;
    private Button[] replyButtons;
    private int from;
    private int to;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = gameObject.GetComponentInChildren<ScrollRect>();
        scrollRect.gameObject.transform.parent.gameObject.SetActive(false);
        EventManager.StartListening(EventSystem.BeginConversation(), dialogInteractionListener);
        UIReplyForward.onClick.AddListener(delegate { goForward(); });
        UIReplyBack.onClick.AddListener(delegate { goBack(); });
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
        EventManager.StopListening(EventSystem.SendDialogOptions(), dialogOptionsListener);
        EventManager.StopListening(EventSystem.EndConversation(), dialogEndListener);
        scrollRect.gameObject.transform.parent.gameObject.SetActive(false);
    }

    private void SetDialog(int index, string[] dialogs)
    {
        UIReplyBack.interactable = false;
        if (dialogs.Length > 0)
        {
            DialogTree.UIDialogItem[] items = DialogTree.UIDialogItem.ToUIDialogItem(dialogs);
            npcAnswer.text = items[0].dialog;
            replyButtons = new Button[items.Length - 1];
            for (int i = 1; i < items.Length; i++)
            {
                Button b = Instantiate(dialogOption);
                int pos = items[i].index;
                b.onClick.AddListener(delegate { GetNextDialog(pos); });
                b.GetComponentInChildren<TextMeshProUGUI>().text = items[i].dialog;
                replyButtons[i - 1] = b;
            }
            displayReplies();
            
        }
    }

    private void displayReplies()
    {
        if (replyButtons.Length <= 3)
        {
            UIReplyBack.interactable = false;
            UIReplyForward.interactable = false;
            setButtons(0, replyButtons.Length, false);
        }
        else
        {
            UIReplyBack.interactable = false;
            UIReplyForward.interactable = true;
            from = 0;
            to = 3;
            setButtons(from, to, false);
        }
        
    }

    private void setButtons(int current, int max, bool clear)
    {
        if(clear)
        {
            for (int i = current; i < max; i++)
            {
                replyButtons[i].transform.SetParent(null);
            }
        }
        else
        {
            for (int i = current; i < max; i++)
            {
                replyButtons[i].transform.SetParent(repliesGrid.transform);
            }
        }
    }

    private void goForward()
    {
        Debug.Log(from + " " + to);
        setButtons(from, to, true);
        from = to;
        to += 3;
        if(from > replyButtons.Length - 3)
        {
            UIReplyForward.interactable = false;         
        }
        if (to >= replyButtons.Length)
        {
            to = replyButtons.Length;
        }
        Debug.Log(from + " " + to);
        setButtons(from, to, false);
        UIReplyBack.interactable = true;
    }

    private void goBack()
    {
        setButtons(from, to, true);
        to = from;
        from -= 3;
        if(from <= 0)
        {
            from = 0;
            if(replyButtons.Length <= 3)
            {
                to = replyButtons.Length;
            }
            UIReplyBack.interactable = false;
        }
        UIReplyForward.interactable = true;
        setButtons(from, to, false);
    }

    private void destroyButtons()
    {
        for(int i = 0; i < replyButtons.Length; i++)
        {
            Destroy(replyButtons[i]);
        }
    }

    public void GetNextDialog(int index)
    {
        setButtons(0, replyButtons.Length, true);
        destroyButtons();
        replyButtons = null;
        UIReplyForward.interactable = false;
        UIReplyBack.interactable = false;
        EventManager.TriggerEvent(EventSystem.GetDialogOptions(), index, new string[] { });
    }
}
