using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyNode : BehaviourNode
{
    public override void Draw()
    {
        throw new System.NotImplementedException();
    }

    public override void OnBehaviourResult(BaseBehaviour.State state)
    {
        throw new System.NotImplementedException();
    }

    public override void Run()
    {
        throw new System.NotImplementedException();
    }

    public override Type GetBehaviourType()
    {
        throw new NotImplementedException();
    }

    public EmptyNode()
    {
        title = "EmptyNode";
    }
}
