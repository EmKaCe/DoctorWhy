using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    public float muzzleVelocity;
    public PartFindingSystem.GunType gunType;
    public MagazineComponent magazine;
    public RailAttachmentComponent rail;
    public MuzzleAttachmentComponent muzzle;

    // Start is called before the first frame update
    void Start()
    {
        if (!AddAttachment(magazine))
        {
            magazine = null;
        }
        if (!AddAttachment(rail))
        {
            rail = null;
        }
        if (!AddAttachment(muzzle))
        {
            muzzle = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddAttachment(AttachmentComponent attachment)
    {
        if(attachment != null)
        {
            if (attachment.gunType.Equals(gunType))
            {
                if (attachment.GetType().Equals(magazine.GetType()))
                {
                    magazine = attachment as MagazineComponent;
                }
                else if (attachment.GetType().Equals(rail.GetType()))
                {
                    rail = attachment as RailAttachmentComponent;
                }
                else if (attachment.GetType().Equals(muzzle.GetType()))
                {
                    muzzle = attachment as MuzzleAttachmentComponent;
                }
                return true;
            }
        }      
        return false;
    }
}
