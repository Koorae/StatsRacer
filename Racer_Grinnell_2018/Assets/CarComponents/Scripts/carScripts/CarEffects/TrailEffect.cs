namespace CarComponents.CarEffects
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class TrailEffect : CarPart
    {
        #region Fields

        TrailRenderer[] childrenTrailEffect;

        #endregion Fields

        #region Methods

        void disableEffect()
        {
            foreach (TrailRenderer trailRenderer in childrenTrailEffect)
                trailRenderer.gameObject.SetActive(false);
        }

        void enableEffect()
        {
            foreach (TrailRenderer trailRenderer in childrenTrailEffect)
                trailRenderer.gameObject.SetActive(true);
        }

        void Start()
        {
            childrenTrailEffect = GetComponentsInChildren<TrailRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (carControl.speedObject.speedInKPH > 100)
                enableEffect();
            else
                disableEffect();
        }

        #endregion Methods
    }
}