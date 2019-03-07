﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public GameObject MinimapDot;
	public float dotMoveSpeed = 2;

	public string Password { get { return this.password; } set { this.password = value; } }

	private string password;

	public ScoreManager ScoreManager;
	public UnityEngine.UI.Text ScoreTextA;
	public UnityEngine.UI.Text ScoreTextB;
	public UnityEngine.UI.Text HaveScoreText;
	public GameObject HaveScoreGuage;


	[SerializeField]
	private Player player;
	[SerializeField]
	private GameObject Player;

	// Update is called once per frame
	void Update(){
		//ミニマップのドット移動
		float mapX = (Player.transform.position.x * dotMoveSpeed) + 95;
		float mapZ = (Player.transform.position.z * dotMoveSpeed) + 75;
		MinimapDot.transform.position = new Vector3(mapX, mapZ, 0);

		//スコア表示
		ScoreTextA.text = ScoreManager.scoreA.ToString();
		ScoreTextB.text = ScoreManager.scoreB.ToString();

		//所持スコア表示
		if (player.HaveDataFlg) {
			HaveScoreText.text = player.HaveScore.ToString();
			HaveScoreGuage.GetComponent<RectTransform>().sizeDelta = new Vector2(player.HaveScore * 2, 70);
		}
		else {
			HaveScoreText.text = "";
			HaveScoreGuage.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 70);
		}

		//Rキーでリザに飛ぶ(デバッグ用)
		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene( sceneName:"ResultScene" );
		}

		Debug.Log(this.Password);
	}
}
