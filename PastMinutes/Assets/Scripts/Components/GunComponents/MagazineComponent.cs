using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineComponent : AttachmentComponent
{
    public AmmunitionComponent projectile;
    public int capacity;
    public int amountOfAmunition;


    // Start is called before the first frame update
    void Start()
    {
        gunType = projectile.gunType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnAttachment()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDetachment()
    {
        throw new System.NotImplementedException();
    }
}
