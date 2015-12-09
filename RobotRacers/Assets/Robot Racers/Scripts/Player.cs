using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public Text speed;
	public Text laps;
	public Text timer;

	public int fuerza = 15000;
	public int vuelta = 5500;

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

		/**
		 * Cargar el id del robot guardado en la seleccion.
		 * Si no existe el archivo de guardado regresar a la pantalla de eleccion del robot.
		 */
		UserGameStatus statusGame = Controller.getUserGameStatus ();
		if (statusGame != null) {
			child [statusGame.botId].SetActive (true);
			fuerza = statusGame.botFuerza;
			vuelta = statusGame.botVuelta;
		} else {
			Application.LoadLevel(1);
		}


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
				int Score = 200 - (int) HoverCarControl.counttime;
				
				SelectCharStyled.statusGame.score += Score;
			if(ready==true){
				if(SelectCharStyled.statusGame.mapId==4){
					SelectCharStyled.statusGame.mapId=5;
				}else{
					SelectCharStyled.statusGame.mapId=4;
				}
			}
				ready=false;
				Controller.setUserGameStatus(SelectCharStyled.statusGame);
				
				
				scoreP.SetActive(true);
			}

	}
}
