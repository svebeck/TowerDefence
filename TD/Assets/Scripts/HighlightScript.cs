using UnityEngine;
using System.Collections;

public class HighlightScript : MonoBehaviour {
	
	private Color startcolor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter()
	{
		startcolor = GetComponent<Renderer>().material.color;
		GetComponent<Renderer>().material.color = Color.yellow;
	}
	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = startcolor;
	}
}
