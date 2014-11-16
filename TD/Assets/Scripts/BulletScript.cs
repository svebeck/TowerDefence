using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	public float speed;
	public int damage;
	Transform transform;
	Vector3 direction;
	Transform target;

	float ttl = 10000;


	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();

		Debug.Log ("direction: " + direction);
		Debug.Log ("target: " + target.position);
		Debug.Log ("transform: " + transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			direction = target.position - transform.position;
		}

		direction.Normalize ();

		transform.position += direction * speed * Time.fixedDeltaTime;

		if (ttl < 0)
			Destroy (gameObject);

		ttl -= Time.fixedDeltaTime*1000;
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Agent")
		{
			col.gameObject.GetComponent<HealthScript>().TakeDamage(damage);
			Destroy (gameObject);
		}
	}

	public void SetTarget(Transform target)
	{
		this.target = target;
	}


}
