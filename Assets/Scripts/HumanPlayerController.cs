using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class HumanPlayerController : MonoBehaviour
{

	public RigidbodyFirstPersonController rbFPSController;
    public PickUpThrowScript pickThrowScript;
    public GameObject drivingCamera;
    public WheelDrive wheelDriveComponent;


    public Transform cargoDoor;

    private Camera fpsCam;
    private Rigidbody rb;
    private bool doorOpen;
    private float normalForwardSpeed;
    private float normalBackwardSpeed;
    private float normalStrafeSpeed;



    // Use this for initialization
    void Start ()
	{
        normalForwardSpeed = rbFPSController.movementSettings.ForwardSpeed;
        normalBackwardSpeed = rbFPSController.movementSettings.BackwardSpeed;
        normalStrafeSpeed = rbFPSController.movementSettings.StrafeSpeed;

        fpsCam = gameObject.transform.Find("MainCamera").gameObject.GetComponent<Camera>() ;
	}
	
	// Physics updates done here1s
	void FixedUpdate ()
	{
        rbFPSController.movementSettings.ForwardSpeed = pickThrowScript.maxSpeedPercentage * normalForwardSpeed;
        rbFPSController.movementSettings.BackwardSpeed = pickThrowScript.maxSpeedPercentage * normalBackwardSpeed;
        rbFPSController.movementSettings.StrafeSpeed = pickThrowScript.maxSpeedPercentage * normalStrafeSpeed;

        RaycastHit hit; 
        if (Input.GetKeyDown(KeyCode.E))
        {

            Ray lookAt = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
            if (Physics.Raycast(lookAt, out hit,10))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "CargoDoor")
                    openDoor();
                if (hit.collider.tag == "VehicleFront")
                    enableDriving();
            }
        }
    }

    void enableDriving()
    {
        gameObject.SetActive(false);
        drivingCamera.SetActive(true);
        wheelDriveComponent.enabled = true;
    }


    void openDoor()
    {
        if (doorOpen)
        {
            cargoDoor.Rotate(new Vector3(0f, 90f, 0f));
            doorOpen = false;
        }
        else
        {
            cargoDoor.Rotate(new Vector3(0f, -90f, 0f));
            doorOpen = true;

        }
    }

}
