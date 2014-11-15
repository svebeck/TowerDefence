using UnityEngine;
using System.Collections;

public class AgentScript : MonoBehaviour
{
	public Transform target;
	NavMeshAgent agent;
	
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(target.position);
	}

	void Update () 
	{
	}

	
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Tower")
		{
			col.gameObject.GetComponent<HealthScript>().TakeDamage(1);
			Destroy (gameObject);
		}
	}


}