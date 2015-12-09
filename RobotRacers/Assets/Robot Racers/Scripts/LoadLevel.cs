using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	public void loadlevel(int level){
		Application.LoadLevel (level);
	}

	public void startGame() {
		Application.LoadLevel (SelectCharStyled.statusGame.mapId);
	}

	public void newGame() {
		// Reset the user game status.
		Controller.resetGameStatus ();
	}
}
