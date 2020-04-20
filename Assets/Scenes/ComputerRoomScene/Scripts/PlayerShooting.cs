using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public float projectileSpeed;
    public float fireRate;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public GameEvent gameOverEvent;
    public GameEvent pauseEvent;
    public GameEvent unpauseEvent;
    public AudioSource shootAudio;

    private bool shootLock = true;
    private bool gameOver = false;
    public bool paused = false;

    void Start() {
        GameEventListener gameOverListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        gameOverListener.SetupListener(gameOverEvent, GameOver);
        GameEventListener pauseEventListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        pauseEventListener.SetupListener(pauseEvent, Pause);
        GameEventListener unpauseEventListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        unpauseEventListener.SetupListener(unpauseEvent, Unpause);
    }

    void Update() {
        if (Input.GetMouseButton(0) && shootLock && !gameOver && !paused) {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot() {
        shootLock = false;
        GameObject projectileInstance = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        projectileInstance.GetComponent<Rigidbody2D>().AddForce(shootPoint.up * projectileSpeed, ForceMode2D.Impulse);

        shootAudio.PlayOneShot(shootAudio.clip);

        yield return new WaitForSeconds(fireRate);
        shootLock = true;
    }

    private void GameOver() {
        gameOver = true;
    }

    private void Pause() {
        paused = true;
    }

    private void Unpause() {
        paused = false;
    }
}
