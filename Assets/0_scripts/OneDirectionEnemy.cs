using UnityEngine;
using System.Collections;

public enum ENEMYDIRECTION {UPDOWN,LEFTRIGHT};

/*
An enemy that goes and come back
*/

public class OneDirectionEnemy : MonoBehaviour {


	public ENEMYDIRECTION movementDirection;
	public float movementLength;
	public float speed=0.1f;

	Life myLife;


	float currentDirectionIncrement;
	float currentLength = 0;


	void Start () {
		currentDirectionIncrement = speed;
		myLife = gameObject.GetComponent("Life") as Life;
	}


	void Update () {

		//Stop movement if I am dead
		if (RoomManager.Instance.Pause || myLife.life<=0) return;

		if (movementDirection==ENEMYDIRECTION.LEFTRIGHT) {
			transform.position += new Vector3(currentDirectionIncrement,0,0);
		} else {
			transform.position += new Vector3(0,currentDirectionIncrement,0);
		}
	
		currentLength += Mathf.Abs(currentDirectionIncrement);

		if (currentLength>=movementLength) {
			currentLength = 0;
			currentDirectionIncrement *= -1;
		}
	}
}
