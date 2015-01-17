using UnityEngine;
using System.Collections.Generic;

public class AimAndShootScript : MonoBehaviour {
	
	public GameObject projectile;
	public string searchTag;
    public float range;
    public float rotationSpeed;
    public float reloadTime;
    
    float baseRange;
    float baseRotationSpeed;
    float baseReloadTime;

    public List<float> upgradeRange;
    public List<float> upgradeRotationSpeed;
    public List<float> upgradeReloadTime;

	int reloadCurrent;

    int upgradeLevel = -1;

	Transform transform;
	Transform target;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();

        ResetBase();
	}

    void ResetBase() 
    {
        baseRotationSpeed = rotationSpeed;
        baseRange = range;
        baseReloadTime = reloadTime;

        Debug.Log("baseRotationSpeed: " + baseRotationSpeed);
        Debug.Log("baseRange: " + baseRange);
        Debug.Log("baseReloadTime: " + baseReloadTime);
    }
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			target = FindNearestTarget ();
		} else {
			Vector3 objectPos = target.transform.position;
			float distanceSqr = (objectPos - transform.position).sqrMagnitude;
			
			if (distanceSqr >= baseRange) {
				target = null;
			}
		}

		if (target == null)
			return;

		Vector3 dir = transform.position - target.transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, baseRotationSpeed);

		reloadCurrent += (int)(Time.fixedDeltaTime*1000);
		//Debug.Log("Modulus: " + (reloadCurrent % reloadTime));
		//Debug.Log("Reload: " + (reloadTime));
		//Debug.Log("Current: " + (reloadCurrent));

		if (reloadCurrent % baseReloadTime == 0) 
		{
			//Debug.Log("Shooting! " + target.transform.position);
			GameObject obj = (GameObject)Instantiate(projectile, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), transform.localRotation);
			BulletScript bs = obj.GetComponent<BulletScript>();
            bs.Upgrade(upgradeLevel);
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
            HealthScript hs = obj.GetComponent<HealthScript>();

            if (!hs.targetable)
                continue;

			Vector3 objectPos = obj.transform.position;
			float distanceSqr = (objectPos - transform.position).sqrMagnitude;

			if (distanceSqr < nearestDistanceSqr && distanceSqr < baseRange) {
				nearestObj = obj.transform;
				nearestDistanceSqr = distanceSqr;
			}
		}
		
		return nearestObj;
	}

    
    public void Upgrade(int level)
    {
        upgradeLevel = level;

        if (level == -1)
            return;

        ResetBase();
        
        Debug.Log("Level: " + level);

        for (int i = 0; i < level; i++)
        {
            baseRotationSpeed += upgradeRotationSpeed[i];
            baseRange += upgradeRange[i];
            baseReloadTime += upgradeReloadTime[i];

            Debug.Log("upgradeReloadTime[level]: " + upgradeReloadTime[i]);
        }

        Debug.Log("baseReloadTime: " + baseReloadTime);
    }
}
