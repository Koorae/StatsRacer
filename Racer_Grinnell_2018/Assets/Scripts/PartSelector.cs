using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PartSelector : MonoBehaviour {

	public GameObject[] cars;
	public int bodyIndex;
	public int engineIndex;
	public int wheelIndex;

	public Dropdown bodyDropDown;
	public Dropdown engineDropDown;
	public Dropdown wheelDropDown;
	public string nextSceneName;

	// Use this for initialization
	void Start () {
		bodyIndex = 0;
		engineIndex = 0;
		wheelIndex = 0;
		cars [0].SetActive(true);
		for (int i = 1; i < cars.Length; i++) {
			cars[i].SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		loadCars(bodyDropDown.value);
		bodyIndex = bodyDropDown.value;
		engineIndex = engineDropDown.value;
		wheelIndex = wheelDropDown.value;
	}

	public void loadCars (int index) {
		if (index != bodyIndex) {
			for (int i = 0; i < cars.Length; i++) {
				if (i == bodyIndex) {
					cars [i].SetActive (false);
				}
				if (i == index) {
					cars [i].SetActive (true);
				}
			}
		}
		if (index == 0) {
			cars [0].SetActive (true);
		}
	}

	public void race () {
		if (bodyIndex != 0 && engineIndex != 0 && wheelIndex != 0) {
			DataManager.Instance.body = bodyIndex - 1;
			DataManager.Instance.engine = engineIndex - 1;
			DataManager.Instance.tire = wheelIndex - 1;
			SceneManager.LoadScene (nextSceneName);
		}
	}
}
