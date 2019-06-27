using System.Linq;
using UnityEngine;
namespace DialogTree {
    [CreateAssetMenu(fileName = "HasItemPrerequisite", menuName = "Prerequisites/HasItemPrerequisite")]
    public class PlayerHasItemPrerequisite : Prerequisite
    {

        public string itemName;

        [HideInInspector]
        public InventoryComponent playerInventory;

        public override bool IsFullfilled()
        {
            playerInventory = FindObjectsOfType<TagSystem>().Where(t => t.Player).First().gameObject.GetComponentInChildren<InventoryComponent>();
            if (playerInventory.ContainsItem(itemName))
            {
                return true;
            }
            return false;
        }

    }
}
