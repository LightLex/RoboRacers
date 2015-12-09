using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class LoadOptions : MonoBehaviour {

	//Inicializando datos de los controles
	void Start () {
		string lc = "A";
		string rc = "D";
		string uc = "W";
		string dc = "S";
		bool ss = true;

		//Cargar el archivo con los controles si existe
		if (File.Exists (Application.persistentDataPath + "/playerSettings.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerSettings.dat", FileMode.Open);
			PlayerOptions options = (PlayerOptions)bf.Deserialize(file);
			file.Close();
			lc = options.leftControl;
			rc = options.rightControl;
			uc = options.upControl;
			dc = options.downControl;

			ss = options.sound;
		}

		//Obteniendo datos de los objetos del juego
		InputField leftControl = GameObject.Find ("configControlLeft").GetComponent<InputField> ();
		InputField rightControl = GameObject.Find ("configControlRight").GetComponent<InputField> ();
		InputField upControl = GameObject.Find ("configControlUP").GetComponent<InputField> ();
		InputField downControl = GameObject.Find ("configControlDown").GetComponent<InputField> ();
		Toggle sound = GameObject.Find ("ToggleSonidos").GetComponent<Toggle> ();

		leftControl.text = lc;
		rightControl.text = rc;
		upControl.text = uc;
		downControl.text = dc;

		sound.isOn = ss;
	}
}


//Datos serializados de los controles
[Serializable]
class PlayerOptions {
	public string leftControl;
	public string rightControl;
	public string upControl;
	public string downControl;

	public bool sound;
}