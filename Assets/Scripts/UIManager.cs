using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour {

	public GameObject MinimapDot;
	public float dotMoveSpeed = 2;

	public string TerminalPassword {
		get { return this.terminalPassword; }
		set { this.terminalPassword = value; }
	}

	public int TerminalScore {
		get { return this.terminalScore; }
		set { this.terminalScore = value; }
	}

	public Terminal AccessedTerminal {
		get { return this.accessedTerminal; }
		set { this.accessedTerminal = value; }
	}

	public int CurrentMapFlg { get { return this.currentMapFlg; } set { this.currentMapFlg = value; } }

	public int HaveDataSide { get { return this.haveDataSide; } set { this.haveDataSide = value; } }

	private string terminalPassword = "";
	private int terminalScore = 0;
	private Terminal accessedTerminal;

	public ScoreManager ScoreManager;
	public UnityEngine.UI.Text ScoreText;
	public UnityEngine.UI.Text HaveScoreText;
	public UnityEngine.UI.Image HaveScoreImage;
	public UnityEngine.UI.Text HaveMB;
	public GameObject HaveScoreGuage;
	//メッセージ
	public UnityEngine.UI.Text MessageText;
	float messageAlpha = 0;

	//マップのGameObjectを登録するところ
	[SerializeField]
	private GameObject[] stageMap = new GameObject[2];

	//今どっちのマップに居るか
	private int currentMapFlg = 0;
	private int haveDataSide;

	public UnityEngine.UI.Text RateText;

	//寿司打UI
	public GameObject PasswordUI;
	public UnityEngine.UI.Text PasswordText;
	public UnityEngine.UI.Text EnteredPasswordText;
	private string enteringPassword = "";
	private int numofTypingPassword = 0;

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

		if (messageAlpha > 0) {
			messageAlpha -= Time.deltaTime;
			MessageText.color = new Color(0, 0, 0, messageAlpha);
		}

		//スコア表示
		ScoreText.text = ScoreManager.score.ToString();

		//レート表示
		RateText.text = "Rate:" + ScoreManager.exchangeRate.ToString();

		//所持スコア表示
		if (player.HaveDataFlg) {
			HaveScoreText.text = player.HaveScore.ToString();
			HaveScoreGuage.GetComponent<RectTransform>().sizeDelta = new Vector2(player.HaveScore , 32);
			HaveScoreImage.gameObject.SetActive(true);
			HaveMB.gameObject.SetActive(true);
		}
		else {
			HaveScoreText.text = "";
			HaveScoreGuage.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 70);
			HaveScoreImage.gameObject.SetActive(false);
			HaveMB.gameObject.SetActive(false);
		}

		//Enterキーでリザに飛ぶ(デバッグ用)
		//if (Input.GetKeyDown(KeyCode.Return)) {
		//	SceneManager.LoadScene(sceneName: "ResultScene");
		//}
		//Mキーでステージ移動(デバッグ用)
		//else if (Input.GetKeyDown(KeyCode.M)) {
		//	if( currentMapFlg == 0) {
		//		this.stageMap[0].SetActive(false);
		//		this.stageMap[1].SetActive(true);
		//		currentMapFlg = 1;
		//	}
		//	else if(currentMapFlg == 1) {
		//		this.stageMap[1].SetActive(false);
		//		this.stageMap[0].SetActive(true);
		//		currentMapFlg = 0;
		//	}
		//}

		if (terminalPassword != "") {
			CompareTerminalPass(terminalPassword);
		}
		else enteringPassword = "";
	}

	private void CompareTerminalPass(string _terminalPass) {
		int strLength = _terminalPass.Length;

		// キー入力
		if (Input.anyKeyDown) {
			if (numofTypingPassword == 0) {
				enteringPassword = "";
				PasswordUI.SetActive(true);
				PasswordText.text = _terminalPass;
				EnteredPasswordText.text = enteringPassword;
			}
			// 入力が成功している場合
			if (Input.GetKeyDown(terminalPassword[0].ToString())) {
				numofTypingPassword++;
				enteringPassword += terminalPassword[0];
				EnteredPasswordText.text = enteringPassword;
				this.terminalPassword = this.terminalPassword.Remove(0, 1);
				strLength--;
				if (strLength == 0) {
					player.HaveDataFlg = true;
					player.HaveScore += terminalScore;
					if(currentMapFlg == 0) {
						haveDataSide = 0;
					}
					else if(currentMapFlg == 1) {
						haveDataSide = 1;
					}
					PasswordUI.SetActive(false);
					enteringPassword = "";
					numofTypingPassword = 0;
					MessageText.text = ("データを入手しました！");
					accessedTerminal.Password = Guid.NewGuid().ToString("N").Substring(0, accessedTerminal.Password.Length);
					messageAlpha = 1;
				}
			}

			// 十字キーでキャンセル
			else if (Input.GetKeyDown(KeyCode.UpArrow) ||
					Input.GetKeyDown(KeyCode.LeftArrow) ||
					Input.GetKeyDown(KeyCode.DownArrow) ||
					Input.GetKeyDown(KeyCode.RightArrow)) {
				terminalPassword = "";
				numofTypingPassword = 0;
				enteringPassword = "";
				PasswordUI.SetActive(false);
				player.HaveDataFlg = false;
			}

			// 入力が失敗している場合
			else if (!Input.GetKeyDown(terminalPassword[0].ToString()) && numofTypingPassword!=0) {
				MessageText.text = ("違います");
				messageAlpha = 1;
			}
		}
	}
}
