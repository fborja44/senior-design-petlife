using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DogNeedsUpdate : MonoBehaviour
{
    private GameManager gameManager;
    private Text needsName;
    private float difficulty;
    public static float max = 500;
    public static float currentEnergy = max;
    public static float currentHunger = max;
    public static float currentThirst = max;
    public static float currentLove = max/2;
    public static float currentBladder = max;
    public static float currentHygiene = max;
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
        // Set needs bar to current values
        energyBar.SetMaxNeeds(max);
        hungerBar.SetMaxNeeds(max);
        thirstBar.SetMaxNeeds(max);
        loveBar.SetMaxNeeds(max/2);
        bladderBar.SetMaxNeeds(max);
        hygieneBar.SetMaxNeeds(max);

        energyBar.SetNeeds(currentEnergy);
        hungerBar.SetNeeds(currentHunger);
        thirstBar.SetNeeds(currentThirst);
        loveBar.SetNeeds(currentLove);
        bladderBar.SetNeeds(currentBladder);
        hygieneBar.SetNeeds(currentHygiene);

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
        float newEnergy = currentEnergy - (energyLost * difficulty);
        currentEnergy = Mathf.Max(newEnergy, 0);
        energyBar.SetNeeds(currentEnergy);
    }

    void LoseHunger(float hungerLost){
        float newHunger = currentHunger - (hungerLost * difficulty);
        currentHunger = Mathf.Max(newHunger, 0);
        hungerBar.SetNeeds(currentHunger);
    }

    void LoseThirst(float thirstLost){
        float newThirst = currentThirst - (thirstLost * difficulty);
        currentThirst = Mathf.Max(newThirst, 0);
        thirstBar.SetNeeds(currentThirst);
    }

    void LoseLove(float loveLost){
        float newLove = currentLove - (loveLost * difficulty);
        currentLove = Mathf.Max(newLove, 0);
        loveBar.SetNeeds(currentLove);
    }

    void LoseBladder(float bladderLost){
        float newBladder = currentBladder - (bladderLost * difficulty);
        currentBladder = Mathf.Max(newBladder, 0);
        bladderBar.SetNeeds(currentBladder);
    }

    void LoseHygiene(float hygieneLost){
        float newHygiene = currentHygiene - (hygieneLost * difficulty);
        currentHygiene = Mathf.Max(newHygiene, 0);
        hygieneBar.SetNeeds(currentHygiene);
    }

    void GainEnergy(float sleep) {
        float newEnergy = currentEnergy + sleep;
        currentEnergy = Mathf.Min(newEnergy, max);
        energyBar.SetNeeds(currentEnergy);
    }

    void GainBladder(float bladderGain){
        float newBladder = currentBladder + bladderGain;
        currentBladder = Mathf.Min(newBladder, max);
        bladderBar.SetNeeds(currentBladder);
    }

    void GainHunger(float hungerGain) {
        float newHunger = currentHunger + hungerGain;
        currentHunger = Mathf.Min(newHunger, max);
        hungerBar.SetNeeds(currentHunger);
    }

    void GainThirst(float thirstGain) {
        float newThirst = currentThirst + thirstGain;
        currentThirst = Mathf.Min(newThirst, max);
        thirstBar.SetNeeds(thirstGain);
    }

    void GainHygiene(float hygieneGain) {
        float newHygiene = currentHygiene + hygieneGain;
        currentHygiene = Mathf.Min(newHygiene, max);
        hygieneBar.SetNeeds(currentHygiene);
    }

    void GainLove(float loveGain){
        float newLove = currentLove + loveGain;
        currentLove = Mathf.Min(newLove, max);
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
        float thirstUsed = (float)(max * 0.1);
        LoseEnergy(energyUsed);
        LoseThirst(thirstUsed);
    }

    public void Speak(){
        float energyUsed = (float)(max * 0.06);
        LoseEnergy(energyUsed);
    }

    public void TakeBath() {
        float hygieneGained = (float)(max * 0.5);
        GainHygiene(hygieneGained);
    }

    public void EatFood() {
        float hungerGained = (float)(max * 0.5);
        GainHunger(hungerGained);
    }

    public void DrinkWater() {
        float thirstGained = (float)(max * 0.5);
        GainThirst(thirstGained);
    }

    public void Rest() {
        float energyGained = (float)(max * 0.5);
        GainEnergy(energyGained);
    }

    public void UseFrisbee() {
        float energyUsed = (float)(max * 0.15);
        float hungerLost = (float)(max * 0.18);
        float thirstLost = (float)(max * 0.18);
        float loveGained = (float)(max * 0.15);
        LoseEnergy(energyUsed);
        LoseHunger(hungerLost);
        LoseThirst(thirstLost);
        GainLove(loveGained);
    }
}