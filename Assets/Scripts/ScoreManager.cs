using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public int scoreA = 0;
	public int scoreB = 0;

	private void Update(){
		scoreA += Random.Range(0, 3);
		scoreB += Random.Range(0, 10);
	}
}