namespace CarComponents
{
    using System.Collections.Generic;

    using UnityEngine;

    /*
     *This Component reads the user input and send it to the CarControl in The parent component.
     * There is a "UserInput" Prefab allready created which this script attached to, and can be used
     * by droping the prefab in the car hierarchy
     */
    public class UserInput : CarPart
    {
        #region Fields

        const string HANDBRAKE_KEY = "Fire1"; //Handbrake botton
        const string Horizontal_KEY = "Horizontal"; //Steering keys
        const KeyCode Throttle_Key_1 = KeyCode.W;
        const KeyCode Throttle_Key_2 = KeyCode.S;
        const KeyCode Throttle_Key_3 = KeyCode.Z;
        const string Vertical_KEY = "Vertical"; // Acceleration, Brake, and Revearse Keys

        Dictionary<KeyCode, float> Throttle_Level_KEYS = new Dictionary<KeyCode, float>
        {
            {Throttle_Key_1,1 }, {Throttle_Key_2,0.5f }, {Throttle_Key_3,0.25f }
        };

        #endregion Fields

        #region Methods

        void check_Throttle_Level_Pressed()
        {
            if (Input.GetKeyDown(Throttle_Key_1))
                carControl.Throttle_Level = Throttle_Level_KEYS[Throttle_Key_1];
            else if (Input.GetKeyDown(Throttle_Key_2))
                carControl.Throttle_Level = Throttle_Level_KEYS[Throttle_Key_2];
            else if (Input.GetKeyDown(Throttle_Key_3))
                carControl.Throttle_Level = Throttle_Level_KEYS[Throttle_Key_3];
        }

        void FixedUpdate()
        {
            //Only Commands That needs
        }

        void Update()
        {
            //Sends the User Steering and Acceleration commands to the carControl Object
            carControl.updateInputs(Input.GetAxis(Horizontal_KEY), Input.GetAxis(Vertical_KEY));

            //Check if HandBrake released
            if (Input.GetButtonUp(HANDBRAKE_KEY))
                carControl.handBrakeOff();
            //Otherwise check if Handbrake pressed
            else if (Input.GetButtonDown(HANDBRAKE_KEY))
                carControl.handBrakeOn();

            check_Throttle_Level_Pressed();
        }

        #endregion Methods
    }
}