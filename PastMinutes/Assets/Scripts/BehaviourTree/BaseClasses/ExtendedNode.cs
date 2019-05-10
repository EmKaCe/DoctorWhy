using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExtendedNode : NodeWithChildState
{
    public Node[] children;
    protected int activeChild;
    protected int childrenCount;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        activeChild = -1;
        childrenCount = children.Length;
    }

    public override void Run()
    {
        if(stateActiveChild == State.NotActive)
        {
            Debug.Log("ExtendedNode: activeChild at Run - notActive: " + (activeChild + 1));
            activeChild++;
            children[activeChild].SetActive();
        }
        if(stateActiveChild == State.Failure)
        {
            OnChildFailure();
        }
        if(stateActiveChild == State.Success)
        {
            OnChildSuccess();
        }
        if(stateActiveChild == State.Running)
        {
            Debug.Log("ExtendedNode: " + activeChild + " " + childrenCount);
        }
    }

    

    

}
