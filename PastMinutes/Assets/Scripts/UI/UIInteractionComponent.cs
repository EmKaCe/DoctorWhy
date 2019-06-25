using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIInteractionComponent : MonoBehaviour
{
    [Header("Message shown to player when close to object")]
    public Text displayMessage;
    [Header("Message shown to player specifing which button to press to interact with object")]
    public Text buttonPressText;
    UnityAction<int, string[]> interactionTriggeredListener;
    UnityAction<int, string[]> interactionExitedListener;

    private void Awake()
    {
        interactionTriggeredListener = new UnityAction<int, string[]>(ChangeDisplayMessage);
        interactionExitedListener = new UnityAction<int, string[]>(ResetDisplayMessage);
    }
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(EventSystem.InteractionTriggered(), interactionTriggeredListener);
        EventManager.StartListening(EventSystem.InteractionExited(), interactionExitedListener);
    }

    public void ChangeDisplayMessage(int nothing, string[] content)
    {
        displayMessage.text = content[0];
        if (typeof(PickUpComponent).GetType().ToString().Equals(content[1]))
        {
            buttonPressText.text = "Press (e)";
        }
        else
        {
            Debug.Log(typeof(PickUpComponent).GetType().ToString());
            buttonPressText.text = content[1];
        }
    }

    public void ResetDisplayMessage(int nothing, string[] contentBefore)
    {
        if (displayMessage.Equals(contentBefore[0]))
        {
            displayMessage.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
