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
        [Space]
        [SerializeField]
        AudioSource engineNoise;
        
        public void Ignite()
        {
            StartCoroutine(EngineBurn());
        }

        IEnumerator EngineBurn()
        {
            engineNoise.Play();
            engineFX.Play(true);
            yield return new WaitForSeconds(burnDuration);
            engineFX.Stop(true);
            engineNoise.Stop();
        }

        public void StartFire()
        {
            fireFX.Play(true);
        }

        public void StopFire()
        {
            fireFX.Stop(true);
        }
    }
}