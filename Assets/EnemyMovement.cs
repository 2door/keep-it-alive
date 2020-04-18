using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed;
    public float walkRate;

    private bool moveLock = true;

    // Update is called once per frame
    void FixedUpdate() {
        if (moveLock) {
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move() {
        moveLock = false;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0), speed);
        yield return new WaitForSeconds(walkRate);
        moveLock = true;
    }
}
