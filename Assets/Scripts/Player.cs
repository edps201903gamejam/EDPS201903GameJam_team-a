using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private float speed = 0.25f;
	private Vector3 movePosition;

	private void Start()
	{
		movePosition = this.transform.position;
	}

	private void Update()
	{
		// プレイヤーの移動
		if (Input.GetKey(KeyCode.UpArrow))
		{
			movePosition.z += speed;
			this.transform.position = movePosition;
		}
		
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			movePosition.x -= speed;
			this.transform.position = movePosition;
		}
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			movePosition.z -= speed;
			this.transform.position = movePosition;
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			movePosition.x += speed;
			this.transform.position = movePosition;
		}
	}
}
