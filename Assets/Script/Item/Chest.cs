using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    [SerializeField] protected GameObject healthItemPrefab;
    [SerializeField] protected GameObject spawnPoint;
    [SerializeField] protected List<GameObject> guards;

    [SerializeField] protected float bloodReward = 250f;
    protected bool firstCheck = true;

    private void Start()
    {
        animator.SetBool(AnimationString.isOpen, false);

        firstCheck = true;
    }

    private void Update()
    {
        GetingReward();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsAllGuardsDie()) return;

        Debug.Log("Player get a chest");
        animator.SetBool(AnimationString.isOpen, true);
    }

    protected void GetingReward()
    {
        if (!firstCheck) return;
        if (!animator.GetBool(AnimationString.isOpen)) return;

        Debug.Log("wow");
        StartCoroutine("GetReward");
        firstCheck = false;
    }


    private IEnumerator GetReward()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject healthItem = Instantiate(healthItemPrefab, spawnPoint.transform.position, healthItemPrefab.transform.rotation);

        CharacterEvents.characterTakeChest.Invoke(gameObject, bloodReward);
        Debug.Log("Invoke blood reward complete");

        float bloodCollection = PlayerPrefs.GetFloat("blood_collection");
        bloodCollection += bloodReward;
        PlayerPrefs.SetFloat("blood_collection", bloodCollection);
    }


    protected bool IsAllGuardsDie()
    {
        foreach (GameObject guard in guards)
        {
            if (guard != null) return false;
        }

        return true;

    }

}
