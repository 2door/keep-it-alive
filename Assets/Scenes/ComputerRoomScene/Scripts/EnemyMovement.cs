using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed;
    public float walkRate;
    public float minDistance;
    public float computerAttackRate;
    public GameEvent computerAttackEvent;
    public GameEvent gameOverEvent;
    public GameEvent pauseEvent;
    public GameEvent unpauseEvent;
    public Animator enemyAnimator;

    private bool moveLock = true;
    private bool computerAttackLock = true;
    private bool atDestination = false;
    private bool gameOver = false;
    private bool paused = false;
    private Rigidbody2D rb;

    void Start() {
        enemyAnimator.SetBool("Walk", true);

        GameEventListener gameOverListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        gameOverListener.SetupListener(gameOverEvent, GameOver);
        GameEventListener pauseEventListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        pauseEventListener.SetupListener(pauseEvent, Pause);
        GameEventListener unpauseEventListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        unpauseEventListener.SetupListener(unpauseEvent, Unpause);

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (!gameOver && !paused) {
            if (moveLock && !atDestination) {
                StartCoroutine(Move());
            }

            if (computerAttackLock && atDestination) {
                enemyAnimator.SetBool("Walk", false);
                enemyAnimator.SetBool("Attack", true);
                StartCoroutine(AttackComputer());
            }
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

        Vector2 shootDirection = new Vector2(.0f, .0f) - rb.position;
        float shootAngle = (Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg) - 90.0f;
        rb.rotation = shootAngle;
    }

    private IEnumerator AttackComputer() {
        computerAttackLock = false;
        computerAttackEvent.Raise();
        yield return new WaitForSeconds(computerAttackRate);
        computerAttackLock = true;
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
