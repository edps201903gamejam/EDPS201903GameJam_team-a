using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour {

	[SerializeField]
	private UnityEngine.UI.Text ResultScore;
	[SerializeField]
	private UnityEngine.UI.Text ResultMB;

	// Use this for initialization
	void Start () {
		int totalScore = ScoreManager.score;
		int totalDatasize = ScoreManager.datasize;
		ResultScore.text = "$" + totalScore.ToString();
		ResultMB.text = totalDatasize.ToString() + "MB";
	}
}
