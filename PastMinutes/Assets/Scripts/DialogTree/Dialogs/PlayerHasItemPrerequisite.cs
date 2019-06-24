﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DialogTree {
    [CreateAssetMenu(fileName = "HasItemPrerequisite", menuName = "Prerequisites/HasItemPrerequisite")]
    public class PlayerHasItemPrerequisite : Prerequisite
    {

        public string itemName;

        private InventoryComponent playerInventory;
       
        // Start is called before the first frame update
        void Start()
        {
            //GetPlayer
            playerInventory = FindObjectOfType<PlayerComponent>().gameObject.GetComponentInChildren<InventoryComponent>();
        }

        public override bool IsFullfilled()
        {
            if (playerInventory.ContainsItem(itemName))
            {
                return true;
            }
                return false;
        }

    }
}
