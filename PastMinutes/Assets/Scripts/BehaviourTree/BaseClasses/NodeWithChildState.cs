using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeWithChildState : Node
{
    protected State stateActiveChild;
    // Start is called before the first frame update
    protected override void Start()
    {
        stateActiveChild = State.NotActive;
        base.Start();
    }

    public abstract void OnChildSuccess();

    public abstract void OnChildFailure();

    public void SetChildState(State state)
    {
        stateActiveChild = state;
    }


}
