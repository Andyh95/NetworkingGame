using UnityEngine;
using System.Collections;

public class NetManager : MonoBehaviour {
    private const string roomName = "SuperUniqueRoomName";
    private TypedLobby lobbyName = new TypedLobby("SuperUniqueNew_Lobby", LobbyType.Default);
    private RoomInfo[] roomsList;
    public GameObject player;
    //public GameObject ball;
	// Use this for initialization
	void Start () {
        PhotonNetwork.ConnectUsingSettings("v4.2");
	}
	
	// Update is called once per frame
	void Update () {

	
	}
    void OnGUI()
    {
        if (!PhotonNetwork.connected)
        {
            GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        }
        else if (PhotonNetwork.room == null)
        {
            if(GUI.Button(new Rect(100,100,250,100),"Start Server"))
            {
                PhotonNetwork.CreateRoom(roomName, new  RoomOptions() { maxPlayers = 4, isOpen = true, isVisible = true }, lobbyName);        
            }
            if(roomsList != null)
            {
                for(int i = 0; i < roomsList.Length; i++)
                {
					if(GUI.Button(new Rect(100,100,250,100),"Join " + roomsList[i].name))
                    {
                        PhotonNetwork.JoinRoom(roomsList[i].name);
                    }

                }

            }
        }

    }

    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(lobbyName);
    }
    void OnRecievedRoomListUpdate()
    {
        Debug.Log("Room was created");
        roomsList = PhotonNetwork.GetRoomList();
    }
    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }
    void OnJoinedRoom()
    {
        Debug.Log("Connected to Room");
        print(PhotonNetwork.playerList.Length);

        if (GUI.Button(new Rect(200, 200, 250, 100), "Player 1"))
        {
            PhotonNetwork.Instantiate(player.name, new Vector3(-4f, 1.5f, -2f), Quaternion.identity, 0);
 
        }
        else if (GUI.Button(new Rect(400,400,250,100), "Player 2"))
        {
            PhotonNetwork.Instantiate(player.name, new Vector3(4f, 1.5f, -2f), Quaternion.identity, 0);
        }
        /*
        if (PhotonNetwork.playerList.Length > 1)
        {
            PhotonNetwork.Instantiate(player.name, new Vector3(-4f, 1.5f, -2f), Quaternion.identity, 0);
           // PhotonNetwork.Instantiate(ball.name, new Vector3(-4f, 1.5f, -2f), Quaternion.identity, 0);

        }
        else
        {
            PhotonNetwork.Instantiate(player.name, new Vector3(4f, 1.5f, -2f), Quaternion.identity, 0);
        }
        */
    }
}
