using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            Debug.Log("There has to be a Player attached to the camera");

        }
        else
        {
            offset = gameObject.transform.position - player.transform.position;
            //offset = new Vector2(0,0);
        }
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void LateUpdate()
    {

        gameObject.transform.position = player.transform.position + offset;
        
        
    }

}
