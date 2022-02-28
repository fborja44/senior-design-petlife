using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltips : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    // Start is called before the first frame update
    void Start()
    {
        tooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Show text
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Hide text
        tooltip.SetActive(false);
    }
}
