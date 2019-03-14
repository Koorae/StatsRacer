namespace CarComponents.ElectronicSystems.BrakeSystems
{
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Wheels;

    using UnityEngine;

    public class ABS_Manager : WheelManager
    {
        #region Fields

        public float fixedTime = 0.1f;
        public float kd;
        public float ki;
        public float kp;
        public float minBrakeLimitPercent = 0.2f;

        #endregion Fields
    }
}