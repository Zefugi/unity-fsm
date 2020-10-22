﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Zefugi.Unity.FiniteStateMachine
{
    public class FiniteStateMachine : MonoBehaviour
    {
        private List<State> _states = new List<State>();
        public ReadOnlyCollection<State> States => _states.AsReadOnly();

        public State CurrentState { get; }

        public FiniteStateMachine(State initialState)
        {
            CurrentState = initialState;
            _states.Add(initialState);
        }
    }
}
