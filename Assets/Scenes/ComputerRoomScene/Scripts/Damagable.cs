using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

    public GameEvent deathEvent;

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("PlayerProjectile")) {
            TakeDamage();
        }
    }

    private void TakeDamage() {
        deathEvent.Raise();
        Destroy(gameObject);
    }
}
