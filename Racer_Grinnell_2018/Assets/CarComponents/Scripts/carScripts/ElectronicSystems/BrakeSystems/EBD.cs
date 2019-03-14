namespace CarComponents.ElectronicSystems.BrakeSystems
{
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Wheels;

    using UnityEngine;

    public class EBD : CarPart
    {
        #region Fields

        private float orginal_predeterminedThreshold;
        [SerializeField]
        private float _predeterminedThreshold = 0.01f;

        #endregion Fields

        #region Methods

        float checkFrontRearSpeedDiffrence()
        {
            if (car.transmission.currentGearRatio >-1)
                return getWheelsSpeed(car.frontWheels) - getWheelsSpeed(car.backWheels);
            else
                return getWheelsSpeed(car.backWheels) - getWheelsSpeed(car.frontWheels);
        }

        void checkWheelBrake()
        {
            if (carControl.speedObject.speedInKPH < 3)
                _predeterminedThreshold = 1f;
            if (checkFrontRearSpeedDiffrence() >= _predeterminedThreshold)
            {
                pressurize(car.frontWheels);
            }
            else
                pressurize(car.backWheels);
        }

        float getWheelsSpeed(List<Wheel> wheels)
        {
            float speed = 0;
            foreach (Wheel wheel in wheels)
                speed += wheel.speedObject.speedInKPH;
            return speed / wheels.Count;
        }

        void pressurize(List<Wheel> wheels)
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.brake.maxAllowedBrakeForce +=
                     // (1 - Runge.runge(0, 1, (1 - wheel.SlipRatio), Time.deltaTime, new Runge.Function(Runge.f1))) *
                     wheel.brake.maxBrakeForce * _predeterminedThreshold;
                //_predeterminedThreshold * wheel.brake.requestedBrakeForce;
            }
        }

        void reset()
        {
            _predeterminedThreshold = orginal_predeterminedThreshold;
              //  foreach (Wheel wheel in car.wheels)
             //       wheel.brake.maxAllowedBrakeForce = _predeterminedThreshold * wheel.brake.maxBrakeForce;
        }

        void Start()
        {
            carControl.brakingListner += checkWheelBrake;
               // carControl.neutralListner += reset;
            carControl.forwardListner += reset;
            carControl.backwardListner += reset;
            carControl.stoppedListner += reset;
            orginal_predeterminedThreshold = _predeterminedThreshold;
        }

        #endregion Methods
    }
}