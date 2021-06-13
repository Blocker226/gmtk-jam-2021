using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    int currentLevel = 1;
    [SerializeField]
    string levelTitle = "Gargantua";
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
    AudioSource audio_drifted;
    [SerializeField]
    AudioSource audio_outoffuel;
    [SerializeField]
    AudioSource bgm;

    void Start()
    {
        gameStartText.text = levelTitle + "\n" + currentLevel + "/4";
        startCanvas.DOFade(0, transitionLength / 2)
            .SetDelay(4).SetEase(Ease.OutQuad);

        if (!gameOverCamera.Follow)
        {
            gameOverCamera.Follow = GameObject.FindWithTag("Player").transform;
        }
    }

    public void Win()
    {
        gameCanvas.DOFade(0, transitionLength / 2);
        if (currentLevel < 4 && SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
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
        if(message == 0)
        {
            audio_drifted.Play();
        }
        else
        {
            audio_outoffuel.volume = 0.5f;
            audio_outoffuel.Play();
        }
        bgm.Stop();
    }
}
