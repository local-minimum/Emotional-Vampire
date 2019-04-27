using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    LifeBar myHealth;

    [SerializeField]
    Joke jokeUI;

    [SerializeField]
    int health = 10;

    [SerializeField]
    int maxHealth = 100;

    private void OnEnable()
    {
        myHealth.SetHealth(health, maxHealth);
        jokeUI.OnJoked += HandleJoked;
    }

    private void OnDisable()
    {
        jokeUI.OnJoked -= HandleJoked;
    }

    private void HandleJoked(int score)
    {
        health = Mathf.Clamp(health + score, 0, maxHealth);
        myHealth.SetHealth(health);
    }
}
