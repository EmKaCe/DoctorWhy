using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed;
    private Rigidbody2D rigid;
    private Animator anim;
    public int direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = -1;
        anim = gameObject.GetComponent<Animator>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        if(rigid == null)
        {
            Debug.Log("The player character needs a 2d rigidbody");
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
        if (anim == null)
        {
            Debug.Log("The player character needs an Animator");
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rigid.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;       
        direction = GetDirection(horizontalInput, verticalInput);
        anim.SetInteger("Direction", direction);
        rigid.MovePosition(newPos);
    }

    private int GetDirection(float horizontalMovement, float verticalMovement)
    {
        if (horizontalMovement == 0)
        {
            if(verticalMovement > 0)
            {
                //North
                anim.SetBool("Walking", true);
                return 0; 
            }
            else if(verticalMovement < 0)
            {
                //South
                anim.SetBool("Walking", true);
                return 4;
            }
            else
            {
                //Idle
                anim.SetBool("Walking", false);
                return direction;
            }
        }
        else if(horizontalMovement > 0)
        {
            if (verticalMovement > 0)
            {
                //North-East
                anim.SetBool("Walking", true);
                return 1;
            }
            else if (verticalMovement < 0)
            {
                //South-East
                anim.SetBool("Walking", true);
                return 3;
            }
            else
            {
                //East
                anim.SetBool("Walking", true);
                return 2;
            }
        }
        else
        {
            if (verticalMovement > 0)
            {
                //North-West
                anim.SetBool("Walking", true);
                return 7;
            }
            else if (verticalMovement < 0)
            {
                //South-West
                anim.SetBool("Walking", true);
                return 5;
            }
            else
            {
                //West
                anim.SetBool("Walking", true);
                return 6;
            }
        }
    }
}
