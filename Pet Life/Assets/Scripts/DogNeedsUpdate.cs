using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DogNeedsUpdate : MonoBehaviour
{
    private GameManager gameManager;
    private Text needsName;
    private float difficulty;
    public float max = 500;
    public static float currentEnergy;
    public static float currentHunger;
    public static float currentThirst;
    public static float currentLove;
    public static float currentBladder;
    public static float currentHygiene;
    public NeedsBar energyBar;
    public NeedsBar hungerBar;
    public NeedsBar thirstBar;
    public NeedsBar loveBar;
    public NeedsBar bladderBar;
    public NeedsBar hygieneBar;
    float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // Set difficulty
        difficulty = GameManager.difficulty;
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
            LoseHygiene(1);
        }
    }

    void LoseEnergy(float energyLost){
        currentEnergy -= energyLost * difficulty;
        energyBar.SetNeeds(currentEnergy);
    }

    void LoseHunger(float hungerLost){
        currentHunger -= hungerLost * difficulty;
        hungerBar.SetNeeds(currentHunger);
    }

    void LoseThirst(float thirstLost){
        currentThirst -= thirstLost * difficulty;
        thirstBar.SetNeeds(currentThirst);
    }

    void LoseLove(float loveLost){
        currentLove -= loveLost * difficulty;
        loveBar.SetNeeds(currentLove);
    }

    void LoseBladder(float bladderLost){
        currentBladder -= bladderLost * difficulty;
        bladderBar.SetNeeds(currentBladder);
    }

    void LoseHygiene(float hygieneLost){
        currentHygiene -= hygieneLost * difficulty;
        hygieneBar.SetNeeds(currentHygiene);
    }

    void GainEnergy(float sleep) {
        currentEnergy += sleep;
        energyBar.SetNeeds(currentEnergy);
    }

    void GainBladder(float bladderGain){
        currentBladder += bladderGain;
        bladderBar.SetNeeds(currentBladder);
    }

    void GainHunger(float hungerGain) {
        currentHunger +=hungerGain;
        hungerBar.SetNeeds(currentHunger);
    }

    void GainThirst(float thirstGain) {
        currentThirst += thirstGain;
        thirstBar.SetNeeds(thirstGain);
    }

    void GainHygiene(float hygieneGain) {
        currentHygiene += hygieneGain;
        hygieneBar.SetNeeds(currentHygiene);
    }

    void GainLove(float loveGain){
        currentLove -= loveGain;
        loveBar.SetNeeds(currentLove);
    }

    public void Sit(){
        float energyUsed = (float)(max * 0.05);
        LoseEnergy(energyUsed);
        float hygieneUsed = (float)(max * 0.05);
        LoseHygiene(hygieneUsed);
    }

    public void Lay(){
        float energyUsed = (float)(max * 0.05);
        LoseEnergy(energyUsed);
        float hygieneUsed = (float)(max * 0.1);
        LoseHygiene(hygieneUsed);
    }

    public void Roll(){
        float energyUsed = (float)(max * 0.08);
        LoseEnergy(energyUsed);
        float hygieneUsed = (float)(max * 0.2);
        LoseHygiene(hygieneUsed);
    }

    public void Fetch(){
        float energyUsed = (float)(max * 0.15);
        LoseEnergy(energyUsed);
    }

    public void Speak(){
        float energyUsed = (float)(max * 0.06);
        LoseEnergy(energyUsed);
    }

    public void TakeBath() {
        float hygieneGained = (float)(max * 0.5);
        GainHygiene(hygieneGained);
    }
}