using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : DecisionNode
{
    public override void ChildFailure()
    {
        throw new System.NotImplementedException();
    }

    public override void ChildSuccess()
    {
        throw new System.NotImplementedException();
    }

    public override void GetChildState(State state)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Run();

    }
}
