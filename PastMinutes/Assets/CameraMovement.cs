using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public bool alternativeCamera;

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
        if (alternativeCamera)
        {
            Vector2 topRight = GetCameraToWorldSpace();
            Vector2 cameraPos = new Vector2(topRight.x / 2, topRight.y / 2);
            if(player.transform.position.x > (topRight.x * 0.8))
            {
               gameObject.transform.position = new Vector3 (player.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
        else
        {
            gameObject.transform.position = player.transform.position + offset;
        }
        
    }

    private Vector2 GetCameraToWorldSpace()
    {
        Vector2 topRight = new Vector2(1, 1);
        return Camera.main.ViewportToWorldPoint(topRight);
    }
}
