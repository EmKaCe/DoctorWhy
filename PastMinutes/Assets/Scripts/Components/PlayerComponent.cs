using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    public string standardSpriteSheet;


    public Sprite GetHead()
    {
        return Resources.LoadAll<Sprite>("Characters/" + standardSpriteSheet)[4];
    }
}
