using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class CarAudio : MonoBehaviour {
		
		public AudioSource jetSound;
		private float jetPitch;
		private const float LowPitch = .1f;
		private const float HighPitch = 2.0f;
		private const float SpeedToRevs = .01f;
		Vector3 myVelocity;
		Rigidbody carRigidbody;
		
		void Awake () 
		{
			carRigidbody = GetComponent<Rigidbody>();

			if (File.Exists (Application.persistentDataPath + "/playerSettings.dat")) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/playerSettings.dat", FileMode.Open);
				PlayerOptions options = (PlayerOptions)bf.Deserialize(file);
				file.Close();
			jetSound.mute = options.sound;
			}

		}
		
		private void FixedUpdate()
		{
			myVelocity = carRigidbody.velocity;
			float forwardSpeed = transform.InverseTransformDirection(carRigidbody.velocity).z;
			float engineRevs = Mathf.Abs (forwardSpeed) * SpeedToRevs;
			jetSound.pitch = Mathf.Clamp (engineRevs, LowPitch, HighPitch);
		}
		
}
