namespace CarComponents.CarEffects
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Wheels;

    using UnityEngine;

    public class TireSmoke : WheelPart
    {
        #region Fields

        public GameObject smokePref;

        float emitAmount;
        private ParticleSystem smoke;

        #endregion Fields

        #region Methods

        void checkSkid()
        {
            smoke.Emit(1);
        }

        void initSmokePref()
        {
            GameObject smokeParticleSystemObject = Instantiate(smokePref);
            smokeParticleSystemObject.transform.SetParent(wheel.transform);
            Vector3 pos = Vector3.zero;
            pos.y = -wheel.radius;
            smokeParticleSystemObject.transform.localPosition = pos;
            Quaternion rotation = Quaternion.identity;
            rotation.y += 180;
            smokeParticleSystemObject.transform.localRotation = rotation;
            smoke = smokeParticleSystemObject.GetComponent<ParticleSystem>();
        }

        void Start()
        {
            initSmokePref();
            wheel.skidListner += checkSkid;
            wheel.stopSkidListner += stopSmoke;
        }

        private void stopSmoke()
        {
            smoke.Emit(0);
            smoke.Stop();
        }

        #endregion Methods
    }
}