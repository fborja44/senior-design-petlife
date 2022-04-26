using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeSound : MonoBehaviour
{   public Sprite volume;
    public Sprite mute;
    public Button button;
    private GameManager gameManager;
    private bool isOn = true;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if(AudioListener.pause){
            button.image.sprite = mute;
            isOn = false;
        }else{
            button.image.sprite = volume;
            isOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonClicked()
    {
        if(isOn)
        {
            button.image.sprite = mute;
            isOn = false;
            AudioListener.pause = true;
        } else{
            button.image.sprite = volume;
            isOn = true;
            AudioListener.pause = false;
        }
    }
}
