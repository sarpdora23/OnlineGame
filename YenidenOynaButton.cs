using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class YenidenOynaButton : MonoBehaviourPunCallbacks
{
    private Button button;
    public GameObject gamemanager;
    private PhotonView pw;
    // Start is called before the first frame update
    void Start()
    {
        //  gameObject.SetActive(false);
        gamemanager = GameObject.Find("GameObject");
        pw = gameObject.GetComponent<PhotonView>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void TaskOnClick()
    {
        pw.RPC("YenidenOyna", RpcTarget.All);
       
    }
    [PunRPC]
    void YenidenOyna()
    {
        if (gameObject.name == "YenidenOynap1")
        {
            Debug.Log("Yenidenoynap1");
            GameObject.Find("player1").GetComponent<Player>().yenidenoyna = true;
            gameObject.SetActive(false);
        }
        else if(gameObject.name == "YenidenOynap2")
        {
            Debug.Log("Yenidenoynap2");
            GameObject.Find("player2").GetComponent<Player>().yenidenoyna = true;
            gameObject.SetActive(false);
        }
    }
}
