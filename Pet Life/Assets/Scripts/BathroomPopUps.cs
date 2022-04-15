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
    
    private void Awake()
    {
        Random rnd = new Random();
        int r = rnd.Next(10);
        if (r < 2) // Chance that event pops up
        {
            PopUp("Your pet splashed you with water!");
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
