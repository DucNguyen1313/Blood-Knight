using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportGem : MonoBehaviour
{
    [SerializeField] protected Vector2 targetPos;
    [SerializeField] protected Transform playerTransform;
    [SerializeField] protected string notification = "You need to collect more blood to use Teleport Gem!";

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("No Player");
            return;
        }

        playerTransform = player.GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float bloodCollection = PlayerPrefs.GetFloat("blood_collection");
        float requiredBlood = PlayerPrefs.GetFloat("required_blood");

        if(bloodCollection < requiredBlood)
        {

            Debug.Log("You need to collect more blood to use Teleport Gem!");
            CharacterEvents.characterTeleport.Invoke(gameObject, notification);
            return;
        }

        //Teleport to end scene
        playerTransform.position = new Vector3 (targetPos.x, targetPos.y, 0);

        UnlockNewLevel();   
    }


    protected void UnlockNewLevel()
    {

        Debug.Log("Unlock level: " + PlayerPrefs.GetInt("unlocked_level", 1));

        if (SceneManager.GetActiveScene().buildIndex < PlayerPrefs.GetInt("reached_index")) return;

        PlayerPrefs.SetInt("reached_index", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("unlocked_level", PlayerPrefs.GetInt("unlocked_level", 1) + 1);
        PlayerPrefs.Save();

        Debug.Log("Unlock level: " + PlayerPrefs.GetInt("unlocked_level", 1));
    }
        
}
