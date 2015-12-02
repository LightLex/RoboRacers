	// Removed two depreciated functions.  Used Deg2Rad and back again because I don't know what I'm doing.  Anyone is welcome to clean this up properly.  -WarpZone
	
	using UnityEngine;
	using System.Collections;
	
using UnityEngine;
using System.Collections;

public class ShipControls : MonoBehaviour {
	
	public float speed = 90f;
	public float turnSpeed = 5f;
	public float hoverForce = 65f;
	public float hoverHeight = 3.5f;
	private float powerInput;
	private float turnInput;
	private Rigidbody carRigidbody;
	
	
	void Awake () 
	{
		carRigidbody = GetComponent <Rigidbody>();
	}
	
	void Update () 
	{
		powerInput = Input.GetAxis ("Vertical");
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			turnInput = Input.GetAxis ("Horizontal");
		} else
			turnInput = 0;
	}
	
	void FixedUpdate()
	{
		Ray ray = new Ray (transform.position, -transform.up);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, hoverHeight))
		{
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
			carRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}
		
		carRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
		carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
		
	}
}