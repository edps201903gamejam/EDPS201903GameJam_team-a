using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
	public bool HaveDataFlg
	{
		get { return this.haveDataFlg;}
		set { this.haveDataFlg = value; }
	}
	
	[SerializeField]
	private float moveSpeed = 10;
	private Rigidbody rb;
	private float moveX;
	private float moveZ;
	private bool haveDataFlg = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		moveX = Input.GetAxis ("Horizontal") * moveSpeed;
		moveZ = Input.GetAxis ("Vertical") * moveSpeed;
		Vector3 direction = new Vector3(moveX / moveSpeed , 0, moveZ / moveSpeed);
		if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 ||
		    Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0)
		{
			this.transform.localRotation = Quaternion.LookRotation(direction);
		}
	}

	private void FixedUpdate()
	{
		rb.velocity = new Vector3(moveX, 0, moveZ);
	}

	private void OnTriggerStay(Collider other)
	{
		// 端末からのデータの取得と端末へのデータの受け渡し
		if (other.CompareTag("GetArea") && haveDataFlg == false)
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				haveDataFlg = true;
				Debug.Log("データを入手しました！");
			}
		}
		
		if (other.CompareTag("CollectArea") && haveDataFlg == true)
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				haveDataFlg = false;
				Debug.Log("データを渡しました！");
			}
		}
	}
}
