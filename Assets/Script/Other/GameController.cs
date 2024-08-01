using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject playerDeathPanel;
    public GameObject gamePausePanel;
    public GameObject pauseButton;

    [SerializeField] protected Animator playerAnimator;

    [SerializeField] protected float requiredBlood = 3000f;

    protected bool firstCheck = true;

    void Awake()
    {
        PlayerPrefs.SetFloat("blood_collection", 0f);
        PlayerPrefs.SetFloat("required_blood", requiredBlood);
    }

    private void Start()
    {
        Time.timeScale = 1;
        this.playerDeathPanel.SetActive(false);
        this.gamePausePanel.SetActive(false);
        this.pauseButton.SetActive(true);

        firstCheck = true;
    }

    private void Update()
    {
        if (!playerAnimator.GetBool(AnimationString.isAlive) && firstCheck)
        {
            StartCoroutine("PlayerDeathEvent");
            firstCheck = false;
        }
    }


    public void ReplayGame()
    {
        Time.timeScale = 1;

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        this.gamePausePanel.SetActive(true);
        this.pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        this.gamePausePanel.SetActive(false);
        this.pauseButton.SetActive(true);
    }

    public void OpenHomeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("HomeScene");
    }

    private IEnumerator PlayerDeathEvent()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
        this.playerDeathPanel.SetActive(true);
    }
}
