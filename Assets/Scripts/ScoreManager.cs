using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	//稼いだ金額の合計
	public static int score = 0;
	//渡したデータ量の合計
	public static int datasize = 0;
	//交換レート
	public static float exchangeRate = 765f;

	[SerializeField]
	Player player;

	private void Update(){
		//スコア加算
		if(player.HaveScore != 0 && !player.HaveDataFlg){
			datasize += player.HaveScore;
			score += (int)Mathf.Floor(player.HaveScore * exchangeRate);
			player.HaveScore = 0;
		}
	}
}