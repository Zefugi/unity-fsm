using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zefugi.Unity.FiniteStateMachine
{
    public class FiniteStateMachine : MonoBehaviour
    {
        public State CurrentState { get; }

        public FiniteStateMachine(State initialState)
        {
            CurrentState = initialState;
        }
    }
}
