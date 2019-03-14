namespace CarComponents.UI
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class DashboardPart : MonoBehaviour
    {
        #region Fields

        protected Dashboard dashboard;

        #endregion Fields

        #region Methods

        public void Start()
        {
            dashboard = GetComponentInParent<Dashboard>();
        }

        #endregion Methods
    }
}