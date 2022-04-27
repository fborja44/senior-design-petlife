using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class BathroomPopUps : MonoBehaviour
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
            PopUp(GameManager.petName + " splashed you with water! Gained some Love.");
            float loveGained = (float)(DogNeedsUpdate.max/2 * 0.2);
            dogNeedsUpdate.GainLove(loveGained);
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
