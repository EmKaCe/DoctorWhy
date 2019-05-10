using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : ExtendedNode
{
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

    public override void OnChildSuccess()
    {
        if(++activeChild < childrenCount)
        {
            Debug.Log("Sequence " + activeChild + " " + childrenCount);
            children[activeChild].SetActive();
        }
        else
        {
            Success();
        }
    }

    public override void OnChildFailure()
    {
        Failure();
    }

    public override void Success()
    {
        Debug.Log("Sequence erfolgreich abgeschlossen");
        if (!root)
        {
            parentNode.SetChildState(State.Success);
            base.Start();
        }
        else
        {
            this.enabled = false;
        }
        
        
    }

    public override void Failure()
    {
        Debug.Log("Sequence nicht erfolgreich abgebrochen");
        if (!root)
        {
            parentNode.SetChildState(State.Failure);
            base.Start();
        }
        else
        {
            this.enabled = false;
        }
    }
}
