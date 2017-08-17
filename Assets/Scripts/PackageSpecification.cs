using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpecification : MonoBehaviour
{
	public GameObject playerCam;
	public TextMesh labelText;
	public Vector3 positionStart;

	public int packageID;
	public int partOfAssignment;

    public bool inCargoHold = true;

    private Rigidbody rb;


	// Use this for initialization
	void Start ()
	{
		rb = GetComponent <Rigidbody> ();
		labelText = transform.GetComponentInChildren <TextMesh> ();
		labelText.text = "" + packageID;
//		rb.MovePosition (positionStart);

//		print ("PACKAGE" + packageID + ": " + transform.position);

		playerCam = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update ()
	{
		labelText.transform.rotation = Quaternion.LookRotation (transform.position - playerCam.transform.position);
	}


}
