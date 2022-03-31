using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class EventPops : MonoBehaviour
{

    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public TextMeshProUGUI popupGUI;
    
    private void Awake()
    {
        Random rnd = new Random();
        int r = rnd.Next(10);
        if (r == 1) // Chance that event pops up
        {
            PopUp("hello");
        }
    }

    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        Debug.Log("box active");
        popUpText.text = text;
        popupGUI.text = text;
        Debug.Log("Text changed");

        animator.SetTrigger("pop");
        Debug.Log("box popped");
    }
}
    //public Image fill;

   /* void Start()
    {
        eventthing.SetActive(false);
        //StartCoroutine(ExampleCoroutine());
    }

*//*    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        eventthing.SetActive(false);
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }*//*

    void Update()
    {
        eventthing.SetActive(true);
        eventthing.SetActive(false);
    }

    // Dog played around in mud -> lower hygeine
    // Dog met stranger on walk outside -> increase love
    // Dog peed in house -> increase bladder, decrease hygeine
    // Dog ran around chasing bug, decrease energy, decrease thirst
}*/
