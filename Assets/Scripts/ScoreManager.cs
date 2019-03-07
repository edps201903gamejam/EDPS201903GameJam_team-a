using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public static int score = 0;

	[SerializeField]
	Player player;

	private void Update(){
		if(player.HaveScore != 0 && !player.HaveDataFlg)
		{
			score += player.HaveScore;
			player.HaveScore = 0;
		}
	}
}