using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviour2 : Node
{
    int i = 0;

    public override void Failure()
    {
        parentNode.SetChildState(State.Failure);
        base.Start();
    }

    public override void Run()
    {
        parentNode.SetChildState(State.Running);
        Debug.Log("Ich bin TestBehaviour2");

        if (i > 5)
        {
            Debug.Log("Erfolg");
            Success();
        }
        else
        {
            i++;
            Debug.Log("Iteration: " + i);
        }
    }

    public override void Success()
    {
        parentNode.SetChildState(State.Success);
        base.Start();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
