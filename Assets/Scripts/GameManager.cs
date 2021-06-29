using Cinemachine;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    int currentLevel = 1;
    [SerializeField]
    string levelTitle = "Gargantua";
    [SerializeField]
    float startLength = 4;
    [SerializeField]
    float transitionLength = 2.5f;
    [SerializeField]
    string[] gameOverMessages;

    [Header("Canvas Groups")]
    [SerializeField]
    CanvasGroup startCanvas;
    [SerializeField]
    CanvasGroup gameCanvas;
    [SerializeField]
    CanvasGroup fadeCanvas;
    [SerializeField]
    CanvasGroup gameOverCanvas;
    
    [Space]
    [SerializeField]
    EndSequence endSequence;
    
    [SerializeField]
    CinemachineVirtualCamera gameOverCamera;
    
    [SerializeField]
    TextMeshProUGUI gameStartText;
    [SerializeField]
    TextMeshProUGUI gameOverText;

    [SerializeField]
    AudioClip clipDrifted;
    [SerializeField]
    AudioClip clipOutOfFuel;
    AudioSource _bgm;
    AudioSource _audio;

    void Start()
    {
        gameStartText.text = levelTitle + "\n" + currentLevel + "/4";

        if (Camera.main)
        {
            _bgm = Camera.main.GetComponentInChildren<AudioSource>();
        }

        _audio = GetComponent<AudioSource>();
        if (!gameOverCamera.Follow)
        {
            gameOverCamera.Follow = GameObject.FindWithTag("Player").transform;
        }
        
        startCanvas.DOFade(0, transitionLength)
            .SetDelay(startLength - transitionLength).SetEase(Ease.OutQuad);
    }

    public void Restart()
    {
        //DOTween.KillAll();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameCanvas.alpha = 1;
        gameOverCamera.enabled = false;
        gameOverCanvas.alpha = 0;

        if (_bgm.volume < 0.5f)
        {
            _bgm.DOKill();
            _bgm.DOFade(1, 2);
        }
        _audio.Play();
    }

    public void MainMenu()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Main Menu");
    }
    
    public void Win()
    {
        gameCanvas.DOFade(0, transitionLength / 2);
        if (currentLevel < 4 && SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            _bgm.DOFade(0.1f, transitionLength).SetDelay(4f);
            fadeCanvas.DOFade(1, transitionLength)
                .SetDelay(4.5f).SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                });
        }
        else if (endSequence)
        {
            endSequence.StartSequence();
        }
        Debug.Log("WINNER");
    }
    
    public void Lose(int message)
    {
        gameCanvas.DOFade(0, transitionLength / 2);
        gameOverCamera.enabled = true;
        gameOverText.text = gameOverMessages[message];
        gameOverCanvas.DOFade(1, transitionLength);
        Debug.Log("YOU LOS");

        _bgm.DOFade(0, 1).OnComplete(() =>
        {
            if(message == 0)
            {
                _audio.PlayOneShot(clipDrifted);
            }
            else
            {
                _audio.PlayOneShot(clipOutOfFuel, 0.5f);
            }
        });
    }
}
