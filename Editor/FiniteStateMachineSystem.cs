using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Zefugi.Unity.FiniteStateMachine
{
    public class FiniteStateMachineSystem
    {
        public event EventHandler<State> TransitioningTo;

        private List<State> _states = new List<State>();
        public ReadOnlyCollection<State> States => _states.AsReadOnly();

        public State CurrentState { get; private set; }

        public FiniteStateMachineController Controller { get; private set; }

        public FiniteStateMachineSystem(State state)
        {
            Add(state);
        }

        public FiniteStateMachineSystem(IEnumerable<State> states)
        {
            foreach (var state in states)
                Add(state);
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
                throw new ArgumentException("The specified state is not part of this state machine.", "state");

            if (_states.Count == 1)
                throw new FiniteStateMachineException("Can not remove the last state from a finite state machine.");

            if (state == CurrentState)
                throw new ArgumentException("Can not remove the current state. Try setting transitioning to another state before removing.", "state");

            _states.Remove(state);
        }

        public void Transition(State state)
        {
            if (state == null)
                throw new ArgumentNullException("state");

            if (!_states.Contains(state))
                throw new ArgumentException("The specified state is not part of this state machine.", "state");

            if (CurrentState == null)
                throw new FiniteStateMachineException("Please call Start, before calling Transition.");

            CurrentState?.OnExit(state);
            TransitioningTo?.Invoke(this, state);
            state?.OnEnter(CurrentState);
            CurrentState = state;
        }

        public void Update()
        {
            if (CurrentState == null)
                throw new FiniteStateMachineException("Please call Start, before calling Update.");

            CurrentState.OnUpdate();
        }

        public void Start(State initialState, FiniteStateMachineController controller)
        {
            if (initialState == null)
                throw new ArgumentNullException("initialState");

            CurrentState = initialState;
            Controller = controller;
            Transition(initialState);
        }
    }
}
