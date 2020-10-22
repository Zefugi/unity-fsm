using System;
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

        public State CurrentState { get; private set; }

        public FiniteStateMachine(State initialState)
        {
            if (initialState == null)
                throw new ArgumentNullException("initialState");
            CurrentState = initialState;
            _states.Add(initialState);
        }

        public void Add(State state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            if (_states.Contains(state))
                throw new ArgumentException("Can not add a state that has already been added.", "state");
            _states.Add(state);
        }

        public void Remove(State state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            if (!_states.Contains(state))
                throw new ArgumentException("The specified state does not exist.", "state");

            _states.Remove(state);
        }

        public void Transition(State state)
        {
            CurrentState = state;
        }
    }
}
