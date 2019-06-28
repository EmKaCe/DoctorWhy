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
        bool shoot = bool.Parse(walking[1]);
        //anim.SetBool("Walking", res);
        //walking
        if (shoot)
        {
            if (res)
            {
                switch (movement)
                {
                    case 0:
                        anim.Play("PistolWalkNorth");
                        break;
                    case 1:
                        anim.Play("PistolWalkNorthEast");
                        break;
                    case 2:
                        anim.Play("PistolWalkEast");
                        break;
                    case 3:
                        anim.Play("PistolWalkSouthEast");
                        break;
                    case 4:
                        anim.Play("PistolWalkSouth");
                        break;
                    case 5:
                        anim.Play("PistolWalkSouthWest");
                        break;
                    case 6:
                        anim.Play("PistolWalkWest");
                        break;
                    case 7:
                        anim.Play("PistolWalkNorthWest");
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
                        anim.Play("PistolTurnNorth");
                        break;
                    case 1:
                        anim.Play("PistolTurnNorthEast");
                        break;
                    case 2:
                        anim.Play("PistolTurnEast");
                        break;
                    case 3:
                        anim.Play("PistolTurnSouthEast");
                        break;
                    case 4:
                        anim.Play("PistolTurnSouth");
                        break;
                    case 5:
                        anim.Play("PistolTurnSouthWest");
                        break;
                    case 6:
                        anim.Play("PistolTurnWest");
                        break;
                    case 7:
                        anim.Play("PistolTurnNorthWest");
                        break;
                    default:
                        anim.Play("Idle");
                        break;
                }
            }
        }
        //No gun equipped
        else
        {
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

   
}
