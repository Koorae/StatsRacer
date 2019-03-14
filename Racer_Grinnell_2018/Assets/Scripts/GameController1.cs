//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityStandardAssets.Vehicles.Car;
//
//public class GameController : MonoBehaviour {
//
//	//Fields controled by gameController
//	public EndPanel endPanel;
//	public RaceTimer raceTimer;
//	public SubmitData submitData;
//	public CountdownManager countdownManager;
//	public RacerCarController racerCarController;
//	public PlayerData playerData;
//
//	//In script fields
//	private bool gameInProgress = true;
//	public bool gameIsInProgress () {
//		return gameInProgress;
//	}
//
//    // Use this for initialization
//    void Start () {
//		gameStart ();
//	}
//
//	//Update on each frame
//	void Update () {
//		//If timer is not started and countdown is done, then start the timer.
//		if (countdownManager.counterDone && !raceTimer.timerStarted) {
//			raceTimer.timerStart ();
//		}
//		//If timer have started, then calculate time.
//		if (raceTimer.timerStarted) {
//			raceTimer.timeLasted = Time.timeSinceLevelLoad - raceTimer.startTime - 3; //Subtract the 3 seconds in the begining countdown.
//		}
//	}
//
//	//Setting the start of the game
//	public void gameStart () {
//		playerData.enabled = true;
//		gameInProgress = true;
//		racerCarController.pickBodyType ();
//	}
//
//	//Respond to a complete game
//	public void raceComplete () {
//		//If it is in progress then it is not complete
//		if (!gameIsInProgress ()) {
//			return;
//		}
//
//		//Show and update end panel
//		endPanel.gameObject.SetActive (true);
//		endPanel.updateStatsText ();
//		gameInProgress = false;
//	}
//		
//	//Back button function
//	public void exitGame() {
//		SceneManager.LoadScene ("menu2Hoang");
//	}
//
//	//main menu function
//	public void mainMenu() {
//		SceneManager.LoadScene ("menu1Hoang");
//	}
//
//	//Submit Data to the server
//	public void uploadData() {
//		submitData.Submit ();
//	}
//
//
//}
