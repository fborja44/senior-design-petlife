using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;


public class PopUpSystem : MonoBehaviour
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
        if (r < 2) // Chance that event pops up
        {
            PopUp(GameManager.petName + " cuddled with you on the couch! Gained some Love.");
            float loveGained = (float)(DogNeedsUpdate.max/2 * 0.2);
            dogNeedsUpdate.GainLove(loveGained);
        }
        if (r == 2)
        {
            PopUp(GameManager.petName + " ate some of the food off your dinner plate! Gained some Hunger.");
            float hungerGained = (float)(DogNeedsUpdate.max * 0.2);
            dogNeedsUpdate.GainLove(hungerGained);
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
