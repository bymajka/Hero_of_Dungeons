using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private HeroScript hero;

    private void Start()
    {
        hero.OnHealthChanged += UpdateHealthbar;
    }

    private void UpdateHealthbar(int currentHealth, int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }

    private void OnDestroy()
    {
        hero.OnHealthChanged -= UpdateHealthbar; 
    }

}
