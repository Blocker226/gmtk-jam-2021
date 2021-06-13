using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Speedometer : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI text;

        CanvasGroup _canvas;
        Player.Player _player;
        Rigidbody2D _rb;
        void Start()
        {
            _canvas = GetComponent<CanvasGroup>();

            GameObject p = GameObject.FindWithTag("Player");
            if (!p) return;
            _player = p.GetComponent<Player.Player>();
            _rb = p.GetComponent<Rigidbody2D>();
            
            _player.ShipLaunched += PlayerOnShipLaunched;
            _player.PlanetReached += PlayerOnPlanetReached;
        }

        void PlayerOnPlanetReached(object sender, EventArgs e)
        {
            _canvas.DOFade(0, 1);
        }

        void PlayerOnShipLaunched(object sender, EventArgs e)
        {
            _canvas.DOFade(1, 1);
        }

        void LateUpdate()
        {
            if (!_player.target && _canvas.alpha > 0)
            {
                text.text = $"{_rb.velocity.magnitude:N1}km/s";
            }
        }
    }
}