using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public Text speed;
	public Text laps;
	public Text timer;

	public Transform[] beacon;
	public Transform direction;

	private Rigidbody m_body;

	private int count=0;
	private int Vueltas=0;
	private int range = 25;
	private float timert = 5;

	public bool ready=false;

	public GameObject[] child;


	public GameObject scoreP;
	
	void Start () {
		m_body = GetComponent<Rigidbody>();
		timert = 5;

		int num = Random.Range(0, 4);

		child[num].SetActive(true);
		

	}

	void funcion(){
		if (timert > 0) {
			timert -= Time.deltaTime;
			timer.text = "" + (int)timert;
		} else {
			timer.enabled = false;
			ready = true;
		}
	}

	void Update () {

		if (count == 0) {
			range = 25;
		} else {
			range = 40;
		}

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

		if (ready == true) {
			speed.text = (int) m_body.velocity.magnitude + "km/h";
			laps.text = "Vueltas: " + Vueltas;
		} else {
			funcion ();
		}

			direction.LookAt(beacon[count]);

			if (Vueltas == 4) {
				ready=false;
				scoreP.SetActive(true);
			}

	}
}
