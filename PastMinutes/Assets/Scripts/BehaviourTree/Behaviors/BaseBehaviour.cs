using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBehaviour : MonoBehaviour
{

    public enum State
    {
        inactive = 0,
        running = 1,
        success = 2,
        failure = 3,
    }

    public State currentState;
    public BaseBehaviour parent;


    // Start is called before the first frame update
    void Start()
    {
        currentState = State.inactive;
    }

    // Update is called once per frame
    void Update()
    {
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

    public abstract void Run();

    public void OnSuccess()
    {
        
    }

    public void OnFailure()
    {
       
    }

    public void ActivateBehaviour()
    {
        currentState = State.running;
        enabled = true;
    }

    /// <summary>
    /// Should react to return values of behaviour(s)
    /// </summary>
    public abstract void OnBehaviourResult(State state);
}
