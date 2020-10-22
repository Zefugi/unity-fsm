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
    [TestFixture]
    public class FiniteStateMachine_Tests
    {
        private FiniteStateMachine _fsm;
        private State _subStateA;
        private State _subStateB;

        [Test]
        public void Ctor_SetsInitialState()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            Assert.AreEqual(_subStateA, _fsm.CurrentState);
        }

        [Test]
        public void Ctor_AddsInitialState()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            Assert.IsTrue(_fsm.States.Contains(_subStateA));
        }

        [Test]
        public void Ctor_Throws_IfInitialStateIsNull()
        {
            Assert.Throws<FiniteStateMachineException>(() =>
            {
                _fsm = new FiniteStateMachine(null);
            });
        }

        [Test]
        public void Add_AddsState()
        {

        }

        [Test]
        public void Add_Throws_IfStateExists()
        {

        }
    }
}
