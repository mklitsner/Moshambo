using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour {
    private const string Rock_Pressed = "Rock_Pressed";
    private const string Paper_Pressed = "Paper_Pressed";
    private const string Scissors_Pressed = "Scissors_Pressed";
    private const string Won = "Won";
    private const string Lost = "Lost";
    private Player_Reciever player_Reciever;
    Title_Screen GetTitle_Screen;
    private bool arcadeMode;
    public AudioClip dud;

	public AudioClip morph;


	AudioSource source;



	public string[] Buttons;

	public PlayerState pState;

	Animator anim;

	public int currentType;

	public string State;

	public int player;
	public int direction;
	float speed;
	public float retreat_limit;
    // Use this for initialization

    void Start()
    {

		player_Reciever =
		transform.parent.parent.GetComponent<Player_Reciever>();

		GetTitle_Screen = FindObjectOfType<Title_Screen>().GetComponent<Title_Screen>();

		arcadeMode = GetTitle_Screen.IsArcadeMode;

		source = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();

		//
		currentType= Random.Range(1,4);
		//
		anim.SetInteger("Start_State",currentType);

		State = "ResetRound";
		//

	//
		speed=10;
	//identify player direction of movement
		if(player==1){
			direction = -1;
			}
		else if (player==2){
			direction = 1;
		}
	}

    // Update is called once per frame
    void Update()
    {
		ButtonsPressed ();
		SetAnimations ();


		switch (pState)
		{
			case PlayerState.InPlay:
				anim.SetBool("reset_round", false);

				Move();
				break;
			case PlayerState.Win:
				//play win anim
				anim.SetBool(Won, true);
				if (player_Reciever.ScoreShown)
				{
					pState = PlayerState.ResetRound;
				}
				break;
			case PlayerState.Lose:
				//play lose anim
				anim.SetBool(Lost, true);
				if (player_Reciever.ScoreShown)
				{
					pState = PlayerState.ResetRound;
				}
				break;
			case PlayerState.Tie:
				BounceBack();
				source.PlayOneShot(dud);
				pState = PlayerState.InPlay;
				break;
			case PlayerState.ResetRound:
				//go back to starting position
				//currentType= Random.Range(1,4);
				anim.SetInteger("Start_State", currentType);

				Retreat(20);
				if (transform.position.x * direction >= retreat_limit * direction)
				{
					pState = PlayerState.Ready;
				}
				break;
			case PlayerState.Ready:
				anim.SetBool("reset_round", true);
				//state once at starting position. once both are at starting position, go to inplay
				if (player_Reciever.playersReady)
				{
					anim.SetBool(Won, false);
					anim.SetBool(Lost, false);

					pState = PlayerState.InPlay;
				}
				break;
			default:
				break;
		}



    }


    public enum ButtonPressed
    {
        Rock,Scissors,Paper,NONE
    }

	public ButtonPressed buttonPressed;


	void SetAnimations(){
		anim.SetBool (Rock_Pressed, IsRockPressed());
		anim.SetBool (Paper_Pressed, IsPaperPressed());
		anim.SetBool (Scissors_Pressed, IsScissorsPressed());
	}
		

	void BounceBack(){
		float recoil = 5f;


		if (transform.position.x * direction >= retreat_limit * direction) {
			transform.Translate (Vector3.right * 0);
		} else {
			transform.Translate (Vector3.right * -1 *recoil);

		}

		//clink sound
	}

	void Retreat(float _speed){
		if (transform.position.x*direction >= retreat_limit*direction) {
			transform.Translate(Vector3.right*0);

		} else {
			transform.Translate (Vector3.right * -1 * _speed *1.1f* Time.deltaTime);
		}
	}


	void Move(){
		if (IsAnyPressed()) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		} else {
//			print (Mathf.Abs (transform.position.x));
//			print ("retreat to"+retreat_limit);
			if (transform.position.x*direction >= retreat_limit*direction) {
				transform.Translate(Vector3.right*0);


			}else {
				transform.Translate (Vector3.right * -1 * speed *1.1f* Time.deltaTime);
			}
		}
	}


    bool IsRockPressed()
    {
		bool pressed;
		if (arcadeMode) {
			pressed=Input.GetKey(Buttons[0]) && Input.GetKey(Buttons[1]) && Input.GetKey(Buttons[3]) || Input.GetKey(Buttons[1]) && Input.GetKey(Buttons[2]) && Input.GetKey(Buttons[5]);
        }
        else
        {
            //middle button pressed
			pressed=!Input.GetKey(Buttons[3]) && Input.GetKey(Buttons[4]) && !Input.GetKey(Buttons[5]);

		}
        if (pressed)
        buttonPressed = ButtonPressed.Rock;
		

		return pressed;
	}
    bool IsScissorsPressed()
    {
		bool pressed;
		if (arcadeMode)
		{
			pressed = Input.GetKey(Buttons[0])
			&& Input.GetKey(Buttons[2])
			&& !Input.GetKey(Buttons[1])
			||
			Input.GetKey(Buttons[3])
			&& Input.GetKey(Buttons[5])
			&& !Input.GetKey(Buttons[4]);
		}
		else
		{
			//middle button pressed
			pressed =
            Input.GetKey(Buttons[3])
			&& Input.GetKey(Buttons[5])
			&& Input.GetKey(Buttons[4])==false;

		}
		if (pressed)
			buttonPressed = ButtonPressed.Scissors;

		return pressed;
	}


	bool IsPaperPressed()
	{
		bool pressed;
		if (arcadeMode)
		{
			pressed = Input.GetKey(Buttons[0]) && Input.GetKey(Buttons[5]) && !Input.GetKey(Buttons[2])&& !Input.GetKey(Buttons[3])
                || Input.GetKey(Buttons[3]) && Input.GetKey(Buttons[2]) && !Input.GetKey(Buttons[0]) && !Input.GetKey(Buttons[5]);
		}
		else
		{
			//middle button pressed
			pressed = Input.GetKey(Buttons[3])
			&& Input.GetKey(Buttons[4])
			&& Input.GetKey(Buttons[5]);

		}
		if (pressed)
			buttonPressed = ButtonPressed.Paper;

		return pressed;
	}

    bool IsAnyPressed()
    {
		bool pressed;
			//middle button pressed
			pressed = IsRockPressed()||IsScissorsPressed()||IsPaperPressed();
		if (!pressed)
			buttonPressed = ButtonPressed.NONE;

		return pressed;
	}




	void ButtonsPressed(){

        if (IsRockPressed())
        {
			if (currentType != 1)
			{
				source.PlayOneShot(morph);
			}
			currentType = 1;
		}else if (IsScissorsPressed())
        {
			if (currentType != 3)
			{
				source.PlayOneShot(morph);
			}
			currentType = 3;
        }
        else if(IsPaperPressed())
        {
			if (currentType != 2)
			{
				source.PlayOneShot(morph);
			}
			currentType = 2;
		}

    }


    void OnTriggerEnter(Collider col){
		print (col.GetComponent<Player_Script>().currentType);

		int opponentType = col.GetComponent<Player_Script> ().currentType;
		//if opponent is rock(1)
		if (opponentType == 1) {
			if (currentType == 1) {
				pState = PlayerState.Tie;
			}
			else if(currentType==3) {
				pState = PlayerState.Lose;

			} else if(currentType==2) {
				pState = PlayerState.Win;
			}
		}

		//if opponent is paper(2)

		if (opponentType == 2) {
			if (currentType == 3) {
				pState=PlayerState.Win;
			} else if(currentType==2) {
				pState = PlayerState.Tie;
			} else if(currentType==1) {
				pState = PlayerState.Lose;
			}
		}

		//if opponent is scissors(3)

		if (opponentType == 3) {
			if (currentType == 2) {
				pState = PlayerState.Lose;
			} else if(currentType==1) {
				pState = PlayerState.Win;
			} else if(currentType==3) {
				pState = PlayerState.Tie;
			}
		}


	}
}
