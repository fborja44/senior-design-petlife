using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DogNeedsUpdate : MonoBehaviour
{
    private GameManager gameManager;
    private Text needsName;
    public int max = 500;
    public int currentEnergy;
    public int currentHunger;
    public int currentThirst;
    public int currentLove;
    public int currentBladder;
    public int currentHygiene;
    public NeedsBar energyBar;
    public NeedsBar hungerBar;
    public NeedsBar thirstBar;
    public NeedsBar loveBar;
    public NeedsBar bladderBar;
    public NeedsBar hygieneBar;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // Set name
        needsName = GameObject.Find("Needs Name").GetComponent<Text>();
        string name = GameManager.petName;
        if (name.EndsWith("S") || name.EndsWith("s")) {
            needsName.text = name + "' Needs";
        } else {
            needsName.text = name + "'s Needs";
        }

        currentEnergy = max;
        currentHunger = max; 
        currentThirst = max;
        currentLove = max/2;
        currentBladder = max;
        currentHygiene = max;
        energyBar.SetMaxNeeds(max);
        hungerBar.SetMaxNeeds(max);
        thirstBar.SetMaxNeeds(max);
        loveBar.SetMaxNeeds(max/2);
        bladderBar.SetMaxNeeds(max);
        hygieneBar.SetMaxNeeds(max);
    }

    // Update is called once per frame
    void Update()
    {
        count += 1;
        if (count == 600){
            count = 0;
            LoseEnergy(1);
            LoseHunger(1);
            LoseThirst(2);
        }
    }

    void LoseEnergy(int energyLost){
        currentEnergy -= energyLost;
        energyBar.SetNeeds(currentEnergy);
    }

    void LoseHunger(int hungerLost){
        currentHunger -= hungerLost;
        hungerBar.SetNeeds(currentHunger);
    }

    void LoseThirst(int thirstLost){
        currentThirst -= thirstLost;
        thirstBar.SetNeeds(currentThirst);
    }

    void LoseLove(int loveLost){
        currentLove -= loveLost;
        loveBar.SetNeeds(currentLove);
    }

    void LoseBladder(int bladderLost){
        currentBladder -= bladderLost;
        bladderBar.SetNeeds(currentBladder);
    }

    void LoseHygiene(int hygieneLost){
        currentHygiene -= hygieneLost;
        hygieneBar.SetNeeds(currentHygiene);
    }

    void GainEnergy(int sleep) {
        currentEnergy += sleep;
        energyBar.SetNeeds(currentEnergy);
    }

    void GainBladder(int bladderGain){
        currentBladder += bladderGain;
        bladderBar.SetNeeds(currentBladder);
    }

    void GainHunger(int hungerGain) {
        currentHunger +=hungerGain;
        hungerBar.SetNeeds(currentHunger);
    }

    void GainThirst(int thirstGain) {
        currentThirst += thirstGain;
        thirstBar.SetNeeds(thirstGain);
    }

    void GainHygiene(int hygieneGain) {
        currentHygiene += hygieneGain;
        hygieneBar.SetNeeds(currentHygiene);
    }

    void GainLove(int loveGain){
        currentLove -= loveGain;
        loveBar.SetNeeds(currentLove);
    }

    public void Sit(){
        int energyUsed = (int)(max * 0.05);
        LoseEnergy(energyUsed);
        int hygieneUsed = (int)(max * 0.05);
        LoseHygiene(hygieneUsed);
    }

    public void Lay(){
        int energyUsed = (int)(max * 0.05);
        LoseEnergy(energyUsed);
        int hygieneUsed = (int)(max * 0.1);
        LoseHygiene(hygieneUsed);
    }

    public void Roll(){
        int energyUsed = (int)(max * 0.08);
        LoseEnergy(energyUsed);
        int hygieneUsed = (int)(max * 0.2);
        LoseHygiene(hygieneUsed);
    }

    public void Fetch(){
        int energyUsed = (int)(max * 0.15);
        LoseEnergy(energyUsed);
    }

    public void Speak(){
        int energyUsed = (int)(max * 0.06);
        LoseEnergy(energyUsed);
    }
}