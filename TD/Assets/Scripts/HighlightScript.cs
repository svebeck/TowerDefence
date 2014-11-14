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
		startcolor = renderer.material.color;
		renderer.material.color = Color.yellow;
	}
	void OnMouseExit()
	{
		renderer.material.color = startcolor;
	}
}
