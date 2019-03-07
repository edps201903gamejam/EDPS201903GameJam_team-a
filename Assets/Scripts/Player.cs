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

	public int HaveScore
	{
		get { return this.haveScore; }
		set { this.haveScore = value; }
	}

	private bool haveDataFlg = false;
	private int haveScore = 0;

	[SerializeField]
	private float moveSpeed = 10;
	private Rigidbody rb;
	private float moveX;
	private float moveZ;
	private string remainingString;
	private string enteredString;
	[SerializeField]
	private UIManager uIManager;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (uIManager.TerminalPassword == "")
		{
			MoveSet();
		}
	}

	private void FixedUpdate()
	{
		// プレイヤーの移動
		if (uIManager.TerminalPassword == "")
		{
			rb.velocity = new Vector3(moveX, 0, moveZ);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		// 端末からのデータの取得と端末へのデータの受け渡し
		if (other.CompareTag("GetArea") && !haveDataFlg　&& haveScore == 0)
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				Terminal terminalData = other.GetComponent<Terminal>();
				Debug.Log(terminalData.Password);
				uIManager.TerminalPassword = terminalData.Password;
				uIManager.TerminalScore = terminalData.TerminalScore;
			}
		}
		
		if (other.CompareTag("CollectArea") && haveDataFlg)
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				haveDataFlg = false;
				Debug.Log("データを渡しました！");
			}
		}
	}

	private void MoveSet()
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
}
