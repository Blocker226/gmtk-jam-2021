using UnityEngine;

/*
 * A hot, volcanic planet; the radiating heat seems to increase the ship's orbital speed after a X period of time
 */
public class PlanetVolcanic : PlanetBase
{
    [SerializeField]
    float countDown = 20.0f; // rate at which the speed increases
    //[SerializeField]
    //double secondsLeft;
    //public TextMeshProUGUI CountText;
    GameObject _player;
    //[SerializeField]
    //GameObject refText;
    //float  _baseOrbitSpeed = 1.0f;
    // [Range(1, 10)]
    // public int adjustForSpeed = 4;
    public int maxSpeed = 10;

    float _defaultCountDown;
    
    void SetDefaultSpeed()
    {
        //_baseOrbitSpeed = transform.localScale.x *_baseOrbitSpeed;
        _defaultCountDown = countDown;
    }

    void TimerRun()
    {
        // Timer System
        if (countDown > 0)
        {
            // effect the ship's orbit here
            countDown -= Time.deltaTime;
            _player.GetComponent<Player.Player>().orbitSpeed += Time.deltaTime * (maxSpeed / _defaultCountDown);
        }

        else
        {
            countDown = 0;
        }

        //double b = System.Math.Round(countDown, 1);
        //secondsLeft = b;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isShipAttached = true;
            //refText.SetActive(true);
            _player = collision.gameObject;
            orbitSpd = collision.gameObject.GetComponent<Player.Player>().orbitSpeed;
            //collision.gameObject.GetComponent<Player.Player>().orbitSpeed += collision.gameObject.GetComponent<Player.Player>().shipVelocity/10;
            //Debug.Log("Volc Orbit Speed: " + orbitSpd.ToString());
            // alternatively this can also be effected by the size of the planet
            orbitReg(collision);
            //camera_ON();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isShipAttached = false;
            //refText.SetActive(false);
            countDown = _defaultCountDown;
            //collision.gameObject.GetComponent<Player.Player>().launchSpeed = collision.gameObject.GetComponent<Player.Player>().orbitSpeed/LaunchControl;
            collision.gameObject.GetComponent<Player.Player>().orbitSpeed = orbitSpd;
            //camera_OFF();
            //Debug.Log("Volc Orbit Speed: " + orbitSpd.ToString());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        planetInit();
        SetDefaultSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isShipAttached)
        {
            TimerRun();
            //double b = System.Math.Round(countDown, 0);
            //CountText.text = b.ToString();
            if (_Tutorial)
            {
                _Tutorial.text = _TutorialWords;
            }
        }
        else
        {
            countDown = _defaultCountDown; // rst countdown
            if(_Tutorial)
            {
                _Tutorial.text = "";
            }
        }
    }
}
