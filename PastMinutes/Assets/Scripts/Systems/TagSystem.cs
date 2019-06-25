using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagSystem : MonoBehaviour
{
    public bool Camera;
    public bool Player;
    public bool NPC;
    public bool Enemy;
    public bool Item;

    internal object Where(Func<object, object> p)
    {
        throw new NotImplementedException();
    }
}
