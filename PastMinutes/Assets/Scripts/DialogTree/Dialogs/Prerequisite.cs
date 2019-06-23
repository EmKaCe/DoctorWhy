﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogTree
{
    public abstract class Prerequisite : MonoBehaviour
    {
        /// <summary>
        /// Checks if prerequisite was fullfilled
        /// </summary>
        /// <returns></returns>
        public abstract bool IsFullfilled();
    }
}
