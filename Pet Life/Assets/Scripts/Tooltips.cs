using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltips : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipText;
    // Start is called before the first frame update
    void Start()
    {
        tooltipText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Show text
        tooltipText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Hide text
        tooltipText.SetActive(false);
    }
}
