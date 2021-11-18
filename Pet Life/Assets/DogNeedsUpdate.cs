using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogNeedsUpdate : MonoBehaviour
{
    public int maxEnergy = 500;
    public int currentEnergy;
    public NeedsBar energyBar;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy;
        energyBar.SetMaxNeeds(maxEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        count += 1;
        if (count == 600){
            count = 0;
            LoseEnergy(1);
        }
    }

    void LoseEnergy(int energyLost){
        currentEnergy -= energyLost;
        energyBar.SetNeeds(currentEnergy);
    }

    public void Sit(){
        int energyUsed = (int)(maxEnergy * 0.05);
        LoseEnergy(energyUsed);
    }

    public void Lay(){
        int energyUsed = (int)(maxEnergy * 0.05);
        LoseEnergy(energyUsed);
    }

    public void Roll(){
        int energyUsed = (int)(maxEnergy * 0.08);
        LoseEnergy(energyUsed);
    }

    public void Fetch(){
        int energyUsed = (int)(maxEnergy * 0.15);
        LoseEnergy(energyUsed);
    }

    public void Speak(){
        int energyUsed = (int)(maxEnergy * 0.06);
        LoseEnergy(energyUsed);
    }
}
