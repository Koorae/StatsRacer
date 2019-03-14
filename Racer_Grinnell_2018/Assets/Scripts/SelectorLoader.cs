using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectorLoader : MonoBehaviour {

	public Text playerIDText;
	public Text groupIDText;
	
	public void loadTutorial() {
		SceneManager.LoadScene ("TutorialInstructions");
	}

	public void loadMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}

	public void loadCarSelect() {
		SceneManager.LoadScene ("CarSelect");
	}

	public void loadPartsSelect() {
		SceneManager.LoadScene ("PartsSelect");
	}

	public void openDataPage() {
		Application.OpenURL("https://www.stat2games.sites.grinnell.edu/data/racer/racer.php");
	}

	public void setIDs() {
		PlayerData.Instance.playerId = playerIDText.text;
		PlayerData.Instance.groupId = groupIDText.text;
	}
}
