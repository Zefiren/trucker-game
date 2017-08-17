using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationScript : MonoBehaviour
{

	public int assignmentID;
	public bool delivered;
	public GameObject assignment;
    public List<PackageSpecification> packagesInArea;
    

	void Start ()
	{
        packagesInArea = new List<PackageSpecification>();
		delivered = false;
	}

    private void OnTriggerExit(Collider other)
    {
        GameObject otherGO = other.transform.gameObject;
        PackageSpecification ps = otherGO.GetComponent<PackageSpecification>();

        if (ps != null)
            if (packagesInArea.Contains(ps))
            {
                packagesInArea.Remove(ps);
                Debug.Log(packagesInArea);

            }
    }

    void OnTriggerEnter (Collider other)
	{
		GameObject otherGO = other.transform.gameObject;
        PackageSpecification ps = otherGO.GetComponent<PackageSpecification>();

        if (ps != null)
            if (ps.inCargoHold)
                return;
            if (!packagesInArea.Contains(ps))
            {
                packagesInArea.Add(ps);
                Debug.Log(packagesInArea);
                checkDeliveryArea();
            }
    }

    void checkDeliveryArea()
    {
        int correctPackagesDelivered = 0;
        AssignmentSpecification aspec = assignment.GetComponent<AssignmentSpecification>();
        List<PackageSpecification> packagesRequired = aspec.packageList;
        foreach(PackageSpecification package in packagesInArea)
        {
            if (packagesRequired.Contains(package))
                correctPackagesDelivered++;
        }
        if (correctPackagesDelivered == packagesRequired.Count)
            print("all required packages delivered");
        else
            print("Delivered " + correctPackagesDelivered + "/" +packagesRequired + "packages");
    }
		
}
