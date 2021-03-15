using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class time : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject textCounter;
    public int period = 10;
    public bool isRunning =false;
    public Text p1text;
    public Text p2text;
    public Text wintext;
    public Text righttext;
    public Text lefttext;
    //public GameObject gameManager;
    private PhotonView pw;
    private GameObject player1;
    private GameObject player2;
    public Text finish;
    public GameObject yenidenoynabutp1;
    public GameObject yenidenoynabutp2;
    public Text p1ptext;
    public Text p2ptext;
    private void Start()
    {
        pw = GetComponent<PhotonView>();
        p1text.enabled = false;
        p2text.enabled = false;
        wintext.enabled = false;
        finish.enabled = false;
       // yenidenoynabut = GameObject.FindGameObjectWithTag("Button");
        yenidenoynabutp1.SetActive(false);
        yenidenoynabutp2.SetActive(false);
    }
    private void Update()
    {
        if (!gameObject.GetComponent<yonet>().battle_check)
        {
            if (GameObject.Find("player1") != null && GameObject.Find("player2") != null)
            {
                if (GameObject.Find("player1").GetComponent<Player>().yenidenoyna && GameObject.Find("player2").GetComponent<Player>().yenidenoyna)
                {
                    Debug.Log("Yeniden BASLAATT");
                    gameObject.GetComponent<time>().Yeniden();
                    gameObject.GetComponent<yonet>().battle_check = true;
                    GameObject.Find("player1").GetComponent<Player>().yenidenoyna = false;
                    GameObject.Find("player2").GetComponent<Player>().yenidenoyna = false;
                }
            }
            
        }
       
    }

    // [PunRPC]
    public void StartTimer()
    {
       StartCoroutine(Timer());
       this.player1 = GameObject.Find("player1");
       this.player2 = GameObject.Find("player2");
    }
    IEnumerator Timer()
    {
        period = 10;
        Debug.Log("Started");
        isRunning = true;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1);
            period -= 1;
           
                textCounter.GetComponent<Text>().text = ""+period;
           
        }
        isRunning = false;
        //pw.RPC("TakeSkillFromPlayers", RpcTarget.All);
        //pw.RPC("WhoIsWinner", RpcTarget.All);
        TakeSkillFromPlayers();
        WhoIsWinner();
    }
   // [PunRPC]
    void TakeSkillFromPlayers()
    {
        p1text.enabled = true;
        p2text.enabled = true;
        p1text.text = "Player1: " + player1.GetComponent<Player>().skill;
        p2text.text = "Player2: " + player2.GetComponent<Player>().skill;
    }
   // [PunRPC]
    void WhoIsWinner()
    {
        wintext.enabled = true;
       if(player1.GetComponent<Player>().skill == "Fire" && player2.GetComponent<Player>().skill == "Fire")
        {
            wintext.text = "Berabere";

        }
       else if (player1.GetComponent<Player>().skill == "Fire" && player2.GetComponent<Player>().skill == "Su")
        {
            wintext.text = "Kazanan:" + "Player2";
            player2.GetComponent<Player>().Attack();
            player2.GetComponent<Player>().puan2 += 1f;
            StartCoroutine(AttackCouldown(player1));
            
            lefttext.GetComponent<Text>().text = player2.GetComponent<Player>().puan2.ToString();

        }
        else if (player1.GetComponent<Player>().skill == "Fire" && player2.GetComponent<Player>().skill == "Ice")
        {
            wintext.text = "Kazanan:" + "Player1";
            player1.GetComponent<Player>().Attack();
            player1.GetComponent<Player>().puan1 += 1f;
            StartCoroutine(AttackCouldown(player2));
           // player2.GetComponent<Player>().TakeDamage();
            righttext.GetComponent<Text>().text = player1.GetComponent<Player>().puan1.ToString();
        }
        else if (player1.GetComponent<Player>().skill == "Su" && player2.GetComponent<Player>().skill == "Fire")
        {
            wintext.text = "Kazanan:" + "Player1";
            player1.GetComponent<Player>().Attack();
            player1.GetComponent<Player>().puan1 += 1f;
            StartCoroutine(AttackCouldown(player2));
           // player2.GetComponent<Player>().TakeDamage();
            righttext.GetComponent<Text>().text = player1.GetComponent<Player>().puan1.ToString();
        }
        else if (player1.GetComponent<Player>().skill == "Su" && player2.GetComponent<Player>().skill == "Su")
        {
            wintext.text = "Berabere";

        }
        else if (player1.GetComponent<Player>().skill == "Su" && player2.GetComponent<Player>().skill == "Ice")
        {
            wintext.text = "Kazanan:" + "Player2";
            player2.GetComponent<Player>().Attack();
            player2.GetComponent<Player>().puan2 += 1f;
            StartCoroutine(AttackCouldown(player1));
            //player1.GetComponent<Player>().TakeDamage();
            lefttext.GetComponent<Text>().text = player2.GetComponent<Player>().puan2.ToString();
        }
        else if (player1.GetComponent<Player>().skill == "Ice" && player2.GetComponent<Player>().skill == "Fire")
        {
            wintext.text = "Kazanan:" + "Player2";
            player2.GetComponent<Player>().Attack();
            player2.GetComponent<Player>().puan2 += 1f;
            StartCoroutine(AttackCouldown(player2));
            //player1.GetComponent<Player>().TakeDamage();
            lefttext.GetComponent<Text>().text = player2.GetComponent<Player>().puan2.ToString();
        }
        else if (player1.GetComponent<Player>().skill == "Ice" && player2.GetComponent<Player>().skill == "Su")
        {
            wintext.text = "Kazanan:" + "Player1";
            player1.GetComponent<Player>().Attack();
            player1.GetComponent<Player>().puan1 += 1f;
            StartCoroutine(AttackCouldown(player2));
            //player2.GetComponent<Player>().TakeDamage();
            righttext.GetComponent<Text>().text = player1.GetComponent<Player>().puan1.ToString();
        }
        else if (player1.GetComponent<Player>().skill == "Ice" && player2.GetComponent<Player>().skill == "Ice")
        {
            wintext.text = "Berabere";
        }
        if(player1.GetComponent<Player>().puan1 <3 && player2.GetComponent<Player>().puan2 <3)
        {
            StartCoroutine(CoulDownFinish());
        }
        else
        {
            StopGame();
        }

    }
        IEnumerator AttackCouldown(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Player>().TakeDamage();

    }
        IEnumerator CoulDownFinish()
    {
        yield return new WaitForSeconds(1f);
        wintext.enabled = false;
        p1text.enabled = false;
        p2text.enabled = false;

        gameObject.GetComponent<yonet>().battle_check = true;
        period = 10;

    }
    void StopGame()
    {
        finish.enabled = true;
        //yenidenoynabutp1.SetActive(true);
        player1.GetComponent<Player>().yenidenoynabut = true;
        player2.GetComponent<Player>().yenidenoynabut = true;
        Debug.Log("StopGame33");
        //finish.text = "Oyun BİTTİ";
    }
   public void Yeniden()
    {
        player1.GetComponent<Animator>().SetBool("Die", false);
        player2.GetComponent<Animator>().SetBool("Die", false);
        pw.RPC("Resetle", RpcTarget.All);
        player1.GetComponent<Player>().skill = null;
        player2.GetComponent<Player>().skill = null;
        p1text.enabled = false;
        p2text.enabled = false;
        wintext.enabled = false;
        player1.GetComponent<Player>().yenidenoynabut = false;
        player2.GetComponent<Player>().yenidenoynabut = false;
        player1.GetComponent<Player>().yenidenoyna = false;
        player2.GetComponent<Player>().yenidenoyna = false;
        yenidenoynabutp1.SetActive(false);
        yenidenoynabutp2.SetActive(false);
        finish.enabled = false;
        //yenidenoynabutp1.SetActive(true);
    }
    [PunRPC]
    void Resetle()
    {
        player1.GetComponent<Player>().puan1 = 0;
        player2.GetComponent<Player>().puan2 = 0;
        player1.GetComponent<Player>().puan2 = 0;
        player2.GetComponent<Player>().puan2 = 0;
        p1ptext.text = "0";
        p2ptext.text = "0";
    }



}
