namespace CarComponents.ElectronicSystems.StabilitySystems
{
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Wheels;

    using UnityEngine;

    public class TCS_Pro : WheelPart
    {
        #region Methods

        float calculateMaxAllowedTorque()
        {
            return wheel.radius * (wheel.wheelHit.force + getWheelDragPercent()) * wheel.wheelCollider.forwardFriction.stiffness;
        }

        float getWheelDragPercent()
        {
            return car.dragForce * wheel.parentDiffrential.currentTorquePercent * wheel.currentTorquePercent;
        }

        void Update()
        {
            wheel.maxAllowedTorque = calculateMaxAllowedTorque();
        }

        #endregion Methods
    }
}