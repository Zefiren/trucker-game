using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverController : MonoBehaviour {
    public KeyCode ExitVehicleKey = KeyCode.R;
    public KeyCode HandbrakeKey = KeyCode.H;


    public GameObject drivingCamera;
    public WheelDrive wheelDriveComponent;
    public GameObject fpsPlayer;


    // Use this for initialization
    void Start () {
        wheelDriveComponent.handbrakeOn = true;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(HandbrakeKey))
        {
            wheelDriveComponent.handbrakeOn = !wheelDriveComponent.handbrakeOn;

        }
        if (Input.GetKeyDown(ExitVehicleKey))
        {
            drivingCamera.SetActive(false);
            //wheelDriveComponent.enabled = false;
            fpsPlayer.SetActive(true);
            fpsPlayer.transform.position = transform.position + transform.right*2;
        }
    }
}
