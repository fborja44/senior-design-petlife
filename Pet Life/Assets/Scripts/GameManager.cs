using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject playerPet;     // Parent player pet object
    public static Transform playerObject;   // First child of player pet object
    public static float difficulty = 1f;    // Default difficulty
    public static string petName = "Dog";   // Default petName
    public GameObject defaultPet;
    public GameObject[] petPrefabs;
    public static Animator animator;        // Animator for pet
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Changes scene to title screen
    public void ToTitle() {
        SceneManager.LoadScene("Title Scene");
    }

    // Instantiates Pet if on Gameplay screen
    void SpawnPet() {
        Scene scene = SceneManager.GetActiveScene();
        Vector3 spawnPos = new Vector3(0, -3.8f, 0);

        if (scene.name == "Living Room Scene" || scene.name == "Bathroom" || scene.name == "Outside Scene" || scene.name == "Kitchen Scene") {
            if (playerPet != null) {
                playerPet = Instantiate(playerPet, spawnPos, playerPet.transform.rotation);
            } else {
                playerPet = Instantiate(defaultPet, spawnPos, defaultPet.transform.rotation);
            }
            playerPet.name = "Player Pet";
            playerObject = playerPet.transform.GetChild(0);
            animator = playerObject.GetComponent<Animator>();
        }
    }
}
