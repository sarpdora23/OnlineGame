using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class yonet : MonoBehaviourPunCallbacks
{
    private PhotonView pw;
    public bool battle_check;
    public GameObject player1;
    public GameObject player2;
    public GameObject playerdef;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        battle_check = false;
        pw = gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(battle_check)
        {
            //pw.RPC("StartTimer", RpcTarget.AllViaServer);
            gameObject.GetComponent<time>().StartTimer();
            battle_check = false;
        }
       
    }
    //public override void OnConnected()
    //{
    //    base.OnConnected();
    //    Debug.Log("Sunucuya girdi");
    //    PhotonNetwork.JoinLobby();
    //}
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Sunucuya girdi");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Lobiye girildi");
        PhotonNetwork.JoinOrCreateRoom("oda",new RoomOptions(){MaxPlayers = 4}, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Odaya girildi");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("1çalıştı");
            player1 = PhotonNetwork.Instantiate("Player", new Vector3(23.92f, 0.8f, 27.44f), Quaternion.identity, 0, null);
            //pw.RPC("MakeP1", RpcTarget.All);
            
        }
        else
        {
            Debug.Log("2çalıştı");
            player2 = PhotonNetwork.Instantiate("Player", new Vector3(23.92f, 0.8f, 34.59f), Quaternion.Euler(0,180,0), 0, null);
            pw.RPC("MakeP2", RpcTarget.All);
            
        }

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Oda oluşturulurken hata oldu");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Odaya girerken bir hata oluştu");
    }
    private void OnConnectedToServer()
    {
        Debug.Log("sunucuya girdi");
       // PhotonNetwork.JoinLobby();
    }
    [PunRPC]
    void MakeP1()
    {
        // player1 = PhotonNetwork.Instantiate("Player", new Vector3(23.92f, 0.8f, 27.44f), Quaternion.identity,0,null);
        //player1 = Instantiate(playerdef, new Vector3(23.92f, 0.8f, 27.44f), Quaternion.identity);
        player1.GetComponent<PhotonView>().Owner.NickName = "Player1";
        player1.GetComponent<PhotonView>().name = "player1";
        player1.GetComponent<Transform>().tag = "Player";
        
        
    }
    [PunRPC]
    void MakeP2()
    {
        Debug.Log("Deneme49");
       // player2 = PhotonNetwork.Instantiate("Player", new Vector3(23.92f, 0.8f, 34.59f), Quaternion.identity,0,null);
        //player2.GetComponent<PhotonView>().Owner.NickName = "Player2";
        //player2.GetComponent<PhotonView>().name = "player2";
        //player2.GetComponent<Transform>().tag = "Player";
        battle_check = true;
        
    }

}
