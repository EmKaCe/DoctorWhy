using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "StartEndAction", menuName = "NPCAction/StartEndAction")]
public class StartEndAction : NPCAction
{
    public override void Act() {
        Debug.Log("Shila: Tanz für mich terraformer, tanz!");
        EventManager.TriggerEvent(EventSystem.StartWorldEnd(), 0, new string[] { });
    }
    
}
