namespace CarComponents.Helpers
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public abstract class DiffrentialAbleField<T>
    {
        #region Fields

        protected TrackableField<T> trackableValue = new TrackableField<T>();

        #endregion Fields

        #region Properties

        public T diffrentialValue
        {
            get; protected set;
        }

        public T value
        {
            get
            {
               return trackableValue._value;
            }
              protected  set
            {
                trackableValue._value = value;
            }
        }

        #endregion Properties

        #region Methods

        public abstract void setValue(T value, float deltaTime);

        #endregion Methods

        #region Other

        //abstract public T fieldDiffrential(float deltaTime);

        #endregion Other
    }
}