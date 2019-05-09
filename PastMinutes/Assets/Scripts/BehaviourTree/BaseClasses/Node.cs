using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public bool root;
    public Node parentNode;
    private bool active;

    public enum State{
        Success,
        Running,
        Failure
    }

    // Start is called before the first frame update
    void Start()
    {
        if (parentNode != null)
        {
            root = false;
        }
        else
        {
            root = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            parentNode.GetChildState(State.Running);
            Run();
        }
    }

    public void SetActive()
    {
        active = true;
    }

    public abstract void GetChildState(State state);


    public abstract void Run();


}
