using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameObject playerPet;     // Parent player pet object
    public static Transform playerObject;   // First child of player pet object
    public static float difficulty = 1f;    // Default difficulty
    public static string petName = "Dog";   // Default petName
    public GameObject defaultPet;
    public GameObject[] petPrefabs;
    public static Animator animator;        // Animator for pet
    private static bool busy = false;        // Keeps track of whether an action is being performed
                                            // or game is paused
    private GameManager gameManager;
    private static int score = 0;           // Player score
    public static int highscore = 0;       // High score

    public Texture2D cursorDefault;
    public Texture2D cursorWait;
    public Texture2D cursorSelect;
    public Texture2D cursorDoor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Title Scene") {
            GameObject.Find("High Score").gameObject.GetComponent<TextMeshProUGUI>().text = "High Score: " + highscore;
        }
        SpawnPet();

        // Set cursor
        Cursor.SetCursor(cursorDefault, hotSpot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        try {
            GameObject scoreText = GameObject.Find("Score");
            scoreText.transform.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
        } catch { /* Do nothing */ }
    }

    public static bool ToggleBusy() {
        busy = !busy;
        return busy;
    }

    public static bool isBusy() {
        return busy;
    }

    // Instantiates Pet if on Gameplay screen
    void SpawnPet() {
        Scene scene = SceneManager.GetActiveScene();
        Vector3 spawnPos = new Vector3(0, -3.8f, 0);

        if (scene.name == "Living Room Scene" || scene.name == "Bathroom" || scene.name == "Outside Scene" || scene.name == "Kitchen Scene") {
            if (!GameObject.Find("Player Pet")) {
                if (playerPet != null) {
                    playerPet = Instantiate(playerPet, spawnPos, playerPet.transform.rotation);
                } else {
                    playerPet = Instantiate(defaultPet, spawnPos, defaultPet.transform.rotation);
                }
            }
            DontDestroyOnLoad(playerPet);
            playerPet.name = "Player Pet";
            playerObject = playerPet.transform.GetChild(0);
            animator = playerObject.GetComponent<Animator>();
        } else {
            if (GameObject.Find("Player Pet")) {
                Destroy(GameObject.Find("Player Pet"));
            }
        }
    }

    // Returns the current game score
    public static int getScore() {
        return score;
    }

    // Resets the score to 0. Returns the value of the score before the reset.
    public static int resetScore() {
        int prevScore = score;
        score = 0;
        return prevScore;
    }

    // Increments the score by value
    public static void incrementScore(int value) {
        score += value;
    }

    // Decrements score by value. Lowest valid score value is 0.
    public static void decrementScore(int value) {
        score = Mathf.Max(0, score - value);
    }
}
