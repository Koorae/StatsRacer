namespace CarComponents.ElectronicSystems.StabilitySystems
{
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Wheels;

    using UnityEngine;

    public class TCS_Manager : WheelManager
    {
        #region Fields

        public float fixedTime = 0.1f;
        public float kd;
        public float ki;
        public float kp;
        public float maxBrakeLimitPercent = 0.2f;

        #endregion Fields
    }
}