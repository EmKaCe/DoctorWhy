using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Finds the offset for the corresponding characterPart
/// </summary>
public static class PartFindingSystem
{
    /// <summary>
    /// Location of bodyparts in spritesheet. Index is first occurence of body part
    /// </summary>
    public enum BodyParts
    {
        Head = 0,
        Torso = 5,
        Arms = 10,
        Legs = 20,
        ArmsWithGun = 30

    }

    public enum GunType
    {
        AssaultRifle = 0,
        SubmachineGun = 1,
        Shotgun = 3,
        Pistol = 4,
        GrenadeLauncher = 5,
        LMG = 6,
        Minigun = 7

    }


    public enum AmmoType
    {
        energy = 0,
        ballistic = 1,
    }

    [System.Serializable]
    public class FiringModes
    {
        [SerializeField]
        bool single;
        [SerializeField]
        bool burst;
        [SerializeField]
        bool auto;
    }


    public enum CurrentFiringMode
    {
        single = 0,
        burst = 2,
        auto = 3,
    }

}
