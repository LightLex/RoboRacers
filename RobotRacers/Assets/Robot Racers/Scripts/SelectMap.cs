using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectMap : MonoBehaviour {

	//Inicializar boton de inicio
	Button start;

	void Start() {
		start = GameObject.Find ("start").GetComponent<Button> (); //Obtener objeto del boton
		start.enabled = false;		//Hasta elegir el mapa hacerlo inactivo
	}

	public void goBack() {
		Application.LoadLevel (1);		//Volver al menu pasado
	}

	public void selectMap () {
		start.enabled = true;		//Activar el boton

		//Guardar valor del mapa elegido
		if (gameObject.name == "map1") {	
			SelectCharStyled.statusGame.mapId = 4;
		}
		else if (gameObject.name == "map2") {
			SelectCharStyled.statusGame.mapId = 5;
		}
	}

	//Cargar el nivel elegido
	public void startGame() {
		SelectCharStyled.statusGame.gameSaved = true;
		Controller.setUserGameStatus (SelectCharStyled.statusGame);
		Application.LoadLevel (SelectCharStyled.statusGame.mapId);
	}
}
