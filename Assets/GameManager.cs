﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static int score = 0;
    public static int currentHealth = 100;
    public Text scoreText;
    public Text gameOver;
    private AudioSource audioSource;
    public AudioClip scream;

    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        scoreText.text = "Score : "+score;

        if (currentHealth == 25)
        {
            audioSource.clip = scream;
            audioSource.Play();
        }

        if (currentHealth <= 0)
        {
            gameOver.enabled = true;
            Time.timeScale = 0;
        }

    }
}