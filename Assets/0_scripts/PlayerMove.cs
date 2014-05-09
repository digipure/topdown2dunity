using UnityEngine;
using System.Collections;



// Possible movement directions
public enum MOVEMENTDIRECTION {UP,DOWN,LEFT,RIGHT,NONE};
// Weapons
public enum WEAPON {SWORD,BOOMERANG,LASTWEAPON};

/*
Player actions
 */
public class PlayerMove : MonoBehaviour {

	public MOVEMENTDIRECTION movementDirection {get; private set;}
	MOVEMENTDIRECTION lookingTo;
	public WEAPON weapon {get; private set;}

	public GameObject boomerang,sword;

	public float speed = 0.05f;

	bool inventoryMode = false;



	public AudioClip SFXSword;




	void Start () {
		movementDirection = MOVEMENTDIRECTION.DOWN;
		lookingTo = MOVEMENTDIRECTION.DOWN;



	}

	void playClick() {
		GameMain gm = Camera.main.GetComponent("GameMain") as GameMain;
		AudioSource.PlayClipAtPoint(gm.SFXGUI,Camera.main.transform.position);
	}

	void Update () {

		if (RoomManager.Instance.Pause) return;

		// Inventory and map screen interaction
		// this should be in another class but I'm to lazy...
		if (Input.GetKeyUp (KeyCode.S)) {
			playClick();
			if (inventoryMode) {
				RoomManager.Instance.Inventory.SetActive(false);
				inventoryMode = false;
			} else {
				RoomManager.Instance.Inventory.SetActive(true);
				inventoryMode = true;
			}
		}
		if (inventoryMode) {
			if (Input.GetKeyUp (KeyCode.RightArrow)) {
				playClick();
				weapon++;
				if (weapon == WEAPON.LASTWEAPON) weapon = 0;
			} else
			if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				weapon--;
				playClick();
				if (weapon < 0) weapon = WEAPON.LASTWEAPON-1;
			}
			GameObject selector = GameObject.Find ("selector");
			GameObject selectorStart = GameObject.Find ("selectorstart");
			selector.transform.position = selectorStart.transform.position + new Vector3((float)weapon,0,-4); //new Vector3(-1.394853f+(float)weapon,-0.3478584f,-4);
		} else {
			checkAction();
		}
	}

	void FixedUpdate () {

		if (RoomManager.Instance.Pause) return;


		//Player basic control
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.position += new Vector3(0,speed,0);
			movementDirection = MOVEMENTDIRECTION.UP;
		} else
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.position += new Vector3(0,-speed,0);
			movementDirection = MOVEMENTDIRECTION.DOWN;
		} else
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += new Vector3(-speed,0,0);
			movementDirection = MOVEMENTDIRECTION.LEFT;
		} else
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += new Vector3(speed,0,0);
			movementDirection = MOVEMENTDIRECTION.RIGHT;
		} else 
			movementDirection = MOVEMENTDIRECTION.NONE;

		if (movementDirection!=MOVEMENTDIRECTION.NONE) lookingTo = movementDirection;



	

	}


	//Use weapon
	void checkAction() {
		if (Input.GetKeyUp (KeyCode.A)) {

			GameObject b;

			AudioSource.PlayClipAtPoint(SFXSword,Camera.main.transform.position);

			switch(weapon) {
			case WEAPON.BOOMERANG: b = Instantiate(boomerang,transform.position,transform.rotation) as GameObject;
				break;
			case WEAPON.SWORD: b = Instantiate(sword,transform.position,transform.rotation) as GameObject;
				break;
			default:
				b = Instantiate(boomerang,transform.position,transform.rotation) as GameObject;
				break;
			}
			
			FixedMove fm = b.GetComponent("FixedMove") as FixedMove;
			fm.movementDirection = lookingTo;
			
			switch(lookingTo) {
			case MOVEMENTDIRECTION.UP: 								
				b.transform.position+=new Vector3(0,0.2f,0);			
				break;
			case MOVEMENTDIRECTION.DOWN: 
				b.transform.position+=new Vector3(0.3f,-0.7f,0);
				b.transform.Rotate(0,0,180);
				break;
			case MOVEMENTDIRECTION.LEFT: 
				b.transform.position+=new Vector3(-0.2f,-0.4f,0);
				b.transform.Rotate(0,0,90);
				break;
			case MOVEMENTDIRECTION.RIGHT: 
				b.transform.position+=new Vector3(0.4f,-0.1f,0);
				break;
			}
			
			
		}
	}


}
