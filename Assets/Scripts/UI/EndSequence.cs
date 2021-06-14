using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField]
        bool useCamera = true;
        [SerializeField]
        bool toggleEscapeKey;
        [SerializeField]
        UnityEvent onSequenceStop;
        
        CanvasGroup _canvas;
        CinemachineVirtualCamera _camera;
        
        void Start()
        {
            _canvas = GetComponentInChildren<CanvasGroup>();
            _camera = GetComponentInChildren<CinemachineVirtualCamera>();
        }

        public void StartSequence()
        {
            if (useCamera)
            {
                _camera.transform.position = GameObject.FindWithTag("Finish").transform.position;
                _camera.transform.Translate(0, 0, -10);
                _camera.enabled = true;

                _camera.transform.DOMoveY(-scrollLength, camSpeed).SetSpeedBased();
            }

            _canvas.blocksRaycasts = true;
            _canvas.DOFade(1, 2);
            credits.DOAnchorPosY(scrollLength, scrollSpeed).SetSpeedBased();
        }

        void StopSequence()
        {
            if (useCamera)
            {
                _camera.enabled = false;
                _camera.DORewind();
                _camera.DOKill();
            }

            _canvas.blocksRaycasts = false;
            _canvas.DOFade(0, 2);
            credits.DORewind();
            credits.DOKill();
            onSequenceStop.Invoke();
        }

        void Update()
        {
            if (!toggleEscapeKey) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopSequence();
            }
        }
    }
}