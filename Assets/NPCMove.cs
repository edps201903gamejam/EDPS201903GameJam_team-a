using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{

	public Transform[] points;
	private int destPoint = 0;
	private NavMeshAgent agent;
	

	private Vector3 eyeDir;
	private Vector3 playerPos;
	private Vector3 npcPos;

	private float angle = 30.0f;
	private float distance = 3.0f;

	public GameObject player;
	public float speed = 0.1f;
	
	void Start ()
	{
		var transform1 = this.transform;
		eyeDir = transform1.forward;
		playerPos = player.transform.position;
		npcPos = transform1.position;
		
		agent = GetComponent<NavMeshAgent>();
		agent.autoBraking = false;
		GotoNextPoint();

	}

	
	void Update ()
	{
		playerPos = player.transform.position;
		npcPos = transform.position;
		
//		Debug.Log(Vector3.Angle((npcPos - playerPos).normalized, eyeDir));
		if (Vector3.Angle((npcPos - playerPos).normalized, eyeDir) <= angle
		    && Vector3.Distance(npcPos, playerPos) <= distance
		    && Physics.Linecast(npcPos, playerPos))
		{
			ChasePlayer();
		}
		else if(!agent.pathPending && agent.remainingDistance < 1.0f)
		{
			GotoNextPoint();
		}
			
	}
	
	void GotoNextPoint()
	{
		if (points.Length == 0)
			return;
		Debug.Log("巡回中……");
		agent.destination = points[destPoint].position;
		destPoint = (destPoint + 1) % points.Length;
	}

	void ChasePlayer()
	{
		Debug.Log("chase中……");
		transform.rotation = Quaternion.Slerp(transform.rotation,
			              Quaternion.LookRotation(playerPos - transform.position), 0.01f);
		transform.position += transform.forward * speed * 0.1f;
	}
}
