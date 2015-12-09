using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class HoverCarControl : MonoBehaviour
{
  Rigidbody m_body;
  float m_deadZone = 0.1f;

  public float m_hoverForce = 9.0f;
	//Force of hover
  public float m_StabilizedHoverHeight = 2.0f;
	//Height of hover
  public GameObject[] HoverPointsGameObjects;
	//Points where hover will push down
  private float m_forwardAcl;
	//Foward Acceleation of car
  public float m_backwardAcl = 25.0f;
	//Backwords/reverse Acceleration of car
  float m_currThrust = 0.0f;
	//Do not modify! Current speed
  private float m_turnStrength;
	//Strength of the turn
  float CurrentTurnAngle = 0.0f;
	//Current Turn Rotation
  public GameObject LeftBreak;
	//Break Left GameObject
  public GameObject RightBreak;

	public GameObject pausemenu;
	//Break Right GameObject

	Player pscript;
  	int m_layerMask;

	private KeyCode adelante;
	private KeyCode atras;
	private KeyCode izquierda;
	private KeyCode derecha;

	bool ready = false;
	private bool pause = false;

	public static float counttime = 0;
	public Text GameTimer;
	public Text scoreT;

  void Start()
  {
		counttime = 0;
    m_body = GetComponent<Rigidbody>();
		pscript = GetComponent<Player> ();
    m_layerMask = 1 << LayerMask.NameToLayer("Characters");
    m_layerMask = ~m_layerMask;

		// Get the user configuration Control keys.
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

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if(pause==false){
				pause=true;
				pausemenu.SetActive(true);
			}else{
				pause=false;
				pausemenu.SetActive(false);
			}
		}
	}

  void FixedUpdate()
  {
		ready = pscript.ready;
		m_forwardAcl = pscript.fuerza;
		m_turnStrength = pscript.vuelta;


			// Main Thrust
			m_currThrust = 0.0f;
			if (ready == true && pause == false) {
			float aclAxis=0;

			if(Input.GetKey(adelante) || Input.GetKey(KeyCode.UpArrow)){
				aclAxis = 1;
			}else
				if(Input.GetKey(atras) || Input.GetKey(KeyCode.DownArrow)){
					aclAxis = -1;
				}else
					aclAxis = Input.GetAxis ("Vertical");

			if (aclAxis > m_deadZone)
				m_currThrust = aclAxis * m_forwardAcl;
			else if (aclAxis < -m_deadZone)
				m_currThrust = aclAxis * m_backwardAcl;
			// Turning
			CurrentTurnAngle = 0.0f;
			float turnAxis = 0;

			if(Input.GetKey(izquierda) || Input.GetKey(KeyCode.LeftArrow)){
				turnAxis = -1;
			}else
				if(Input.GetKey(derecha) || Input.GetKey(KeyCode.RightArrow)){
					turnAxis = 1;
				}else
					turnAxis = Input.GetAxis ("Horizontal");

			if (Mathf.Abs (turnAxis) > m_deadZone)
				CurrentTurnAngle = turnAxis;

			
			counttime += Time.deltaTime;
			GameTimer.text = "Time: " + (int)counttime;
			scoreT.text = "Puntos obtenidos: " + (200 - (int)counttime);
			}
			//  Hover Force
			RaycastHit hit;
			for (int i = 0; i < HoverPointsGameObjects.Length; i++) {
				var hoverPoint = HoverPointsGameObjects [i];
				if (Physics.Raycast (hoverPoint.transform.position, 
                          -Vector3.up, out hit,
                          m_StabilizedHoverHeight,
                          m_layerMask))
					m_body.AddForceAtPosition (Vector3.up 
						* m_hoverForce
						* (1.0f - (hit.distance / m_StabilizedHoverHeight)), 
                                  hoverPoint.transform.position);
				else {
					if (transform.position.y > hoverPoint.transform.position.y)
						m_body.AddForceAtPosition (
            hoverPoint.transform.up * m_hoverForce,
            hoverPoint.transform.position);
					else
					//adding force to car
						m_body.AddForceAtPosition (
            hoverPoint.transform.up * -m_hoverForce,
            hoverPoint.transform.position);
				}
			}

			// Forward
			if (Mathf.Abs (m_currThrust) > 0)
				m_body.AddForce (transform.forward * m_currThrust);

			// Turn
			if (CurrentTurnAngle > 0) {
				m_body.AddRelativeTorque (Vector3.up * CurrentTurnAngle * m_turnStrength);
			} else if (CurrentTurnAngle < 0) {
				m_body.AddRelativeTorque (Vector3.up * CurrentTurnAngle * m_turnStrength);
			}
		}
}
