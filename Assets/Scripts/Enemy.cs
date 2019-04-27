using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    string Name;

    [SerializeField]
    int maxHealth;

    [SerializeField]
    int health;

    [SerializeField]
    LifeBar lifeBar;

    [SerializeField]
    Joke joke;

    private void OnEnable()
    {
        lifeBar.SetHealth(health, maxHealth);
        lifeBar.SetText(Name);
        joke.OnJoked += HandleJoke;
    }

    private void OnDisable()
    {
        joke.OnJoked -= HandleJoke;
    }

    private void HandleJoke(int score)
    {
        if (score > 0)
        {
            health = Mathf.Max(0, health - score);
            lifeBar.SetHealth(health);
        } else
        {
            health = Mathf.Max(0, health - 1);
            lifeBar.SetHealth(health);
        }        
    }
}
