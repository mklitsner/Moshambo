using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Hand : MonoBehaviour {
	Animator anim;

	public int player;

	public GameObject player_reciever;

	int player_score;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player == 1) {
			player_score = player_reciever.GetComponent<Player_Reciever> ().p1Score;
		} else if (player == 2) {
			player_score = player_reciever.GetComponent<Player_Reciever> ().p2score;
		}
		anim.SetInteger ("player_Score", player_score);
	}


}
