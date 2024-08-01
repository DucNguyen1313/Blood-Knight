using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject damageText;
    public GameObject heathText;
    public GameObject notificationText;
    public GameObject bloodRewardText;

    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
        CharacterEvents.characterTeleport += CharacterTeleport;
        CharacterEvents.characterTakeChest += CharacterTakeChest;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
        CharacterEvents.characterTeleport -= CharacterTeleport;
        CharacterEvents.characterTakeChest -= CharacterTakeChest;
    }

    public void CharacterTookDamage(GameObject character, float damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageText, spawnPosition, quaternion.identity,
            gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = "-" + damageReceived.ToString();
        
    }
    
    public void CharacterHealed(GameObject character, float healthRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(heathText, spawnPosition, quaternion.identity,
            gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = "+" + healthRestored.ToString();
    }

    public void CharacterTeleport(GameObject character, String notification)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(notificationText, spawnPosition, quaternion.identity,
            gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = notification;
    }

    public void CharacterTakeChest(GameObject character, float bloodReward)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(bloodRewardText, spawnPosition, quaternion.identity,
            gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = "+" + bloodReward.ToString();
    }

}
