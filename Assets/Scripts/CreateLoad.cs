using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLoad : MonoBehaviour
{

	public int numberOfDeliveriesAssigned;
	public int maxPackagesPerAssignment;
	public GameObject AssignmentPrefab;
	public GameObject PackagePrefab;
	public GameObject DestinationPrefab;

	public GameObject buildings_go;
	public GameObject cargoPositions_go;

	public GameObject assignments;
	public GameObject packages;
	public GameObject destinations;

	private int pckgIDCounter;
	private int assignmentCounter;
	private List<AssignmentSpecification> assignmentsList;
	private List<PackageSpecification> packagesList;
	private Transform[] buildings;
	private Transform[] cargoPositions;
	private bool[] cargoPositionUsed;



	// Use this for initialization
	void Start ()
	{
		pckgIDCounter = 0;
		assignmentCounter = 0;
		assignmentsList = new List<AssignmentSpecification> ();
		packagesList = new List<PackageSpecification> ();
        buildings = new Transform[buildings_go.transform.childCount];
        for (int i = 0; i < buildings_go.transform.childCount; i++)
        {
            buildings[i] = buildings_go.transform.GetChild(i).GetComponent<Transform>();
        }
            buildings_go.GetComponentsInChildren<Transform> ();
        print(buildings.Length);
//		cargoPositions = cargoPositions_go.GetComponentsInChildren<Transform> ();
		List<Transform> trs = new List<Transform> ();
		foreach (Transform tr in cargoPositions_go.GetComponentsInChildren<Transform> ()) {
			if (tr.GetInstanceID () != cargoPositions_go.transform.GetInstanceID ())
				trs.Add (tr);
		}
		cargoPositions = trs.ToArray ();
		cargoPositionUsed = new bool[cargoPositions.Length];

		for (int i = 0; i < numberOfDeliveriesAssigned; i++) {
			createAssignment ();
		}

		GameObject AssignmentsContent = GameObject.FindGameObjectWithTag ("AssignmentContent");
		print (AssignmentsContent.transform.name);
		AssignmentList contentScript = AssignmentsContent.GetComponent<AssignmentList> ();
		foreach (AssignmentSpecification a in assignmentsList) {
			contentScript.itemList.Add (a);
		}
		print (contentScript.itemList.Count + " is num of assignments in new list");
		contentScript.RefreshDisplay ();
	}

	void createAssignment ()
	{
		GameObject newAssignment = Instantiate(AssignmentPrefab, assignments.transform);
		//will be modified to allow randomised number of packages


		//Create destination for assignment
		GameObject newAssignmentDestination = Instantiate(DestinationPrefab, destinations.transform);
        int randomBuilding = Mathf.FloorToInt(Random.value * buildings.Length);

        Vector3 destination = buildings [randomBuilding].position +
		                      buildings [randomBuilding].forward * 20;
//		Vector3 destination = new Vector3 (Random.value * mapDimensions.x, 50, Random.value * mapDimensions.y);

		AssignmentSpecification assignSpec = newAssignment.GetComponent<AssignmentSpecification> ();
		assignSpec.destination = destination;
		assignSpec.assignmentID = assignmentCounter;
        assignSpec.packageList = new List<PackageSpecification>();

        assignmentCounter++;

		int numPackages = Mathf.FloorToInt (Random.value * maxPackagesPerAssignment);
		//create x to max number of packages per assignment
		for (int i = 0; i <= numPackages; i++) {
			createPackage (assignSpec);
		}


		DestinationScript destScript = newAssignmentDestination.GetComponent <DestinationScript> ();
		destScript.assignmentID = assignSpec.assignmentID;
        destScript.assignment = newAssignment;
		newAssignmentDestination.transform.position = destination;

		newAssignment.transform.name = "del" + assignSpec.assignmentID;
		newAssignmentDestination.transform.name = "dest" + destScript.assignmentID;
		assignmentsList.Add (assignSpec);

	}

	void createPackage (AssignmentSpecification assignment)
	{
		int cargoPos = Mathf.FloorToInt (Random.value * cargoPositions.Length);
		int tryNum = 0;
		while (cargoPositionUsed [cargoPos]) {
			cargoPos = Mathf.FloorToInt (Random.value * cargoPositions.Length);
			tryNum++;
		}
		cargoPositionUsed [cargoPos] = true;

		GameObject newAssignmentPackage = Instantiate(PackagePrefab, cargoPositions [cargoPos].position, Quaternion.identity);
		newAssignmentPackage.transform.SetParent (packages.transform);

		PackageSpecification packSpec = newAssignmentPackage.GetComponent <PackageSpecification> ();
		packSpec.packageID = pckgIDCounter;
//		packSpec.positionStart = cargoPositions [cargoPos].position;
		newAssignmentPackage.transform.name = "pckg" + packSpec.packageID;
		pckgIDCounter++;



		packSpec.partOfAssignment = assignment.assignmentID;
		//add to package list of assignment
		assignment.packageList.Add (packSpec);

		//add to list of packages ingame
		packagesList.Add (packSpec);



	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}
