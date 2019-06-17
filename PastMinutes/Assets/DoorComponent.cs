using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorComponent : MonoBehaviour
{
    public BoxCollider2D otherPoint;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        TagSystem tags = collider.gameObject.GetComponentInParent<TagSystem>();
        if (tags.Player || tags.NPC)
        {
            Vector3 newPos = otherPoint.transform.position + offset;
            newPos.z = collider.gameObject.transform.position.z;
            collider.gameObject.transform.parent.position = newPos;
        }
    }
}
