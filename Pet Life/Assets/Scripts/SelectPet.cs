using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectPet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager gameManager;
    public GameObject inputField;
    public GameObject errorText;
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
        string nameText = inputField.GetComponent<TMP_InputField>().text;
        if (nameText.Trim().Length != 0) {
            errorText.SetActive(false);
        }
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
        string nameText = inputField.GetComponent<TMP_InputField>().text;
        Debug.Log(nameText);
        // Check if name was provided
        if (!string.IsNullOrEmpty(nameText.Trim())) {
            // Update player's pet name
            GameManager.petName = nameText;

            // Set Pet GameObject in GameManager
            GameManager.playerPet = pet;

            // Change scene to gameplay
            SceneManager.LoadScene("Living Room Scene");
        } else {
            errorText.SetActive(true);
        }
    }
}
