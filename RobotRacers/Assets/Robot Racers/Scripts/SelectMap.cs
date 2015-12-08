﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectMap : MonoBehaviour {
	public static int mapid;

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
			mapid = 4;
		}
		else if (gameObject.name == "map2") {
			mapid = 5;
		}
	}

	public void startGame() {
		SelectCharStyled.statusGame.mapId = mapid;
		Application.LoadLevel (mapid);
	}
}
