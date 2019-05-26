using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPointSave : ScriptableObject
{
    public Rect rect;
    public BehaviourConnectionPointType type;
    public GUIStyle style;

    public void Init(BehaviourConnectionPoint point)
    {
        rect = new Rect(point.rect.x, point.rect.y, point.rect.width, point.rect.height);
        type = point.type;
        style = point.style;

    }
}
