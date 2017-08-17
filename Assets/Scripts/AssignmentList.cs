using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]


public class AssignmentList : MonoBehaviour
{

	public List<AssignmentSpecification> itemList;
	public Transform contentPanel;
	public SimpleObjectPool buttonObjectPool;
    public GameObject assignmentDetailsPrefab;



    // Use this for initialization
    void Start ()
	{
		RefreshDisplay ();
	}

	public void RefreshDisplay ()
	{
		RemoveButtons ();
		AddButtons ();
	}

	private void RemoveButtons ()
	{
		while (contentPanel.childCount > 0) {
			GameObject toRemove = transform.GetChild (0).gameObject;
			buttonObjectPool.ReturnObject (toRemove);
		}
	}

	private void AddButtons ()
	{
		for (int i = 0; i < itemList.Count; i++) {
			AssignmentSpecification item = itemList [i];
			GameObject newButton = buttonObjectPool.GetObject ();
			newButton.transform.SetParent (contentPanel, false);

			SampleAssignment sampleButton = newButton.GetComponent<SampleAssignment> ();
			sampleButton.Setup (item, this);
		}
	}

    public void ViewAssignment(AssignmentSpecification item)
    {
        GameObject assignView = Instantiate(assignmentDetailsPrefab,gameObject.transform.parent.parent.parent.parent);
        AssignmentViewScript avScript = assignView.GetComponent<AssignmentViewScript>();
        avScript.Setup(item);

    }
    //	{
    //		if (otherShop.gold >= item.price) {
    //			gold += item.price;
    //			otherShop.gold -= item.price;
    //
    //			AddItem (item, otherShop);
    //			RemoveItem (item, this);
    //
    //			RefreshDisplay ();
    //			otherShop.RefreshDisplay ();
    //			Debug.Log ("enough gold");
    //
    //		}
    //		Debug.Log ("attempted");
    //	}

    void AddItem (AssignmentSpecification itemToAdd, AssignmentList assignList)
	{
		assignList.itemList.Add (itemToAdd);
	}

	private void RemoveItem (AssignmentSpecification itemToRemove, AssignmentList assignList)
	{
		for (int i = assignList.itemList.Count - 1; i >= 0; i--) {
			if (assignList.itemList [i] == itemToRemove) {
				assignList.itemList.RemoveAt (i);
			}
		}
	}
}