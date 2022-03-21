using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoGamePlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider t)
    {
        if (t.CompareTag("Player"))
        {
            //ÉQÅ[ÉÄäJén
            Debug.Log("StartGame");
            SceneManager.LoadScene("GhostPunch");
            PlayerPrefs.SetInt("GameStart", 1);
            PlayerPrefs.Save();
        }
    }
}
