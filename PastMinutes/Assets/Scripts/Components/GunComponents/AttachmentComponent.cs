using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttachmentComponent : MonoBehaviour
{
    public PartFindingSystem.GunType gunType;

    public abstract void OnAttachment();

    public abstract void OnDetachment();
}
