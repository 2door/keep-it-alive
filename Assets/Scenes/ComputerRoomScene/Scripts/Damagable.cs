using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("PlayerProjectile")) {
            TakeDamage();
        }
    }

    private void TakeDamage() {
        Destroy(gameObject);
    }
}
