using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour {

	[SerializeField]
	private UnityEngine.UI.Text ResultScore;
	// Use this for initialization
	void Start () {
		int totalScore = ScoreManager.scoreA + ScoreManager.scoreB;
		Debug.Log( totalScore );
		ResultScore.text = totalScore.ToString();
	}
}
