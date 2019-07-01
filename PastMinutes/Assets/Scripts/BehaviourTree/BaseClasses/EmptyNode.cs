using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyNode : BehaviourNode
{
#if UNITY_EDITOR
    public override void Draw()
    {
        throw new System.NotImplementedException();
    }
#endif
    public override string GetBehaviourType()
    {
        throw new NotImplementedException();
    }



    public override void Run()
    {
        throw new NotImplementedException();
    }

    public override void SetChildState(BaseBehaviour.State state, BehaviourNode childNode)
    {
        throw new NotImplementedException();
    }

    public EmptyNode()
    {
        title = "EmptyNode";
    }

    public override void Init()
    {
        throw new NotImplementedException();
    }
}
