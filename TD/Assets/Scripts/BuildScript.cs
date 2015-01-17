using UnityEngine;
using System.Collections;

public class BuildScript : MonoBehaviour {
	bool hasBuilt = false;
	bool selected = false;
    
    Transform transform;
    GameObject turret;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
        GameObject[] turretPositions = GameObject.FindGameObjectsWithTag("TurretPosition");

        foreach (GameObject tp in turretPositions)
        {
            BuildScript bs = tp.GetComponent<BuildScript>();
            bs.selected = false;
        }

        HideMenus();

		if (hasBuilt)
        {
            ShowUpgradeMenu();
        } 
        else
        {
            ShowBuildMenu();
        }

		selected = true;
	}
	

	public void BuildTower(GameObject prefab) 
	{
		if (hasBuilt) {
            Debug.LogError("Tower has already been built!");
			return;
		}
        
        TurretScript ts = prefab.GetComponent<TurretScript>();
        Debug.Log("try buy tower" + ts.cost);

        if (!GameObject.Find("GameManager").GetComponent<GameManagerScript>().TryRemoveCredits(ts.cost))
        {
            Debug.LogWarning("Could not buy tower!");
            HideMenus();
            return;
        }

        Debug.Log("tower built");
		
		GameObject bm = GameObject.Find("BuildMenu");
		MoveBuildMenuScript mbms = bm.GetComponent<MoveBuildMenuScript> ();
		mbms.Hide ();

		selected = false;
		hasBuilt = true;
		turret = (GameObject)Instantiate (prefab, transform.position, transform.localRotation);
	}

    public void UpgradeTower()
    {
        if (turret == null)
        {
            Debug.LogError("No turret to upgrade");
            return;
        }
        Debug.Log("Upgrade turret");

        TurretScript ts = turret.GetComponent<TurretScript>();

        if (!ts.CanUpgrade())
        {
            HideMenus();
            return;
        }

        if (!GameObject.Find("GameManager").GetComponent<GameManagerScript>().TryRemoveCredits(ts.GetUpgradeCost()))
        {
            HideMenus();
            return;
        }

        ts.Upgrade();

        selected = false;

        HideMenus();
    }

    public void SellTower()
    {
        if (turret == null)
        {
            Debug.LogError("No turret to sell");
            return;
        }

        Debug.Log("Sell turret");
        
        TurretScript ts = turret.GetComponent<TurretScript>();
        
        GameObject.Find("GameManager").GetComponent<GameManagerScript>().AddCredits(ts.salePrice);
        
        
        hasBuilt = false;
        selected = false;
        
        HideMenus();

        Destroy(turret);

    }

    void ShowBuildMenu() 
    {
        GameObject bm = GameObject.Find("BuildMenu");
        MoveBuildMenuScript mbms = bm.GetComponent<MoveBuildMenuScript> ();
        mbms.Show();
    }

    void ShowUpgradeMenu() 
    {
        GameObject um = GameObject.Find("UpgradeMenu");
        MoveBuildMenuScript mums = um.GetComponent<MoveBuildMenuScript> ();
        mums.Show();
    }

    void HideMenus() 
    {
        GameObject um = GameObject.Find("UpgradeMenu");
        MoveBuildMenuScript mums = um.GetComponent<MoveBuildMenuScript> ();
        mums.Hide();
        
        GameObject bm = GameObject.Find("BuildMenu");
        MoveBuildMenuScript mbms = bm.GetComponent<MoveBuildMenuScript> ();
        mbms.Hide();
        
        selected = false;
    }

	public bool IsSelected()
	{
		return selected;
	}
}
