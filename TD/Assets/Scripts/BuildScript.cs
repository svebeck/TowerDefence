using UnityEngine;
using System.Collections;

public class BuildScript : MonoBehaviour {
	Transform transform;
	bool hasBuilt = false;
	bool selected = false;
	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		if (hasBuilt)
			return;

		GameObject bm = GameObject.FindGameObjectWithTag ("BuildMenu");
		MoveBuildMenuScript mbms = bm.GetComponent<MoveBuildMenuScript> ();
		mbms.Show ();

		selected = true;
	}
	

	public void buildTower(GameObject prefab) 
	{
		if (hasBuilt) {
			return;
		}
        
        TurretScript ts = prefab.GetComponent<TurretScript>();
        Debug.Log("try buy tower" + ts.cost);

        if (!GameObject.Find("GameManager").GetComponent<GameManagerScript>().TryRemoveCredits(ts.cost))
            return;

        Debug.Log("tower built");
		
		GameObject bm = GameObject.FindGameObjectWithTag ("BuildMenu");
		MoveBuildMenuScript mbms = bm.GetComponent<MoveBuildMenuScript> ();
		mbms.Hide ();

		selected = false;
		hasBuilt = true;
		Instantiate (prefab, transform.position, transform.localRotation);

	}

	public bool IsSelected()
	{
		return selected;
	}
}
