using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataModel : MonoBehaviour {
	//unrecorded
	public List<int> gameNum = new List<int>();
	public List<DateTime> dateTime = new List<DateTime>();
	public List<string> playerID = new List<string>();
	public List<string> groupID = new List<string>();
	public List<int> level = new List<int>();
	//unrecorded
	public List<int> order = new List<int> ();
	//unrecorded
	public List<string> track = new List<string>();
	public List<int> car = new List<int> ();
	public List<string> body = new List<string>();
	public List<string> engine = new List<string>();
	public List<string> tire = new List<string>();
	public List<bool> finished = new List<bool>();
	public List<float> finishTime = new List<float>();
	public List<float> timeto30 = new List<float>();
	public List<float> timeto60 = new List<float>();
	public List<float> timeto100 = new List<float>();
	public List<float> checkPoint1 = new List<float>();
	public List<float> checkPoint2 = new List<float>();
	public List<float> checkPoint3 = new List<float>();
	public List<float> timeOnTrack = new List<float>();
	public List<float> timeOffTrack = new List<float>();
	public List<float> mass = new List<float>();
	public List<float> drag = new List<float>();
	public List<float> traction = new List<float>();
	public List<float> torque = new List<float>();
	public List<float> topSpeed = new List<float>();
	public List<float> tireRadius = new List<float>();

	public DataModel() {

	}

	public void AddData() {

		this.dateTime.Add (DateTime.Now);
		this.playerID.Add (PlayerData.Instance.playerId);
		this.groupID.Add (PlayerData.Instance.groupId);
		this.level.Add (DataManager.Instance.level);
		this.level.Add (DataManager.Instance.car);
		this.body.Add (DataManager.Instance.bodyName);
		this.engine.Add (DataManager.Instance.engineName);
		this.tire.Add (DataManager.Instance.tireName);
		this.finished.Add (!GameController.Instance.racing);
		this.finishTime.Add (truncate(GameController.Instance.time));
		this.timeto30.Add (truncate(CarData.Instance.timeTo30));
		this.timeto60.Add (truncate(CarData.Instance.timeTo60));
		this.timeto100.Add (truncate(CarData.Instance.timeTo100));
		this.mass.Add (truncate(DataManager.Instance.mass));
		this.drag.Add (truncate(DataManager.Instance.drag));
		this.traction.Add (truncate(DataManager.Instance.traction));
		this.torque.Add (truncate(DataManager.Instance.torque));
		this.topSpeed.Add (truncate(DataManager.Instance.topSpeed));
		this.tireRadius.Add (truncate(DataManager.Instance.tireRadius));
	}

	private float truncate(float f) {
		return (float)(Math.Truncate((double)f*100.0) / 100.0);
	}
}
