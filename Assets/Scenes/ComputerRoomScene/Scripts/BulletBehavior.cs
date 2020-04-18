using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public Rigidbody2D rb;
    public float range;

    private Vector2 startPoint;

    void Start() {
        startPoint = rb.position;
    }

    void FixedUpdate() {
        Vector2 pos = rb.position;
        if (Vector2.Distance(startPoint, pos) >= range) {
            Hit();
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
}
