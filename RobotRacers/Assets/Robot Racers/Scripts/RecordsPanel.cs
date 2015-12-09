using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecordsPanel : MonoBehaviour {

	//Inicialisando los textos
	public Text puntos;
	public Text time1;
	public Text time2;

	void Start(){
		//Cargando datos ya guardados
		UserGameStatus statusGame = Controller.getUserGameStatus ();
		if (statusGame != null) {
			//Estableciendo los datos cargados en los paneles de texto
			time1.text = "Mejor tiempo del mapa 1: " + statusGame.time1;
			time2.text = "Mejor tiempo del mapa 2: " + statusGame.time2;
			puntos.text = "" + statusGame.score;
		} else {
			time1.text = "Mejor tiempo del mapa 1: 150";
			time2.text = "Mejor tiempo del mapa 2: 150";
		}
	}


}
