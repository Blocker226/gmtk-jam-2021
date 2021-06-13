using Cinemachine;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        Transform target;
        [SerializeField]
        public float launchSpeed = 1;
        [SerializeField]
        public float orbitSpeed = 1;
        [SerializeField]
        public int fuel = 5;
        [SerializeField]
        float stopThreshold = 0.125f;
        [SerializeField]
        CinemachineVirtualCamera playerCamera;
        [SerializeField]
        UnityEvent onPlayerLost;
        [SerializeField]
        UnityEvent onPlayerStranded;

        float _defaultOrbitSpeed;
        bool _launch;
        bool _stopped;

        Transform _prevPlanet;
        Rigidbody2D _rb;

        RangeDisplay _rangeDisplay;
        LogbookDisplay _logbook;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rangeDisplay = GetComponent<RangeDisplay>();
            _logbook = GetComponent<LogbookDisplay>();
            _defaultOrbitSpeed = orbitSpeed;
        
            if (target)
            {
                _prevPlanet = target;
                _logbook.AddPlanet(target);
                _rangeDisplay.DrawLines(target, _prevPlanet);
            }

            Assert.IsNotNull(playerCamera);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && fuel > 0 && target)
            {
                _launch = true;
            }
            else if (fuel == 0 && target && !target.GetComponent<PlanetFuel>())
            {
                orbitSpeed = Mathf.MoveTowards(orbitSpeed, 0.1f, Time.deltaTime / 2);
                if (_stopped) return;
                _stopped = true;
                onPlayerStranded.Invoke();
            }
        }

        void FixedUpdate()
        {
            if (!_stopped && !target && _rb.velocity.magnitude < stopThreshold)
            {
                _stopped = true;
                _rb.velocity = Vector2.zero;
                onPlayerLost.Invoke();
            }
        
            if (!target) return;
            Quaternion q = Quaternion.AngleAxis(orbitSpeed, Vector3.forward);
            Vector3 targetPosition = target.position;
            Vector3 currentPosition = transform.position;
            //_rb.MoveRotation(_rb.transform.rotation * q);
            float theta = Mathf.Atan2(
                currentPosition.x - targetPosition.x,
                targetPosition.y - currentPosition.y);
            _rb.MoveRotation(Quaternion.AngleAxis(Mathf.Rad2Deg * theta - 90, Vector3.forward));
            _rb.MovePosition(q * (_rb.transform.position - targetPosition) + targetPosition);

        
            if (_launch)
            { 
                target.GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
                LaunchShip();
            }
        }

        void LateUpdate()
        {
            if (target && playerCamera.enabled)
            {
                playerCamera.enabled = false;
                target.GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
            }
            else if (!playerCamera.enabled && !target)
            {
                playerCamera.enabled = true;
            }
        }

        void LaunchShip()
        {
            _launch = false;
            --fuel;
            _prevPlanet = target;
            target = null;
            _rb.velocity = Vector2.zero;
            _rb.AddForce(transform.up * launchSpeed, ForceMode2D.Impulse);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (target) return;
            if (!other.CompareTag("Planet") &&
                !other.CompareTag("Finish") || 
                other.transform == _prevPlanet) return;
        
            target = other.transform;
            _rangeDisplay.DrawLines(target, _prevPlanet);
            _logbook.AddPlanet(target);
        }
    }
}
