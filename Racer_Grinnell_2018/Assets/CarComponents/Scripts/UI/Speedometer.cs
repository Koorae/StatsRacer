namespace CarComponents.UI
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    public class Speedometer : DashboardPart
    {
        #region Fields

        Text speed;

        #endregion Fields

        #region Methods

        public new void Start()
        {
            base.Start();
            speed = GetComponent<Text>();
        }

        void Update()
        {
            speed.text = Mathf.Ceil( dashboard.carControl.speedObject.speedInKPH)+string.Empty;
        }

        #endregion Methods
    }
}