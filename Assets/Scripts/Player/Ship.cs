using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class Ship : MonoBehaviour
    {
        [SerializeField]
        float burnDuration = 2;
        [SerializeField]
        ParticleSystem engineFX;
        [Space]
        [SerializeField]
        ParticleSystem fireFX;
        [SerializeField]
        float fireMax;
        [SerializeField]
        float fireDuration;
        [SerializeField]
        Color fireColour = Color.red;
        [Space]
        [SerializeField]
        AudioSource engineNoise;

        SpriteRenderer[] _spriteRenderers;

        void Start()
        {
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        public void Ignite(float seconds = 0)
        {
            StartCoroutine(EngineBurn(seconds > 0 ? seconds : burnDuration));
        }

        IEnumerator EngineBurn(float seconds)
        {
            engineNoise.Play();
            engineFX.Play(true);
            yield return new WaitForSeconds(seconds);
            engineFX.Stop(true);
            engineNoise.Stop();
        }

        public void StartFire()
        {
            fireFX.Play(true);
            StartCoroutine(BurnShip());
        }

        IEnumerator BurnShip()
        {
            ParticleSystem.EmissionModule fireEmission = fireFX.emission;
            float i = 0;
            while (i < fireDuration)
            {
                i += Time.deltaTime;
                fireEmission.rateOverTime = Mathf.Lerp(0, fireMax, i / fireDuration);

                foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
                {
                    spriteRenderer.color = Color.Lerp(Color.white, fireColour, i / fireDuration);
                }
                
                yield return null;
            }
        }
        
        public void StopFire()
        {
            StopCoroutine(BurnShip());
            ParticleSystem.EmissionModule fireEmission = fireFX.emission;
            fireEmission.rateOverTime = 0;
            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = Color.white;
            }
            fireFX.Stop(true);
        }

        void OnDestroy()
        {
            StopCoroutine(BurnShip());
        }
    }
}