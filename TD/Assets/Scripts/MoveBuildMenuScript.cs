using UnityEngine;
using System.Collections;

public class MoveBuildMenuScript : MonoBehaviour {

	bool isVisible = false;

	public void Show()
	{
		if (isVisible)
			return;

		isVisible = true;

		gameObject.transform.Translate (new Vector3(0, 100, 0));
	}

	public void Hide()
	{
		isVisible = false;
		gameObject.transform.Translate (new Vector3(0, -100, 0));
	}
}
