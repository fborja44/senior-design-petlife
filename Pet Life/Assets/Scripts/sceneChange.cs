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
    public void toOutside()
    {
        SceneManager.LoadScene("Outside Scene");
    }

    public void toBathroom()
    {
        SceneManager.LoadScene("Bathroom");
    }

    public void toLivingRoom()
    {
        SceneManager.LoadScene("Living Room Scene");
    }

    public void toKitchen()
    {
        SceneManager.LoadScene("Kitchen Scene");
    }
}
