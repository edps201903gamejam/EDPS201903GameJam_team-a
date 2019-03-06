using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public GameObject MinimapDot;
	public float moveSpeed = 10;

	public ScoreManager ScoreManager;
	public UnityEngine.UI.Text ScoreTextA;
	public UnityEngine.UI.Text ScoreTextB;
	public UnityEngine.UI.Text HaveScoreText;

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
		ScoreTextA.text = "A：" + ScoreManager.scoreA;
		ScoreTextB.text = "B：" + ScoreManager.scoreB;

		//所持スコア表示
		if ( player.HaveDataFlg ) {
			HaveScoreText.text = player.HaveScore.ToString();
		}
		else {
			HaveScoreText.text = "";
		}
	}
}
