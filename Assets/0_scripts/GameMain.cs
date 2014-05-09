using UnityEngine;
using System.Collections;

/*
Start up the game and hold some misc data
*/

public class GameMain : MonoBehaviour {

	public int startRoomJ,startRoomI; 
	public GameObject Inventory;

	public AudioClip SFXDoor;
	public AudioClip SFXDestroy;
	public AudioClip SFXGUI;
	public AudioClip SFXLife;

	public Sprite SpriteDestroy;

	// Use this for initialization
	void Start () {
		RoomManager.Instance.Inventory = Inventory;
		RoomManager.Instance.Setup();
		RoomManager.Instance.SetCurrentRoom(startRoomJ,startRoomI);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
