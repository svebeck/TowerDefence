using UnityEngine;
using System.Collections;

public class AgentScript : MonoBehaviour
{
	public GameObject target;
	NavMeshAgent agent;
	
	void Start () 
	{
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