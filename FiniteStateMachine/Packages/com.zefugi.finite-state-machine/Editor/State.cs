﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zefugi.Unity.FiniteStateMachine;

namespace Zefugi.Unity.FiniteStateMachine
{
    public abstract class State
    {
        public FiniteStateMachine Machine { get; internal set; }
    }
}