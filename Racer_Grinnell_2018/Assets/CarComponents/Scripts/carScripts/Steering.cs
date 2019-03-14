namespace CarComponents
{
    using System.Collections;

    using CarComponents.Wheels;

    using UnityEngine;

    public class Steering : CarPart
    {
        #region Fields

        public float maxSteerAngle = 15;

        [SerializeField]
        private bool isAckermanSteering;
        Vector3 wheelsRotation;
        private float _steerAngle;

        #endregion Fields

        #region Properties

        public float cotSteeringAngle
        {
            get
            {
                return 1 / Mathf.Tan(Mathf.Deg2Rad* steerAngle);
            }
        }

        public float L
        {
            get
            {
                return Mathf.Abs(car.frontWheels[0].transform.parent.localPosition.z)
                    + Mathf.Abs(car.backWheels[0].transform.parent.localPosition.z);
            }
        }

        public float Lw
        {
            get
            {
                return Mathf.Abs( car.frontWheels[0].transform.localPosition.x)
                    + Mathf.Abs(car.frontWheels[1].transform.localPosition.x);
            }
        }

        public float steerAngle
        {
            get
            {
                return _steerAngle;
            }
        }

        #endregion Properties

        #region Methods

        public void FixedUpdate()
        {
            updateSteerongAngle();
            foreach (Wheel wheel in this.car.frontWheels)
            {
                if (isAckermanSteering)
                    steerWheel(wheel);
                else
                    wheel.wheelCollider.steerAngle = steerAngle;
            }
        }

        public void steerWheel(Wheel wheel)
        {
            if (wheel.isLeft)
                wheel.wheelCollider.steerAngle =Mathf.Rad2Deg*  Mathf.Atan(
                    L/((L*cotSteeringAngle)+(Lw/2))
                    );
            else
                wheel.wheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(
                   L / ((L * cotSteeringAngle) - (Lw / 2))
                   );
        }

        void updateSteerongAngle()
        {
            _steerAngle = this.carControl.CurrentSteerInput* maxSteerAngle;
        }

        #endregion Methods
    }
}