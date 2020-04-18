using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed;
    public float walkRate;
    public float minDistance;
    public float computerAttackRate;
    public GameEvent computerAttackEvent;

    private bool moveLock = true;
    private bool computerAttackLock = true;
    private bool atDestination = false;

    void FixedUpdate() {
        if (moveLock && !atDestination) {
            StartCoroutine(Move());
        }

        if (computerAttackLock && atDestination) {
            StartCoroutine(AttackComputer());
        }
    }

    private IEnumerator Move() {
        moveLock = false;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0), speed);
        
        if (Vector2.Distance(transform.position, new Vector2(0, 0)) <= minDistance) {
            atDestination = true;
        } else {
            atDestination = false;
        }

        yield return new WaitForSeconds(walkRate);
        moveLock = true;
    }

    private IEnumerator AttackComputer() {
        computerAttackLock = false;
        computerAttackEvent.Raise();
        yield return new WaitForSeconds(computerAttackRate);
        computerAttackLock = true;
    }
}
