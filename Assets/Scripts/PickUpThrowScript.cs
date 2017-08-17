using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpThrowScript : MonoBehaviour
{
    public float pickUpRange = 10f;
    public float holdDistance = 2f;
    public float liftStrength = 15f;
    public float throwingForce = 10f;
    public float throwActivateAfterSeconds;
    public float maxSpeedPercentage=1f;

    public Rigidbody liftingObject;
    public GameObject liftingObject_go;

    private Camera fpsCam;
    private Transform oldParent;
    private PickupScript ps;

    //for throwing
    private float startPress;
    private bool isButtonPressed;
    private float throwPower=0;

    // Use this for initialization
    void Start()
    {
        fpsCam = gameObject.transform.Find("MainCamera").gameObject.GetComponent<Camera>();

    }

    private void FixedUpdate()
    {
        if (isButtonPressed)
            if ((Time.time - startPress) > throwActivateAfterSeconds)
            {
                throwPower += 0.1f * throwingForce;
                if (throwPower > (throwingForce * 0.5f * maxSpeedPercentage * 100))
                    throwPower =  throwingForce * 0.5f * maxSpeedPercentage * 100;
                Debug.Log("throwing power at:"+throwPower);
            }

        if (liftingObject != null)
            carryingPackage();
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (liftingObject != null)
            {
                isButtonPressed = true;
                startPress = Time.time;
                Debug.Log("we were here");
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            if (liftingObject != null)
            {
                if (throwPower > 0)
                {
                    dropObject(true);
                    throwPower = 0;
                    startPress = 0;
                    isButtonPressed = false;
                }
                else
                {
                    dropObject(false);
                    startPress = 0;
                    isButtonPressed = false;
                }
                return;
            }

            Ray lookAt = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
            if (Physics.Raycast(lookAt, out hit, pickUpRange))
            {
                PickupScript ps = hit.transform.gameObject.GetComponent<PickupScript>();
                if (ps != null)
                {
                    pickUpPackage(hit);
                }


                //Debug.Log(hit.collider.gameObject.tag);
                //Debug.Log(hit.transform.name);
            }
        }
    }

    void pickUpPackage(RaycastHit hit)
    {
        liftingObject = hit.rigidbody;
        liftingObject_go = liftingObject.gameObject;
        ps = liftingObject_go.GetComponent<PickupScript>();
        ps.SetPickup(true);

        oldParent = liftingObject_go.transform.parent;
        liftingObject_go.transform.SetParent(gameObject.transform);

        maxSpeedPercentage = 1 - liftingObject.mass / liftStrength;
        if (maxSpeedPercentage < 0.2f)
            maxSpeedPercentage = 0.2f;


    }

    void dropObject(bool isThrowing)
    {
        if (ps.canDrop)
        {
            ps.SetPickup(false);
            if (oldParent == null)
                liftingObject_go.transform.SetParent(transform.root);
            else
                liftingObject_go.transform.SetParent(oldParent);

            if (isThrowing)
                liftingObject.AddForce(fpsCam.transform.forward * throwPower,ForceMode.Impulse);

            liftingObject = null;
            liftingObject_go = null;
            maxSpeedPercentage = 1;
        }
    }
    void carryingPackage()
    {
        liftingObject_go.transform.position = Vector3.Lerp(liftingObject_go.transform.position, fpsCam.transform.position + fpsCam.transform.forward * holdDistance, Time.deltaTime * 4);
    }



}
