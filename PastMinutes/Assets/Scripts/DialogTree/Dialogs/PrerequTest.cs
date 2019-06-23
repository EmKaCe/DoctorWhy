using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogTree
{
    /// <summary>
    /// Returns always true
    /// </summary>
    public class PrerequTest : Prerequisite
    {
        public GameObject test;

        public override bool IsFullfilled()
        {
            return true;
        }

    }
}
