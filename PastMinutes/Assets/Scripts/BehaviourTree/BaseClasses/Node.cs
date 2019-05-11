using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Node : MonoBehaviour
{
    public bool root;
    public ExtendedNode parentNode;
    private State state;

    public enum State{
        Success,
        Running,
        Failure,
        NotActive
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (!root)
        {
            if (parentNode != null)
            {
                state = State.NotActive;
                root = false;
            }
            else
            {
                state = State.Running;
                root = true;
            }
        }
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (State.Running == state)
        {
            Run();
        }
    }

    public void SetActive()
    {
        if (!root)
        {
            parentNode.SetChildState(State.Running);
        }       
        state = State.Running;
    }



    public abstract void Run();

    public abstract void Success();

    public abstract void Failure();


}
