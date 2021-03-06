using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Class to do a character selection on Unity3D. Styled Version
 * 
 * @author Jefferson Henrique
 * */
public class SelectCharStyled : MonoBehaviour {
	
	// The left marker out of visible scence
	public Transform markerLeft2;
	// The left marker of visible scence
	public Transform markerLeft;
	// The middle marker of visible scence
	public Transform markerMiddle;
	// The Right marker of visible scence
	public Transform markerRight;

	// The characters prefabs to pick
	public Transform[] charsPrefabs;
	// An aux array to be used on ShowSelectedChar.cs
	public static Transform[] charsPrefabsAux;
	
	// The game objects created to be showed on screen
	private GameObject[] chars;
	
	// The index of the current character
	public static int currentChar = 0;

	public static UserGameStatus statusGame;

	//Funcion inicial de incializacion
	void Start() {
		int bots = 2;
		UserGameStatus status = Controller.getUserGameStatus ();
		if (status.gameSaved) {
			if (status.score >= 100) bots = 3;
			if (status.score >= 200) bots = 4;
		}

		charsPrefabsAux = charsPrefabs;
		// We initialize the chars array
		chars = new GameObject[bots];
		
		//Leendo la lista de los robots
		int index = 0;
		foreach (Transform t in charsPrefabs) {
			if (index < bots){
				chars[index++] = GameObject.Instantiate(t.gameObject, markerMiddle.position, Quaternion.identity) as GameObject;
			}
		}
	}

	//Creando controles en la pantalla
	void OnGUI() {
		//Creando boton de robot anterior
		if (GUI.Button(new Rect(10, (Screen.height - 50) / 2, 100, 50), "Anterior")) {
			currentChar--;
			
			if (currentChar < 0) {
				currentChar = 0;
			}
		}
		
		// Creando boton del siguente robot
		if (GUI.Button(new Rect(Screen.width - 100 - 10, (Screen.height - 50) / 2, 100, 50), "Siguiente")) {
			currentChar++;
			
			if (currentChar >= chars.Length) {
				currentChar = chars.Length - 1;
			}
		}
		
		//Mostrando los modelos de robots
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GameObject selectedChar = chars[currentChar];
		string labelChar = selectedChar.name;
		GUI.Label(new Rect((Screen.width - 200) / 2, 20, 200, 50), labelChar);
		
		if (GUI.Button(new Rect((Screen.width - 100) / 2, Screen.height - 70, 100, 50), "Elegir")) {
			statusGame = Controller.getUserGameStatus();
			statusGame.botId = currentChar;
			switch(currentChar){
			case 0: statusGame.botFuerza=15500; statusGame.botVuelta=5500;break;
			case 1: statusGame.botFuerza=16500; statusGame.botVuelta=5000;break;
			case 2: statusGame.botFuerza=14000; statusGame.botVuelta=5500;break;
			case 3: statusGame.botFuerza=16000; statusGame.botVuelta=6000;break;
			}
			statusGame.botName = selectedChar.name;
			Controller.setUserGameStatus(statusGame);

			Application.LoadLevel(3);
		}

		if (GUI.Button(new Rect(10, Screen.height - 70, 100, 50), "Volver")) {
			Application.LoadLevel(0);
		}
		
		// The index of the middle character
		int middleIndex = currentChar;	
		// The index of the left character
		int leftIndex = currentChar - 1;
		// The index of the right character
		int rightIndex = currentChar + 1;
		
		// For each character we set the position based on the current index
		for (int index = 0; index < chars.Length; index++) {
			Transform transf = chars[index].transform;
			
			// If the index is less than left index, the character will dissapear in the left side
			if (index < leftIndex) {
				transf.position = Vector3.Lerp(transf.position, markerLeft2.position, Time.deltaTime);
				
			// If the index is less than right index, the character will dissapear in the right side
			} else if (index > rightIndex) {
				transf.position = Vector3.Lerp(transf.position, markerRight.position, Time.deltaTime);
				
			// If the index is equals to left index, the character will move to the left visible marker
			} else if (index == leftIndex) {
				transf.position = Vector3.Lerp(transf.position, markerLeft.position, Time.deltaTime);
				
			// If the index is equals to middle index, the character will move to the middle visible marker
			} else if (index == middleIndex) {
				transf.position = Vector3.Lerp(transf.position, markerMiddle.position, Time.deltaTime);
				
			// If the index is equals to right index, the character will move to the right visible marker
			} else if (index == rightIndex) {
				transf.position = Vector3.Lerp(transf.position, markerRight.position, Time.deltaTime);
			}
		}
	}
	
}
