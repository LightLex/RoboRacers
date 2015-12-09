using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectMap : MonoBehaviour {

	Button start;
	void Start() {
		start = GameObject.Find ("start").GetComponent<Button> ();
		start.enabled = false;
	}

	public void goBack() {
		Application.LoadLevel (1);
	}

	public void selectMap () {
		start.enabled = true;
		if (gameObject.name == "map1") {
			SelectCharStyled.statusGame.mapId = 4;
		}
		else if (gameObject.name == "map2") {
			SelectCharStyled.statusGame.mapId = 5;
		}
	}

	public void startGame() {
		SelectCharStyled.statusGame.gameSaved = true;
		Controller.setUserGameStatus (SelectCharStyled.statusGame);
		Application.LoadLevel (SelectCharStyled.statusGame.mapId);
	}
}
