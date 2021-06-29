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
    GameObject player;
    //[SerializeField]
    //GameObject refText;
    float  BaseOrbitSpeed = 1.0f;
    [Range(1, 10)]
    public int adjustForSpeed = 4;
    [Range(1, 10)]
    public int LaunchControl = 1;

    void setDefaultSpeed()
    {
        BaseOrbitSpeed = transform.localScale.x *BaseOrbitSpeed;
    }
    

    void TimerRun()
    {
        // Timer System
        if (countDown > 0)
        {
            // effect the ship's orbit here
            countDown -= Time.fixedDeltaTime;
            player.GetComponent<Player.Player>().orbitSpeed += Time.deltaTime/adjustForSpeed * BaseOrbitSpeed;
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
            player = collision.gameObject;
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
            countDown = 20.0f;
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
        setDefaultSpeed();
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
            countDown = 20.0f; // rst countdown
            if(_Tutorial)
            {
                _Tutorial.text = "";
            }
        }
    }
}
