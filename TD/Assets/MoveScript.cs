using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	Transform transform;

	// Use this for initialization
	void Start () 
	{
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(new Vector3(-0.1f,0f, -0.1f));
	}
}
