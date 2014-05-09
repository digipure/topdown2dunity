using UnityEngine;
using System.Collections;

/*
Top bar GUI
 */
public class HeroGui : MonoBehaviour {

	Life myLife;
	PlayerMove pm;
	GameObject h1,h2,h3;
	GameObject[] weaponsgui;


	
	void Start () {
		pm = gameObject.GetComponent("PlayerMove") as PlayerMove;
		myLife = gameObject.GetComponent("Life") as Life;
		h1 = GameObject.Find("hearth100");
		h2 = GameObject.Find("hearth200");
		h3 = GameObject.Find("hearth300");

		//Weapon top indicator initizalization
		weaponsgui = new GameObject[(int)WEAPON.LASTWEAPON];
		for (int i=0;i<(int)WEAPON.LASTWEAPON;i++) {
			weaponsgui[i] = GameObject.Find("weapongui_"+i);
			if ((int)pm.weapon!=i) weaponsgui[i].SetActive(false); else weaponsgui[i].SetActive(true);
		}
	}

	void Update () {
		h1.SetActive( (myLife.life>100) );  
		h2.SetActive( (myLife.life>=200) );
		h3.SetActive( (myLife.life>=300) );

		//Updates the top selected weapon icon
		for (int i=0;i<(int)WEAPON.LASTWEAPON;i++)
			if ((int)pm.weapon!=i) weaponsgui[i].SetActive(false); else weaponsgui[i].SetActive(true);
	}


}
