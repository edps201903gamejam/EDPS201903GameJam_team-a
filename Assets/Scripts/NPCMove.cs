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

	public float angle = 60.0f;
	private float distance = 20.0f;
	private RaycastHit hit;

	public GameObject player;
	public float speed = 10.0f;
	
	public float chaseRemaining = 5.0f;

	public bool isMapB = false;
	
	public bool IsGameOver = false;

	private Animation anim;

	private Rigidbody rb;

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
//Debug.Log(Vector3.Distance(npcPos, playerPos) <= distance);
		//Debug.Log(Physics.Linecast(npcPos, playerPos));
		if (Vector3.Angle((playerPos - npcPos).normalized, eyeDir) <= angle
		    && Physics.Linecast(npcPos, playerPos, out hit)
			&& Vector3.Distance(playerPos, npcPos) <= distance)
		{
			ChasePlayer();
			chaseRemaining = 5.0f;
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
		//while (chaseRemaining >= 0)
		//{
			//chaseRemaining -= Time.deltaTime;
			Debug.Log("chase中……");
			//transform.rotation = Quaternion.Slerp(transform.rotation,
			//Quaternion.LookRotation(playerPos - transform.position), 0.009f);
			//transform.position += transform.forward * speed * 0.1F;
			speed = 30;
			agent.destination = playerPos;
		//}
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
