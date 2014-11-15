using UnityEngine;
using System.Collections;

public class AimAndShoot : MonoBehaviour {
	
	public GameObject prefab;
	public string searchTag;
	public float range;
	public float speed;
	public int reloadTime;

	int reloadCurrent;

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

		Vector3 dir = transform.position - target.transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed);

		reloadCurrent += (int)(Time.fixedDeltaTime*1000);
		Debug.Log("Modulus: " + (reloadCurrent % reloadTime));
		Debug.Log("Reload: " + (reloadTime));
		Debug.Log("Current: " + (reloadCurrent));



		if (reloadCurrent % reloadTime == 0) 
		{
			Debug.Log("Shooting! " + target.transform.position);
			GameObject obj = (GameObject)Instantiate(prefab, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), transform.localRotation);
			BulletScript bs = obj.GetComponent<BulletScript>();
			bs.SetTarget(target.transform);
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
