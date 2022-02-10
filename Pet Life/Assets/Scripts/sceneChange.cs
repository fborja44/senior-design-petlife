using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using Random = System.Random;

public class sceneChange : MonoBehaviour
{
    public string popUp;
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
        Random rnd = new Random();
        int r = rnd.Next(10);
        if (r == 1) // Chance that event pops up
        {
            EventPops pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EventPops>();
            pop.PopUp(popUp);
        }
    }

    public void toBathroom()
    {
        SceneManager.LoadScene("Bathroom");
        Random rnd = new Random();
        int r = rnd.Next(10);
        if (r == 1) // Chance that event pops up
        {
            EventPops pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EventPops>();
            pop.PopUp(popUp);
        }
    }

    public void toLivingRoom()
    {
        SceneManager.LoadScene("Living Room Scene");
        Random rnd = new Random();
        int r = rnd.Next(10);
        if (r < 10) // Chance that event pops up
        {
            Debug.Log("In loop");
            EventPops pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EventPops>();
            print(GameObject.FindGameObjectWithTag("GameManager").GetComponent<EventPops>());
            pop.PopUp(popUp);
        }
    }

    public void toKitchen()
    {
        SceneManager.LoadScene("Kitchen Scene");
        Random rnd = new Random();
        int r = rnd.Next(10);
        if (r < 10) // Chance that event pops up
        {
            EventPops pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EventPops>();
            pop.PopUp(popUp);
        }
    }
}
