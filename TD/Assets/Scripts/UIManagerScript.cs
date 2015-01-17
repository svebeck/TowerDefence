using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	public void SelectTower(GameObject prefab)
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("TurretPosition");

		foreach (GameObject obj in objs) {
			BuildScript bs = obj.GetComponent<BuildScript>();
			if (bs.IsSelected())
			{
				bs.BuildTower(prefab);
				break;
			}
		}

	}

    public void UpgradeTower()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag ("TurretPosition");
        
        foreach (GameObject obj in objs) {
            BuildScript bs = obj.GetComponent<BuildScript>();
            if (bs.IsSelected())
            {
                bs.UpgradeTower();
                break;
            }
        }
    }

    
    public void SellTower()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag ("TurretPosition");
        
        foreach (GameObject obj in objs) {
            BuildScript bs = obj.GetComponent<BuildScript>();
            if (bs.IsSelected())
            {
                bs.SellTower();
                break;
            }
        }
    }

}
