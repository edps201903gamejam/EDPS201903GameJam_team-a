using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public int scoreA = 0;
	public int scoreB = 0;

	[SerializeField]
	Player player;

	private void Update(){
		if(player.HaveScore != 0 && !player.HaveDataFlg)
		{
			scoreA += player.HaveScore;
			scoreB += player.HaveScore;
			player.HaveScore = 0;
		}
	}
}