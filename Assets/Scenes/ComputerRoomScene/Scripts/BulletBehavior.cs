using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public Rigidbody2D rb;
    public float range;

    private Vector2 startPoint;

    // Start is called before the first frame update
    void Start() {
        startPoint = rb.position;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector2 pos = rb.position;
        if (Vector2.Distance(startPoint, pos) >= range) {
            Hit();
        }
    }

    private void Hit() {
        // Place for any animations and destroys when bullet hits
        Destroy(gameObject);
    }
}
