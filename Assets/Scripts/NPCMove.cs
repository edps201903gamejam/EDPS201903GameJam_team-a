using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NPCMove : MonoBehaviour
{

	public Transform[] points;
	private int destPoint = 0;
	private NavMeshAgent agent;
	

	private Vector3 eyeDir;
	private Vector3 playerPos;
	private Vector3 npcPos;

	private float angle = 30.0f;
	private float distance = 10.0f;

	public GameObject player;
	public float speed = 0.1f;
	
	public float chaseRemaining = 3.0f;

	public bool isMapB = false;
	
	public bool IsGameOver = false;

	private Animation anim;
	
	void Start ()
	{
		
		agent = GetComponent<NavMeshAgent>();
//		agent.autoBraking = false;
		GotoNextPoint();
		anim = GetComponent<Animation>();
	}

	
	void Update ()
	{
		this.anim.Play();
		var transform1 = this.gameObject.transform;
		eyeDir = transform1.forward;
		playerPos = player.transform.position;
		npcPos = transform1.position;
		
//		Debug.Log(Vector3.Angle((playerPos - npcPos).normalized, eyeDir));
//		Debug.Log(Vector3.Distance(npcPos, playerPos) <= distance);
//		Debug.Log(Physics.Linecast(npcPos, playerPos));
		if (Vector3.Angle((playerPos - npcPos).normalized, eyeDir) <= angle
		    && Vector3.Distance(playerPos, npcPos) <= distance
		    && Physics.Linecast(npcPos, playerPos))
		{
			ChasePlayer();
			chaseRemaining = 3.0f;
		}
		else if (!agent.pathPending && agent.remainingDistance < 1.0f)
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
		if(isMapB && destPoint + 1 == points.Length)
			Array.Reverse(points);
		destPoint = (destPoint + 1) % points.Length;
	}

	void ChasePlayer()
	{
		while (chaseRemaining >= 0)
		{
			chaseRemaining -= Time.deltaTime;
			Debug.Log("chase中……");
			transform.rotation = Quaternion.Slerp(transform.rotation,
			Quaternion.LookRotation(playerPos - transform.position), 0.01f);
			transform.position += transform.forward * speed * 0.1f;
		}
	}

	private void OnCollisionEnter(Collision other)
	{	
			if (other.gameObject.CompareTag("Player"))
			{
				IsGameOver = true;
			}

			if (IsGameOver == true)
			{
				SceneManager.LoadScene("ResultScene");
			}
	}
}
