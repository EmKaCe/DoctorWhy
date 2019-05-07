using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{

    public float weight;
    [Header("Size in cm:                       length             width            height")]
    [Tooltip("Arbitary text message")]
    public Vector3 size;

    //[ContextMenuItem("Reset", "ResetBiography")]
    //[Multiline(8)]
    //public string playerBiography = "";

    //void ResetBiography()
    //{
    //    playerBiography = "";
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
