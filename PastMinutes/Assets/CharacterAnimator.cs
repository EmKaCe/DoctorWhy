using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("The player character needs an Animator");
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }


    public void TurnAndWalk(int movement, string[] walking)
    {
        bool res = bool.Parse(walking[0]);
        //anim.SetBool("Walking", res);
        //walking
        if (res)
        {
            switch (movement)
            {
                case 0:
                    anim.Play("WalkNorth");
                    break;
                case 1:
                    anim.Play("WalkNorthEast");
                    break;
                case 2:
                    anim.Play("WalkEast");
                    break;
                case 3:
                    anim.Play("WalkSouthEast");
                    break;
                case 4:
                    anim.Play("WalkSouth");
                    break;
                case 5:
                    anim.Play("WalkSouthWest");
                    break;
                case 6:
                    anim.Play("WalkWest");
                    break;
                case 7:
                    anim.Play("WalkNorthWest");
                    break;
                default:
                    anim.Play("Idle");
                    break;
            }
        }
        //turning
        else
        {
            switch (movement)
            {
                case 0:
                    anim.Play("TurnNorth");
                    break;
                case 1:
                    anim.Play("TurnNorthEast");
                    break;
                case 2:
                    anim.Play("TurnEast");
                    break;
                case 3:
                    anim.Play("TurnSouthEast");
                    break;
                case 4:
                    anim.Play("TurnSouth");
                    break;
                case 5:
                    anim.Play("TurnSouthWest");
                    break;
                case 6:
                    anim.Play("TurnWest");
                    break;
                case 7:
                    anim.Play("TurnNorthWest");
                    break;
                default:
                    anim.Play("Idle");
                    break;
            }
        }
        
    }

   
}
