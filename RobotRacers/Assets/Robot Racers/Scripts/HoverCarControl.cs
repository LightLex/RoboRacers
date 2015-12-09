using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class HoverCarControl : MonoBehaviour
{

	/*
	 * Script que controla al robot.
	 * Contiene los valores del mismo robot que se establesen al inicio de la carrera.
	 * Hace flotar al robot y tiene todos los controles posibles guardadas para poder controlar al robot con teclado o un control.
	 * 
	 * */

	//Objeto robot
  Rigidbody m_body;
  float m_deadZone = 0.1f;

  public float m_hoverForce = 9.0f;
	//Fuerza de vuelo
  public float m_StabilizedHoverHeight = 2.0f;
	//Altura de vuelo
  public GameObject[] HoverPointsGameObjects;
	//Puntos de estabilizacion
  private float m_forwardAcl;
	//Aceleramiento
  public float m_backwardAcl = 25.0f;
	//Reversa
  float m_currThrust = 0.0f;
	//Velocidad actual (NO MODIFICAR)
  private float m_turnStrength;
	//SGira
  float CurrentTurnAngle = 0.0f;
	//Giro actual
  public GameObject LeftBreak;
	//Freno izquierdo
  public GameObject RightBreak;
	//Freno derecho

	//Panel de pausa
	public GameObject pausemenu;


	Player pscript;
  	int m_layerMask;

	//Codigos de teclas guardadas en opciones
	private KeyCode adelante;
	private KeyCode atras;
	private KeyCode izquierda;
	private KeyCode derecha;

	//Booleanos de inicio y fin de carrera
	bool ready = false;
	private bool pause = false;

	//Tiempo de carrera
	public static float counttime = 0;

	//Paneles de texto
	public Text GameTimer;
	public Text scoreT;

  void Start()
  {

		counttime = 0;		//Inicialisando tiempo de carrera
    m_body = GetComponent<Rigidbody>(); //Inicialsando el objeto robot
		pscript = GetComponent<Player> ();
    m_layerMask = 1 << LayerMask.NameToLayer("Characters");
    m_layerMask = ~m_layerMask;

		// Obteniendo controles de las opciones guardadas
		string lc = "A";
		string rc = "D";
		string uc = "W";
		string dc = "S";
		if (File.Exists (Application.persistentDataPath + "/playerSettings.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerSettings.dat", FileMode.Open);
			PlayerOptions options = (PlayerOptions)bf.Deserialize(file);
			file.Close();
			lc = options.leftControl;
			rc = options.rightControl;
			uc = options.upControl;
			dc = options.downControl;
		}
		adelante = (KeyCode)System.Enum.Parse(typeof(KeyCode), uc) ;
		atras = (KeyCode)System.Enum.Parse(typeof(KeyCode), dc) ;
		derecha = (KeyCode)System.Enum.Parse(typeof(KeyCode), rc) ;
		izquierda = (KeyCode)System.Enum.Parse(typeof(KeyCode), lc) ;
  }

	//Funcion de control para menu de pausa
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {  //Si tecla escape fue precionada aparece menu de pausa
			if(pause==false){
				pause=true;
				pausemenu.SetActive(true);
			}else{
				pause=false;
				pausemenu.SetActive(false);
			}
		}
	}


	//Funcion de control del robot
  void FixedUpdate()
  {
		ready = pscript.ready;		//Recibiendo valor de iniciar la carrera

		//Recibiendo valores del robor
		m_forwardAcl = pscript.fuerza;		
		m_turnStrength = pscript.vuelta;


			// Aceleramiento
			m_currThrust = 0.0f;
			if (ready == true && pause == false) {
			float aclAxis=0;

			if(Input.GetKey(adelante) || Input.GetKey(KeyCode.UpArrow)){		//Leendo teclas para acelerar con teclado
				aclAxis = 1;
			}else
				if(Input.GetKey(atras) || Input.GetKey(KeyCode.DownArrow)){		//Leendo teclas para reversa del teclado
					aclAxis = -1;
				}else
					aclAxis = Input.GetAxis ("Vertical");		//Leendo control si existe

			//Funcion de aceleracion
			if (aclAxis > m_deadZone)
				m_currThrust = aclAxis * m_forwardAcl;
			else if (aclAxis < -m_deadZone)
				m_currThrust = aclAxis * m_backwardAcl;


			// Giro
			CurrentTurnAngle = 0.0f;
			float turnAxis = 0;

			if(Input.GetKey(izquierda) || Input.GetKey(KeyCode.LeftArrow)){		//Leendo teclas para giro izquierdo con teclado 
				turnAxis = -1;
			}else
				if(Input.GetKey(derecha) || Input.GetKey(KeyCode.RightArrow)){		//Leendo teclas para giro derecho con teclado 
					turnAxis = 1;
				}else
					turnAxis = Input.GetAxis ("Horizontal");		//Leendo control si existe

			//Asiendo el giro
			if (Mathf.Abs (turnAxis) > m_deadZone)
				CurrentTurnAngle = turnAxis;

			//Contando el tiempo duracion de carrera
			counttime += Time.deltaTime;
			GameTimer.text = "Time: " + (int)counttime;
			scoreT.text = "Puntos obtenidos: " + (200 - (int)counttime);
			}
			// Empujandose de la tierra
			RaycastHit hit;
			for (int i = 0; i < HoverPointsGameObjects.Length; i++) { 	//Recibiendo los puntos de estabilizacion
				var hoverPoint = HoverPointsGameObjects [i];
			if (Physics.Raycast (hoverPoint.transform.position, 		//Calculando la distancia hasta la tierra
                          -Vector3.up, out hit,
                          m_StabilizedHoverHeight,
                          m_layerMask))
					m_body.AddForceAtPosition (Vector3.up 				//Aplicando fuerza para despegar
						* m_hoverForce
						* (1.0f - (hit.distance / m_StabilizedHoverHeight)), 
                                  hoverPoint.transform.position);
				else {
					if (transform.position.y > hoverPoint.transform.position.y)		//Si es muy alto disminuir fuerza
						m_body.AddForceAtPosition (
            hoverPoint.transform.up * m_hoverForce,
            hoverPoint.transform.position);
					else
						m_body.AddForceAtPosition (
            hoverPoint.transform.up * -m_hoverForce,
            hoverPoint.transform.position);
				}
			}

			// Aceleraminto calculado
			if (Mathf.Abs (m_currThrust) > 0)
				m_body.AddForce (transform.forward * m_currThrust);

			// Giro calculado
			if (CurrentTurnAngle > 0) {
				m_body.AddRelativeTorque (Vector3.up * CurrentTurnAngle * m_turnStrength);
			} else if (CurrentTurnAngle < 0) {
				m_body.AddRelativeTorque (Vector3.up * CurrentTurnAngle * m_turnStrength);
			}
		}
}
