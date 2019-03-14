namespace CarComponents.UI
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    public class GearLabel : DashboardPart
    {
        #region Fields

        Text currentGear;

        #endregion Fields

        #region Methods

        public new void Start()
        {
            base.Start();
            currentGear = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            currentGear.text = dashboard.carControl.car.transmission.getCurrentGearLabel();
        }

        #endregion Methods
    }
}