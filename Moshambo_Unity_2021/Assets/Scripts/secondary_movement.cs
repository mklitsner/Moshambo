using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondary_movement : MonoBehaviour {
	public GameObject player;
	public int playerNum;
	public float movementOffset;
	public float offset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		//movement
		transform.position = new Vector3 
			(player.transform.position.x*movementOffset+
				player.GetComponent<Player_Script>().retreat_limit+offset*playerNum,
			player.transform.position.y, 
				transform.position.z);


	}
}
