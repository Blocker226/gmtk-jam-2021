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
        // [SerializeField]
        // Material shipMaterial;
        [Space]
        [SerializeField]
        AudioSource engineNoise;

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
            while (i < burnDuration)
            {
                i += Time.deltaTime;
                fireEmission.rateOverTime = Mathf.Lerp(0, fireMax, i / burnDuration);
                //shipMaterial.SetColor(Color1, Color.Lerp(Color.white, Color.red, i / burnDuration));
                yield return null;
            }
        }
        
        public void StopFire()
        {
            StopCoroutine(BurnShip());
            ParticleSystem.EmissionModule fireEmission = fireFX.emission;
            fireEmission.rateOverTime = 0;
            
            fireFX.Stop(true);
        }

        void OnDestroy()
        {
            StopCoroutine(BurnShip());
        }
    }
}