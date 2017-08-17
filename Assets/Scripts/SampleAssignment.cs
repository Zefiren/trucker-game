using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SampleAssignment : MonoBehaviour
{

	public Button buttonComponent;
	public Text title;
	public Text packageCount;
	public Text destinationDistance;

	private Transform player;
	private AssignmentSpecification item;
	private AssignmentList scrollList;

	// Use this for initialization
	void Start ()
	{
		buttonComponent.onClick.AddListener (HandleClick);
	}

	public void Setup (AssignmentSpecification currentItem, AssignmentList currentScrollList)
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		item = currentItem;
		title.text = "Assignment " + item.assignmentID;
		packageCount.text = item.packageList.Count + " Packages";
		destinationDistance.text = Vector3.Distance (player.position, item.destination) + " is the destination";
		scrollList = currentScrollList;

	}

	public void HandleClick ()
	{
		print (item.packageList.Count + "packages");
        scrollList.ViewAssignment(item);
	}

	void Update ()
	{
		destinationDistance.text = Mathf.Ceil (Vector3.Distance (player.position, item.destination)) + " metres to the destination";
	}
}