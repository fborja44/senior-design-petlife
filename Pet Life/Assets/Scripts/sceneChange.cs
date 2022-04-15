using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Changes scene to title screen
    public void ToTitle() {
        if (GameManager.busy) return; // busy doing another action
        SceneManager.LoadScene("Title Scene");
    }

    public void startMainGame()
    {
        if (GameManager.busy) return; // busy doing another action
        SceneManager.LoadScene("Difficulty Scene");
    }
    public void startTutorial()
    {
        if (GameManager.busy) return; // busy doing another action
        SceneManager.LoadScene("Tutorial Scene");
    }
    public void toOutside()
    {
        if (GameManager.busy) return; // busy doing another action
        SceneManager.LoadScene("Outside Scene");
    }

    public void toBathroom()
    {
        if (GameManager.busy) return; // busy doing another action
        SceneManager.LoadScene("Bathroom");
    }

    public void toLivingRoom()
    {
        if (GameManager.busy) return; // busy doing another action
        SceneManager.LoadScene("Living Room Scene");
    }

    public void toKitchen()
    {
        if (GameManager.busy) return; // busy doing another action
        SceneManager.LoadScene("Kitchen Scene");
    }
}
