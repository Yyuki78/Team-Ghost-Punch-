using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoGameButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("GhostPunch");
    }
}
