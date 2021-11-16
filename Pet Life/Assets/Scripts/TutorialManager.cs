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
    public GameObject[] pages; // array of page game objects
    public Button nextButton;
    public Button prevButton;
    public Button titleButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to change page elements
    public void ChangePage(int newPage) {
        // Hide current page
        pages[page-1].gameObject.SetActive(false);

        // Change page text
        page = newPage;
        pageText.text = "Page " + page;

        // Show new page
        pages[page-1].gameObject.SetActive(true);

        // Show/hide page buttons
        if (page <= 1) {
            prevButton.gameObject.SetActive(false);
        } else {
            prevButton.gameObject.SetActive(true);
        }
        if (page == pages.Length) {
            nextButton.gameObject.SetActive(false);
        } else {
            nextButton.gameObject.SetActive(true);
        }
    }

    // Changes scene to title screen
    public void ToTitle() {
        SceneManager.LoadScene("Title Scene");
    }
}
