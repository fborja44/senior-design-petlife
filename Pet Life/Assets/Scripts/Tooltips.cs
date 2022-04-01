using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltips : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager gameManager;
    public GameObject tooltip;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize vars
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        tooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Show text
        if (GameManager.busy) return;
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Hide text
        tooltip.SetActive(false);
    }
}
