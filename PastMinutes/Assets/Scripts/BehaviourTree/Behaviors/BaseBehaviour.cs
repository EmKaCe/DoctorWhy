using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{

    public enum State
    {
        inactive = 0,
        running = 1,
        success = 2,
        failure = 3,
    }

    public State currentState;
    public BehaviourNode parent;

    private float test;
    private float goal;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.inactive;
        test = 0;
        goal = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Test");
        //Disables Behaviour to save unnecessary calls of update
        if (currentState == State.inactive)
        {
            enabled = false;
        }
        else if(currentState == State.running)
        {
            Run();
        }
        else if(currentState == State.success)
        {
            OnSuccess();
        }
        else if (currentState == State.failure)
        {
            OnFailure();
        }
    }

    public virtual void Run()
    {
        if(test < goal)
        {
            test += Time.deltaTime;
        }
        else
        {
            currentState = State.success;
        }
    }

    public void OnSuccess()
    {
        parent.OnBehaviourResult(State.success);
    }

    public void OnFailure()
    {
        parent.OnBehaviourResult(State.failure);
    }

    public void ActivateBehaviour()
    {
        currentState = State.running;
        enabled = true;
    }
}
