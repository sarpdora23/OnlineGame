using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using DG.Tweening;
public class Player : MonoBehaviourPunCallbacks
{
    public string skill;
    private PhotonView pw;
    public Animator animator;
    private GameObject gameManager;
    public float puan1 = 0;
    public float puan2 = 0;
    public GameObject spell2;
    public bool yenidenoyna = false;
    public bool yenidenoynabut = false;

    private void Start()
    {
        pw = gameObject.GetComponent<PhotonView>();
        pw.RPC("MakeDefPlay", RpcTarget.All);
        gameManager = GameObject.Find("GameObject");
        yenidenoyna = false;
    }
    [PunRPC]
    void MakeDefPlay()
    {
        Debug.Log("calisti3");
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            Debug.Log("op");
            if (go.GetComponent<PhotonView>().Controller == PhotonNetwork.MasterClient || go.GetComponent<PhotonView>().Controller == PhotonNetwork.PlayerList[0])
            {
                Debug.Log("calisti4");
                go.name = "player1";
            }
            else if (go.GetComponent<PhotonView>().Controller == PhotonNetwork.PlayerList[1])
            {
                Debug.Log("calisti5");
                go.name = "player2";
            }
        }
    }
    [PunRPC]
    void GetSkillP1(string spell)
    {
         GameObject.Find("player1").GetComponent<Player>().skill = spell;
         Debug.Log("Player1:" + " " + skill);
    }
    [PunRPC]
    void GetSkillP2(string spell)
    {
        GameObject.Find("player2").GetComponent<Player>().skill = spell;
        Debug.Log("Player2:" + " " + skill);
    }
    private void Update()
    {
        if (puan1 == 3)
        {
            // pw.RPC("Die", RpcTarget.All);
            //GameObject.Find("player2").GetComponent<PhotonView>().RPC("Die", RpcTarget.All);
            GameObject.Find("player2").GetComponent<Player>().Die();
        }
        if (puan2 == 3)
        {
            // pw.RPC("Die", RpcTarget.All);
            // GameObject.Find("player1").GetComponent<PhotonView>().RPC("Die", RpcTarget.All);
            GameObject.Find("player1").GetComponent<Player>().Die();
        }
        if (pw.IsMine)
        {
            if (gameManager.GetComponent<time>().isRunning)
            {
                if (pw.Controller == PhotonNetwork.MasterClient)
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        pw.RPC("GetSkillP1", RpcTarget.All, "Fire");
                    }
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        pw.RPC("GetSkillP1", RpcTarget.All, "Su");
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pw.RPC("GetSkillP1", RpcTarget.All, "Ice");
                    }
                   
                }
                else if (pw.Controller == PhotonNetwork.PlayerList[1])
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        pw.RPC("GetSkillP2", RpcTarget.All, "Fire");
                    }
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        pw.RPC("GetSkillP2", RpcTarget.All, "Su");
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pw.RPC("GetSkillP2", RpcTarget.All, "Ice");
                    }
                   

                }
            }
        }
        if (pw.Controller == PhotonNetwork.MasterClient)
        {
            if (yenidenoynabut)
            {
                Debug.Log("1");
                gameManager.GetComponent<time>().yenidenoynabutp1.SetActive(true);
                gameManager.GetComponent<time>().yenidenoynabutp2.SetActive(false);
                yenidenoynabut = false;
            }
        }
        else if (pw.Controller == PhotonNetwork.PlayerList[1])
        {
            if (yenidenoynabut)
            {
                Debug.Log("2");
                gameManager.GetComponent<time>().yenidenoynabutp1.SetActive(false);
                gameManager.GetComponent<time>().yenidenoynabutp2.SetActive(true);
                yenidenoynabut = false;
            }
        }
    }
    
   // [PunRPC]
    public void Attack()
    {
        Debug.Log("Attack");
        animator.SetBool("Attack", true);
        if(gameObject.name == "player1")
        {
          GameObject spell =  Instantiate(spell2,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 2,gameObject.transform.position.z + 0.4f),Quaternion.identity);
          spell.transform.tag = "Spell";
          spell.transform.DOMoveZ(GameObject.Find("player2").transform.position.z, 0.6f);
          Destroy(spell, 0.6f);
        }
        else
        {
            GameObject spell = PhotonNetwork.Instantiate("Spell", new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z + 0.4f), Quaternion.identity);
            spell.transform.tag = "Spell";
            spell.transform.DOMoveZ(GameObject.Find("player1").transform.position.z, 0.6f);
            Destroy(spell, 0.6f);
        }

        StartCoroutine(CoulDown());
    }
    [PunRPC]
    void Idle()
    {

    }
    //[PunRPC]
   public void TakeDamage()
    {
        Debug.Log("TakeDamage");
        animator.SetBool("TakeDamage", true);
        StartCoroutine(CoulDown());
    }
   // [PunRPC]
    void Die()
    {
      //  Debug.Log("Die");
       // animator.SetBool("Die", true);
        StartCoroutine(DeathCoulDown());
       
    }
    IEnumerator CoulDown()
    {
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("Attack", false);
        animator.SetBool("TakeDamage", false);
    }
    IEnumerator DeathCoulDown()
    {
        yield return new WaitForSeconds(1f);
        //gameObject.SetActive(false);
        animator.SetBool("Attack", false);
        animator.SetBool("TakeDamage", false);
    }
    
}
