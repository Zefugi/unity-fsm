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

        [SetUp]
        public void Setup()
        {
            _subStateA = Substitute.For<State>();
            _subStateB = Substitute.For<State>();
        }

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
            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm = new FiniteStateMachine(null);
            });
        }

        [Test]
        public void Ctor_SetsStates()
        {
            _fsm = new FiniteStateMachine(_subStateA, new List<State> { _subStateB });

            Assert.IsTrue(_fsm.States.Contains(_subStateB));
        }

        [Test]
        public void Add_AddsStates()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            _fsm.Add(_subStateB);

            Assert.IsTrue(_fsm.States.Contains(_subStateB));
        }

        [Test]
        public void Add_Throws_IfStateExists()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.Add(_subStateA);
            });
        }

        [Test]
        public void Add_Throws_IfStateIsNull()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm.Add(null);
            });
        }

        public void Add_DoesNotSetCurrenState()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            _fsm.Add(_subStateB);

            Assert.AreEqual(_subStateA, _fsm.CurrentState);
        }

        [Test]
        public void Remove_RemovesState()
        {
            _fsm = new FiniteStateMachine(_subStateA);
            _fsm.Add(_subStateB);

            _fsm.Remove(_subStateB);

            Assert.IsFalse(_fsm.States.Contains(_subStateB));
        }

        [Test]
        public void Remove_OnlyRemovesSpecifiedState()
        {
            _fsm = new FiniteStateMachine(_subStateA);
            _fsm.Add(_subStateB);

            _fsm.Remove(_subStateB);

            Assert.IsTrue(_fsm.States.Contains(_subStateA));
        }

        [Test]
        public void Remove_Throws_IfStateDoesNotExist()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.Remove(_subStateB);
            });
        }

        [Test]
        public void Remove_Throws_IfRemovingLastState()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            Assert.Throws<FiniteStateMachineException>(() =>
            {
                _fsm.Remove(_subStateA);
            });
        }

        [Test]
        public void Remove_TransitionsToInitialState()
        {
            _fsm = new FiniteStateMachine(_subStateA);
            _fsm.Add(_subStateB);

            _fsm.Transition(_subStateB);
            _fsm.Remove(_subStateB);

            Assert.AreEqual(_subStateA, _fsm.CurrentState);
        }

        [Test]
        public void Remove_Throws_IfRemovingInitialState()
        {
            _fsm = new FiniteStateMachine(_subStateA);
            _fsm.Add(_subStateB);

            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.Remove(_subStateA);
            });
        }

        [Test]
        public void Remove_Throws_IfStateIsNull()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm.Remove(null);
            });
        }

        [Test]
        public void InitialStateGet_ReturnsInitialState()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            Assert.AreEqual(_subStateA, _fsm.InitialState);
        }

        [Test]
        public void InitialStateSet_SetsInitialState()
        {
            _fsm = new FiniteStateMachine(_subStateA);
            _fsm.Add(_subStateB);

            _fsm.InitialState = _subStateB;

            Assert.AreEqual(_subStateB, _fsm.InitialState);
        }

        [Test]
        public void InitialStateSet_Throws_IfValueIsNull()
        {
            _fsm = new FiniteStateMachine(_subStateA);
            _fsm.Add(_subStateB);

            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm.InitialState = null;
            });
        }

        [Test]
        public void InitialStateSet_Throws_IfDoesNotContainValue()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.InitialState = _subStateB;
            });
        }

        [Test]
        public void Transition_ChangesCurrentState()
        {
            _fsm = new FiniteStateMachine(_subStateA);
            _fsm.Add(_subStateB);

            _fsm.Transition(_subStateB);

            Assert.AreEqual(_subStateB, _fsm.CurrentState);
        }

        [Test]
        public void Transition_Throws_IfStateIsNull()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm.Transition(null);
            });
        }

        [Test]
        public void Transition_Throws_IfNotContainingState()
        {
            _fsm = new FiniteStateMachine(_subStateA);

            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.Transition(_subStateB);
            });
        }
    }
}
