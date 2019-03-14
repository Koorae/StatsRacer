namespace CarComponents
{
    using System.Collections;

    using UnityEngine;

    public class LightCollider : MonoBehaviour
    {
        #region Methods

        void OnTriggerEnter(Collider collider)
        {
            CarControl m_car = collider.transform.root.GetComponentInChildren<CarControl>();
            if (m_car != null)
            {
                m_car.frontLightsOn();
            }
        }

        void OnTriggerExit(Collider collider)
        {
            CarControl m_car = collider.transform.root.GetComponentInChildren<CarControl>();
            if (m_car != null)
            {
                m_car.frontLightsOff();
            }
        }

        #endregion Methods
    }
}