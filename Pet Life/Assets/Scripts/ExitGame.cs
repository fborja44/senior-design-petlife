using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    private GameObject dialogue;
    private DogNeedsUpdate dogNeedsUpdate;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GameObject.Find("Confirmation Dialogue");
        dialogue.SetActive(false);
        dogNeedsUpdate = GameObject.Find("DogNeeds").GetComponent<DogNeedsUpdate>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ExitDialogue() {
        dialogue.SetActive(true);
        Time.timeScale = 0;
    }

    public void ConfirmExit() {
        // Reset all Needs
        dogNeedsUpdate.ResetNeeds();

        Time.timeScale = 1;
        SceneManager.LoadScene("Title Scene");
    }

    public void CancelExit() {
        Time.timeScale = 1;
        dialogue.SetActive(false);
    }
}