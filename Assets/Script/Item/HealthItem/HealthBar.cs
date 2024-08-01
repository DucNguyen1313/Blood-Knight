using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] protected Slider healthSlider;
    [SerializeField] protected TMP_Text healthBarText;

    Damageable playerDamageable;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
   

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.Log("No Player");
            return;
        }

        playerDamageable = player.GetComponent<Damageable>();
    }

    void Start()
    {
        healthSlider.value = Mathf.Max(playerDamageable.Health, 0f) / playerDamageable.MaxHealth;
        healthBarText.text = "HP: " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChange);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChange);
    }

    protected void OnPlayerHealthChange(float newHealth, float maxHealth)
    {
        newHealth = Mathf.Max(newHealth, 0f);

        healthSlider.value = newHealth / maxHealth;
        healthBarText.text = "HP: " + newHealth + " / " + maxHealth;
    }
}
