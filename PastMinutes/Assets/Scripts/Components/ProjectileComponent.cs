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
}
