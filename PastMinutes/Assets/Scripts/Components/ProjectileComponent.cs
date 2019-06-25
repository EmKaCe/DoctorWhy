using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ProjectileComponent : MonoBehaviour
{

    public float speed;
    public float seconds;
    private float startingTime;
    private float endTime;
    public Vector2 direction;
    public float damage;
    [HideInInspector]
    //Id of character that shot the bullet
    public int ownerID;

    // Start is called before the first frame update
    void Start()
    {
        startingTime = Time.time;
        endTime = startingTime + seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if(endTime > Time.time)
        {
            gameObject.transform.position += (Vector3) (direction * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        TagSystem tag;
        if ((tag = collision.gameObject.GetComponent<TagSystem>()) != null)
        {
            if (tag.Player || tag.NPC)
            {
                Debug.Log("Treffer");
                EventManager.TriggerEvent(EventSystem.TakeDamage(), tag.gameObject.GetComponent<EntityComponent>().entityID, new string[] { damage.ToString(), ownerID.ToString() });
            }
        }
        Destroy(gameObject);
    }
}
