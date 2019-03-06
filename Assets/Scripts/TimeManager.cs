using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {

	public float timelimit;

	private float left = 0f;
	private float remain = 0f;

	[SerializeField]
	private UnityEngine.UI.Text TimeText;

	void Update () {
		left += Time.deltaTime;
		remain = timelimit - left;
		TimeText.text = Mathf.Floor(remain).ToString();

		if ( remain <= 0 ) {
			SceneManager.LoadScene(sceneName: "ResultScene");
		}
	}
}
