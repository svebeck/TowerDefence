using UnityEngine;
using System.Collections;

public class AgentScript : MonoBehaviour
{
	public Transform target;
	NavMeshAgent agent;
	
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
	}

	void Update () 
	{
		agent.SetDestination(target);
	}

}