using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    public class MainMenuButtons : MonoBehaviour
    {
        AudioSource _audio;
        [SerializeField]
        GameObject quitButton;
        void Start()
        {
            _audio = GetComponent<AudioSource>();
#if UNITY_WEBGL
            Destroy(quitButton);
#endif
        }

        public void Play()
        {
            _audio.Play();
            DOTween.KillAll();
            SceneManager.LoadScene("Tutorial");
        }
        
        public void Quit()
        {
#if UNITY_EDITOR
            Debug.Log("Application Quitting!");
#endif
            _audio.Play();
            DOTween.KillAll();
            Application.Quit();
        }

        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }

        public void FadeCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.DOFade(canvasGroup.alpha < 0.5f ? 1 : 0, 2f);
        }
    }
}