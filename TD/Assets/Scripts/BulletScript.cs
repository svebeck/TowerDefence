using UnityEngine;
using System.Collections.Generic;

public class BulletScript : MonoBehaviour {
	
    public float speed;
    public float acceleration = 20;   
	public float damage;
    public float areaDamage; 
    public float areaRadius;
    
    float baseSpeed;
    float baseAcceleration;   
    float baseDamage;
    float baseAreaDamage; 
    float baseAreaRadius;
    
    public List<float> upgradeSpeed;
    public List<float> upgradeAcceleration;
    public List<float> upgradeDamage;
    public List<float> upgradeAreaDamage;
    public List<float> upgradeAreaRadius;

    public float parableHeight;
    public float parableDecay;

	Transform transform;
	Vector3 direction;
	Transform target;
    Vector3 oldTarget;


	float ttl = 10000;

    float currentSpeed = 0;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();

		//Debug.Log ("direction: " + direction);
		//Debug.Log ("target: " + target.position);
		//Debug.Log ("transform: " + transform.position);

        ResetBase();
	}

    void ResetBase() {
        baseSpeed = speed;
        baseAcceleration = acceleration;
        baseDamage = damage;
        baseAreaDamage = areaDamage;
        baseAreaRadius = areaRadius;
    }
	
	// Update is called once per frame
	void Update () {
        
        Vector3 tempTarget = new Vector3();
        bool isTargetPrefered = false;
		if (target != null)
        {
            tempTarget = target.position;
            
            oldTarget = target.position;
            isTargetPrefered = true;
        } else
        {
            float distance = (tempTarget - transform.position).sqrMagnitude;

            if (parableHeight > 0 && distance > 2)
            {
                tempTarget = oldTarget;
                isTargetPrefered = true;
            }
        }

        if (isTargetPrefered)
        {
            if (parableHeight > 0)
            {
                parableHeight -= parableDecay;
                Vector3 heightTarget = new Vector3(0, parableHeight, 0);
                direction = tempTarget + heightTarget - transform.position;
            } else
            {
                direction = tempTarget - transform.position;
            }

            transform.LookAt(tempTarget);
        }

		direction.Normalize ();
        
        if (currentSpeed < baseSpeed)
            currentSpeed += Time.fixedDeltaTime * Mathf.Pow(acceleration, 2);


		transform.position += direction * currentSpeed * Time.fixedDeltaTime;

		if (ttl < 0)
			Destroy (gameObject);

		ttl -= Time.fixedDeltaTime*1000;

	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Agent")
		{
			if (baseAreaRadius > 0)
			{
				GameObject[] objs = GameObject.FindGameObjectsWithTag("Agent");
				foreach (GameObject obj in objs)
				{
					float distance = (obj.transform.position - transform.position).sqrMagnitude;
					if (distance < baseAreaRadius)
						obj.GetComponent<HealthScript>().TakeDamage(baseAreaDamage);
				}
			}

			col.gameObject.GetComponent<HealthScript>().TakeDamage(baseDamage);
			Destroy (gameObject);
		}
	}

	public void SetTarget(Transform target)
	{
		this.target = target;
        oldTarget = target.position;
	}

    public void Upgrade(int level)
    {
        if (level == -1)
            return;
        ResetBase();

        for (int i = 0; i < level; i++)
        {
            baseSpeed += upgradeSpeed[i];
            baseAcceleration += upgradeAcceleration[i];
            baseDamage += upgradeDamage[i];
            baseAreaDamage += upgradeAreaDamage[i];
            baseAreaRadius += upgradeAreaRadius[i];
        }
    }


}
