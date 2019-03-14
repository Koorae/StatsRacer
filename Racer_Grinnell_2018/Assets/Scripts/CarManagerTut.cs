using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarManagerTut : MonoBehaviour {

	//Car Skins
	public GameObject[] skins;
	private bool picked;

	void Start() {
		SceneManager.sceneLoaded += OnLevelLoaded;
		picked = false;
	}

	void Update() {
		if (!picked) {
			pickBodyType ();
			picked = true;
		}
	}

	//Pick Bodytype
	public void pickBodyType() {
		for (int i = 0; i < skins.Length; i++) {
			if (TutorialController.Instance.trialNum == i) {
				skins [i].gameObject.SetActive (true);
			} else {
				skins [i].gameObject.SetActive (false);
			}
		}
	}

	void OnLevelLoaded(Scene scene, LoadSceneMode mode)
	{
		picked = false;
	}

}