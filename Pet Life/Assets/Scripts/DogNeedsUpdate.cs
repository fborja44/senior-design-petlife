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
    public TrickBars sitBar;
    public TrickBars layBar;
    public TrickBars rollBar;
    public TrickBars fetchBar;
    public TrickBars speakBar;
    public GameObject alert;
    private TextMeshProUGUI alertText;
    private bool alertToggle = true;
    //float count = 0;
    public int min = 0;

    private float time = 0.0f; 
    public float period = 1f;   // 1 second period

    
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

        // Set tricks bar to current values
        sitBar.SetMinTrick(min);
        layBar.SetMinTrick(min);
        rollBar.SetMinTrick(min);
        fetchBar.SetMinTrick(min);
        speakBar.SetMinTrick(min);

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
        // Start coroutine for bonus
        StartCoroutine(BonusScore());
    }

    // Update is called once per frame
    void Update()
    {
        Alert();
        time += Time.deltaTime;
        if (time > period && !GameManager.isBusy()){
            time = time - period;
            // Decay needs over time
            LoseEnergy(1);
            LoseHunger(1);
            LoseThirst(2);
            LoseHygiene(1);
            LoseLove(1);
        }
        sitBar.SetTrick(sitLevel);
        layBar.SetTrick(layLevel);
        rollBar.SetTrick(rollLevel);
        fetchBar.SetTrick(fetchLevel);
        speakBar.SetTrick(speakLevel);
        sitBar.GetComponentInChildren<Text>().text = "Sit                                     " + sitLevel;
        layBar.GetComponentInChildren<Text>().text = "Lay                                    " + layLevel;
        rollBar.GetComponentInChildren<Text>().text = "Roll                                   " + rollLevel;
        fetchBar.GetComponentInChildren<Text>().text = "Fetch                                 " + fetchLevel;
        speakBar.GetComponentInChildren<Text>().text = "Speak                                " + speakLevel;
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
    bool GainEnergy(float energyGain) {
        float newEnergy = currentEnergy + energyGain;
        if (newEnergy <= max) {
            currentEnergy = newEnergy;
            energyBar.SetNeeds(currentEnergy);
            return true;
        } else if (currentEnergy == max) {
            // Display warning
            FailedAction("Energy");
            return false;
        } else  {
            currentEnergy = max;
            energyBar.SetNeeds(currentEnergy);
            return true;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool GainBladder(float bladderGain) {
        float newBladder = currentBladder + bladderGain;
        if (newBladder <= max) {
            currentBladder = newBladder;
            bladderBar.SetNeeds(currentBladder);
            return true;
        } else if (currentBladder == max) {
            // Display warning
            FailedAction("Bladder");
            return false;
        } else  {
            currentBladder = max;
            bladderBar.SetNeeds(currentBladder);
            return true;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool GainHunger(float hungerGain) {
        float newHunger = currentHunger + hungerGain;
        if (newHunger <= max) {
            currentHunger = newHunger;
            hungerBar.SetNeeds(currentHunger);
            return true;
        } else if (currentHunger == max) {
            // Display warning
            FailedAction("Hunger");
            return false;
        } else  {
            currentHunger = max;
            hungerBar.SetNeeds(currentHunger);
            return true;
        }
    }


    /**
     * Returns true if successful, false otherwise
     */
    bool GainThirst(float thirstGain) {
        float newThirst = currentThirst + thirstGain;
        if (newThirst <= max) {
            currentThirst = newThirst;
            thirstBar.SetNeeds(currentThirst);
            return true;
        } else if (currentThirst == max) {
            // Display warning
            FailedAction("Thirst");
            return false;
        } else  {
            currentThirst = max;
            thirstBar.SetNeeds(currentThirst);
            return true;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool GainHygiene(float hygieneGain) {
        float newHygiene = currentHygiene + hygieneGain;
        if (newHygiene <= max) {
            currentHygiene = newHygiene;
            hygieneBar.SetNeeds(currentHygiene);
            return true;
        } else if (currentHygiene == max) {
            // Display warning
            FailedAction("Hygiene");
            return false;
        } else  {
            currentHygiene = max;
            hygieneBar.SetNeeds(currentHygiene);
            return true;
        }
    }

    /**
     * Returns true if successful, false otherwise
     */
    bool GainLove(float loveGain){
        float newLove = currentLove + loveGain;
        if (newLove <= max/2) {
            currentLove = newLove;
            loveBar.SetNeeds(currentLove);
            return true;
        } else {
            currentLove = max/2;
            return true;
        }
    }

    public void giveTreat(){
        treatButton.interactable = false;
        if (lastTrickSuccess == true) {
            if (lastTrick == 1 & sitLevel < 10) {
                sitLevel = sitLevel + 1;
                // Debug.Log("Treat given succesfully. Adding 1 to sit level. Sit Level is now" + sitLevel);
                StartCoroutine(SendAlert("Treat given succesfully. Adding 1 to sit level. Sit Level is now " + sitLevel));
                // Increment score
                GameManager.incrementScore(100 * sitLevel);
            }
            if (lastTrick == 2 & layLevel < 10) {
                layLevel = layLevel + 1;
                // Debug.Log("Treat given succesfully. Adding 1 to lay level. Lay Level is now" + layLevel);
                StartCoroutine(SendAlert("Treat given succesfully. Adding 1 to lay level. Lay Level is now " + layLevel));
                // Increment score
                GameManager.incrementScore(100 * layLevel);
            } 
            if (lastTrick == 3 & rollLevel < 10) {
                rollLevel = rollLevel + 1;
                // Debug.Log("Treat given succesfully. Adding 1 to roll level. Roll Level is now" + rollLevel);
                StartCoroutine(SendAlert("Treat given succesfully. Adding 1 to roll level. Roll Level is now " + rollLevel));
                // Increment score
                GameManager.incrementScore(100 * rollLevel);
            } 
            if (lastTrick == 4 & fetchLevel < 10) {
                fetchLevel = fetchLevel + 1;
                // Debug.Log("Treat given succesfully. Adding 1 to fetch level. Fetch Level is now" + fetchLevel);
                StartCoroutine(SendAlert("Treat given succesfully. Adding 1 to fetch level. Fetch Level is now " + fetchLevel));
                // Increment score
                GameManager.incrementScore(100 * fetchLevel);
            } 
            if (lastTrick == 5 & speakLevel < 10) {
                speakLevel = speakLevel + 1;
                // Debug.Log("Treat given succesfully. Adding 1 to speak level. Speak Level is now" + speakLevel);
                StartCoroutine(SendAlert("Treat given succesfully. Adding 1 to speak level. Speak Level is now " + speakLevel));
                // Increment score
                GameManager.incrementScore(100 * speakLevel);
            }  
        }
        else {
            if (lastTrick == 1 & sitLevel > 1) {
                sitLevel = sitLevel - 1;
                // Debug.Log("Treat given for incorrect trick. Removing 1 from sit level. Sit Level is now" + sitLevel);
                StartCoroutine(SendAlert("Treat given for incorrect trick. Removing 1 from sit level. Sit Level is now " + sitLevel));
                // Decrement score
                GameManager.decrementScore(100 * sitLevel);
            }
            if (lastTrick == 2 & layLevel > 1) {
                layLevel = layLevel - 1;
                // Debug.Log("Treat given for incorrect trick. Removing 1 from lay level. Lay Level is now" + layLevel);
                StartCoroutine(SendAlert("Treat given for incorrect trick. Removing 1 from lay level. Lay Level is now " + layLevel));
                // Decrement score
                GameManager.decrementScore(100 * layLevel);
            } 
            if (lastTrick == 3 & rollLevel > 1) {
                rollLevel = rollLevel - 1;
                // Debug.Log("Treat given for incorrect trick. Removing 1 from roll level. Roll Level is now" + rollLevel);
                StartCoroutine(SendAlert("Treat given for incorrect trick. Removing 1 from roll level. Roll Level is now " + rollLevel));
                // Decrement score
                GameManager.decrementScore(100 * rollLevel);
            } 
            if (lastTrick == 4 & fetchLevel > 1) {
                fetchLevel = fetchLevel - 1;
                // Debug.Log("Treat given for incorrect trick. Removing 1 from fetch level. Fetch Level is now" + fetchLevel);
                StartCoroutine(SendAlert("Treat given for incorrect trick. Removing 1 from fetch level. Fetch Level is now " + fetchLevel));
                // Decrement score
                GameManager.decrementScore(100 * fetchLevel);
            } 
            if (lastTrick == 5 & speakLevel > 1) {
                speakLevel = speakLevel - 1;
                // Debug.Log("Treat given for incorrect trick. Removing 1 from speak level. Speak Level is now" + speakLevel);
                StartCoroutine(SendAlert("Treat given for incorrect trick. Removing 1 from speak level. Speak Level is now " + speakLevel));
                // Decrement score
                GameManager.decrementScore(100 * speakLevel);
            }  
        }
    }
    public void Sit(){
        if (GameManager.isBusy()) return; // busy doing another action

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
        float energyUsed = (float)(max * 0.05);
        float hygieneUsed = (float)(max * 0.05);
        if (!CheckNeeds("Energy", energyUsed, false)) {
            FailedAction("Energy", false);
        } else if (!CheckNeeds("Hygiene", hygieneUsed, false)) {
            FailedAction("Hygiene", false);
        } else {
            LoseEnergy(energyUsed);
            LoseHygiene(hygieneUsed);
            StartCoroutine(SendAlert(GameManager.petName + " sat down!"));
            // Debug.Log("Dog has executed Sit");
        }
    }

    public void Lay(){
        if (GameManager.isBusy()) return; // busy doing another action

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
        float energyUsed = (float)(max * 0.05);
        float hygieneUsed = (float)(max * 0.1);
        if (!CheckNeeds("Energy", energyUsed, false)) {
            FailedAction("Energy", false);
        } else if (!CheckNeeds("Hygiene", hygieneUsed, false)) {
            FailedAction("Hygiene", false);
        } else {
            treatButton.interactable = true;
            LoseEnergy(energyUsed);
            LoseHygiene(hygieneUsed);
            StartCoroutine(playLay());
            StartCoroutine(SendAlert(GameManager.petName + " laid down!"));
            // Debug.Log("Dog has executed Lay");
        }
    }

    public void Roll(){
        if (GameManager.isBusy()) return; // busy doing another action

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
        float energyUsed = (float)(max * 0.08);
        float hygieneUsed = (float)(max * 0.2);
        if (!CheckNeeds("Energy", energyUsed, false)) {
            FailedAction("Energy", false);
        } else if (!CheckNeeds("Hygiene", hygieneUsed, false)) {
            FailedAction("Hygiene", false);
        } else {
            LoseEnergy(energyUsed);
            LoseHygiene(hygieneUsed);
            StartCoroutine(SendAlert(GameManager.petName + " did a roll!"));
            // Debug.Log("Dog has executed Roll");
        }
    }

    public void Fetch(){
        if (GameManager.isBusy()) return; // busy doing another action

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
        float energyUsed = (float)(max * 0.15);
        float hygieneUsed = (float)(max * 0.1);
        float thirstUsed = (float)(max * 0.1);
        if (!CheckNeeds("Energy", energyUsed, false)) {
            FailedAction("Energy", false);
        } else if (!CheckNeeds("Hygiene", hygieneUsed, false)) {
            FailedAction("Hygiene", false);
        } else if (!CheckNeeds("Thirst", thirstUsed, false)) {
            FailedAction("Thirst", false);
        } else {
            LoseEnergy(energyUsed);
            LoseHygiene(hygieneUsed);
            LoseThirst(thirstUsed);
            StartCoroutine(SendAlert(GameManager.petName + " played fetch!"));
            StartCoroutine(playJump());
            // Debug.Log("Dog has executed Fetch");
        }
    }

    public void Speak(){
        if (GameManager.isBusy()) return; // busy doing another action

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
        float energyUsed = (float)(max * 0.06);
        if (!CheckNeeds("Energy", energyUsed, false)) {
            FailedAction("Energy", false);
        } else {
            LoseEnergy(energyUsed);
            StartCoroutine(SendAlert(GameManager.petName + " barked at you!"));
            StartCoroutine(playSpeak());
            // Debug.Log("Dog has executed Speak");
        }
    }

    public void TakeBath() {
        float hygieneGained = (float)(max * 0.5);
        if (GameManager.isBusy()) return; // busy doing another action
        if (GainHygiene(hygieneGained)) {
            StartCoroutine(SendAlert("Gave " + GameManager.petName + " a bath!"));
            StartCoroutine(playBathe());
            // Increment score
            GameManager.incrementScore(100);
        }
    }

    public void EatFood() {
        float hungerGained = (float)(max * 0.5);
        if (GameManager.isBusy()) return; // busy doing another action
        if (GainHunger(hungerGained)) {
            StartCoroutine(SendAlert(GameManager.petName + " ate some food!"));
            StartCoroutine(playEat());
            // Increment score
            GameManager.incrementScore(100);
        }
    }

    public void DrinkWater() {
        float thirstGained = (float)(max * 0.5);
        if (GameManager.isBusy()) return; // busy doing another action
        if (GainThirst(thirstGained)) {
            StartCoroutine(SendAlert(GameManager.petName + " drank some water!"));
            StartCoroutine(playDrink());
            StartCoroutine(DelayLoseBladder());
            // Increment score
            GameManager.incrementScore(100);
        }
    }

    public void Rest() {
        float energyGained = (float)(max * 0.5);
        if (GameManager.isBusy()) return; // busy doing another action
        if (GainEnergy(energyGained)) {
            StartCoroutine(SendAlert(GameManager.petName + " rested for a bit!"));
            StartCoroutine(playSleep());
            // Increment score
            GameManager.incrementScore(100);
        }
    }

    public void UseBall() {
        float energyUsed = (float)(max * 0.15);
        float hungerLost = (float)(max * 0.18);
        float thirstLost = (float)(max * 0.18);
        float loveGained = (float)(max * 0.15);
        if (GameManager.isBusy()) return; // busy doing another action
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
            StartCoroutine(playJump());
            LoseEnergy(energyUsed);
            LoseHunger(hungerLost);
            LoseThirst(thirstLost);
            GainLove(loveGained);
            // Increment score
            GameManager.incrementScore(100);
        }
    }

    public void UseBrush() {
        float hygieneGained = (float)(max * 0.25);
        if (GameManager.isBusy()) return; // busy doing another action
        if (GainHygiene(hygieneGained)) {
            StartCoroutine(SendAlert("Groomed " + GameManager.petName + "!"));
            StartCoroutine(playJump());
            // Increment score
            GameManager.incrementScore(100);
        }
    }

    public void UsePuddle() {
        float energyUsed = (float)(max * 0.2);
        float hygieneLost = (float)(max * 0.4);
        float hungerLost = (float)(max * 0.10);
        float thirstLost = (float)(max * 0.10);
        float loveGained = (float)(max * 0.10);
        if (GameManager.isBusy()) return; // busy doing another action
        if (!CheckNeeds("Energy", energyUsed, false)) {
            FailedAction("Energy", false);
        } else if (!CheckNeeds("Hygiene", hygieneLost, false)) {
            FailedAction("Hygiene", false);
        } else if (!CheckNeeds("Hunger", hungerLost, false)) {
            FailedAction("Hunger", false);
        } else if (!CheckNeeds("Thirst", thirstLost, false)) {
            FailedAction("Thirst", false);
        } else if (!CheckNeeds("Love", loveGained)) {
            FailedAction("Love");
        } else {
            LoseEnergy(energyUsed);
            LoseHygiene(hygieneLost);
            LoseHunger(hungerLost);
            LoseThirst(thirstLost);
            GainLove(loveGained);
            StartCoroutine(SendAlert(GameManager.petName + " played in the puddle!"));
            StartCoroutine(playPuddle());
            // Increment score
            GameManager.incrementScore(50);
        }
    }

    public void UseJump() {
        if (GameManager.isBusy()) return; // busy doing another action
        StartCoroutine(playJump());
    }

    public void UseBathroom() {
        if (GameManager.isBusy()) return;
        float bladderGained = (float)(max * 0.5);
        if (GainBladder(bladderGained)) {
            StartCoroutine(SendAlert(GameManager.petName + " used the bathroom!"));
            // Increment score
            GameManager.incrementScore(100);
        }
    }

    IEnumerator Idle() {
        GameManager.animator.SetFloat("speed", 0);
        yield return new WaitForSecondsRealtime(Random.Range(1, 4));
        GameManager.animator.SetFloat("speed", 0);
    }

    IEnumerator playEat() {
        GameManager.ToggleBusy();

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

        GameManager.ToggleBusy();
    }

    IEnumerator playDrink() {
        GameManager.ToggleBusy();

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

        GameManager.ToggleBusy();
    }

    IEnumerator playSleep() {
        GameManager.ToggleBusy();

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

        GameManager.ToggleBusy();
    }

    IEnumerator playBathe() {
        GameManager.ToggleBusy();

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

        GameManager.ToggleBusy();
    }

    IEnumerator playJump() {
        GameManager.ToggleBusy();

        GameObject playerPet = GameManager.playerPet;
        GameManager.animator.SetFloat("speed", 0);

        // Play jumping animation
        GameManager.animator.SetBool("is_jumping", true);
        yield return new WaitForSecondsRealtime((float) 1.1);

        // Stop jumping and reset position
        GameManager.animator.SetBool("is_jumping", false);

        GameManager.ToggleBusy();
    }

    IEnumerator playLay() {
        GameManager.ToggleBusy();

        GameObject playerPet = GameManager.playerPet;
        GameManager.animator.SetFloat("speed", 0);

        // Play sleeping animation
        GameManager.animator.SetBool("is_sleeping", true);
        yield return new WaitForSecondsRealtime(4);

        // Stop sleeping
        GameManager.animator.SetBool("is_sleeping", false);

        GameManager.ToggleBusy();
    }

    IEnumerator playPuddle() {
        GameManager.ToggleBusy();

        GameObject playerPet = GameManager.playerPet;
        Vector3 petPos = playerPet.transform.position;
        GameManager.animator.SetFloat("speed", 0);
        GameObject puddle = GameObject.Find("Puddle");
        Vector3 puddlePos = puddle.transform.position;

        // Move dog
        playerPet.transform.position = new Vector3(puddlePos.x, puddlePos.y, playerPet.transform.position.z);

        // Play jumping animation
        GameManager.animator.SetBool("is_jumping", true);
        yield return new WaitForSecondsRealtime((float) 1.25);

        // Stop jumping
        GameManager.animator.SetBool("is_jumping", false);
        // Reset position
        playerPet.transform.position = petPos;

        GameManager.ToggleBusy();
    }

    IEnumerator playSpeak() {
        GameManager.ToggleBusy();

        GameManager.animator.SetFloat("speed", 0);

        // Play drinking ("barking") animation
        GameManager.animator.SetBool("is_drinking", true);
        yield return new WaitForSecondsRealtime((float) 0.5);

        // Stop "barking"
        GameManager.animator.SetBool("is_drinking", false);

        GameManager.ToggleBusy();
    }

    // Every 30 seconds, award bonus score per needs bar with > half
    IEnumerator BonusScore() {
        while (true) {
            yield return new WaitForSeconds(60);
            bool bonus = false;
            if (currentBladder > max/2) {
                GameManager.incrementScore(100);
                bonus = true;
            }
            if (currentEnergy > max/2) {
                GameManager.incrementScore(100);
                bonus = true;
            }
            if (currentHunger > max/2) {
                GameManager.incrementScore(100);
                bonus = true;
            }
            if (currentHygiene > max/2) {
                GameManager.incrementScore(200);
                bonus = true;
            }
            if (currentLove > max/4) {
                GameManager.incrementScore(200);
                bonus = true;
            }
            if (currentThirst > max/2) {
                GameManager.incrementScore(200);
                bonus = true;
            }
            if (bonus) {
                StartCoroutine(SendAlert("Awarded bonus points for taking care of " + GameManager.petName + ". Keep it up!"));
            }
        }
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
                // StartCoroutine(DisableAlert());
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
        lastTrickSuccess = false;
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
        // StartCoroutine(DisableAlert());
    }

    IEnumerator SendAlert(string text) {
        alertText.text = text;
        alert.SetActive(true);
        yield return new WaitForSeconds(5);
        // alert.SetActive(false);
    }

    IEnumerator DisableAlert() {
        alertToggle = false;
        yield return new WaitForSeconds(10);
        alertToggle = true;
    }

    public void CloseAlert() {
        alert.SetActive(false);
    }

    IEnumerator DelayLoseBladder() {
        yield return new WaitForSeconds(15);
        LoseBladder((float)(max * 0.25));
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