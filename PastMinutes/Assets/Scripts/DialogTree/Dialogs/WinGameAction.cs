using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "WinGameAction", menuName = "NPCAction/WinGameAction")]
public class WinGameAction : NPCAction
{
    public override void Act()
    {

        Debug.Log("yay we won!");
        EventManager.TriggerEvent(EventSystem.WinGame(), 0, new string[] { });
    }
}
