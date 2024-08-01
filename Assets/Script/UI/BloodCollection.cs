using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BloodCollection : MonoBehaviour
{
    [SerializeField] protected Slider bloodCollectionSlider;
    [SerializeField] protected TMP_Text bloodCollectionBarText;

    protected float requiredBlood;

    private void Awake()
    {
        bloodCollectionSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        requiredBlood = PlayerPrefs.GetFloat("required_blood");
    }

    private void Update()
    {
        float bloodCollection = Mathf.Min(PlayerPrefs.GetFloat("blood_collection"), requiredBlood);

        bloodCollectionSlider.value = bloodCollection / requiredBlood;
        bloodCollectionBarText.text = "Collection: " + bloodCollection + " / " + requiredBlood;
    }
}
