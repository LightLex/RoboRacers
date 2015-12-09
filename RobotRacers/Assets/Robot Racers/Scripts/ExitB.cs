using UnityEngine;
using System.Collections;

public class ExitB : MonoBehaviour {

	//Fucnion para salir del juego
	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}