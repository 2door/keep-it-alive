﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public Rigidbody2D rb;
    public Camera mainCam;
    public GameEvent gameOverEvent;

    private Vector2 movement;
    private Vector2 mousePos;
    private bool moveLock = true;
    private bool gameOver = false;

    void Start() {
        GameEventListener gameOverListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        gameOverListener.SetupListener(gameOverEvent, GameOver);
    }

    void Update() {
        if (moveLock && !gameOver) {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + (movement * speed * Time.fixedDeltaTime));

        Vector2 shootDirection = mousePos - rb.position;
        float shootAngle = (Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg) - 90.0f;
        
        rb.rotation = shootAngle;
    }

    private void GameOver() {
        movement = new Vector2(0f, 0f);
        gameOver = true;
    }
}
