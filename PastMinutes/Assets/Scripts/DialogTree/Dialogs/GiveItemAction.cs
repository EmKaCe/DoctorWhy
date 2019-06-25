using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "GiveItemNPCAction", menuName = "NPCAction/GiveItemNPCAction")]
public class GiveItemAction : NPCAction
{
    
    public ItemComponent item;

    public override void Act()
    {
        EventManager.TriggerEvent(EventSystem.AddItemToInventory(), Instantiate(item.gameObject).GetInstanceID(), new string[] { FindObjectsOfType<TagSystem>().Where(t => t.Player).First().gameObject.GetComponent<EntityComponent>().entityID.ToString() });
    }
}
