using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Zefugi.Unity.FiniteStateMachine
{
    public class FiniteStateMachineSystem
    {
        private List<State> _states = new List<State>();
        public ReadOnlyCollection<State> States => _states.AsReadOnly();

        public State CurrentState { get; private set; }

        private State _initialState;
        public State InitialState
        {
            get => _initialState;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("InitialState");

                if (!_states.Contains(value))
                    throw new ArgumentException("Can not set InitialState due to state not being part of this statemachine.", "InitialState");

                _initialState = value;
            }
        }

        public FiniteStateMachineSystem(State initialState, IEnumerable<State> states = null)
        {
            if (initialState == null)
                throw new ArgumentNullException("initialState");

            Add(initialState);
            InitialState = CurrentState = initialState;

            if (states != null)
                foreach (var state in states)
                    _states.Add(state);
        }

        public void Add(State state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            if (_states.Contains(state))
                throw new ArgumentException("Can not add a state that has already been added.", "state");

            _states.Add(state);
            state.Machine = this;
        }

        public void Remove(State state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            if (!_states.Contains(state))
                throw new ArgumentException("The specified state does not exist.", "state");

            if (_states.Count == 1)
                throw new FiniteStateMachineException("Can not remove the last state from a finite state machine.");

            if (state == InitialState)
                throw new ArgumentException("Can not remove the initial state. Try setting initial state before removing.", "state");

            Transition(InitialState);
            _states.Remove(state);
        }

        public void Transition(State state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            if (!_states.Contains(state))
                throw new ArgumentException("The specified state does not exist.", "state");

            CurrentState = state;
        }
    }
}
