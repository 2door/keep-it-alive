using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour {

    public int maxHealth;
    public int enemyDamage;
    public float invulnerabilityTime;
    public GameEvent gameOverEvent;
    public GameEvent pauseEvent;
    public GameEvent unpauseEvent;
    public GameObject hpDisplay;

    private int hp;
    private bool damageLock = true;
    private bool gameOver = false;
    private bool paused = false;

    void Start() {
        hp = maxHealth;
        GameEventListener gameOverListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        gameOverListener.SetupListener(gameOverEvent, GameOver);
        GameEventListener pauseEventListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        pauseEventListener.SetupListener(pauseEvent, Pause);
        GameEventListener unpauseEventListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        unpauseEventListener.SetupListener(unpauseEvent, Unpause);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy") && damageLock && !gameOver && !paused) {
            StartCoroutine(TakeDamage());
        }
    }

    private IEnumerator TakeDamage() {
        damageLock = false;
        hp -= enemyDamage;
        if (hp <= 0) {
            hp = 0;
            gameOverEvent.Raise();
        }

        hpDisplay.GetComponent<Slider>().value = hp;

        // TODO Visual damage que
        yield return new WaitForSeconds(invulnerabilityTime);
        damageLock = true;
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