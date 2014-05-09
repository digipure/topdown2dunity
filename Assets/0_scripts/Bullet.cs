using UnityEngine;
using System.Collections;

/*
Attach to a gameobject (enemy or weapon attack) that deals damage (reduce life) to other gameobjects with different algiment
 */

public class Bullet : MonoBehaviour {

	//Damage
	public int power = 100;
	//Destroy if hit a wall
	public bool destroyOnHitWall=true;
	//Destroy automatically after some time
	public int timeToLive = -1;


	void FixedUpdate() {
		if (RoomManager.Instance.Pause) return;
		if (timeToLive!=-1) {
			if (timeToLive==0) Destroy(gameObject);
			timeToLive--;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		DoCollision(other.gameObject);
	}

	void OnCollisionEnter2D(Collision2D other) {
		DoCollision(other.gameObject);
	}

	void DoCollision(GameObject other) {

		if (RoomManager.Instance.Pause) return;

		Life otherLife = other.GetComponent("Life") as Life;

		//No life attached, so it's a wall
		if (otherLife==null) {
			if (destroyOnHitWall) Destroy(gameObject);
		} else {
			Life myLife = gameObject.GetComponent("Life") as Life;
			//If I am alive, and the other object is not a weapon attack and the other object 
			//aligment is different to mine does damage
			if (myLife.life>0 && otherLife.type!=TYPE.WEAPON &&	otherLife.aligment!=myLife.aligment)
				otherLife.Hit(power);
			
		}
	}
}
