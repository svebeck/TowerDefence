using UnityEngine;
using System.Collections.Generic;

public class TurretScript : MonoBehaviour {

    public int cost = 50;
    public int salePrice = 20;
    public List<int> upgradeCost = new List<int>(5);

    private int upgradeLevel = 0;

    Transform transform;

	// Use this for initialization
	void Start () {
        
        transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool CanUpgrade() 
    {
        return upgradeLevel < 4;
    }

    public void Upgrade()
    {
        upgradeLevel++;

        AimAndShootScript ass =  gameObject.GetComponent<AimAndShootScript>();
        ass.Upgrade(upgradeLevel);


        transform.localScale = new Vector3(2f,(float)(3 + upgradeLevel),2f);

        Debug.LogError(transform.localScale);
    }

    public int GetUpgradeCost() 
    {
        return (int)upgradeCost[upgradeLevel];
    }
}
