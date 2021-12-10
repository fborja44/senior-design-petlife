using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToSceneButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject toText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Show text
        toText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Hide text
        toText.SetActive(false);
    }
}
