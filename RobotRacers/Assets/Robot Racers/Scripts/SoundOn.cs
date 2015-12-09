using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SoundOn : MonoBehaviour {

	//Variable de sonido
	private bool on=true;

	void Awake(){

		//Leer el archivo de opciones
		if (File.Exists (Application.persistentDataPath + "/playerSettings.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerSettings.dat", FileMode.Open);
			PlayerOptions options = (PlayerOptions)bf.Deserialize(file);
			file.Close();
			on = options.sound; //Leer opciones
		}
		AudioSource audio = GetComponent<AudioSource> ();
		audio.mute = on;	//Estableser deacuerdo con las opciones
	}
}
