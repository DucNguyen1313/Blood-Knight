using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    [SerializeField] protected Vector3 moveSpeed = new Vector3(0, 75, 0);
    [SerializeField] protected float timeToFade = 1f;
    [SerializeField] protected float timer = 0f;
    protected Color startColor;
    
    [SerializeField] protected RectTransform textTransform;
    [SerializeField] protected TextMeshProUGUI textMeshPro;
    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    protected void UpdateText()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > timeToFade)
        {
            Destroy(gameObject);
        }

        float newApha = startColor.a * (1 - timer / timeToFade); 
        textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, newApha);
    }
}

