using System;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zefugi.Unity.FiniteStateMachine;

namespace Tests
{
    public class FiniteStateMachine_Tests
    {
        [Test]
        public void Ctor_SetsInitialState()
        {
            var initState = Substitute.For<State>();
            var fsm = new FiniteStateMachine(initState);

            Assert.AreEqual(initState, fsm.CurrentState);
        }
    }
}
