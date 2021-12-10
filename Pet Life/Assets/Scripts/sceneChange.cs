using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    public void startMainGame()
    {
        SceneManager.LoadScene("Difficulty Scene");
    }
    public void startTutorial()
    {
        SceneManager.LoadScene("Tutorial Scene");
    }
}
