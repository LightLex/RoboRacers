using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	//Cargar otra escena
	public void loadlevel(int level){
		Application.LoadLevel (level);
	}

	//Cargar mapa
	public void startGame() {
		Application.LoadLevel (SelectCharStyled.statusGame.mapId);
	}

	//Cargar juego nuevo
	public void newGame() {
		// Reset the user game status.
		Controller.resetGameStatus ();
		Application.LoadLevel (1);
	}
}
