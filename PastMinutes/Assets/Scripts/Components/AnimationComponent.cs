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
    private int offset;
    private bool turn;
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
        offset = 4;
        turn = false;
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
            spriteRenderer.sprite = sprites[startingIndex + offset];
            spriteRenderer.flipX = turn;
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
        offset = 4;
        turn = false;
    }

    public void TurnBack()
    {
        spriteRenderer.flipX = false;
        spriteRenderer.sprite = sprites[startingIndex + 3];
        offset = 3;
        turn = false;

    }

    public void TurnLeft()
    {
        spriteRenderer.flipX = true;
        spriteRenderer.sprite = sprites[startingIndex + 2];
        offset = 2;
        turn = true;
    }

    public void TurnRight()
    {
        spriteRenderer.flipX = false;
        spriteRenderer.sprite = sprites[startingIndex + 2];
        offset = 2;
        turn = false;
    }

    public void TurnDiagonalUpRight()
    {
        spriteRenderer.flipX = false;
        spriteRenderer.sprite = sprites[startingIndex];
        offset = 0;
        turn = false;
    }
    public void TurnDiagonalDownRight()
    {
        spriteRenderer.flipX = false;
        spriteRenderer.sprite = sprites[startingIndex + 1];
        offset = 1;
        turn = false;
    }

    public void TurnDiagonalUpLeft()
    {
        spriteRenderer.flipX = true;
        spriteRenderer.sprite = sprites[startingIndex];
        offset = 0;
        turn = true;
    }
    public void TurnDiagonalDownLeft()
    {
        spriteRenderer.flipX = true;
        spriteRenderer.sprite = sprites[startingIndex + 1];
        offset = 1;
        turn = true;
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
