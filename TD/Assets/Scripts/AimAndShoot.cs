using UnityEngine;
using System.Collections;

public class AimAndShoot : MonoBehaviour {
	
	public GameObject prefab;
	public string searchTag;
	public float range;
	public float speed;
	public float reloadTime;

	float reloadCurrent;

	Transform transform;
	Transform target;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			target = FindNearestTarget ();
		} else {
			Vector3 objectPos = target.transform.position;
			float distanceSqr = (objectPos - transform.position).sqrMagnitude;
			
			if (distanceSqr >= range) {
				target = null;
			}
		}

		if (target == null)
			return;

		Vector3 dir = target.transform.position - transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed);

		reloadCurrent += Time.deltaTime;

		if (reloadCurrent % reloadTime == 0) 
		{
			Instantiate(prefab, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), transform.localRotation);

			reloadCurrent = 0;
		}

		/*
		Quaternion oldRot = transform.rotation;
		transform.LookAt ( target ) ;
		Quaternion newRot = transform.rotation ;
		transform.rotation = Quaternion.Lerp ( oldRot , newRot, speed ) ;
		*/
	}

	Transform FindNearestTarget () {
		float nearestDistanceSqr = Mathf.Infinity;
		GameObject[] taggedGameObjects = GameObject.FindGameObjectsWithTag(searchTag); 
		Transform nearestObj = null;
		
		// loop through each tagged object, remembering nearest one found
		foreach (GameObject obj in taggedGameObjects) {
			Vector3 objectPos = obj.transform.position;
			float distanceSqr = (objectPos - transform.position).sqrMagnitude;

			if (distanceSqr < nearestDistanceSqr && distanceSqr < range) {
				nearestObj = obj.transform;
				nearestDistanceSqr = distanceSqr;
			}
		}
		
		return nearestObj;
	}
}
