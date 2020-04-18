using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public float range;
    public Rigidbody2D rb;
    public GameEvent gameOverEvent;

    private bool gameOver = false;
    private Vector2 startPoint;

    void Start() {
        startPoint = rb.position;
        GameEventListener gameOverListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        gameOverListener.SetupListener(gameOverEvent, GameOver);
    }

    void FixedUpdate() {
        if (!gameOver) {
            Vector2 pos = rb.position;
            if (Vector2.Distance(startPoint, pos) >= range) {
                Hit();
            }
        }
    }

    private void Hit() {
        // Place for any animations and destroys when bullet hits
        // TODO Add effect for bullet hit
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Player")) {
            Hit();
        }
    }

    private void GameOver() {
        gameOver = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }
}
