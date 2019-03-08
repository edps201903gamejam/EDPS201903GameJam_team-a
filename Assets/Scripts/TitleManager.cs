using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	public UnityEngine.UI.Text TitleStart;
	public UnityEngine.UI.Text TitleCredit;
	public UnityEngine.UI.Text TitleQuit;

	public GameObject Credits;

	private int focus = 0;
	private bool isCredit = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TitleStart.color = new Color(1, 1, 1);
		TitleCredit.color = new Color(1, 1, 1);
		TitleQuit.color = new Color(1, 1, 1);
		switch (focus) {
			case 0:
				TitleStart.color = new Color(1, 0, 0);
				break;
			case 1:
				TitleCredit.color = new Color(1, 0, 0);
				break;
			case 2:
				TitleQuit.color = new Color(1, 0, 0);
				break;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			focus++;
			focus %= 3;
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow)) {
			focus--;
			focus %= 3;
			if (focus == -1) focus = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Return)) {
			if(isCredit) { 
				Credits.SetActive(false);
				isCredit = false;
				return;
			}
			switch (focus) {
				case 0:
					SceneManager.LoadScene(sceneName: "StageA_UI");
					ScoreManager.score = 0;
					ScoreManager.datasize = 0;
					ScoreManager.typemiss = 0;
					break;
				case 1:
					isCredit = true;
					Credits.SetActive(true);
					break;
			}
		}
	}
}
