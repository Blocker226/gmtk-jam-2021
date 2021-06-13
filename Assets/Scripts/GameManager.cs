using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    float transitionLength = 2.5f;
    [SerializeField]
    string[] gameOverMessages;

    [SerializeField]
    CinemachineVirtualCamera gameOverCamera;
    [SerializeField]
    CanvasGroup gameOverCanvas;
    [SerializeField]
    TextMeshProUGUI gameOverText;

    public void Win()
    {
        
    }
    
    public void Lose(int message)
    {
        gameOverCamera.enabled = true;
        gameOverText.text = gameOverMessages[message];
        gameOverCanvas.DOFade(1, transitionLength);
        Debug.Log("YOU LOS");
    }
}
