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
        private FiniteStateMachineSystem _fsm;
        private State _subStateA;
        private State _subStateB;

        [SetUp]
        public void Setup()
        {
            _subStateA = Substitute.For<State>();
            _subStateB = Substitute.For<State>();

            _fsm = new FiniteStateMachineSystem(_subStateA);
        }

        [Test]
        public void Ctor_SetsInitialState()
        {
            _fsm = new FiniteStateMachineSystem(_subStateA);

            Assert.AreEqual(_subStateA, _fsm.CurrentState);
        }

        [Test]
        public void Ctor_AddsInitialState()
        {
            _fsm = new FiniteStateMachineSystem(_subStateA);

            Assert.IsTrue(_fsm.States.Contains(_subStateA));
        }

        [Test]
        public void Ctor_Throws_IfInitialStateIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm = new FiniteStateMachineSystem(null);
            });
        }

        [Test]
        public void Ctor_SetsStates()
        {
            _fsm = new FiniteStateMachineSystem(_subStateA, new List<State> { _subStateB });

            Assert.IsTrue(_fsm.States.Contains(_subStateB));
        }

        [Test]
        public void Ctor_TransitionsIntoInitialState()
        {
            _subStateA.Received().OnEnter(null);
        }

        [Test]
        public void Add_AddsStates()
        {
            _fsm.Add(_subStateB);

            Assert.IsTrue(_fsm.States.Contains(_subStateB));
        }

        [Test]
        public void Add_Throws_IfStateExists()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.Add(_subStateA);
            });
        }

        [Test]
        public void Add_Throws_IfStateIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm.Add(null);
            });
        }

        [Test]
        public void Add_DoesNotSetCurrenState()
        {
            _fsm.Add(_subStateB);

            Assert.AreEqual(_subStateA, _fsm.CurrentState);
        }

        [Test]
        public void Add_SetsStateMachine_ThusInvokingOnMachineSetOrOnMachineChanged()
        {
            _fsm.Add(_subStateB);

            _subStateB.Received().OnMachineSet();

            var fsm2 = new FiniteStateMachineSystem(_subStateB);

            _subStateB.Received().OnMachineChanged();
        }

        [Test]
        public void Remove_RemovesState()
        {
            _fsm.Add(_subStateB);

            _fsm.Remove(_subStateB);

            Assert.IsFalse(_fsm.States.Contains(_subStateB));
        }

        [Test]
        public void Remove_OnlyRemovesSpecifiedState()
        {
            _fsm.Add(_subStateB);

            _fsm.Remove(_subStateB);

            Assert.IsTrue(_fsm.States.Contains(_subStateA));
        }

        [Test]
        public void Remove_Throws_IfStateDoesNotExist()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.Remove(_subStateB);
            });
        }

        [Test]
        public void Remove_Throws_IfRemovingLastState()
        {
            Assert.Throws<FiniteStateMachineException>(() =>
            {
                _fsm.Remove(_subStateA);
            });
        }

        [Test]
        public void Remove_TransitionsToInitialState()
        {
            _fsm.Add(_subStateB);

            _fsm.Transition(_subStateB);
            _fsm.Remove(_subStateB);

            Assert.AreEqual(_subStateA, _fsm.CurrentState);
        }

        [Test]
        public void Remove_Throws_IfRemovingInitialState()
        {
            _fsm.Add(_subStateB);

            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.Remove(_subStateA);
            });
        }

        [Test]
        public void Remove_Throws_IfStateIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm.Remove(null);
            });
        }

        [Test]
        public void InitialStateGet_ReturnsInitialState()
        {
            Assert.AreEqual(_subStateA, _fsm.InitialState);
        }

        [Test]
        public void InitialStateSet_SetsInitialState()
        {
            _fsm.Add(_subStateB);

            _fsm.InitialState = _subStateB;

            Assert.AreEqual(_subStateB, _fsm.InitialState);
        }

        [Test]
        public void InitialStateSet_Throws_IfValueIsNull()
        {
            _fsm.Add(_subStateB);

            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm.InitialState = null;
            });
        }

        [Test]
        public void InitialStateSet_Throws_IfDoesNotContainValue()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.InitialState = _subStateB;
            });
        }

        [Test]
        public void Transition_ChangesCurrentState()
        {
            _fsm.Add(_subStateB);

            _fsm.Transition(_subStateB);

            Assert.AreEqual(_subStateB, _fsm.CurrentState);
        }

        [Test]
        public void Transition_Throws_IfStateIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _fsm.Transition(null);
            });
        }

        [Test]
        public void Transition_Throws_IfNotContainingState()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _fsm.Transition(_subStateB);
            });
        }

        [Test]
        public void Transition_InvokesOnEnterAndOnExit()
        {
            _subStateA.Received().OnEnter(null);

            _fsm.Add(_subStateB);

            _fsm.Transition(_subStateB);

            _subStateA.Received().OnExit(_subStateB);
            _subStateB.Received().OnEnter(_subStateA);
        }

        [Test]
        public void Update_InvokesOnUpdate()
        {
            _fsm.Update(null);

            _subStateA.Received().OnUpdate(null);
        }

        /* TODO Refactor so that States will reli on dependency injection.
         * TODO States will only receive a reference to their monobehaviour,
         * when OnUpdate is called.
        */
    }
}
