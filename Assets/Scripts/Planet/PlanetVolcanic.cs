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
    float countDown = 30.0f; // base value is 30s
    [SerializeField]
    double secondsLeft;
    public TextMeshProUGUI CountText;
    GameObject player;

    void timerRun()
    {
        // Timer System
        if (countDown > 0)
        {
            // effect the ship's orbit here
            countDown -= Time.deltaTime;
            player.gameObject.GetComponent<Player>().orbitSpeed += Time.deltaTime / 2;
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
            player = collision.gameObject;
            Debug.Log("Timer Started");
            // alternatively this can also be effected by the size of the planet
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isShipAttached = false;
            countDown = 30.0f;
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
        }
        else
        {
            countDown = 30.0f; // rst countdown
            
        }
    }
}
