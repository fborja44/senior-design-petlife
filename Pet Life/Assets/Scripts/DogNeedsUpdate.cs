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
    float count = 0;
    public int min = 0;

    
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
    }

    // Update is called once per frame
    void Update()
    {
        Alert();
        count += 1;
        if (count == 600){
            count = 0;
            LoseEnergy(1);
            LoseHunger(1);
            LoseThirst(2);
            LoseHygiene(1);
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
        LoseEnergy(energyUsed);
        LoseThirst(thirstUsed);
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
        StartCoroutine(SendAlert("Gave " + GameManager.petName + " a bath!"));
        float hygieneGained = (float)(max * 0.5);
        GainHygiene(hygieneGained);
    }

    public void EatFood() {
        StartCoroutine(SendAlert(GameManager.petName + " ate some food!"));
        StartCoroutine(Eat());
        float hungerGained = (float)(max * 0.5);
        GainHunger(hungerGained);
    }

    public void DrinkWater() {
        StartCoroutine(SendAlert(GameManager.petName + " drank some water!"));
        StartCoroutine(Drink());
        float thirstGained = (float)(max * 0.5);
        GainThirst(thirstGained);
    }

    public void Rest() {
        StartCoroutine(SendAlert(GameManager.petName + " rested for a bit!"));
        StartCoroutine(Sleep());
        float energyGained = (float)(max * 0.5);
        GainEnergy(energyGained);
    }

    public void UseBall() {
        StartCoroutine(SendAlert("Played some fetch with " + GameManager.petName + "!"));
        float energyUsed = (float)(max * 0.15);
        float hungerLost = (float)(max * 0.18);
        float thirstLost = (float)(max * 0.18);
        float loveGained = (float)(max * 0.15);
        LoseEnergy(energyUsed);
        LoseHunger(hungerLost);
        LoseThirst(thirstLost);
        GainLove(loveGained);
    }

    IEnumerator Idle() {
        GameManager.animator.SetFloat("speed", 0);
        yield return new WaitForSecondsRealtime(Random.Range(1, 4));
        GameManager.animator.SetFloat("speed", 0);
    }

    IEnumerator Eat() {
        GameObject playerPet = GameManager.playerPet;
        Vector3 petPos = playerPet.transform.position;
        float petSpeed = GameManager.animator.GetFloat("speed");
        GameManager.animator.SetFloat("speed", 0);
        GameObject foodBowl = GameObject.Find("Food Bowl");
        Vector3 bowlPos = foodBowl.transform.position;
        GameManager.animator.SetBool("is_eating", true);
        playerPet.transform.position = new Vector3(bowlPos.x + 1, bowlPos.y - (float)0.5, petPos.z);
        yield return new WaitForSecondsRealtime(4);
        GameManager.animator.SetFloat("speed", petSpeed);
        GameManager.animator.SetBool("is_eating", false);
        
        playerPet.transform.position = petPos;
    }

    IEnumerator Drink() {
        GameObject playerPet = GameManager.playerPet;
        Vector3 petPos = playerPet.transform.position;
        // float petSpeed = GameManager.animator.GetFloat("speed");
        GameManager.animator.SetFloat("speed", 0);
        GameObject waterBowl = GameObject.Find("Water Bowl");
        Vector3 bowlPos = waterBowl.transform.position;
        GameManager.animator.SetBool("is_drinking", true);
        playerPet.transform.position = new Vector3(bowlPos.x + 1, bowlPos.y - (float)0.5, petPos.z);
        yield return new WaitForSecondsRealtime(4);
        // GameManager.animator.SetFloat("speed", petSpeed);
        GameManager.animator.SetBool("is_drinking", false);
        
        playerPet.transform.position = petPos;
    }

    IEnumerator Sleep() {
        GameObject playerPet = GameManager.playerPet;
        Vector3 petPos = playerPet.transform.position;
        GameManager.animator.SetFloat("speed", 0);
        GameObject dogBed = GameObject.Find("Dog Bed");
        Vector3 bedPos = dogBed.transform.position;
        GameManager.animator.SetBool("is_sleeping", true);
        playerPet.transform.position = new Vector3(bedPos.x, bedPos.y - (float)0.1, playerPet.transform.position.z);
        yield return new WaitForSecondsRealtime(4);
        GameManager.animator.SetBool("is_sleeping", false);
        
        playerPet.transform.position = petPos;
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