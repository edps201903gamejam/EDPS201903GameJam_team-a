using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {

	private float timelimit = 60;

	private float left = 0f;
	private float remain = 0f;

	[SerializeField]
	private UnityEngine.UI.Text TimeText;

	void Update () {
		left += Time.deltaTime;
		remain = timelimit - left;
		TimeText.text = Mathf.Floor(remain).ToString();

		if ( remain <= 0.2f ) {
			SceneManager.LoadScene(sceneName: "ResultScene");
		}
		else if ( Mathf.Floor(remain) == 30 ) {
			TimeText.color = new Color(255.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f);
		}
	}
}
