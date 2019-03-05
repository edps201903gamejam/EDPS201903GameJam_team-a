using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private float speed = 10;
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		// プレイヤーの移動
		if (Input.GetKey(KeyCode.UpArrow))
		{
			rb.velocity = new Vector3(0, 0, speed);
		}
		
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			rb.velocity = new Vector3(-speed, 0, 0);
		}
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			rb.velocity = new Vector3(0, 0, -speed);
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			rb.velocity = new Vector3(speed, 0, 0);
		}

		if (Input.GetKeyUp(KeyCode.UpArrow) ||
		    Input.GetKeyUp(KeyCode.LeftArrow) ||
		    Input.GetKeyUp(KeyCode.DownArrow) ||
		    Input.GetKeyUp(KeyCode.RightArrow))
		{
			rb.velocity = new Vector3(0, 0, 0);
		}
	}
}
