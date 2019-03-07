using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public GameObject MinimapDot;
	public float moveSpeed = 10;

	public string TerminalPassword
	{
		get { return this.terminalPassword; }
		set { this.terminalPassword = value; }
	}

	public int TerminalScore
	{
		get { return this.terminalScore; }
		set { this.terminalScore = value; }
	}

	private string terminalPassword = "";
	private int terminalScore = 0;

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
		float mapX = (Player.transform.position.x * moveSpeed) + 85;
		float mapZ = (Player.transform.position.z * moveSpeed) + 70;
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

		if (terminalPassword != "")
		{
			CompareTerminalPass(terminalPassword);
		}
	}

	private void CompareTerminalPass(string _terminalPass)
	{
		int strLength = _terminalPass.Length;
		
		// キー入力
		if (Input.anyKeyDown)
		{
			// 入力が成功している場合
			if (Input.GetKeyDown(terminalPassword[0].ToString()))
			{
				Debug.Log("OK!");
				this.terminalPassword = this.terminalPassword.Remove(0, 1);
				Debug.Log(terminalPassword);
				strLength--;
				if (strLength == 0)
				{
					player.HaveDataFlg = true;
					player.HaveScore += terminalScore;
					Debug.Log("データを入手しました！");
				}
			}
			
			// 十字キーでキャンセル
			else if(Input.GetKeyDown(KeyCode.UpArrow) ||
			        Input.GetKeyDown(KeyCode.LeftArrow) ||
			        Input.GetKeyDown(KeyCode.DownArrow) ||
			        Input.GetKeyDown(KeyCode.RightArrow))
			{
				terminalPassword = "";
				player.HaveDataFlg = false;
			}
			
			// 入力が失敗している場合
			else if (!Input.GetKeyDown(terminalPassword[0].ToString()))
			{
				Debug.Log("NO!");
			}
		}
	}
	
}
