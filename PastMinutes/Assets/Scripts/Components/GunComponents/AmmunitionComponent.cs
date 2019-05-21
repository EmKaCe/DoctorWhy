using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "GunComponents/Projectile", order = 20)]
public class AmmunitionComponent : ScriptableObject
{

    public PartFindingSystem.AmmoType ammoType;
    public PartFindingSystem.GunType gunType;
    public float weight;
    public Sprite projectile;
    
}
