using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecordsPanel : MonoBehaviour {

	public GameObject panel;
	public Text time1;
	public Text time2;


	public void Show(){
		UserGameStatus statusGame = Controller.getUserGameStatus ();
		if (statusGame != null) {
			time1.text = "Mejor tiempo del mapa 1: " + statusGame.time1;
			time2.text = "Mejor tiempo del mapa 2: " + statusGame.time2;
		} else {
			time1.text = "Mejor tiempo del mapa 1: 150";
			time2.text = "Mejor tiempo del mapa 2: 150";
		}
		panel.SetActive (true);
	}

	public void Close(){
		panel.SetActive (false);
	}

}
