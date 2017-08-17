using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{

	public KeyCode InventoryKey=KeyCode.I;

    public GameObject packages_go;
	public GameObject assignments_go;
	public GameObject destinations_go;
	public GameObject UI_go;
	public GameObject inventory_go;

 

    // Use this for initialization
    void Start ()
	{
		inventory_go.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (InventoryKey)) {
			inventory_go.SetActive (!inventory_go.activeSelf);
		}
        
    }
}
