using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviour : MonoBehaviour
{
    public string text;

    //public TestBehaviour(string text)
    //{
    //    this.text = text;
    //}

    private void Update()
    {
        Debug.Log(text);
    }
}
