using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTraining : MonoBehaviour
{
    private Button button;
    public GameObject trainingPanel;
    public GameObject trainingLevels;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize button and onClick event listener
        button = GetComponent<Button>();
        button.onClick.AddListener(Toggle);
    }

    void Toggle() {
        trainingPanel.SetActive(!trainingPanel.activeInHierarchy);
        trainingLevels.SetActive(!trainingLevels.activeInHierarchy);
    }
}
