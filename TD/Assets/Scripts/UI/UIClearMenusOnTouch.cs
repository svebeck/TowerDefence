using UnityEngine;
using System.Collections;

public class UIClearMenusOnTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    
    void OnMouseDown()
    {
        GameObject[] turretPositions = GameObject.FindGameObjectsWithTag("TurretPosition");

        foreach (GameObject tp in turretPositions)
        {
            BuildScript bs = tp.GetComponent<BuildScript>();
            bs.selected = false;
        }

        GameObject um = GameObject.Find("UpgradeMenu");
        MoveBuildMenuScript mums = um.GetComponent<MoveBuildMenuScript> ();
        mums.Hide();
        
        GameObject bm = GameObject.Find("BuildMenu");
        MoveBuildMenuScript mbms = bm.GetComponent<MoveBuildMenuScript> ();
        mbms.Hide();
    }
}
