using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public void SelectTower(GameObject prefab)
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("TurretPosition");

		foreach (GameObject obj in objs) {
				
			BuildScript bs = obj.GetComponent<BuildScript>();
			if (bs.IsSelected())
			{
				bs.buildTower(prefab);
				break;
			}
		}

	}
}
