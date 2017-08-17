using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInfo : MonoBehaviour
{

	public float currentSpeed;

	public enum speedUnits
	{
		metrePerSecond,
		KMH,
		MPH
	}

	public speedUnits speedUnitChoice;

	private Rigidbody vehicleBody;


	// Use this for initialization
	void Start ()
	{
		GameObject Player_go = GameObject.FindGameObjectWithTag ("Player");
		vehicleBody = Player_go.GetComponent<Rigidbody> ();
		switch (speedUnitChoice) {
		case speedUnits.metrePerSecond:
			currentSpeed = vehicleBody.velocity.magnitude;
			break;
		case speedUnits.KMH:
			currentSpeed = vehicleBody.velocity.magnitude * 3.6f;
			break;
		case speedUnits.MPH:
			currentSpeed = vehicleBody.velocity.magnitude * 2.23694f;
			break;
		default: 
			currentSpeed = vehicleBody.velocity.magnitude;
			break;
		}


	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (speedUnitChoice) {
		case speedUnits.metrePerSecond:
			currentSpeed = vehicleBody.velocity.magnitude;
			break;
		case speedUnits.KMH:
			currentSpeed = vehicleBody.velocity.magnitude * 3.6f;
			break;
		case speedUnits.MPH:
			currentSpeed = vehicleBody.velocity.magnitude * 2.23694f;
			break;
		default: 
			currentSpeed = vehicleBody.velocity.magnitude;
			break;
		}
	}
}
