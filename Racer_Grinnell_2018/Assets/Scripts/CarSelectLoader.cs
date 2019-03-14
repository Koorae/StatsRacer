using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSelectLoader : MonoBehaviour {

	public GameObject dropdown;

	// Use this for initialization
	void Start () {

	}

	// Load the scene with the specified index
	public void loadLevel (int a) {
		SceneManager.LoadScene (a);
	}

	// Load the race scene with the corresponding car selected from the dropdown menu
	public void race (int a) {
		int carSelected = dropdown.GetComponent<Dropdown> ().value; //Get car number
		List<Dropdown.OptionData> menuOptions = dropdown.GetComponent<Dropdown> ().options;
		string carName = menuOptions [carSelected].text;

		//Store data to the playerData and PlayerPrefs
		//PlayerData.playerdata.car = carName;
		PlayerPrefs.SetString ("car", carName);

		// Do nothing if no car is selected
		if (carSelected != 0) {
			SceneManager.LoadScene (2, LoadSceneMode.Single);
		}
	}
}
