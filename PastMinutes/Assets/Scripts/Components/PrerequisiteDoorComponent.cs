using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrerequisiteDoorComponent : MonoBehaviour
{
    public Vector3 offset;
    public PrerequisiteDoorComponent otherDoor;
    public DialogTree.Prerequisite prerequisite;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        TagSystem tags = collider.gameObject.GetComponentInParent<TagSystem>();
        if ((tags.Player || tags.NPC) && !collider.isTrigger)
        {
            if (prerequisite.IsFullfilled())
            {
                Vector3 newPos = otherDoor.transform.position + offset;
                newPos.z = collider.gameObject.transform.position.z;
                collider.gameObject.transform.position = newPos;
            }
        }
        
       
    }
}
