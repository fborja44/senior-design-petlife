using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSound : MonoBehaviour
{   private Sprite volume;
    public Sprite mute;
    public Button button;
    private bool isOn = true;
    // Start is called before the first frame update
    public AudioSource audioSource;
    void Start()
    {
        volume = button.image.sprite;
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
            audioSource.mute = true;
        } else{
            button.image.sprite = volume;
            isOn = true;
            audioSource.mute = false;
        }
    }
}
