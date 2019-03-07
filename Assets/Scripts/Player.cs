using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	//メッセージ
	public UnityEngine.UI.Text MessageText;
	float messageAlpha = 0;
	private Animation anim;

	//マップのGameObjectを登録するところ
	[SerializeField]
	private GameObject[] stageMap = new GameObject[2];


	//マップのGameObjectを登録するところ
	[SerializeField]
	private GameObject[] stageMap = new GameObject[2];


	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animation>();
	}

	private void Update()
	{
		if (uIManager.TerminalPassword == "") {
			MoveSet();
		}

		if (messageAlpha > 0) {
			messageAlpha -= Time.deltaTime;
			MessageText.color = new Color(0, 0, 0, messageAlpha);
		}


	}

	private void FixedUpdate()
	{
		// プレイヤーの移動
		if (uIManager.TerminalPassword == "") {
			rb.velocity = new Vector3(moveX, 0, moveZ);
		}
		//Dキーで加速(デバッグ用)
		if (Input.GetKeyDown(KeyCode.D)) {
			moveSpeed = 20;
		}
	}

	//柱に入ったらマップ移動
	private void OnTriggerEnter(Collider other) {
		if( other.CompareTag("MoveArea")) {
			if (uIManager.CurrentMapFlg == 0) {
				stageMap[0].SetActive(false);
				stageMap[1].SetActive(true);
				uIManager.CurrentMapFlg = 1;
				transform.position = new Vector3(-39, -16, -23);
				MessageText.text = "マップBに移動";
				messageAlpha = 1;
			}
			else if (uIManager.CurrentMapFlg == 1) {
				stageMap[0].SetActive(true);
				stageMap[1].SetActive(false);
				uIManager.CurrentMapFlg = 0;
				transform.position = new Vector3(44, -16, -24);
				MessageText.text = "マップAに移動";
				messageAlpha = 1;
			}
		}
	}

	private void OnTriggerStay(Collider other) {
		// 端末からのデータの取得と端末へのデータの受け渡し
		if (other.CompareTag("GetArea") && !haveDataFlg && haveScore == 0)
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				Terminal terminalData = other.GetComponent<Terminal>();
				Debug.Log(terminalData.Password);
				uIManager.TerminalPassword = terminalData.Password;
				uIManager.TerminalScore = terminalData.TerminalScore;
			}
		}

		if (other.CompareTag("GetArea") && !haveDataFlg && haveScore == 0) {
			if (Input.GetKeyDown(KeyCode.Z)) {
				Terminal terminalData = other.GetComponent<Terminal>();
				Debug.Log(terminalData.Password);
				uIManager.TerminalPassword = terminalData.Password;
				uIManager.TerminalScore = terminalData.TerminalScore;
				uIManager.AccessedTerminal = terminalData;
			}
		}

		if (other.CompareTag("CollectArea") && haveDataFlg)
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				if (uIManager.HaveDataSide == uIManager.CurrentMapFlg) {
					MessageText.text = "それはむり";
					messageAlpha = 1; 
					return;
				}
				haveDataFlg = false;
				MessageText.text = "データを渡しました！";
				messageAlpha = 1;
			}
		}
	}

	private void MoveSet() {
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			anim["Walk"].speed = 1.5f;
			this.anim.Play();
		}
		else
		{
			this.anim["Walk"].speed = 0;
		}
		
		moveX = Input.GetAxis("Horizontal") * moveSpeed;
		moveZ = Input.GetAxis("Vertical") * moveSpeed;
		Vector3 direction = new Vector3(moveX / moveSpeed, 0, moveZ / moveSpeed);
		if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 ||
			Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0)
		{
			this.transform.localRotation = Quaternion.LookRotation(direction);
		}
	}
}
