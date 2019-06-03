using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
public class PlayerController : MonoBehaviour
{

    public bool gunTest;
    public float movementSpeed;
    private Rigidbody2D rigid;
    private CharacterAnimator cAnim;
    public int direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = -1;        
        rigid = gameObject.GetComponent<Rigidbody2D>();
        cAnim = gameObject.GetComponent<CharacterAnimator>();
        if(rigid == null)
        {
            Debug.Log("The player character needs a 2d rigidbody");
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
        if(cAnim == null)
        {
            Debug.Log("The character `" + gameObject.name + "´ needs a CharcterAnimator");
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
        bool walking = false;
        if(movement.magnitude > .01f)
        {
            walking = true;
        }
        cAnim.TurnAndWalk(direction, new string[] { walking.ToString(), gunTest.ToString() });
        rigid.MovePosition(newPos);
    }

    private int GetDirection(float horizontalMovement, float verticalMovement)
    {
        if (horizontalMovement == 0)
        {
            if(verticalMovement > 0)
            {
                //North
                return 0; 
            }
            else if(verticalMovement < 0)
            {
                //South
                return 4;
            }
            else
            {
                //Idle
                return direction;
            }
        }
        else if(horizontalMovement > 0)
        {
            if (verticalMovement > 0)
            {
                //North-East
                return 1;
            }
            else if (verticalMovement < 0)
            {
                //South-East
                return 3;
            }
            else
            {
                //East
                return 2;
            }
        }
        else
        {
            if (verticalMovement > 0)
            {
                //North-West
                return 7;
            }
            else if (verticalMovement < 0)
            {
                //South-West
                return 5;
            }
            else
            {
                //West
                return 6;
            }
        }
    }
}
