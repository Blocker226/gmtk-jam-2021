using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    [RequireComponent(typeof(AudioSource))]
    public class MainMenuButtons : MonoBehaviour
    {
        AudioSource _audio;

        void Start()
        {
            _audio = GetComponent<AudioSource>();
        }

        public void Play()
        {
            _audio.Play();
            SceneManager.LoadScene("Tutorial");
        }
        
        public void Quit()
        {
#if UNITY_EDITOR
            Debug.Log("Application Quitting!");
#endif
            _audio.Play();
            Application.Quit();
        }
    }
}