using UnityEngine;
using System.Collections;

/*
Move the gameobject non-stop in one direction
 */

public class FixedMove : MonoBehaviour {

	public MOVEMENTDIRECTION movementDirection;
	public float speed = 5f;
	public bool rotating = true;


	void Start () {
		if (rotating) {
			rigidbody2D.AddTorque(5f);
		}

	}

	void FixedUpdate () {
		if (RoomManager.Instance.Pause) return;

		switch(movementDirection) {
			case MOVEMENTDIRECTION.UP: 								
				gameObject.rigidbody2D.AddForce(Vector3.up * speed);				
				break;
			case MOVEMENTDIRECTION.DOWN: 
				gameObject.rigidbody2D.AddForce(Vector3.down * speed);			                             
				break;
			case MOVEMENTDIRECTION.LEFT: 
				gameObject.rigidbody2D.AddForce(Vector3.left * speed);			                             
				break;
			case MOVEMENTDIRECTION.RIGHT: 
				gameObject.rigidbody2D.AddForce(Vector3.right * speed);		
				break;
			}
	}


	
}
