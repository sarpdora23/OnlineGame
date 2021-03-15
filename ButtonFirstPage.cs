using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFirstPage : MonoBehaviour
{
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it
        //Destroy(gameObject);
       // Debug.Log("Butona Baasıldı");
    }
    void TaskOnClick()
    {
        Debug.Log("Butona Baasıldı");
        SceneManager.LoadScene("GameLevel");
    }
}
