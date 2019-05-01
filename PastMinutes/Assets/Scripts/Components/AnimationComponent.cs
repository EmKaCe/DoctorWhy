using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to sprites of characters
/// </summary>
public class AnimationComponent : MonoBehaviour
{


    public string spritesheet;
    public bool sheetChanged;
    private int startingIndex;
    private SpriteRenderer spriteRenderer;
    Sprite[] sprites;


    // Start is called before the first frame update
    void Start()
    {
        if (spritesheet.Equals(""))
        {
            spritesheet = (gameObject.GetComponentInParent<PlayerComponent>() as PlayerComponent).standardSpriteSheet;
        }
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if(Enum.TryParse(name, out PartFindingSystem.BodyParts part))
        {
            startingIndex = (int) part;
        }
        sprites = Resources.LoadAll<Sprite>("Characters/" + spritesheet);
        sheetChanged = false;

        //TurnLeft();
    }

    private void LateUpdate()
    {
        if (sheetChanged)
        {
            sheetChanged = false;
            sprites = Resources.LoadAll<Sprite>("Characters/" + spritesheet);
        }
    }

    public void SetSheetChanged(bool changed)
    {
        this.sheetChanged = changed;
    }

    public void SheetChanged()
    {
        sheetChanged = true;
    }


    public void TurnFront()
    {
        spriteRenderer.flipX = false;
        spriteRenderer.sprite = sprites[startingIndex + 4];
    }

    public void TurnBack()
    {
        spriteRenderer.flipX = false;
        spriteRenderer.sprite = sprites[startingIndex + 3];
        
    }

    public void TurnLeft()
    {
        spriteRenderer.flipX = true;
        spriteRenderer.sprite = sprites[startingIndex + 2];
    }

    public void TurnRight()
    {
        spriteRenderer.flipX = false;
        spriteRenderer.sprite = sprites[startingIndex + 2];
    }

    public void TurnDiagonalUpRight()
    {
        spriteRenderer.flipX = false;
        spriteRenderer.sprite = sprites[startingIndex];
    }
    public void TurnDiagonalDownRight()
    {
        spriteRenderer.flipX = false;
        spriteRenderer.sprite = sprites[startingIndex + 1];
    }

    public void TurnDiagonalUpLeft()
    {
        spriteRenderer.flipX = true;
        spriteRenderer.sprite = sprites[startingIndex];
    }
    public void TurnDiagonalDownLeft()
    {
        spriteRenderer.flipX = true;
        spriteRenderer.sprite = sprites[startingIndex + 1];
    }

    //public void DrawGunHands()
    //{
    //    spriteRenderer.sprite = sprites[startingIndex + 4];
    //}
    //public void DrawDefaultHands()
    //{
    //    spriteRenderer.sprite = sprites[startingIndex + 4];
    //}
}
