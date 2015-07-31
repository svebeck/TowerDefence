using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBuildTowerButtonScript : MonoBehaviour {

    public GameObject turret;
	// Use this for initialization
	void Start () {
        
        TurretScript ts = turret.GetComponent<TurretScript>();
        gameObject.GetComponent<Text>().text = "$" + ts.cost;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
