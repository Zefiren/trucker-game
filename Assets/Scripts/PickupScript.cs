using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {

    private Rigidbody rb;
    private Collider col;


    public HumanPlayerController humanHolding;
    public GameObject lightEffect;
    public bool pickedUp=false;
    public bool canDrop;

    private GameObject light_go;

    private int direction;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

    }


    private void OnTriggerStay(Collider other)
    {
        if (pickedUp)
        {
            DestinationScript destCheck = other.gameObject.GetComponent<DestinationScript>();
            if (destCheck != null)
            {
                light_go.GetComponent<Light>().color = Color.blue;
                canDrop = true;
                return;
            }
            light_go.GetComponent<Light>().color = Color.red;
            canDrop = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (pickedUp)
        {
            light_go.GetComponent<Light>().color = Color.blue;
            canDrop = true;
        }
    }

    public void SetPickup(bool isPickedUp)
    {
        pickedUp = isPickedUp;
        if (pickedUp)
        {
            rb.isKinematic = true;
            col.isTrigger = true;
            light_go = Instantiate(lightEffect, gameObject.transform);
            light_go.transform.localPosition = new Vector3(0, 5, 0);
        }
        else
        {
            rb.isKinematic = false;
            col.isTrigger = false;
            GameObject.Destroy(light_go);
        }
    }
  
}
