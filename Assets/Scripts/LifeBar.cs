using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeBar : MonoBehaviour
{

    [SerializeField]
    Image healthBar;

    [SerializeField]
    int maxHealth;

    public void SetHealth(int health, int maxHealth)
    {
        this.maxHealth = maxHealth;
        SetHealth(health);
    }

    public void SetHealth(int health)
    {
        healthBar.fillAmount = Mathf.Clamp01((float) health / maxHealth);
    }

    public void SetText(string text)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
