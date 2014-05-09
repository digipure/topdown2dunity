using UnityEngine;
using System.Collections;

/*
Attached to a door, move the player to the next room
 */

public class Door : MonoBehaviour {

	public MOVEMENTDIRECTION doorTo;



	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log ("door "+ name);


		if (other.gameObject.name!="hero") return;

		// First, pause the game and start a fade out
		GameMain gm = Camera.main.GetComponent("GameMain") as GameMain;
		AudioSource.PlayClipAtPoint(gm.SFXDoor,Camera.main.transform.position);

		FadeOut fo = Camera.main.GetComponent("FadeOut") as FadeOut;
		fo.DoFadeOut(5,endFadeOut);
		RoomManager.Instance.Pause = true;
	
	}

	// Second, after the fade out finishes, move the player and start a fade-in
	void endFadeOut() {
		moveToDor();
		FadeOut fo = Camera.main.GetComponent("FadeOut") as FadeOut;
		fo.DoFadeIn(5,endFadeIn);

	}

	//After the fadeout, start the game again
	void endFadeIn() {
		RoomManager.Instance.Pause = false;
	}

	//Move the player (and the GUI) to the next room
	void moveToDor() {
		switch (doorTo) {
		case MOVEMENTDIRECTION.UP: 
			Camera.main.transform.Translate(0,4,0);
			GameObject.Find("GUI").transform.Translate(0,4,0);
			GameObject.Find("hero").transform.Translate(0,2,0);
			RoomManager.Instance.Translate(1,0);
			break;
		case MOVEMENTDIRECTION.DOWN: 
			Camera.main.transform.Translate(0,-4,0);
			GameObject.Find("GUI").transform.Translate(0,-4,0);
			GameObject.Find("hero").transform.Translate(0,-2,0);
			RoomManager.Instance.Translate(-1,0);
			break;
		case MOVEMENTDIRECTION.RIGHT: 
			Camera.main.transform.Translate(6,0,0);
			GameObject.Find("GUI").transform.Translate(6,0,0);
			GameObject.Find("hero").transform.Translate(3,0,0);
			RoomManager.Instance.Translate(0,1);
			break;
		case MOVEMENTDIRECTION.LEFT: 
			Camera.main.transform.Translate(-6,0,0);
			GameObject.Find("GUI").transform.Translate(-6,0,0);
			GameObject.Find("hero").transform.Translate(-3,0,0);
			RoomManager.Instance.Translate(0,-1);
			break;
		}
	
	}

}
