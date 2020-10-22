using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zefugi.Unity.FiniteStateMachine;

namespace Zefugi.Unity.FiniteStateMachine
{
    public abstract class State
    {
        private FiniteStateMachine _machine;
        public FiniteStateMachine Machine
        {
            get => _machine;
            set
            {
                _machine = value;
            }
        }

        public virtual void OnMachineSet() { }

        public virtual void OnMachineChanged() { }

        public virtual void OnEnter(State fromState) { }

        public virtual void OnExit(State toState) { }

        public virtual void OnUpdate() { }
    }
}
