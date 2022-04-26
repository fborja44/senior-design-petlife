using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToSceneButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager gameManager;
    public GameObject toText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Show text and change cursor
        if (GameManager.isBusy()) {
            Cursor.SetCursor(gameManager.cursorWait, gameManager.hotSpot, gameManager.cursorMode);
            return;
        }
        Cursor.SetCursor(gameManager.cursorDoor, gameManager.hotSpot, gameManager.cursorMode);
        toText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Hide text
        toText.SetActive(false);
        // Reset cursor
        Cursor.SetCursor(gameManager.cursorDefault, gameManager.hotSpot, gameManager.cursorMode);
    }
}
