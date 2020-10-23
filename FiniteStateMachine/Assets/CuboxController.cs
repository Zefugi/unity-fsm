using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zefugi.Unity.FiniteStateMachine;

public class CuboxController : FiniteStateMachineController
{
    public override FiniteStateMachineSystem StateMachine { get; protected set; }

    void Start()
    {
        var idleState = new CuboxIdleState();
        var lookState = new CuboxLookAtState();
        idleState.LookState = lookState;
        lookState.IdleState = idleState;

        StateMachine = new FiniteStateMachineSystem(
            idleState,
            new List<State>
            {
                lookState
            }
            );
    }

    void Update()
    {
        StateMachine.Update(this);
    }
}
