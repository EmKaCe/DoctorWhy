using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{

    public Sprite spritesheetHead;
    public Sprite spritesheetArms;
    public Sprite spritesheetTorso;
    public Sprite spritesheetLegs;
    private int offset;
    public SpriteRenderer head;
    public SpriteRenderer arms;
    public SpriteRenderer torso;
    public SpriteRenderer legs;
    // Start is called before the first frame update
    void Start()
    {
        TurnBack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnFront()
    {
        head.sprite = Resources.LoadAll<Sprite>(spritesheetHead.name)[9];
        arms.sprite = Resources.LoadAll<Sprite>(spritesheetHead.name)[19];
        torso.sprite = Resources.LoadAll<Sprite>(spritesheetHead.name)[14];
        legs.sprite = Resources.LoadAll<Sprite>(spritesheetHead.name)[29];
    }

    public void TurnBack()
    {
        head.sprite = Resources.LoadAll<Sprite>(spritesheetHead.name)[8];
        arms.sprite = Resources.LoadAll<Sprite>(spritesheetHead.name)[18];
        torso.sprite = Resources.LoadAll<Sprite>(spritesheetHead.name)[13];
        legs.sprite = Resources.LoadAll<Sprite>(spritesheetHead.name)[28];
    }

    public void TurnLeft()
    {
        Sprite head = Resources.LoadAll<Sprite>(spritesheetHead.name)[7];
        Sprite arms = Resources.LoadAll<Sprite>(spritesheetHead.name)[17];
        Sprite torso = Resources.LoadAll<Sprite>(spritesheetHead.name)[12];
        Sprite legs = Resources.LoadAll<Sprite>(spritesheetHead.name)[27];
    }

    public void TurnRight()
    {
        Sprite head = Resources.LoadAll<Sprite>(spritesheetHead.name)[7];
        Sprite arms = Resources.LoadAll<Sprite>(spritesheetHead.name)[17];
        Sprite torso = Resources.LoadAll<Sprite>(spritesheetHead.name)[12];
        Sprite legs = Resources.LoadAll<Sprite>(spritesheetHead.name)[27];
    }

    public void TurnDiagonalUpRight()
    {
        Sprite head = Resources.LoadAll<Sprite>(spritesheetHead.name)[9];
        Sprite arms = Resources.LoadAll<Sprite>(spritesheetHead.name)[19];
        Sprite torso = Resources.LoadAll<Sprite>(spritesheetHead.name)[14];
        Sprite legs = Resources.LoadAll<Sprite>(spritesheetHead.name)[29];
    }
    public void TurnDiagonalDownRight()
    {
        Sprite head = Resources.LoadAll<Sprite>(spritesheetHead.name)[9];
        Sprite arms = Resources.LoadAll<Sprite>(spritesheetHead.name)[19];
        Sprite torso = Resources.LoadAll<Sprite>(spritesheetHead.name)[14];
        Sprite legs = Resources.LoadAll<Sprite>(spritesheetHead.name)[29];
    }

    public void TurnDiagonalUpLeft()
    {
        Sprite head = Resources.LoadAll<Sprite>(spritesheetHead.name)[9];
        Sprite arms = Resources.LoadAll<Sprite>(spritesheetHead.name)[19];
        Sprite torso = Resources.LoadAll<Sprite>(spritesheetHead.name)[14];
        Sprite legs = Resources.LoadAll<Sprite>(spritesheetHead.name)[29];
    }
    public void TurnDiagonalDownLeft()
    {
        Sprite head = Resources.LoadAll<Sprite>(spritesheetHead.name)[9];
        Sprite arms = Resources.LoadAll<Sprite>(spritesheetHead.name)[19];
        Sprite torso = Resources.LoadAll<Sprite>(spritesheetHead.name)[14];
        Sprite legs = Resources.LoadAll<Sprite>(spritesheetHead.name)[29];
    }

    public void DrawGunHands()
    {
        Sprite head = Resources.LoadAll<Sprite>(spritesheetHead.name)[9];
        Sprite arms = Resources.LoadAll<Sprite>(spritesheetHead.name)[19];
        Sprite torso = Resources.LoadAll<Sprite>(spritesheetHead.name)[14];
        Sprite legs = Resources.LoadAll<Sprite>(spritesheetHead.name)[29];
    }
    public void DrawDefaultHands()
    {
        Sprite head = Resources.LoadAll<Sprite>(spritesheetHead.name)[9];
        Sprite arms = Resources.LoadAll<Sprite>(spritesheetHead.name)[19];
        Sprite torso = Resources.LoadAll<Sprite>(spritesheetHead.name)[14];
        Sprite legs = Resources.LoadAll<Sprite>(spritesheetHead.name)[29];
    }
}
