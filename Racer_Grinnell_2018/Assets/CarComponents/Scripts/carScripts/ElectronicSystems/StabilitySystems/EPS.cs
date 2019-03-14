namespace CarComponents.ElectronicSystems.StabilitySystems
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Helpers;
    using CarComponents.Wheels;

    using UnityEngine;

    public class EPS : CarPart
    {
        #region Fields

        public float fixedTime = 0.1f;
        public float kd;
        public float ki;
        public float kp;
        public float maxBrakeLimitPercent = 0.2f;

        const string EPS_BRAKE_KEY = "EPS"; //A key for the Brake Request Dictionary

        PID_Runner pid_runner;
        Wheel targetedWheel;

        #endregion Fields

        #region Methods

        void applyEPS(Wheel wheel, float value)
        {
            freeWheels();
            wheel.brake.brakeControllersRequestes[EPS_BRAKE_KEY] = value * wheel.brake.maxBrakeForce
                * (wheel.parentDiffrential.currentTorquePercent + 0.00001f)
                * (wheel.currentTorquePercent + 0.00001f)
                * Mathf.Max(Mathf.Abs(carControl.AccelInput), 0.00001f)
            * Mathf.Abs(carControl.getSpeedFactor());
        }

        private void checkWheel()
        {
            if (Mathf.Abs(carControl.getSpeedFactor()) < 0.05f)
            {
                reset();
                return;
            }

            if (pid_runner.isRunning)
                return;
            StartCoroutine(pid_runner.run());
        }

        private void freeWheels()
        {
            foreach (Wheel wheel in car.wheels)
            {
                if (wheel.Equals(targetedWheel))
                    continue;
                wheel.brake.brakeControllersRequestes[EPS_BRAKE_KEY] = 0;
            }
        }

        private float getCurrentSlip()
        {
            return car.backWheels[0].wheelHit.sidewaysSlip;
        }

        private float getDesiredSlip()
        {
            return 0;
        }

        private void reset()
        {
            targetedWheel = null;
            pid_runner.stop();
            freeWheels();
        }

        private void setBrake(float value)
        {
            if (value == 0 )
            {
                reset();
                return;
            }
            float slipSign = Mathf.Sign(getCurrentSlip());
            if (car.steering.steerAngle != 0)
                slipSign = Mathf.Sign(car.steering.steerAngle);

            if (value > 0)
            {
                if (Mathf.Sign(value) == slipSign)
                    targetedWheel = car.rearRightWheel;
                else
                    targetedWheel = car.frontLeftWheel;
            }
            else
            {
                if (Mathf.Sign(value) == slipSign)
                    targetedWheel = car.rearLeftWheel;
                else
                    targetedWheel = car.frontRightWheel;
            }

            applyEPS(targetedWheel, Mathf.Abs(value));
        }

        void Start()
        {
            pid_runner = new PID_Runner(kp, ki, kd,
                1, -1,
                    maxBrakeLimitPercent,
                    -maxBrakeLimitPercent,
                getCurrentSlip, getDesiredSlip, setBrake
                , fixedTime
                );

            // carControl.brakingListner += reset;
            carControl.forwardListner += checkWheel;
            carControl.backwardListner += checkWheel;
            // carControl.neutralListner += reset;
            carControl.stoppedListner += reset;
            foreach (Wheel wheel in car.wheels)
                wheel.brake.brakeControllersRequestes.Add(EPS_BRAKE_KEY, 0);
            reset();
        }

        #endregion Methods

        #region Other

        // [DBG_Track("Blue")]
        // public float v = 0;
        /*  float sumSideSlip()
        {
            float slip = 0;
            foreach (Wheel wheel in car.backWheels)
            {
                slip += Mathf.Abs( wheel.wheelHit.sidewaysSlip);
            }

            return slip / 2;
        }
        */

        #endregion Other
    }
}