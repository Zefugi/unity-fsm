using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Zefugi.Unity.FiniteStateMachine;

namespace Zefugi.Unity.FiniteStateMachine
{
    public abstract class State
    {
        private FiniteStateMachineSystem _machine;
        public FiniteStateMachineSystem Machine
        {
            get => _machine;
            set
            {
                bool changed = _machine != null;

                _machine = value;

                if (changed)
                    OnMachineChanged();
                else
                    OnMachineSet();
            }
        }

        public virtual void OnMachineSet() { }

        public virtual void OnMachineChanged() { }

        public virtual void OnEnter(State fromState) { }

        public virtual void OnExit(State toState) { }

        public virtual void OnUpdate() { }
    }
}
