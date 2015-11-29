using UnityEngine;
using System.Collections;

public class Movment : MonoBehaviour
{
	public float speed;
	public float tilt;

	void Update(){
		if(Input.GetKey(KeyCode.W)){
			speed=20;
			transform.Translate(new Vector3(0,0,speed)*Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.S)){
			speed=-20;
			transform.Translate(new Vector3(0,0,speed)*Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.D)){
			speed=20;
			transform.Translate(new Vector3(speed,0,0)*Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.A)){
			speed=-20;
			transform.Translate(new Vector3(speed,0,0)*Time.deltaTime);
		}

	}
}