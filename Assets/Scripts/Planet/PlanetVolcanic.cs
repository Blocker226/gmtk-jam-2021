using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
 * A hot, volcanic planet; the radiating heat seems to increase the ship's orbital speed after a X period of time
 */
public class PlanetVolcanic : PlanetBase
{
    float countDown = 20.0f; // base value is 20s
    [SerializeField]
    double secondsLeft;
    public TextMeshProUGUI CountText;
    GameObject player;
    [SerializeField]
    GameObject refText;

    void timerRun()
    {
        // Timer System
        if (countDown > 0)
        {
            // effect the ship's orbit here
            countDown -= Time.deltaTime;
            player.gameObject.GetComponent<Player.Player>().orbitSpeed += (Time.deltaTime / 12 * size * 3/2);
        }

        else
        {
            countDown = 0;
        }

        double b = System.Math.Round(countDown, 1);

        secondsLeft = b;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isShipAttached = true;
            refText.SetActive(true);
            player = collision.gameObject;
            orbitSpd = collision.gameObject.GetComponent<Player.Player>().orbitSpeed;
            //Debug.Log("Volc Orbit Speed: " + orbitSpd.ToString());
            // alternatively this can also be effected by the size of the planet
            orbitReg(collision);
            camera_ON();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isShipAttached = false;
            refText.SetActive(false);
            countDown = 20.0f;
            collision.gameObject.GetComponent<Player.Player>().orbitSpeed = orbitSpd;
            camera_OFF();
            //Debug.Log("Volc Orbit Speed: " + orbitSpd.ToString());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        planetInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isShipAttached)
        {
            timerRun();
            double b = System.Math.Round(countDown, 0);
            CountText.text = b.ToString();
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
