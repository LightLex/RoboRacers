using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveOptions : MonoBehaviour {
	public void save() {
		InputField leftControl = GameObject.Find ("configControlLeft").GetComponent<InputField> ();
		InputField rightControl = GameObject.Find ("configControlRight").GetComponent<InputField> ();
		InputField upControl = GameObject.Find ("configControlUP").GetComponent<InputField> ();
		InputField downControl = GameObject.Find ("configControlDown").GetComponent<InputField> ();
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
}
