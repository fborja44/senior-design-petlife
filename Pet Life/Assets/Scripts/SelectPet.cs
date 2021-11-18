using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectPet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager gameManager;
    public GameObject pet;
    public int petIndex;
    public Image selection;
    public Sprite disc;
    public Sprite disc_alt;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize vars
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Highlight selection
        selection.GetComponent<Image>().sprite = disc_alt;
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Remove highlight
        selection.GetComponent<Image>().sprite = disc;
    }

    public void OnMouseDown() {
        // Update the player's pet sprite
        Debug.Log("Clicked " + petIndex);

        // Set Pet GameObject in GameManager
        // TODO

        // Change scene to gameplay
        // TODO
    }
}
