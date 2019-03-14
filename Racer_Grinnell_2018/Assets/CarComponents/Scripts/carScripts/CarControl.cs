namespace CarComponents
{
    using System.Collections;

    using CarComponents.Physics;
    using CarComponents.Wheels;

    using UnityEngine;
    using UnityEngine.UI;

    #region Enumerations

    public enum CarStause
    {
        Forward,
        Stopped,
        Backward,
        Braking,
        Neutral
    }

    #endregion Enumerations

    #region Delegates

    public delegate void CarStatusChangeListner();

    /*public delegate void OnBackwardListner();
    public delegate void OnForwardListner();
    public delegate void OnBrakingListner();
    public delegate void OnStoppedListner();
    public delegate void OnNeutralListner();*/
    public delegate void OnFrontLightsTrigger(bool isOn);

    public delegate void OnHandBraking();

    public delegate void OnHandBrakReleased();

    #endregion Delegates

    public class CarControl : MonoBehaviour
    {
        #region Fields

        public SpeedObject speedObject = new SpeedObject();
        public CarStause statuse = CarStause.Stopped;
        [DBG_Track("Green")]
        public float Wheel1Brake;
        [DBG_Track("Red")]
        public float Wheel1Slip;

        #endregion Fields

        #region Events

        public event CarStatusChangeListner backwardListner;

        public event CarStatusChangeListner brakingListner;

        public event CarStatusChangeListner forwardListner;

        public event OnFrontLightsTrigger frontLightsTrigger;

        public event OnHandBraking handBrakeListner;

        public event OnHandBrakReleased handBrakeReleasedListner;

        public event CarStatusChangeListner neutralListner;

        public event CarStatusChangeListner stoppedListner;

        #endregion Events

        #region Properties

        public float AccelInput
        {
            get; set;
        }

        public Car car
        {
            get
            {
                return GetComponent<Car>();
            }
        }

        public float CurrentSteerInput
        {
            get; set;
        }

        public float Throttle_Level
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public void frontLightsOff()
        {
            frontLightsTrigger(false);
        }

        public void frontLightsOn()
        {
            frontLightsTrigger(true);
        }

        public float getAvregWheelsRPM()
        {
            float rpmSum = 0;
            int count = 0;
            foreach (Wheel wheel in car.motorWheels)
            {
                rpmSum += Mathf.Abs(wheel.wheelCollider.rpm);
                count++;
            }

            return rpmSum / count;
        }

        public float getAvregWheelsSpeed()
        {
            float speedsSum = 0;
            int count = 0;
            foreach (Wheel wheel in car.motorWheels)
            {
                //if (!wheel.wheelCollider.isGrounded)
                //  continue;
                speedsSum += wheel.speedObject.speedInKPH;
                count++;
            }
            return speedsSum / count;
        }

        public Vector3 getCarSpeedVector3()
        {
            // Rigidbody rigidbody = GetComponentInChildren<Rigidbody>();
            return  transform.InverseTransformDirection(car._rigidbody.velocity);
        }

        public float getMaxWheelsRPM()
        {
            float rpmSum = 0;
            foreach (Wheel wheel in car.motorWheels)
            {
                rpmSum = Mathf.Max(rpmSum, Mathf.Abs(wheel.wheelCollider.rpm));
            }
            return rpmSum;
        }

        public float getMaxWheelsSpeed()
        {
            float speedsSum = 0;
            foreach (Wheel wheel in car.motorWheels)
            {
                speedsSum = Mathf.Max(speedsSum, wheel.speedObject.speedInKPH);
            }
            return speedsSum;
        }

        public float getMinWheelsSpeed()
        {
            float speedsSum = car.motorWheels[0].speedObject.speedInKPH;
            foreach (Wheel wheel in car.motorWheels)
            {
                speedsSum = Mathf.Min(speedsSum, wheel.speedObject.speedInKPH);
            }
            return speedsSum;
        }

        public float getSpeedFactor()
        {
            return Mathf.Abs( speedObject.speedInKPH / car.maxSpeed);
        }

        public float getSumWheelsRPM()
        {
            float rpmSum = 0;
            foreach (Wheel wheel in car.motorWheels)
                rpmSum += wheel.wheelCollider.rpm * wheel.currentTorquePercent * wheel.parentDiffrential.currentTorquePercent;

            return rpmSum;
        }

        public float getSumWheelsSpeed()
        {
            float speedsSum = 0;
            foreach (Wheel wheel in car.motorWheels)
            {
                speedsSum += wheel.speedObject.speedInKPH * wheel.currentTorquePercent * wheel.parentDiffrential.currentTorquePercent;
            }
            return speedsSum;
        }

        public float getWheelsRPM()
        {
            //return getMaxWheelsRPM();
            // return getSumWheelsRPM();
            return getAvregWheelsRPM();
        }

        public float getWheelsSpeed()
        {
            // return getMaxWheelsSpeed();
            // return getMinWheelsSpeed();
            return getAvregWheelsSpeed();
            // return getSumWheelsSpeed();
        }

        public void handBrakeOff()
        {
            handBrakeReleasedListner();
        }

        public void handBrakeOn()
        {
            handBrakeListner();
        }

        public void setCarStatus(CarStause c_status)
        {
            statuse = c_status;
            switch (statuse)
            {
                case CarStause.Backward:
                    backwardListner();
                    break;
                case CarStause.Forward:
                    forwardListner();
                    break;
                case CarStause.Braking:
                    brakingListner();
                    break;
                case CarStause.Neutral:
                    neutralListner();
                    break;
                case CarStause.Stopped:
                    {
                        stoppedListner();

                    }
                    break;
            }
        }

        public void updateInputs(float steering, float accel)
        {
            AccelInput = accel * Throttle_Level;
            CurrentSteerInput = steering;
        }

        public void updateSpeed()
        {
            float speed = getCarSpeedVector3().z;
            if (Mathf.Abs(speed) < 0.01f)
                speedObject.setSpeed(0, Time.fixedDeltaTime);
            else
                speedObject.setSpeed(speed, Time.fixedDeltaTime);
        }

        private void ApplyDrive()
        {
            if (AccelInput > 0 && car.transmission.currentGear > -1 && statuse != CarStause.Braking)
                setCarStatus(CarStause.Forward);
            else if (AccelInput < 0 && car.transmission.currentGear < 1 && statuse != CarStause.Braking)
                setCarStatus(CarStause.Backward);
            else if (AccelInput != 0)
                setCarStatus(CarStause.Braking);
            else
            {
                setCarStatus(CarStause.Neutral);
            }
            if (speedObject.speedInKPH == 0 && (statuse == CarStause.Neutral || statuse == CarStause.Braking))
                setCarStatus(CarStause.Stopped);

            // Debug.Log(statuse);
        }

        // [DBG_Track("Blue")]
        // public float EngineTorque;
        void FixedUpdate()
        {
            updateSpeed();
            Wheel1Slip = car.wheels[1].slipRatio;
            Wheel1Brake = car.wheels[1].wheelCollider.brakeTorque;
              //  EngineTorque = car.engine.outputTorque;
        }

        void Start()
        {
            Throttle_Level = 1;
        }

        void Update()
        {
            ApplyDrive();
            //Debug.Log(car.backWheels[0].wheelCollider.brakeTorque);
        }

        #endregion Methods
    }
}