using UnityEngine;
using System.Collections;

public class AgentScript : MonoBehaviour
{
	GameObject target;
	NavMeshAgent agent;
	
	void Start () 
	{
        target = GameObject.Find("Tower");

		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(target.transform.position);
	}

	void Update () 
	{
	}

	
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Tower")
		{
			col.gameObject.GetComponent<TowerHealthScript>().TakeDamage(1);
			Destroy (gameObject);
		}
	}


}