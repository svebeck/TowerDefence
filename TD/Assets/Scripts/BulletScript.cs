using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	public float speed;
	public Transform target;
	Transform transform;
	Vector3 direction;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();
		direction = (target.transform.position - transform.position).Normalize();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = target;
		transform.Translate (direction * speed);
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Agent")
		{
			Destroy(col.gameObject);
		}
	}


}
