using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    private Button button;
    private TutorialManager tutorialManager; // Tutorial Manager Script component
    public bool next; // true = next; false = prev

    // Start is called before the first frame update
    void Start()
    {
        // Initialize button and onClick event listener
        button = GetComponent<Button>();
        button.onClick.AddListener(ScrollPage);

        tutorialManager = GameObject.Find("Game Manager").GetComponent<TutorialManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ScrollPage() {
        int page = tutorialManager.page;
        // Debug.Log("Button clicked");
        if (next) {
            // Go to next page
            tutorialManager.ChangePage(page + 1);
        } else {
            // Go back a page
            if (page > 1) {
                tutorialManager.ChangePage(page - 1);
            }
        }
    }
}
