using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public static void setUserGameStatus(UserGameStatus gameStatus) {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/UserGameStatus.dat");
		bf.Serialize (file, gameStatus);
		file.Close ();
	}
	
	public static UserGameStatus getUserGameStatus() {
		if (File.Exists (Application.persistentDataPath + "/UserGameStatus.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/UserGameStatus.dat", FileMode.Open);
			UserGameStatus gameStatus = (UserGameStatus)bf.Deserialize (file);
			file.Close ();
			return gameStatus;
		} else {
			return null;
		}
	}
}

[Serializable]
public class UserGameStatus{
	public int botId;
	public string botName;
	public int botFuerza;
	public int botVuelta;
	public int mapId;
	public int score;
}
