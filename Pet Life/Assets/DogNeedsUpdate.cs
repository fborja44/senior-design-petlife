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
}
