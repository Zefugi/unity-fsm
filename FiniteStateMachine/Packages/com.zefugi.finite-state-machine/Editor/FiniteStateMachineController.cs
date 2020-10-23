using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zefugi.Unity.FiniteStateMachine;

public abstract class FiniteStateMachineController : MonoBehaviour
{
    public abstract FiniteStateMachineSystem StateMachine { get; }
}
