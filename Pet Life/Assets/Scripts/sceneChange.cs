using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    public void startMainGame()
    {
        SceneManager.LoadScene(1);
    }
    public void startTutorial()
    {
        SceneManager.LoadScene(2);
    }
}
