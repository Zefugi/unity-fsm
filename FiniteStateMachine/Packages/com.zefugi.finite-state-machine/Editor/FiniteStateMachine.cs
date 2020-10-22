using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zefugi.Unity.FiniteStateMachine
{
    public class FiniteStateMachine : MonoBehaviour
    {
        public bool Untrue => false;

        private void Start()
        {
            Debug.Log($"Untrue is: {Untrue}.");
        }
    }
}
