namespace CarComponents
{
    using CarComponents.Wheels;

    using UnityEngine;

    [System.Serializable]
    public class HandBrake : CarPart
    {
        #region Fields

        const string HANDBRAKE_REQUEST_KEY = "HandBrake"; //A key for the Brake Request Dictionary

        #endregion Fields

        #region Methods

        public void doHandBrake()
        {
            foreach (Wheel wheel in this.car.backWheels)
                wheel.brake.brakeControllersRequestes[HANDBRAKE_REQUEST_KEY] = wheel.brake.maxBrakeForce;
            // Debug.Log("HandBrake On");
        }

        public void releaseHandBrake()
        {
            foreach (Wheel wheel in car.backWheels)
                wheel.brake.brakeControllersRequestes[HANDBRAKE_REQUEST_KEY]= 0;
            //Debug.Log("HandBrake Off");
        }

        void Start()
        {
            carControl.handBrakeListner += doHandBrake;
            carControl.handBrakeReleasedListner += releaseHandBrake;
            foreach (Wheel wheel in car.backWheels)
                wheel.brake.brakeControllersRequestes.Add(HANDBRAKE_REQUEST_KEY,0);
        }

        #endregion Methods
    }
}