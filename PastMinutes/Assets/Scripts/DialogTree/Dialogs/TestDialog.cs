using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DialogTree
{


    public class TestDialog : MonoBehaviour
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
}
