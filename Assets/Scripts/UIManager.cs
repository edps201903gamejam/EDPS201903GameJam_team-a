using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public GameObject MinimapDot;
	public float dotMoveSpeed = 2;

	public string Password { get { return this.password; } set { this.password = value; } }

	private string password;

	public ScoreManager ScoreManager;
	public UnityEngine.UI.Text ScoreText;
	public UnityEngine.UI.Text HaveScoreText;
	public UnityEngine.UI.Image HaveScoreImage;
	public UnityEngine.UI.Text HaveMB;
	public GameObject HaveScoreGuage;

	public UnityEngine.UI.Text RateText;

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
		ScoreText.text = ScoreManager.score.ToString();

		//レート表示
		RateText.text = "Rate:" + ScoreManager.exchangeRate.ToString();

		//所持スコア表示
		if (player.HaveDataFlg) {
			HaveScoreText.text = player.HaveScore.ToString();
			HaveScoreGuage.GetComponent<RectTransform>().sizeDelta = new Vector2(player.HaveScore * 2, 70);
			HaveScoreImage.gameObject.SetActive(true);
			HaveMB.gameObject.SetActive(true);
		}
		else {
			HaveScoreText.text = "";
			HaveScoreGuage.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 70);
			HaveScoreImage.gameObject.SetActive(false);
			HaveMB.gameObject.SetActive(false);
		}

		//Rキーでリザに飛ぶ(デバッグ用)
		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene(sceneName: "ResultScene");
		}
		//Sキーでステージ移動(デバッグ用)
		else if (Input.GetKeyDown(KeyCode.M)) {
			if (SceneManager.GetActiveScene().name == "StageA_UI") SceneManager.LoadScene(sceneName: "StageB_UI");
			else if (SceneManager.GetActiveScene().name == "StageB_UI") SceneManager.LoadScene(sceneName: "StageA_UI");
		}

		Debug.Log(this.Password);
	}
}
