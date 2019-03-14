namespace CarComponents.Helpers
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class TrackableField<T>
    {
        #region Fields

        protected T _oldValue;
        protected T __value;

        #endregion Fields

        #region Properties

        public T oldValue
        {
            get
            {
                return _oldValue;
            }
        }

        public T _value
        {
            get
            {
                return __value;
            }
            set
            {
                _oldValue = __value;
                __value = value;
            }
        }

        #endregion Properties
    }
}