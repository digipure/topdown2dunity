using UnityEngine;
using System.Collections;

public enum ALIGMENT {PLAYER,ENEMY};
public enum TYPE {HERO,ENEMY_BAT,BLOCK,WEAPON}

/*
 Life control for the player, attacks, enemies and breakable blocks
*/

public class Life : MonoBehaviour {

	public TYPE type;
	public ALIGMENT aligment;
	public int life = 100;
	public int maxLife = 300;

	public GameObject createWhenDestroyed=null;

	public AudioClip SFXDestroy;

	
	public void IncrementLife(int qty) {
		life+=qty;
		if (life>maxLife) life = maxLife;
	}

	public void Hit(int power) {
		life -= power;
		if (life<=0) {
			//Player will not die for now
			if (name!="hero") {
				//Play destroy sound
				GameMain gm = Camera.main.GetComponent("GameMain") as GameMain;
				if (SFXDestroy!=null) {
					AudioSource.PlayClipAtPoint(SFXDestroy,Camera.main.transform.position);
				} else {
					AudioSource.PlayClipAtPoint(gm.SFXDestroy,Camera.main.transform.position);
				}
				//Change sprite to explosion
				SpriteRenderer sr =  gameObject.GetComponent("SpriteRenderer") as SpriteRenderer;
				sr.sprite = gm.SpriteDestroy;
				//And destroy after some time
				Destroy(gameObject,0.3f);
				// If there is something to create, instantiate it
				if (createWhenDestroyed) {
					Instantiate(createWhenDestroyed,gameObject.transform.position,gameObject.transform.rotation);
				}
			}
		} 
	}
}
