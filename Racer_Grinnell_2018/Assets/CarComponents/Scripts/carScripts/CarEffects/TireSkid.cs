namespace CarComponents.CarEffects
{
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Wheels;

    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class TireSkid : WheelPart
    {
        #region Fields

        public GameObject skidPref;

        float skidAmount;
        GameObject skidTrail;

        #endregion Fields

        #region Methods

        public void EndSkidTrail()
        {
            if (skidTrail == null)
                return;
            //skidTrail.transform.parent = SceneManager.GetActiveScene().GetRootGameObjects()[0].transform;
            Destroy(skidTrail);
        }

        public void StartSkidTrail()
        {
            if(skidTrail==null)
            initSkidPref();
        }

        void checkSkid()
        {
            //if (wheel.skidAmount * 3 >= 1)
                StartSkidTrail();
        }

        void initSkidPref()
        {
            skidTrail = Instantiate(skidPref);
            skidTrail.transform.SetParent(wheel.transform);
            Vector3 pos = wheel.wheelHit.point; //wheel.transform.up * -wheel.radius;
            skidTrail.transform.position = pos;
            Destroy(skidTrail, 10);
        }

        void Start()
        {
            wheel.skidListner += checkSkid;
            wheel.stopSkidListner += EndSkidTrail;
        }

        #endregion Methods
    }
}