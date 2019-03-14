using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelector : MonoBehaviour {

	public GameObject[] cars;
	public int activeIndex;

	public Dropdown dropDown;
	public string nextSceneName;

	// Use this for initialization
	void Start () {
		activeIndex = 0;
		cars [0].SetActive(true);
		for (int i = 1; i < cars.Length; i++) {
			cars[i].SetActive (false);
		}
	}

	void Update () {
		// Load corresponding scene according to selection from the dropdown
		loadCars(dropDown.value);
	}

	public void loadCars (int index) {
		if (index != activeIndex) {
			for (int i = 0; i < cars.Length; i++) {
				if (i == activeIndex) {
					cars [i].SetActive (false);
				}
				if (i == index) {
					cars [i].SetActive (true);
				}
			}
			activeIndex = index;
		}
	}

	public void race () {
		List<Dropdown.OptionData> menuOptions = dropDown.GetComponent<Dropdown> ().options;
		string carName = menuOptions [activeIndex].text;

		DataManager.Instance.body = activeIndex;
		DataManager.Instance.engine = activeIndex;
		DataManager.Instance.tire = activeIndex;

		SceneManager.LoadScene (nextSceneName);
	}
}
