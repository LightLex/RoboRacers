using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

	// Inicializando el controller para guardar los datos del juego
	void Start () {
		UserGameStatus ugstatus = getUserGameStatus ();
		Button btnContinue = GameObject.Find("Continue").GetComponent<Button>();
		if (ugstatus.gameSaved) {
			btnContinue.enabled = true;
		} else {
			btnContinue.enabled = false;
		}
	}

	//Funcion para guardar los datos durante el juego
	public static void setUserGameStatus(UserGameStatus gameStatus) {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/UserGameStatus.dat");
		bf.Serialize (file, gameStatus);
		file.Close ();
	}

	//Funcion para obtener los datos durante el juego
	public static UserGameStatus getUserGameStatus() {
		if (File.Exists (Application.persistentDataPath + "/UserGameStatus.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/UserGameStatus.dat", FileMode.Open);
			UserGameStatus gameStatus = (UserGameStatus)bf.Deserialize (file);
			file.Close ();
			return gameStatus;
		} else {
			return new UserGameStatus();
		}
	}


	//Funcion para anular los datos a preterminados
	public static void resetGameStatus() {
		if (File.Exists (Application.persistentDataPath + "/UserGameStatus.dat")) {
			File.Delete(Application.persistentDataPath + "/UserGameStatus.dat");
		}
	}
}

//Datos serializados que seran gravadas en el archivo
[Serializable]
public class UserGameStatus{
	public bool gameSaved = false;
	public int botId;
	public string botName;
	public int botFuerza;
	public int botVuelta;
	public int mapId;
	public int score;
	public int time1;
	public int time2;
}
