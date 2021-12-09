using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject playerPet;
    public GameObject defaultPet;
    public static string petName = "Dog";
    public GameObject[] petPrefabs;
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

        if (scene.name == "Living Room Scene" || scene.name == "Bathroom") {
            if (playerPet != null) {
                Instantiate(playerPet, spawnPos, playerPet.transform.rotation);
            } else {
                Instantiate(defaultPet, spawnPos, defaultPet.transform.rotation);
            }
        }
    }
}
