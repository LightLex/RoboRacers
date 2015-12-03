using UnityEngine;
using System.Collections;

public class TargetFollow : MonoBehaviour {
	public Transform target;
	public float distUP;
	public float distBack;
	public float minHeight;

	private Vector3 positionVel;

	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 newPosition = target.position + (target.forward * distBack);
		newPosition.y = Mathf.Max (newPosition.y + distUP, minHeight);

		transform.position = Vector3.SmoothDamp (transform.position, newPosition, ref positionVel, 0.18f);

		Vector3 point = target.position + (target.forward * 5);

		transform.LookAt (point);
	}
}
