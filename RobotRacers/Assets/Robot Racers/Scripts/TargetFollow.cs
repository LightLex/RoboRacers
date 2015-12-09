using UnityEngine;
using System.Collections;

public class TargetFollow : MonoBehaviour {

	/*
	 * Script del seguiminto de camara.
	 * La camara sige al jugador dependiendo de los valores establecidos.
	 */

	//Inicializamos el objeto al cual seguir
	public Transform target;

	//Inicializamos el valor de la distancia desde arriba
	public float distUP;

	//Inicializamos el valor de la distancia desde atras
	public float distBack;

	//Inicializamos el valor de la altura minima
	public float minHeight;

	//Inicializamos el valor de la posision dependiendo de la velocidad
	private Vector3 positionVel;

	
	//Funcion llamada cada segundo
	void FixedUpdate () {
		Vector3 newPosition = target.position + (target.forward * distBack);		//Obtener la posision del obejto a seguir
		newPosition.y = Mathf.Max (newPosition.y + distUP, minHeight);

		transform.position = Vector3.SmoothDamp (transform.position, newPosition, ref positionVel, 0.18f);		//Seguir al objeto

		Vector3 point = target.position + (target.forward * 5);

		transform.LookAt (point);		//Mirar el objeto a seguir
	}
}
