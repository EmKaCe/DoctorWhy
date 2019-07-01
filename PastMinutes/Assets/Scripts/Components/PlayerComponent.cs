using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    public string standardSpriteSheet;
    public float damage;
    public float timeBetweenAttacks;
    public float meleeRange;


    public Sprite GetHead()
    {
        return Resources.LoadAll<Sprite>("Characters/" + standardSpriteSheet)[4];
    }
}
