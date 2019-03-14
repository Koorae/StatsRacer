namespace CarComponents.UI
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;

    public class Dashboard : MonoBehaviour
    {
        #region Fields

        public CarControl carControl;

        #endregion Fields

        #region Methods

        void OnEnable()
        {
            if (carControl == null)
                carControl = GameObject.FindObjectOfType<CarControl>();
            if (carControl == null)
                gameObject.SetActive(false);
        }

        #endregion Methods
    }
}