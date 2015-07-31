using UnityEngine;
using System.Collections;

public class DeadScript : MonoBehaviour {

	public float deadTime;
	public float sinkTime;
	public float sinkSpeed;

	bool startedSinking = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		deadTime -= Time.deltaTime * 1000;

		if (deadTime < 0) {
			Sink();		
		}
	}

	void Sink()
	{
		if (!startedSinking) {
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
			gameObject.GetComponent<Rigidbody>().useGravity = false;
			startedSinking = true;
			Debug.Log ("started sinking!");
		}
		gameObject.transform.Translate (new Vector3(0,-sinkSpeed,0), Space.World);
		sinkTime -= Time.deltaTime*1000;

		if (sinkTime < 0) {
			Destroy (gameObject);		
		}
	}
}
