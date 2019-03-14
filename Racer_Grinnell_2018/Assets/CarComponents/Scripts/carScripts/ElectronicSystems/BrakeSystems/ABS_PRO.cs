namespace CarComponents.ElectronicSystems.StabilitySystems
{
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Wheels;

    using UnityEngine;

    public class ABS_PRO : WheelPart
    {
        #region Fields

        [SerializeField]
        float addedBrakeForce = 0.5f;

        #endregion Fields

        #region Methods

        float calculateMaxAllowedTorque()
        {
            return wheel.radius * (Mathf.Abs(wheel.wheelHit.force) + getWheelDragPercent()) * wheel.wheelCollider.forwardFriction.stiffness;
        }

        float getWheelDragPercent()
        {
            return car.dragForce * Time.deltaTime;
        }

        float getWheelInteriaEnergy()
        {
            return wheel.inertia * wheel.angularSpeedObject.speed;
        }

        void Update()
        {
            wheel.brake.maxAllowedBrakeForce = calculateMaxAllowedTorque() + getWheelInteriaEnergy();
            if (wheel.slipRatio > -wheel.extremSlip)
                wheel.brake.maxAllowedBrakeForce += wheel.brake.maxAllowedBrakeForce * addedBrakeForce;
        }

        #endregion Methods
    }
}