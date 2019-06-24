using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GiveItemNPCAction", menuName = "NPCAction/GiveItemNPCAction")]
public class GiveItemAction : NPCAction
{
    public string itemName;


    public override void Act()
    {
        EventManager.TriggerEvent(EventSystem.AddItemToInventory(), 0, null);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
