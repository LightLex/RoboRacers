using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveOptions : MonoBehaviour {

	//Inicializando varialbes de los controles
	InputField leftControl;
	InputField rightControl;
	InputField upControl;
	InputField downControl;

	//Funcion que le los objetos del juego y los guarda en archivo
	public void save() {
		Toggle sound = GameObject.Find ("ToggleSonidos").GetComponent<Toggle> ();

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerSettings.dat");
		PlayerOptions options = new PlayerOptions ();

		// Controles.
		options.leftControl = leftControl.text != "" ? leftControl.text : "A";
		options.rightControl = rightControl.text != "" ? rightControl.text : "D";
		options.upControl = upControl.text != "" ? upControl.text : "W";
		options.downControl = downControl.text != "" ? downControl.text : "S";

		// Sonido.
		options.sound = sound.isOn;

		bf.Serialize (file, options);
		file.Close ();
		print ("Se han guardado la opciones");
	}

	//Fucnion que incializa los objetos del juego y eschucha sus cambios
	void Awake () {
		leftControl = GameObject.Find ("configControlLeft").GetComponent<InputField> ();
		rightControl = GameObject.Find ("configControlRight").GetComponent<InputField> ();
		upControl = GameObject.Find ("configControlUP").GetComponent<InputField> ();
		downControl = GameObject.Find ("configControlDown").GetComponent<InputField> ();

		// Sets the ToUpperCase function to each element.
		leftControl.onValueChange.AddListener( delegate { ToUpperCase(leftControl); } );
		rightControl.onValueChange.AddListener( delegate { ToUpperCase(rightControl); } );
		upControl.onValueChange.AddListener( delegate { ToUpperCase(upControl); } );
		downControl.onValueChange.AddListener( delegate { ToUpperCase(downControl); } );
	}

	//Funcion para estableser los controles en Mayuscula
	void ToUpperCase(InputField field) {
		string text = field.text;
		if(text != field.text.ToUpper())
		{
			field.text = field.text.ToUpper();
		}
	}
}
