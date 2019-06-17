using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IsometricSpriteRenderComponent : MonoBehaviour
{
    public List<SpriteRenderer> ren;
    public bool staticObject;

    // Update is called once per frame
    void Update()
    {

        foreach(SpriteRenderer r in ren)
        {
            r.sortingOrder = (int)((transform.position.y * -10) + 23.5);
        }
        if (staticObject)
        {
            enabled = false;
        }
    }

    private void Start()
    {
        foreach (SpriteRenderer r in ren)
        {
            r.sortingOrder = (int)transform.position.y * -10;
        }
        
    }
}
