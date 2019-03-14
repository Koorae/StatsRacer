using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

	// Store and persist the input group ID and player ID
public class PlayerData : MonoBehaviour {
	private static PlayerData _instance;
	public static PlayerData Instance { get { return _instance; } }

	public string playerId = "";
	public string groupId = "";

	void Awake () {
		if (_instance == null) {
			DontDestroyOnLoad (gameObject);
			_instance = this;
	} else if (_instance != this) {
			Destroy (gameObject);
		}
	}
}
