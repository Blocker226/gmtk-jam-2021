using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class EndSequence : MonoBehaviour
    {
        [SerializeField]
        float camSpeed = 1;
        [SerializeField]
        float scrollSpeed = 1;
        [SerializeField]
        float scrollLength = 3200;
        [SerializeField]
        RectTransform credits;
        CanvasGroup _canvas;
        CinemachineVirtualCamera _camera;
        
        void Start()
        {
            _canvas = GetComponentInChildren<CanvasGroup>();
            _camera = GetComponentInChildren<CinemachineVirtualCamera>();
        }

        public void StartSequence()
        {
            _camera.transform.position = GameObject.FindWithTag("Finish").transform.position;
            _camera.transform.Translate(0, 0, -10);
            _camera.enabled = true;

            _canvas.DOFade(1, 2f);
            _camera.transform.DOMoveY(-scrollLength, camSpeed).SetSpeedBased();
            credits.DOAnchorPosY(scrollLength, scrollSpeed).SetSpeedBased();
        }
    }
}