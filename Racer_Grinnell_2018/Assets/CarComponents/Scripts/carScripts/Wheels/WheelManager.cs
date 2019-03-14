namespace CarComponents.Wheels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class WheelManager : CarPart
    {
        #region Fields

        public GameObject _PrefabToAddToWheels;

        #endregion Fields

        #region Methods

        public virtual void add(Wheel wheel)
        {
            GameObject tireSmoke = Instantiate(_PrefabToAddToWheels);
            tireSmoke.transform.SetParent(wheel.transform);
            tireSmoke.transform.localPosition = Vector3.zero;
        }

        // Use this for initialization
        public virtual void Start()
        {
            foreach (var wheel in car.wheels)
                add(wheel);
        }

        #endregion Methods
    }
}