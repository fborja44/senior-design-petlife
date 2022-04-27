using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class OutsidePopUps : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public TextMeshProUGUI popupGUI;
    public DogNeedsUpdate dogNeedsUpdate;
    
    private void Awake()
    {
        Random rnd = new Random();
        int r = rnd.Next(10);
        if (r == 1 ) // Chance that event pops up
        {
            PopUp(GameManager.petName + " jumped in some mud! You should give them a bath! Lost some Hygiene.");
            float hygieneLost = (float)(DogNeedsUpdate.max * 0.4);
            dogNeedsUpdate.LoseHygiene(hygieneLost);
        }
        if (r == 2)
        {
            PopUp(GameManager.petName + " met a friendly dog outside and played with them! Gained some Love. Lost some Energy.");
            float loveGained = (float)(DogNeedsUpdate.max/2 * 0.2);
            float energyLost = (float)(DogNeedsUpdate.max * 0.2);
            dogNeedsUpdate.GainLove(loveGained);
            dogNeedsUpdate.LoseEnergy(energyLost);
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
