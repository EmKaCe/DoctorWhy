using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionNode : Node
{
    private Node[] children;
    private int activeChild;
    private State childState;
    // Start is called before the first frame update
    void Start()
    {
        activeChild = -1;
    }

    public override void Run()
    {
        if (childState == State.Running)
        {
            //Wait
        }
        else if (childState == State.Success)
        {
            ChildSuccess();
        }
        else if (childState == State.Failure)
        {
            ChildFailure();
        }
    }

    public void ActivateChild()
    {
        childState = State.Running;
        children[activeChild++].SetActive();

    }

    public abstract void ChildSuccess();

    public abstract void ChildFailure();
}
