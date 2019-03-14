namespace CarComponents
{
    using System.Collections;

    using UnityEngine;

    public class CarPart : MonoBehaviour
    {
        #region Fields

        protected Car _car;
        protected CarControl _carControl;

        #endregion Fields

        #region Properties

        public Car car
        {
            get
            {
                if (this._car == null)
                    this._car = GetComponentInParent<Car>();
                return this._car;
            }
        }

        public CarControl carControl
        {
            get
            {
                if (this._carControl == null)
                    this._carControl = GetComponentInParent<CarControl>();
                return this._carControl;
            }
        }

        #endregion Properties
    }
}