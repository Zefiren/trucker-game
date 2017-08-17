using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour {
    
    public class Interactable {
        public enum typeOfInteract
        {
            vehicle,
            door
        };
        public typeOfInteract type;
        public GameObject interactable;

    }
    public class DoorInteract :Interactable
    {
        public bool doorOpen;
        public Transform door;
        public bool isVertical;

        void openDoor()
        {
            if (doorOpen)
            {
                if(!isVertical)
                    door.transform.Rotate(new Vector3(0f, 90f, 0f));
                else
                    door.transform.Rotate(new Vector3(90f, 0f, 0f));

                doorOpen = false;
            }
            else
            {
                if (!isVertical)
                    door.transform.Rotate(new Vector3(0f, -90f, 0f));
                else
                    door.transform.Rotate(new Vector3(-90f, 0f, 0f));
                doorOpen = true;

            }
        }
    } 

    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
