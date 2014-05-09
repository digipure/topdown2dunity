using UnityEngine;
using System.Collections;

public enum ITEMTYPE { LIFE };

/*
Attach to pick up items
*/

public class Item : MonoBehaviour {

	public ITEMTYPE type;

	void playSound() {
		GameMain gm = Camera.main.GetComponent("GameMain") as GameMain;
		AudioSource.PlayClipAtPoint(gm.SFXLife,Camera.main.transform.position);
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name=="hero") {
			switch (type) {
				case ITEMTYPE.LIFE:
					Life life = other.gameObject.GetComponent("Life") as Life;
					life.IncrementLife(100);
					playSound();
					Destroy(gameObject);
				break;
			}

		}
	}




}
