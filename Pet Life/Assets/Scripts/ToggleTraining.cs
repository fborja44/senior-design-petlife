using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTraining : MonoBehaviour
{
    private Button button;
    public GameObject trainingPanel;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize button and onClick event listener
        button = GetComponent<Button>();
        button.onClick.AddListener(Toggle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Toggle() {
        trainingPanel.SetActive(!trainingPanel.activeInHierarchy);
    }
}
