using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public int page = 1;
    public TextMeshProUGUI pageText;
    public Button nextButton;
    public Button prevButton;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        // Hide previous button if on first page
    }

    // Function to change page elements
    public void ChangePage(int newPage) {
        page = newPage;
        pageText.text = "Page " + page;

        if (page <= 1) {
            prevButton.gameObject.SetActive(false);
        } else {
            prevButton.gameObject.SetActive(true);
        }
    }
}
