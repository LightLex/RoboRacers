using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	//Inicialisando paneles de texto
	public Text speed;
	public Text laps;
	public Text timer;

	//Inicializando valores preterminados de los robots
	public int fuerza = 15000;
	public int vuelta = 5500;

	//Inicializando idicadores y flecha
	public Transform[] beacon;
	public Transform direction;

	//Inicializando robot
	private Rigidbody m_body;

	//Inicializando vueltas y distancia de los indicadores
	private int count=0;
	private int Vueltas=0;
	private int range = 25;
	private float timert = 5;

	//Inicializando valores de inicio y fin de carrera
	public bool ready=false;
	private bool finish = false;

	//Obteniendo robots
	public GameObject[] child;

	//Inicializando panel del fin de carrera
	public GameObject scoreP;
	
	void Start () {
		m_body = GetComponent<Rigidbody>(); //Obteniendo objeto robot
		timert = 5;		//Inicializando	timer de inicio
		finish = false;		//Inicializando valor de finalizacion de carerra

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

	//Funcion de conteo reverso para inicio de carrera
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

		//Estableciendo rangos de indicadores
		if (count == 0) {
			range = 25;
		} else {
			range = 40;
		}

			//Obteniendo la distancia entre robot y idicador actual
			if(Vector3.Distance(beacon[count].transform.position,m_body.transform.position)<=range){
				if(count==0){
					Vueltas++;	//Si pasa indicador 0 sumar vuelta
				}

				//Estableciendo siguente indicador
				if((count+1)==10){
					count=0;
				}else{
				count++;
				}
					
			}

		//Calculando velocidad y inmpimiendo velocidad y cantidad de vueltas en paneles de texto
		if (ready == true) {
			speed.text = (int) m_body.velocity.magnitude + "km/h";
			laps.text = "Vueltas: " + Vueltas;
		} else {
			funcion ();
		}

			//Direccionar la flecha hacia el siguente indicador
			direction.LookAt(beacon[count]);

		//Si cantidad de vueltas llega a 4 finalizar la carrera
			if (Vueltas == 4 ) {
			if(finish==false){
				int Score = 200 - (int) HoverCarControl.counttime;	//calcular el puntaje
				
				
				SelectCharStyled.statusGame.score += Score; //guardar el puntaje
				Debug.Log(SelectCharStyled.statusGame.mapId);

				//Guargar el tiempo y estableser siguente pista
				switch(SelectCharStyled.statusGame.mapId){	
						case 4:	if(SelectCharStyled.statusGame.time1 == 0 || SelectCharStyled.statusGame.time1 > (int) HoverCarControl.counttime){
									SelectCharStyled.statusGame.time1 = (int) HoverCarControl.counttime;
									}
								SelectCharStyled.statusGame.mapId=5;break;
						case 5:	if(SelectCharStyled.statusGame.time2 == 0 || SelectCharStyled.statusGame.time2 > (int) HoverCarControl.counttime){
									SelectCharStyled.statusGame.time2 = (int) HoverCarControl.counttime;
									}
					SelectCharStyled.statusGame.mapId=4;break;
				};

				//Guardar todo lo establecido
				Controller.setUserGameStatus(SelectCharStyled.statusGame);
			}
			//Estableser que carrera termino
				ready=false;
				finish=true;
				
			//Mostrar el panel de fin de carrera
				scoreP.SetActive(true);
			}

	}
}
