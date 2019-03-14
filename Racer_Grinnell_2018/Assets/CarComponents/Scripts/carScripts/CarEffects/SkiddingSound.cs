namespace CarComponents.CarEffects
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using CarComponents.Wheels;

    using UnityEngine;

    public class SkiddingSound : CarPart
    {
        #region Fields

        public float maxRolloffDistance = 30;

        float camDist;
        float skidAmount;

        //AudioClip skiddingAudioClip;
        AudioSource skiddingAudioSource;

        #endregion Fields

        #region Methods

        private void checkSkid(Wheel wheel)
        {
            skiddingAudioSource.volume =  Mathf.Max(wheel.skidAmount, skiddingAudioSource.volume);
            if (skiddingAudioSource.isPlaying || camDist > maxRolloffDistance * maxRolloffDistance)
                return;
            if (wheel.skidAmount > 0.2f)
                //skiddingAudioSource.volume *= camDist / (maxRolloffDistance * maxRolloffDistance);
                startSound();
        }

        private void checkToStopSound()
        {
            if (!skiddingAudioSource.isPlaying)
                return;

            foreach (Wheel wheel in car.wheels)
            {
                if (wheel.skidAmount >0)
                    return;
            }
            stopSkidSound();
        }

        // Update is called once per frame
        void initAudioSource()
        {
            skiddingAudioSource = GetComponent<AudioSource>();
            skiddingAudioSource.loop = false;
            skiddingAudioSource.minDistance = 5;
            skiddingAudioSource.maxDistance = maxRolloffDistance;
        }

        // Use this for initialization
        void Start()
        {
            initAudioSource();
            foreach (var wheel in car.wheels)
            {
                wheel.wheelSkidListner += checkSkid;
                // wheel.stopSkidListner += stopSkidSound;
            }
        }

        private void startSound()
        {
            skiddingAudioSource.Play();
        }

        private void stopSkidSound()
        {
            skiddingAudioSource.volume = 0;
            skiddingAudioSource.Stop();
        }

        void Update()
        {
            // get the distance to main camera
            camDist = (Camera.main.transform.position - transform.position).sqrMagnitude;
            checkToStopSound();
        }

        #endregion Methods
    }
}