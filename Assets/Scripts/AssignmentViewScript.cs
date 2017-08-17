using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignmentViewScript : MonoBehaviour {
    public Text AssignmentID_text;
    public GameObject packageContainer;
    public GameObject packageText_prefab;

    public List<Text> packagesShown;

    public void Setup(AssignmentSpecification assignSpec)
    {
        gameObject.GetComponent<Button>().onClick.AddListener(HandleClick);
        AssignmentID_text.text = "Assignment " + assignSpec.assignmentID;
        foreach(PackageSpecification p in assignSpec.packageList)
        {   
            GameObject packText_go =(GameObject) Instantiate(packageText_prefab,packageContainer.transform);
            packText_go.transform.name = "package " + p.packageID + "view";
            Text packText = packText_go.GetComponent<Text>();
            Debug.Log(packText.text);
            packText.text = "package " + p.packageID;
        } 
    }


    public void HandleClick()
    {
        Debug.Log("clicked outside");
        Destroy(gameObject);
    }

}
