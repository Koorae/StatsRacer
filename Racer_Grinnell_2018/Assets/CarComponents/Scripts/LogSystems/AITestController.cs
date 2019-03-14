namespace CarComponents.LogSystems
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using MonsterLove.StateMachine;

    using UnityEngine;

    public class AITestController : CarPart
    {
        #region Fields

        StateMachine<States> fsm;

        #endregion Fields

        #region Enumerations

        public enum States
        {
            Idle,
            Accelerate,
            Brake,
            Exit
        }

        #endregion Enumerations

        #region Methods

        void Accelerate_Enter()
        {
            carControl.updateInputs(0, 1);
        }

        void Brake_Enter()
        {
            carControl.updateInputs(0, -1);
        }

        void Exit_Enter()
        {
            carControl.updateInputs(0, 0);
        }

        void FixedUpdate()
        {
            var state = fsm.State;
            if (state == States.Idle && car.wheels[0].wheelCollider.isGrounded)
                fsm.ChangeState(States.Accelerate);
            if (state == States.Accelerate && carControl.speedObject.speedInKPH >= 96)
                fsm.ChangeState(States.Brake);
            if (state == States.Brake && carControl.statuse == CarStause.Stopped)
                fsm.ChangeState(States.Exit);
        }

        void Start()
        {
            fsm = StateMachine<States>.Initialize(this, States.Idle);
            car.GetComponentInChildren<UserInput>().gameObject.SetActive(false);
        }

        #endregion Methods
    }
}