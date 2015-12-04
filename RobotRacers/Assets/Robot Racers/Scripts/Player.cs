using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public Text speed;
	public Text laps;
	public Text timer;
	public Text GameTimer;

	public Transform[] beacon;
	public Transform direction;

	private Rigidbody m_body;

	private int count=0;
	private int Vueltas=0;
	private int range = 20;
	private float timert = 5;

	float counttime = 0;

	public bool ready=false;

	
	void Start () {
		m_body = GetComponent<Rigidbody>();
		timert = 5;

	}

	void Update () {

			if(Vector3.Distance(beacon[count].transform.position,m_body.transform.position)<=range){
				if(count==0){
					Vueltas++;
				}

				if((count+1)==10){
					count=0;
				}else{
				count++;
				}
					
			}

		if (timert > 0) {
			timert -= Time.deltaTime;
			timer.text = ""+ (int) timert;
		} else {
			timer.enabled=false;
			ready=true;
			counttime+=Time.deltaTime;
			GameTimer.text = "Time: "+(int) counttime;
		}


			speed.text = (int) m_body.velocity.magnitude + "km/h";
			laps.text = "Vueltas: " + Vueltas;

			direction.LookAt(beacon[count]);

			if (Vueltas == 4) {
				Application.LoadLevel (0);
			}

	}
}
