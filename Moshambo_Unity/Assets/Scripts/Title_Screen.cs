using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Screen : MonoBehaviour {
	Animator anim;
	public GameObject P1;
	public GameObject P2;
	public GameObject Reciever;
	public bool IsArcadeMode;

	public int startSize = 3;
	public float minSize = 1;
	public float maxSize = 6;

	public float speed = 2.0f;

	private Vector3 targetScale;
	private Vector3 baseScale;
	private float currScale;


public string[] Buttonsp1;
public string[] Buttonsp2;


	public AudioClip pause;

	public AudioClip fight;

	AudioSource source;
    private bool newgame;

    // Use this for initialization
    void Start () {

		source = GetComponent<AudioSource> ();
		baseScale = transform.localScale;
		transform.localScale = baseScale * startSize;
		currScale = startSize;
		targetScale = baseScale * startSize;

		anim = GetComponent<Animator> ();

		Buttonsp1=P1.GetComponent<Player_Script> ().Buttons;
		Buttonsp2=P2.GetComponent<Player_Script> ().Buttons;



	}
	
	// Update is called once per frame
	void Update () {


		StartNewGame();

		





		bool menu = Reciever.GetComponent<Player_Reciever> ().menu;
		transform.localScale = Vector3.Lerp (transform.localScale, targetScale, speed * Time.deltaTime);

		if (menu) {

		
			ChangeSize (true);
			anim.SetBool ("Open", true);
			if(transform.position.y>=1.2){
				transform.Translate (Vector3.up * -4*Time.deltaTime);
			}else{
				transform.Translate (Vector3.up * 0);
			}
		} else {


			ChangeSize(false);

			anim.SetBool("Open",false);
			print ("close");
			if(transform.position.y<8){
				transform.Translate (Vector3.up * 4*Time.deltaTime);
			}else{
				transform.Translate (Vector3.up * 0);
			}
		}

		if (newgame) {
			Reciever.GetComponent<Player_Reciever> ().menu = false;
	
			}

	}

	void StartNewGame(){

		if (IsArcadeMode)
		{
			if (Input.GetKey(Buttonsp1[0]) &&
				Input.GetKey(Buttonsp1[1]) &&
				Input.GetKey(Buttonsp1[2]) &&
				Input.GetKey(Buttonsp1[3]) &&
					Input.GetKey(Buttonsp1[4]) &&
						Input.GetKey(Buttonsp1[5])
							|| Input.GetKey(Buttonsp2[0]) &&
								Input.GetKey(Buttonsp2[1]) &&
									Input.GetKey(Buttonsp2[2]) &&
										Input.GetKey(Buttonsp2[3]) &&
											Input.GetKey(Buttonsp2[4]) &&
				Input.GetKey(Buttonsp2[5]))
			{


	

				newgame = true;
                //close animation
                //dissapear
            }
            else
            {
				newgame = false;
			}
        }
        else
        {
			if (Input.GetKey(Buttonsp1[3]) &&
				Input.GetKey(Buttonsp1[4]) &&
				Input.GetKey(Buttonsp1[5])|| Input.GetKey(Buttonsp2[3]) &&
								Input.GetKey(Buttonsp2[4]) &&
                                    Input.GetKey(Buttonsp2[5]))
            {

				newgame = true;
            }
            else
            {
				newgame = false;
            }

		}
	}
	void ChangeSize(bool bigger) {

		if (bigger)
			currScale++;
		else
			currScale--;

		currScale = Mathf.Clamp (currScale, minSize, maxSize+1);

		targetScale = baseScale * currScale;
	}    
}
