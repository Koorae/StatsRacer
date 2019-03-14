namespace CarComponents.UI
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    public class RpmMeter : DashboardPart
    {
        #region Fields

        Slider rpm;

        #endregion Fields

        #region Methods

        new void Start()
        {
            base.Start();
            rpm = GetComponent<Slider>();
            rpm.maxValue = dashboard.carControl.car.engine.maxRPM;
        }

        // Update is called once per frame
        void Update()
        {
            rpm.value = dashboard.carControl.car.engine.currentRPM;
        }

        #endregion Methods
    }
}