using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // library for scene management/interaction

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    public float difficulty;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize button and add onclick event listener
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Sets the difficulty
    void SetDifficulty() {
        GameManager.difficulty = difficulty;
        SceneManager.LoadScene("Pet Selection Scene");
    }
}
