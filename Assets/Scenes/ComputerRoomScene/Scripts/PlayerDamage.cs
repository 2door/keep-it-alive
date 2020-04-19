using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

    public int maxHealth;
    public int enemyDamage;
    public float invulnerabilityTime;
    public GameEvent gameOverEvent;

    private int hp;
    private bool damageLock = true;
    private bool gameOver = false;

    void Start() {
        hp = maxHealth;
        GameEventListener gameOverListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        gameOverListener.SetupListener(gameOverEvent, GameOver);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy") && damageLock && !gameOver) {
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

        // TODO Visual damage que
        yield return new WaitForSeconds(invulnerabilityTime);

        damageLock = true;
    }

    private void GameOver() {
        gameOver = true;
    }
}