using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoDetector : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        PackageSpecification ps = other.gameObject.GetComponent<PackageSpecification>();
        if (ps != null)
        {
            ps.inCargoHold = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PackageSpecification ps = other.gameObject.GetComponent<PackageSpecification>();
        if (ps != null)
        {
            Debug.Log(other.gameObject.name);
            ps.inCargoHold = false;
        }
    }

}
