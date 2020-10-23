using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zefugi.Unity.FiniteStateMachine;

public class CuboxController : FiniteStateMachineController
{
    public override FiniteStateMachineSystem StateMachine { get; protected set; }

    private CuboxIdleState _idleState;
    private CuboxLookAtState _lookState;

    void Start()
    {
        _idleState = new CuboxIdleState();
        _lookState = new CuboxLookAtState();
        _idleState.LookState = _lookState;
        _lookState.IdleState = _idleState;

        StateMachine = new FiniteStateMachineSystem(
            _idleState,
            new List<State>
            {
                _lookState
            }
            );
    }

    void Update()
    {
        StateMachine.Update(this);
    }
}
