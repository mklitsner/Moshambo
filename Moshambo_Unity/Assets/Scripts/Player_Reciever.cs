using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Reciever : MonoBehaviour {
	public GameObject player_1;
	public GameObject player_2;
	// Use this for initialization
	public int p1Score;
	public int p2score;
	bool	ScoreMarked;
	public bool playersReady;
	public bool ScoreShown;
	float timer;
	public bool menu;

	public AudioClip win;

	public AudioClip final;

	AudioSource source;



	void Start () {
		menu = true;
		timer = 2;
		ScoreShown = false;
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		string player1State = player_1.transform.GetComponent<Player_Script> ().State;
		string player2State = player_2.transform.GetComponent<Player_Script> ().State;

	if (player1State == "InPlay") {
	
			ScoreMarked = false;
			playersReady = false;

		}
			
			if (player1State == "Win") {
				print ("Win:Lose");
				//display win/lose
				//add one score to player 1
			if (ScoreMarked == false) {
				source.PlayOneShot (win);
				p1Score = p1Score + 1;
				ScoreMarked = true;
			}
			timer= timer-Time.deltaTime;
			if (timer <= 0 && p1Score<5) {
				ScoreShown = true;
				timer = 2;
			}else if(timer < -2 && p1Score>=5){
				menu = true;
				source.PlayOneShot (final);
			}
			} else if (player1State == "Lose") {
				print ("Lose:Win");
				//display lose/win
				//add one score to player 2
			if (ScoreMarked == false) {
				source.PlayOneShot (win);
				p2score = p2score + 1;
				ScoreMarked = true;
			}else if(timer < -2 && p2score>=5){
				menu = true;
				source.PlayOneShot (final);
			}
			timer= timer-Time.deltaTime;
			if (timer <= 0 && p2score<5) {
				ScoreShown = true;
				timer = 2;
			}


			} else if (player1State == "Tie") {
				print ("Tie");
				//clank sound
			}



		if (player1State == "Ready" && player2State == "Ready"&& menu==false) {
			playersReady = true;
			//display "go"
			ScoreShown = false;
		}

		if (menu) {
			NewGame ();

		}

	}

	void NewGame(){

		p1Score = 0;
		p2score = 0;
	}

}
