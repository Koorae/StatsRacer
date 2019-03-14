namespace CarComponents
{
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Wheels;

    using UnityEngine;

    public class AntiRollBar : CarPart
    {
        #region Fields

        [SerializeField]
        private float backAntiRollForce = 5000.0f;
        private WheelsPare backWheelPare;
        [SerializeField]
        private float frontAntiRollForce = 5000.0f;
        private WheelsPare frontWheelPare;
        bool groundedL;
        bool groundedR;
        WheelHit hit;
        float travelL;
        float travelR;

        #endregion Fields

        #region Methods

        void checkWheelAntiRoll(WheelsPare wheelPare, float antiRollForce)
        {
            travelL = 1.0f;
             travelR = 1.0f;

            groundedL = wheelPare.leftWheel.wheelCollider.GetGroundHit(out hit);

            if (groundedL)
                travelL = (-car.transform.InverseTransformPoint(hit.point).y
                    - wheelPare.leftWheel.wheelCollider.radius)
                    / wheelPare.leftWheel.wheelCollider.suspensionDistance;

            groundedR = wheelPare.rightWheel.wheelCollider.GetGroundHit(out hit);
            if (groundedR)
                travelR = (-car.transform.InverseTransformPoint(hit.point).y
                    - wheelPare.rightWheel.wheelCollider.radius) / wheelPare.rightWheel.wheelCollider.suspensionDistance;

            antiRollForce = (travelL - travelR) * antiRollForce;

            if (groundedL)
                car._rigidbody.AddForceAtPosition(car.transform.up * -antiRollForce,
                       wheelPare.leftWheel.wheelCollider.transform.position);
            if (groundedR)
                car._rigidbody.AddForceAtPosition(car.transform.up * antiRollForce,
                       wheelPare.rightWheel.wheelCollider.transform.position);
        }

        void FixedUpdate()
        {
            checkWheelAntiRoll(frontWheelPare, frontAntiRollForce);
            checkWheelAntiRoll(backWheelPare, backAntiRollForce);
        }

        void Start()
        {
            frontWheelPare = new WheelsPare(car.frontWheels);
            backWheelPare = new WheelsPare(car.backWheels);
        }

        #endregion Methods

        #region Nested Types

        class WheelsPare
        {
            #region Fields

            public Wheel leftWheel;
            public Wheel rightWheel;

            #endregion Fields

            #region Constructors

            public WheelsPare(List<Wheel> wheels)
            {
                foreach (Wheel wheel in wheels)
                {
                    if (wheel.isRight)
                        rightWheel = wheel;
                    else
                        leftWheel = wheel;
                }
            }

            #endregion Constructors
        }

        #endregion Nested Types
    }
}