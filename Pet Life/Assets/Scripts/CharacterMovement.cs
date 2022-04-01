using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private GameManager gameManager;
    private float speed = 1f;
    private static int direction = -1; // -1 = left, 1 = right
    private float idlePauseTime;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize vars
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        idlePauseTime = Random.Range(3, 10);
        StartCoroutine(Idle());
    }

    // Update is called once per frame
    async void Update()
    {
        if (!(GameManager.animator.GetBool("is_eating") || GameManager.animator.GetBool("is_drinking") || GameManager.animator.GetBool("is_sleeping") || GameManager.animator.GetBool("is_jumping") || GameManager.busy)) { // only move if not in another animation
            if (Time.time > idlePauseTime) {
                StartCoroutine(Idle());
                idlePauseTime = Time.time + Random.Range(3, 10);
            }
            GameManager.animator.SetFloat("speed", speed);
            if (direction == -1) {
                GameManager.playerPet.transform.Translate(Vector3.right * Time.deltaTime * speed * direction);
            } else if (direction == 1) {
                GameManager.playerPet.transform.Translate(Vector3.right * Time.deltaTime * speed * direction);
            }
            if (direction == 1 && GameManager.playerPet.transform.position.x > 5) {
                direction = -1;
                GameManager.playerPet.transform.localScale = new Vector3(1, 1, 1);
            }
            if (direction == -1 && GameManager.playerPet.transform.position.x < -5) {
                direction = 1;
                GameManager.playerPet.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    IEnumerator Idle() {
        speed = 0f;
        yield return new WaitForSecondsRealtime(Random.Range(2, 5));
        speed = 1f;
    }
}
