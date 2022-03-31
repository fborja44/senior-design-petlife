using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

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
    public static int sitLevel = 1;
    public static int layLevel = 1;
    public static int rollLevel = 1;
    public static int fetchLevel = 1;
    public static int speakLevel = 1;
    public static bool lastTrickSuccess = false;
    public static int lastTrick; // 1 = sit, 2 = lay, 3 = roll, 4 = fetch, 5 = speak
    public NeedsBar energyBar;
    public NeedsBar hungerBar;
    public NeedsBar thirstBar;
    public NeedsBar loveBar;
    public NeedsBar bladderBar;
    public NeedsBar hygieneBar;
    public Button treatButton; 
    public GameObject alert;
    private TextMeshProUGUI alertText;
    private bool alertToggle = true;
    float count = 0;
    
    // Start is called before the first frame update
    void Start()
    {

        // Hide alert
        GameObject.Find("Alert").SetActive(false);
        // Set alert text
        alertText = alert.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        // Set needs bar to current values
        energyBar.SetMaxNeeds(max);
        hungerBar.SetMaxNeeds(max);
        thirstBar.SetMaxNeeds(max);
        loveBar.SetMaxNeeds(max/2);
        bladderBar.SetMaxNeeds(max);
        hygieneBar.SetMaxNeeds(max);

        treatButton.interactable = false;

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
        Alert();
        count += 1;
        if (count == 600 && !GameManager.busy){
            count = 0;
            LoseEnergy(1);
            LoseHunger(1);
            LoseThirst(2);
            LoseHygiene(1);
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool LoseEnergy(float energyLost){
        float newEnergy = currentEnergy - (energyLost * difficulty);
        // Check if there is enough energy
        if (newEnergy >= 0) {
            currentEnergy = Mathf.Max(newEnergy, 0);
            energyBar.SetNeeds(currentEnergy);
            return true;
        } else {
            // Display warning
            FailedAction("energy", false);
            return false;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool LoseHunger(float hungerLost){
        float newHunger = currentHunger - (hungerLost * difficulty);
        if (newHunger >= 0) {
            currentHunger = Mathf.Max(newHunger, 0);
            hungerBar.SetNeeds(currentHunger);
            return true;
        } else {
            // Display warning
            FailedAction("Hunger", false);
            return false;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool LoseThirst(float thirstLost){
        float newThirst = currentThirst - (thirstLost * difficulty);
        if (newThirst >= 0) {
            currentThirst = Mathf.Max(newThirst, 0);
            thirstBar.SetNeeds(currentThirst);
            return true;
        } else {
            // Display warning
            FailedAction("Thirst", false);
            return false;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool LoseLove(float loveLost){
        float newLove = currentLove - (loveLost * difficulty);
        if (newLove >= 0) {
            currentLove = Mathf.Max(newLove, 0);
            loveBar.SetNeeds(currentLove);
            return true;
        } else {
            // Display warning
            FailedAction("Love", false);
            return false;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool LoseBladder(float bladderLost){
        float newBladder = currentBladder - (bladderLost * difficulty);
        if (newBladder >= 0) {
            currentBladder = Mathf.Max(newBladder, 0);
            bladderBar.SetNeeds(currentBladder);
            return true;
        } else {
            // Display warning
            FailedAction("Bladder", false);
            return false;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool LoseHygiene(float hygieneLost){
        float newHygiene = currentHygiene - (hygieneLost * difficulty);
        if (newHygiene >= 0) {
            currentHygiene = Mathf.Max(newHygiene, 0);
            hygieneBar.SetNeeds(currentHygiene);
            return true;
        } else {
            // Display warning
            FailedAction("Hygiene", false);
            return false;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool GainEnergy(float sleep) {
        float newEnergy = currentEnergy + sleep;
        if (newEnergy <= max) {
            currentEnergy = Mathf.Min(newEnergy, max);
            energyBar.SetNeeds(currentEnergy);
            return true;
        } else {
            // Display warning
            FailedAction("Energy");
            return false;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool GainBladder(float bladderGain){
        float newBladder = currentBladder + bladderGain;
        if (newBladder <= max) {
            currentBladder = Mathf.Min(newBladder, max);
            bladderBar.SetNeeds(currentBladder);
            return true;
        } else {
            // Display warning
            FailedAction("Bladder");
            return false;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool GainHunger(float hungerGain) {
        float newHunger = currentHunger + hungerGain;
        if (newHunger <= max) {
            currentHunger = Mathf.Min(newHunger, max);
            hungerBar.SetNeeds(currentHunger);
            return true;
        } else {
            // Display warning
            FailedAction("Hunger");
            return false;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool GainThirst(float thirstGain) {
        float newThirst = currentThirst + thirstGain;
        if (newThirst <= max) {
            currentThirst = Mathf.Min(newThirst, max);
            thirstBar.SetNeeds(thirstGain);
            return true;
        } else {
            // Display warning
            FailedAction("Thirst");
            return false;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool GainHygiene(float hygieneGain) {
        float newHygiene = currentHygiene + hygieneGain;
        if (newHygiene <= max) {
            currentHygiene = Mathf.Min(newHygiene, max);
            hygieneBar.SetNeeds(currentHygiene);
            return true;
        } else {
            // Display warning
            FailedAction("Hygiene");
            return false;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool GainLove(float loveGain){
        float newLove = currentLove + loveGain;
        if (newLove <= max/2) {
            currentLove = Mathf.Min(newLove, max/2);
            loveBar.SetNeeds(currentLove);
            return true;
        } else {
            // Display warning
            FailedAction("Love");
            return false;
        }
    }

    public void giveTreat(){
        treatButton.interactable = false;
        if (lastTrickSuccess == true) {
            if (lastTrick == 1 & sitLevel < 10) {
                sitLevel = sitLevel + 1;
                Debug.Log("Treat given succesfully. Adding 1 to sit level. Sit Level is now" + sitLevel);
            }
            if (lastTrick == 2 & layLevel < 10) {
                layLevel = layLevel + 1;
                Debug.Log("Treat given succesfully. Adding 1 to lay level. Lay Level is now" + layLevel);
            } 
            if (lastTrick == 3 & rollLevel < 10) {
                rollLevel = rollLevel + 1;
                Debug.Log("Treat given succesfully. Adding 1 to roll level. Roll Level is now" + rollLevel);
            } 
            if (lastTrick == 4 & fetchLevel < 10) {
                fetchLevel = fetchLevel + 1;
                Debug.Log("Treat given succesfully. Adding 1 to fetch level. Fetch Level is now" + fetchLevel);
            } 
            if (lastTrick == 5 & speakLevel < 10) {
                speakLevel = speakLevel + 1;
                Debug.Log("Treat given succesfully. Adding 1 to speak level. Speak Level is now" + speakLevel);
            }  
        }
        else {
            if (lastTrick == 1 & sitLevel > 1) {
                sitLevel = sitLevel - 1;
                Debug.Log("Treat given for incorrect trick. Removing 1 from sit level. Sit Level is now" + sitLevel);
            }
            if (lastTrick == 2 & layLevel > 1) {
                layLevel = layLevel - 1;
                Debug.Log("Treat given for incorrect trick. Removing 1 from lay level. Lay Level is now" + layLevel);
            } 
            if (lastTrick == 3 & rollLevel > 1) {
                rollLevel = rollLevel - 1;
                Debug.Log("Treat given for incorrect trick. Removing 1 from roll level. Roll Level is now" + rollLevel);
            } 
            if (lastTrick == 4 & fetchLevel > 1) {
                fetchLevel = fetchLevel - 1;
                Debug.Log("Treat given for incorrect trick. Removing 1 from fetch level. Fetch Level is now" + fetchLevel);
            } 
            if (lastTrick == 5 & speakLevel > 1) {
                speakLevel = speakLevel - 1;
                Debug.Log("Treat given for incorrect trick. Removing 1 from speak level. Speak Level is now" + speakLevel);
            }  
        }
    }
    public void Sit(){
        int rand_num = Random.Range(1,10);
        lastTrick = 1;
        Debug.Log("Sit level is " + sitLevel + ", random number was " + rand_num);
        if (sitLevel >= rand_num) {
            Debug.Log("Dog succesfully did sit");
            lastTrickSuccess = true;
            executeSit();
        } else {
            Debug.Log("Dog failed sit");
            lastTrickSuccess = false;
            int rand_num1 = Random.Range(1,4);        
            if (rand_num1 == 1) {
                executeLay();
            }
            else if (rand_num1 == 2) {
                executeRoll();
            }
            else if (rand_num1 == 3) {
                executeFetch();
            }
            else if (rand_num1 == 4) {
                executeSpeak();
            }
        }
    }

    public void executeSit(){
        treatButton.interactable = true;
        Debug.Log("Dog has executed Sit");
        float energyUsed = (float)(max * 0.05);
        LoseEnergy(energyUsed);
        float hygieneUsed = (float)(max * 0.05);
        LoseHygiene(hygieneUsed);
    }

    public void Lay(){
        lastTrick = 2;
        int rand_num = Random.Range(1,10);
        Debug.Log("Lay level is " + layLevel + ", random number was " + rand_num);
        if (layLevel >= rand_num) {
            Debug.Log("Dog succesfully did lay");
            lastTrickSuccess = true;
            executeLay();
        } else {
            Debug.Log("Dog failed lay");
            lastTrickSuccess = false;
            int rand_num1 = Random.Range(1,4);
            if (rand_num1 == 1) {
                executeSit();
            }
            else if (rand_num1 == 2) {
                executeRoll();
            }
            else if (rand_num1 == 3) {
                executeFetch();
            }
            else if (rand_num1 == 4) {
                executeSpeak();
            }
        }
    }

    public void executeLay(){
        treatButton.interactable = true;
        Debug.Log("Dog has executed Lay");
        float energyUsed = (float)(max * 0.05);
        LoseEnergy(energyUsed);
        float hygieneUsed = (float)(max * 0.1);
        LoseHygiene(hygieneUsed);
    }

    public void Roll(){
        lastTrick = 3;
        int rand_num = Random.Range(1,10);
        Debug.Log("Roll level is " + rollLevel + ", random number was " + rand_num);
        if (rollLevel >= rand_num) {
            Debug.Log("Dog succesfully did roll");
            lastTrickSuccess = true;
            executeRoll();
        } else {
            Debug.Log("Dog failed roll");
            lastTrickSuccess = false;
            int rand_num1 = Random.Range(1,4);
            if (rand_num1 == 1) {
                executeSit();
            }
            else if (rand_num1 == 2) {
                executeLay();
            }
            else if (rand_num1 == 3) {
                executeFetch();
            }
            else if (rand_num1 == 4) {
                executeSpeak();
            }
        }
    }

    public void executeRoll(){
        treatButton.interactable = true;
        Debug.Log("Dog has executed Roll");
        float energyUsed = (float)(max * 0.08);
        LoseEnergy(energyUsed);
        float hygieneUsed = (float)(max * 0.2);
        LoseHygiene(hygieneUsed);
    }

    public void Fetch(){
        lastTrick = 4;
        int rand_num = Random.Range(1,10);
        Debug.Log("Fetch level is " + fetchLevel + ", random number was " + rand_num);
        if (fetchLevel >= rand_num) {
            Debug.Log("Dog succesfully did fetch");
            lastTrickSuccess = true;
            executeFetch();
        } else {
            Debug.Log("Dog failed fetch");
            lastTrickSuccess = false;
            int rand_num1 = Random.Range(1,4);
            if (rand_num1 == 1) {
                executeSit();
            }
            else if (rand_num1 == 2) {
                executeLay();
            }
            else if (rand_num1 == 3) {
                executeRoll();
            }
            else if (rand_num1 == 4) {
                executeSpeak();
            }
        }
    }

    public void executeFetch() {
        treatButton.interactable = true;
        Debug.Log("Dog has executed Fetch");
        float energyUsed = (float)(max * 0.15);
        float thirstUsed = (float)(max * 0.1);
        float hygieneUsed = (float)(max * 0.1);
        LoseEnergy(energyUsed);
        LoseThirst(thirstUsed);
        LoseHygiene(hygieneUsed);
    }

    public void Speak(){
        lastTrick = 5;
        int rand_num = Random.Range(1,10);
        Debug.Log("Speak level is " + speakLevel + ", random number was " + rand_num);
        if (speakLevel >= rand_num) {
            Debug.Log("Dog succesfully did speak");
            lastTrickSuccess = true;
            executeSpeak();
        } else {
            Debug.Log("Dog failed speak");
            lastTrickSuccess = false;
            int rand_num1 = Random.Range(1,4);
            if (rand_num1 == 1) {
                executeSit();
            }
            else if (rand_num1 == 2) {
                executeLay();
            }
            else if (rand_num1 == 3) {
                executeRoll();
            }
            else if (rand_num1 == 4) {
                executeFetch();
            }
        }
    }
    public void executeSpeak(){
        treatButton.interactable = true;
        Debug.Log("Dog has executed Speak");
        float energyUsed = (float)(max * 0.06);
        LoseEnergy(energyUsed);
    }

    public void TakeBath() {
        float hygieneGained = (float)(max * 0.5);
        if (GameManager.busy) return; // busy doing another action
        if (GainHygiene(hygieneGained)) {
            StartCoroutine(SendAlert("Gave " + GameManager.petName + " a bath!"));
            StartCoroutine(Bathe());
            GameManager.score += 100;
        }
    }

    public void EatFood() {
        float hungerGained = (float)(max * 0.5);
        if (GameManager.busy) return; // busy doing another action
        if (GainHunger(hungerGained)) {
            StartCoroutine(SendAlert(GameManager.petName + " ate some food!"));
            StartCoroutine(Eat());
            GameManager.score += 100;
        }
    }

    public void DrinkWater() {
        float thirstGained = (float)(max * 0.5);
        if (GameManager.busy) return; // busy doing another action
        if (GainThirst(thirstGained)) {
            StartCoroutine(SendAlert(GameManager.petName + " drank some water!"));
            StartCoroutine(Drink());
            GameManager.score += 100;
        }
    }

    public void Rest() {
        float energyGained = (float)(max * 0.5);
        if (GameManager.busy) return; // busy doing another action
        if (GainEnergy(energyGained)) {
            StartCoroutine(SendAlert(GameManager.petName + " rested for a bit!"));
            StartCoroutine(Sleep());
            GameManager.score += 100;
        }
    }

    public void UseBall() {
        float energyUsed = (float)(max * 0.15);
        float hungerLost = (float)(max * 0.18);
        float thirstLost = (float)(max * 0.18);
        float loveGained = (float)(max * 0.15);
        if (GameManager.busy) return; // busy doing another action
        if (!CheckNeeds("Energy", energyUsed, false)) {
            FailedAction("Energy", false);
        } else if (!CheckNeeds("Hunger", hungerLost, false)) {
            FailedAction("Hunger", false);
        } else if (!CheckNeeds("Thirst", thirstLost, false)) {
            FailedAction("Thirst", false);
        } else if (!CheckNeeds("Love", loveGained)) {
            FailedAction("Love");
        } else {
            StartCoroutine(SendAlert("Played some fetch with " + GameManager.petName + "!"));
            LoseEnergy(energyUsed);
            LoseHunger(hungerLost);
            LoseThirst(thirstLost);
            GainLove(loveGained);
            GameManager.score += 100;
        }
    }

    public void UseJump() {
        if (GameManager.busy) return; // busy doing another action
        StartCoroutine(Jump());
    }

    IEnumerator Idle() {
        GameManager.animator.SetFloat("speed", 0);
        yield return new WaitForSecondsRealtime(Random.Range(1, 4));
        GameManager.animator.SetFloat("speed", 0);
    }

    IEnumerator Eat() {
        GameManager.busy = true;

        GameObject playerPet = GameManager.playerPet;
        Vector3 petPos = playerPet.transform.position;
        float petSpeed = GameManager.animator.GetFloat("speed");
        GameManager.animator.SetFloat("speed", 0);
        GameObject foodBowl = GameObject.Find("Food Bowl");
        Vector3 bowlPos = foodBowl.transform.position;

        // Play eating animation
        GameManager.animator.SetBool("is_eating", true);
        // Move dog
        playerPet.transform.position = new Vector3(bowlPos.x + 1, bowlPos.y - (float)0.5, petPos.z);
        yield return new WaitForSecondsRealtime(4);

        // Stop eating, reset position and speed
        GameManager.animator.SetBool("is_eating", false);
        GameManager.animator.SetFloat("speed", petSpeed);
        playerPet.transform.position = petPos;

        GameManager.busy = false;
    }

    IEnumerator Drink() {
        GameManager.busy = true;

        GameObject playerPet = GameManager.playerPet;
        Vector3 petPos = playerPet.transform.position;
        // float petSpeed = GameManager.animator.GetFloat("speed");
        GameManager.animator.SetFloat("speed", 0);
        GameObject waterBowl = GameObject.Find("Water Bowl");
        Vector3 bowlPos = waterBowl.transform.position;

        // Play eating animation
        GameManager.animator.SetBool("is_drinking", true);
        // Move dog
        playerPet.transform.position = new Vector3(bowlPos.x + 1, bowlPos.y - (float)0.5, petPos.z);
        yield return new WaitForSecondsRealtime(4);
        // GameManager.animator.SetFloat("speed", petSpeed);

        // Stop drinking, reset position
        GameManager.animator.SetBool("is_drinking", false);
        playerPet.transform.position = petPos;

        GameManager.busy = false;
    }

    IEnumerator Sleep() {
        GameManager.busy = true;

        GameObject playerPet = GameManager.playerPet;
        Vector3 petPos = playerPet.transform.position;
        GameManager.animator.SetFloat("speed", 0);
        GameObject dogBed = GameObject.Find("Dog Bed");
        Vector3 bedPos = dogBed.transform.position;

        // Play sleeping animation
        GameManager.animator.SetBool("is_sleeping", true);
        // Move dog
        playerPet.transform.position = new Vector3(bedPos.x, bedPos.y - (float)0.1, playerPet.transform.position.z);
        yield return new WaitForSecondsRealtime(4);

        // Stop sleeping and reset position
        GameManager.animator.SetBool("is_sleeping", false);
        playerPet.transform.position = petPos;

        GameManager.busy = false;
    }

    IEnumerator Bathe() {
        GameManager.busy = true;

        GameObject playerPet = GameManager.playerPet;
        Vector3 petPos = playerPet.transform.position;
        GameManager.animator.SetFloat("speed", 0);
        GameObject bathtub = GameObject.Find("Bathtub");
        Vector3 bathpos = bathtub.transform.position;

        // Move dog
        playerPet.transform.position = new Vector3(bathpos.x, bathpos.y + (float) 0.1, bathpos.z - 1);
        // Play jumping animation
        GameManager.animator.SetBool("is_jumping", true);
        yield return new WaitForSecondsRealtime((float)1);
        // Stop jumping and reset position
        GameManager.animator.SetBool("is_jumping", false);
        // Wait a bit more
        yield return new WaitForSecondsRealtime((float)1);
        // Reset position
        playerPet.transform.position = petPos;

        GameManager.busy = false;
    }

    IEnumerator Jump() {
        GameManager.busy = true;

        GameObject playerPet = GameManager.playerPet;
        GameManager.animator.SetFloat("speed", 0);

        // Play jumping animation
        GameManager.animator.SetBool("is_jumping", true);
        yield return new WaitForSecondsRealtime((float) 1.25);

        // Stop jumping and reset position
        GameManager.animator.SetBool("is_jumping", false);

        GameManager.busy = false;
    }

    public void Alert() {
        float[] needs = {currentEnergy, currentHunger, currentThirst, currentLove, currentBladder, currentHygiene};
        for (int i = 0; i < needs.Length; i++) {
            string need;
            switch (i) {
                case 0:
                    need = "Energy";
                    break;
                case 1:
                    need = "Hunger";
                    break;
                case 2:
                    need = "Thirst";
                    break;
                case 3:
                    need = "Love";
                    break;
                case 4:
                    need = "Bladder";
                    break;
                case 5:
                    need = "Hygiene";
                    break;
                default:
                    need = "Needs";
                    break;
            }
            string name = GameManager.petName;
            if (needs[i] < max/4 && alertToggle) {
                if (needs[i] == 0) {
                    if (name.ToLower()[name.Length - 1] == 's') {
                        StartCoroutine(SendAlert(name + "' " + need + " is empty!"));
                    } else {
                        StartCoroutine(SendAlert(name + "'s " + need + " is empty!"));
                    }
                } else {
                    if (name.ToLower()[name.Length - 1] == 's') {
                        StartCoroutine(SendAlert(name + "' " + need + " is getting low!"));
                    } else {
                        StartCoroutine(SendAlert(name + "'s " + need + " is getting low!"));
                    }
                }
                StartCoroutine(DisableAlert());
                return;
            }
        }
    }

    public bool CheckNeeds(string need, float change, bool gain = true) {
        if (gain) {
            switch (need) {
                case "Hunger":
                    float newHunger = currentHunger + (change * difficulty);
                    if (newHunger <= max) return true;
                    break;
                case "Thirst":
                    float newThirst = currentThirst + (change * difficulty);
                    if (newThirst <= max) return true;
                    break;
                case "Love":
                    float newLove = currentLove + (change * difficulty);
                    if (newLove <= max/2) return true;
                    break;
                case "Hygiene":
                    float newHygiene = currentHygiene + (change * difficulty);
                    if (newHygiene <= max) return true;
                    break;
                case "Bladder":
                    float newBladder = currentBladder + (change * difficulty);
                    if (newBladder <= max) return true;
                    break;
                case "Energy":
                    float newEnergy = currentEnergy + (change * difficulty);
                    if (newEnergy <= max) return true;
                    break;
                default:
                    break;
            }
        } else {
            switch (need) {
                case "Hunger":
                    float newHunger = currentHunger - (change * difficulty);
                    if (newHunger >= 0) return true;
                    break;
                case "Thirst":
                    float newThirst = currentThirst - (change * difficulty);
                    if (newThirst >= 0) return true;
                    break;
                case "Love":
                    float newLove = currentLove - (change * difficulty);
                    if (newLove >= 0) return true;
                    break;
                case "Hygiene":
                    float newHygiene = currentHygiene - (change * difficulty);
                    if (newHygiene > 0) return true;
                    break;
                case "Bladder":
                    float newBladder = currentBladder - (change * difficulty);
                    if (newBladder >= 0) return true;
                    break;
                case "Energy":
                    float newEnergy = currentEnergy - (change * difficulty);
                    if (newEnergy >= 0) return true;
                    break;
                default:
                    break;
            }
        }
        return false;
    }

    /**
     * false = not enough of need
     * true = needs is too full
     */
    public void FailedAction(string need, bool gain = true) {
        string name = GameManager.petName;
        if (!gain) {
            if (name.ToLower()[name.Length - 1] == 's') {
                StartCoroutine(SendAlert(name + "' " + need + " is too low!"));
            } else {
                StartCoroutine(SendAlert(name + "'s " + need + " is too low!"));
            }
        } else {
            if (name.ToLower()[name.Length - 1] == 's') {
                StartCoroutine(SendAlert(name + "' " + need + " is too full!"));
            } else {
                StartCoroutine(SendAlert(name + "'s " + need + " is too full!"));
            }
        }
        StartCoroutine(DisableAlert());
    }

    IEnumerator SendAlert(string text) {
        alertText.text = text;
        alert.SetActive(true);
        yield return new WaitForSeconds(5);
        alert.SetActive(false);
    }

    IEnumerator DisableAlert() {
        alertToggle = false;
        yield return new WaitForSeconds(10);
        alertToggle = true;
    }

    public void ResetNeeds() {
        GainEnergy(max);
        GainHunger(max);
        GainThirst(max);
        GainLove(max/2);
        GainBladder(max);
        GainHygiene(max);
        energyBar.SetNeeds(currentEnergy);
        hungerBar.SetNeeds(currentHunger);
        thirstBar.SetNeeds(currentThirst);
        loveBar.SetNeeds(currentLove);
        bladderBar.SetNeeds(currentBladder);
        hygieneBar.SetNeeds(currentHygiene);
    }
}