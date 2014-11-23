using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
    public float speed;
    public float acceleration = 20;
	public int damage;
	public int areaDamage;
	public float areaRadius;
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

		Debug.Log ("direction: " + direction);
		Debug.Log ("target: " + target.position);
		Debug.Log ("transform: " + transform.position);

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
        
        if (currentSpeed < speed)
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
			if (areaRadius > 0)
			{
				GameObject[] objs = GameObject.FindGameObjectsWithTag("Agent");
				foreach (GameObject obj in objs)
				{
					float distance = (obj.transform.position - transform.position).sqrMagnitude;
					if (distance < areaRadius)
						obj.GetComponent<HealthScript>().TakeDamage(areaDamage);
				}
			}

			col.gameObject.GetComponent<HealthScript>().TakeDamage(damage);
			Destroy (gameObject);
		}
	}

	public void SetTarget(Transform target)
	{
		this.target = target;
        oldTarget = target.position;
	}


}
