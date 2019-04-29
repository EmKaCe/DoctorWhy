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
        Head = 5,
        Torso = 10,
        Arms = 15,
        Legs = 25,
        ArmsWithGun = 35
        
    }
}
