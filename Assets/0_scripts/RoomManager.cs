using UnityEngine;
using System.Collections;

/*
Manage the games rooms:
- Reads all the rooms at startup
- Build the room map of the inventory screen
- Hides all the room but the current one
*/
public class RoomManager   {
	int i_pos;
	int j_pos;

	int Max_i = 5;
	int Max_j = 4;

	GameObject[,] rooms;
	GameObject[,] maprooms;

	public GameObject Inventory;

	protected RoomManager() {}

	private static RoomManager _instance = null;	


	public bool Pause = false;

	public static RoomManager Instance { get {
			if (RoomManager._instance==null) RoomManager._instance=new RoomManager();
			return RoomManager._instance;
			/*return RoomManager._instance == null ? 
				new RoomManager() : RoomManager._instance;*/
		} 
	}


	public string GetRoomId() { return i_pos+"_"+j_pos;}

	public void SetRoomActive(int j, int i, bool active) { 
		if (i_pos<0 || i_pos>=Max_i) Debug.LogError("Impossible i "+i);
		if (j_pos<0 || j_pos>=Max_j) Debug.LogError("Impossible j "+j);

		if (rooms[j,i]!=null) 
			rooms[j,i].SetActive(active);
	}

	public void SetCurrentRoom(int j, int i) {

		//Deactivate the previous room and the "p" map mark
		GameObject maproom = maprooms[j_pos,i_pos]; 
		if (maproom!=null) 
			maproom.transform.FindChild("p").renderer.enabled = false;
		SetRoomActive(j_pos,i_pos,false);
		i_pos = i; j_pos = j; 
		//Activate the new room and also set as active in the map
		SetRoomActive(j,i,true); 
		maproom = maprooms[j,i];
		maproom.SetActive(true);
		maproom.transform.FindChild("p").renderer.enabled = true;

	}

	public void Translate(int j,int i) {
		SetCurrentRoom(j_pos+j,i_pos+i);
	}

	//Check if we can go to some direction from a room, in order to build the map
	public bool CanGo(MOVEMENTDIRECTION dir) {
		return CanGo(dir,j_pos,i_pos);
	}

	public bool CanGo(MOVEMENTDIRECTION dir,int j,int i) {

		switch(dir) {
		case MOVEMENTDIRECTION.UP: 								
			return rooms[j,i].transform.FindChild("door_top")!=null;
		case MOVEMENTDIRECTION.DOWN: 
			return rooms[j,i].transform.FindChild("door_bottom")!=null;
		case MOVEMENTDIRECTION.LEFT: 
			return rooms[j,i].transform.FindChild("door_left")!=null;
		case MOVEMENTDIRECTION.RIGHT: 
			return rooms[j,i].transform.FindChild("door_right")!=null;		
		}
		Debug.LogError("Impossible direction");
		return false;
	}


	//Build the map, set all the rooms as not visited
	public void Setup() {

		rooms = new GameObject[Max_j,Max_i];
		maprooms = new GameObject[Max_j,Max_i];

		//Activate the inventory, so we can find the maprooms with the FindChild function
		Inventory.SetActive(true);

		for (int j = 0;j<Max_j;j++) {
			for (int i = 0;i<Max_i;i++) {
				GameObject room = GameObject.Find("Room_"+j+"_"+i);
				rooms[j,i] = room;
				GameObject maproom = GameObject.Find ("maproom_"+j+"_"+i);
				maprooms[j,i] = maproom;
				if (room!=null) {
					if (!CanGo(MOVEMENTDIRECTION.UP, j,i)) maproom.transform.FindChild("top").gameObject.SetActive(false);
					if (!CanGo(MOVEMENTDIRECTION.DOWN, j,i)) maproom.transform.FindChild("bottom").gameObject.SetActive(false);
					if (!CanGo(MOVEMENTDIRECTION.LEFT, j,i)) maproom.transform.FindChild("left").gameObject.SetActive(false);
					if (!CanGo(MOVEMENTDIRECTION.RIGHT, j,i)) maproom.transform.FindChild("right").gameObject.SetActive(false);
					room.SetActive(false);
					maproom.transform.FindChild("p").renderer.enabled = false;
					maproom.SetActive(false);
				} else {
					maproom.SetActive(false);
				}
			}
		}
		Inventory.SetActive(false);
	}


}
