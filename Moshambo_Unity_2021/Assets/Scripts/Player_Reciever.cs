using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
	InPlay, Win, Lose, Tie, ResetRound, Ready
}

public class Player_Reciever : MonoBehaviour {
	public GameObject player_1;
	[SerializeField] Player_Script P1, P2;
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
		PlayerState player1State = P1.pState;
		PlayerState player2State = P2.pState;

        //bad code where I judge the winner based on the state of player 1
        //instead of just computing everything in this script
        //TODO mirgrate colision detection to this, which is effectivily the 'game manager' script
        switch (player1State)
        {
            case PlayerState.InPlay:
				ScoreMarked = false;
				playersReady = false;
				break;
            case PlayerState.Win:
				print("Win:Lose");
				//display win/lose
				//add one score to player 1
				if (ScoreMarked == false)
				{
					source.PlayOneShot(win);
					p1Score = p1Score + 1;
					ScoreMarked = true;
				}
				timer = timer - Time.deltaTime;
				if (timer <= 0 && p1Score < 5)
				{
					ScoreShown = true;
					timer = 2;
				}
				else if (timer < -2 && p1Score >= 5)
				{
					menu = true;
					source.PlayOneShot(final);
				}
				break;
            case PlayerState.Lose:
				print("Lose:Win");
				//display lose/win
				//add one score to player 2
				if (ScoreMarked == false)
				{
					source.PlayOneShot(win);
					p2score = p2score + 1;
					ScoreMarked = true;
				}
				else if (timer < -2 && p2score >= 5)
				{
					menu = true;
					source.PlayOneShot(final);
				}
				timer = timer - Time.deltaTime;
				if (timer <= 0 && p2score < 5)
				{
					ScoreShown = true;
					timer = 2;
				}
				break;
            case PlayerState.Tie:
				print("Tie");
				break;
            case PlayerState.ResetRound:
                break;
            case PlayerState.Ready:
                break;
            default:
                break;
        }



		if (player1State == PlayerState.Ready && player2State == PlayerState.Ready && menu ==false) {
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
