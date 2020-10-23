using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zefugi.Unity.FiniteStateMachine;

public class CuboxController : FiniteStateMachineController
{
    public override FiniteStateMachineSystem StateMachine { get; }

    void Start()
    {
        
    }

    void Update()
    {
        StateMachine.Update(this);
    }
}
