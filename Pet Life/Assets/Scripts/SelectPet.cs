using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectPet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject pet;
    public Image selection;
    public Sprite disc;
    public Sprite disc_alt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        selection.GetComponent<Image>().sprite = disc_alt;
    }

    public void OnPointerExit(PointerEventData eventData) {
        selection.GetComponent<Image>().sprite = disc;
    }
}
