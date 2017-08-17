using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class SpeedDisplay : MonoBehaviour
{

	private VehicleInfo m_vehicleInfo;
	private Text speedText;
	private int speed;

	// Use this for initialization
	void Start ()
	{
		GameObject car = GameObject.FindGameObjectWithTag ("PlayerVehicle");
		m_vehicleInfo = car.GetComponent<VehicleInfo> ();
		if (m_vehicleInfo == null)
			Debug.LogError ("no car control found");
		speed = Mathf.CeilToInt (m_vehicleInfo.currentSpeed);
		speedText = gameObject.GetComponent<Text> ();

		speedText.text = speed + " MPH";
	}
	
	// Update is called once per frame
	void Update ()
	{
		speed = Mathf.CeilToInt (m_vehicleInfo.currentSpeed);
		speedText.text = speed + " MPH";

	}
}
