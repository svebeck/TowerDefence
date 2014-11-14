using UnityEngine;
using System.Collections;

public class BuildScript : MonoBehaviour {
	public GameObject prefab;
	Transform transform;
	bool hasBuilt = false;
	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		if (hasBuilt) {
			return;
		}

		hasBuilt = true;
		Instantiate(prefab, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), transform.localRotation);
	}
}
