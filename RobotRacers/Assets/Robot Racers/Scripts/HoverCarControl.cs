using UnityEngine;
using System.Collections;
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
  public float m_forwardAcl = 100.0f;
	//Foward Acceleation of car
  public float m_backwardAcl = 25.0f;
	//Backwords/reverse Acceleration of car
  float m_currThrust = 0.0f;
	//Do not modify! Current speed
  public float m_turnStrength = 10f;
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

	bool ready = false;
	private bool pause = false;

	float counttime = 0;
	public Text GameTimer;
	public Text scoreT;

  void Start()
  {
    m_body = GetComponent<Rigidbody>();
		pscript = GetComponent<Player> ();
    m_layerMask = 1 << LayerMask.NameToLayer("Characters");
    m_layerMask = ~m_layerMask;
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

			// Main Thrust
			m_currThrust = 0.0f;
			if (ready == true && pause == false) {
			float aclAxis = Input.GetAxis ("Vertical");
			if (aclAxis > m_deadZone)
				m_currThrust = aclAxis * m_forwardAcl;
			else if (aclAxis < -m_deadZone)
				m_currThrust = aclAxis * m_backwardAcl;
			// Turning
			CurrentTurnAngle = 0.0f;
			float turnAxis = Input.GetAxis ("Horizontal");
			if (Mathf.Abs (turnAxis) > m_deadZone)
				CurrentTurnAngle = turnAxis;

			
			counttime += Time.deltaTime;
			GameTimer.text = "Time: " + (int)counttime;
			scoreT.text = "Tiempo total: " + (int)counttime;
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
