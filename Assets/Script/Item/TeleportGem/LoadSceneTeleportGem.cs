using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LoadSceneTeleportGem : MonoBehaviour
{
    [SerializeField] protected string targetSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
