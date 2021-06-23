using System;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        public Transform target;
        public float launchSpeed = 1;
        [Space]
        [SerializeField]
        float boostSpeed = 3;
        [SerializeField]
        public float orbitSpeed = 1;
        [SerializeField]
        float startSpeed = 0.75f;
        [Space]
        [SerializeField]
        float blackHoleSucc = 1;
        [SerializeField]
        public int fuel = 5;
        [SerializeField]
        float stopThreshold = 0.125f;
        [SerializeField]
        Ship ship;
        [SerializeField]
        AudioSource attached;

        float _defaultOrbitSpeed;
        int _defaultFuel;
        float _blackHoleDist;
        bool _launch;
        bool _boost;
        bool _stopped;
        
        Transform _prevPlanet;
        Rigidbody2D _rb;

        CinemachineVirtualCamera _playerCamera;
        GameManager _gameManager;
        RangeDisplay _rangeDisplay;
        LogbookDisplay _logbook;

        Vector3 _startPosition;
        Transform _startPlanet;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
            _playerCamera = GameObject.FindWithTag("Player Camera").GetComponent<CinemachineVirtualCamera>();
            _rangeDisplay = GetComponent<RangeDisplay>();
            _logbook = GetComponent<LogbookDisplay>();

            _startPosition = transform.position;
            _defaultOrbitSpeed = orbitSpeed;
            _defaultFuel = fuel;
            orbitSpeed = startSpeed;
            
            if (target)
            {
                _prevPlanet = target;
                _startPlanet = target;
                
                _logbook.AddPlanet(target);
                _rangeDisplay.DrawLines(target, _prevPlanet);
            }

            Assert.IsNotNull(_playerCamera);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                _gameManager.MainMenu();
                return;
            }
            
            if (Input.GetKeyUp(KeyCode.R) && target != _startPlanet)
            {
                ResetShip();
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && fuel > 0)
            {
                if (target)
                {
                    _launch = true;
                }
                else if (!_boost)
                {
                    _boost = true;
                }
            }
            else if (fuel == 0 && target && !target.GetComponent<PlanetFuel>() 
                     && !target.CompareTag("Finish"))
            {
                //Player stranded.
                orbitSpeed = Mathf.MoveTowards(orbitSpeed, 0.1f, Time.deltaTime / 2);
                if (_stopped) return;
                _stopped = true;
                _gameManager.Lose(1);
            }
        }

        void FixedUpdate()
        {
            if (!target)
            {
                if (_boost)
                {
                    BoostShip();
                }
                else if (!_stopped && _rb.velocity.magnitude < stopThreshold)
                {
                    //Player lost.
                    _stopped = true;
                    _rb.velocity = Vector2.zero;
                    _gameManager.Lose(0);
                }
            }

            if (!target) return;
            Quaternion q = Quaternion.AngleAxis(orbitSpeed, Vector3.forward);
            Vector3 targetPosition = target.position;
            Vector3 currentPosition = transform.position;
            float theta = Mathf.Atan2(
                currentPosition.x - targetPosition.x,
                targetPosition.y - currentPosition.y);
            _rb.MoveRotation(Quaternion.AngleAxis(Mathf.Rad2Deg * theta - 90, Vector3.forward));

            if (!target.CompareTag("Finish"))
            {
                _rb.MovePosition(q * (_rb.transform.position - targetPosition) + targetPosition);
            }
            else
            {
                _blackHoleDist = Mathf.MoveTowards(
                    _blackHoleDist, 0, blackHoleSucc * Time.fixedDeltaTime);
                _rb.MovePosition(q * (_rb.transform.position - targetPosition).normalized *
                                 _blackHoleDist + targetPosition);
            }


            if (!_launch) return;
            target.GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
            LaunchShip();
        }

        void LateUpdate()
        {
            if (target && _playerCamera.enabled)
            {
                _playerCamera.enabled = false;
                target.GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
            }
            else if (!_playerCamera.enabled && !target)
            {
                _playerCamera.enabled = true;
            }
        }

        void LaunchShip()
        {
            if (target == _startPlanet)
            {
                orbitSpeed = _defaultOrbitSpeed;
            }
            _launch = false;
            --fuel;
            _prevPlanet = target;
            target = null;
            _rb.velocity = Vector2.zero;
            ship.Ignite();
            ship.StopFire();
            _rb.AddForce(transform.up * launchSpeed, ForceMode2D.Impulse);
            _rangeDisplay.ClearLines();

            ShipLaunched?.Invoke(this, null);
        }

        void BoostShip()
        {
            --fuel;
            ship.Ignite(0.5f);
            _rb.AddForce(transform.up * boostSpeed, ForceMode2D.Impulse);
            _boost = false;
        }

        void ResetShip()
        {
            _gameManager.Restart();
            if (target)
            {
                target.GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
                ShipLaunched?.Invoke(this, null);
            }
            transform.position = _startPosition;
            orbitSpeed = startSpeed;
            fuel = _defaultFuel;
            target = _startPlanet;
            target.GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
            PlanetReached?.Invoke(this, null);
            ship.StopFire(true);
            _stopped = false;
            _logbook.ResetLine();
            _rangeDisplay.DrawLines(target, target);
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (target) return;
            if (!other.CompareTag("Planet") &&
                !other.CompareTag("Finish") || 
                other.transform == _prevPlanet) return;
        
            target = other.transform;
            attached.Play();
            
            PlanetReached?.Invoke(this, null);
            
            if (!other.CompareTag("Finish"))
            {
                if (other.GetComponent<PlanetVolcanic>())
                {
                    ship.StartFire();
                }
                
                _rangeDisplay.DrawLines(target, _prevPlanet);
                _logbook.AddPlanet(target);
            }
            else
            {
                _blackHoleDist = Vector2.Distance(target.position, transform.position);
                GetComponentInChildren<SpriteRenderer>()
                    .DOFade(0, 1)
                    .SetDelay(blackHoleSucc)
                    .SetEase(Ease.OutSine);
                _gameManager.Win();
            }
        }

        public event EventHandler PlanetReached;
        public event EventHandler ShipLaunched;
    }
}
