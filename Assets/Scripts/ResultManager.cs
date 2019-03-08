using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour {

	[SerializeField]
	private UnityEngine.UI.Text ResultScore;
	[SerializeField]
	private UnityEngine.UI.Text ResultMB;
	[SerializeField]
	private UnityEngine.UI.Text ResultTM;

	// Use this for initialization
	void Start () {
		int totalScore = ScoreManager.score;
		int totalDatasize = ScoreManager.datasize;
		ResultScore.text = "$" + totalScore.ToString();
		ResultMB.text = totalDatasize.ToString() + "MB";
		ResultTM.text = ScoreManager.typemiss + "回";
	}

	private void Update() {
		if( Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene( sceneName: "TitleScene" );
		}
	}
}
