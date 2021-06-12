using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * A hot, volcanic planet; the radiating heat seems to increase the ship's orbital speed after a X period of time
 */
public class PlanetVolcanic : PlanetBase
{
    float countDown = 30.0f; // base value is 30s
    public Text disVariable;
    GameObject player;


    void timerRun()
    {
        // Timer System
        if (countDown > 0)
        {
            // effect the ship's orbit here
            countDown -= Time.deltaTime;
            player.gameObject.GetComponent<Player>().orbitSpeed+=Time.deltaTime/2;
        }

        else
        {
            Debug.Log("Timer set 0");
            countDown = 0;
        }

        double b = System.Math.Round(countDown, 1);

        GetComponent<Text>().text = b.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isShipAttached = true;
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            Debug.Log("Timer Started");
            // alternatively this can also be effected by the size of the planet
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isShipAttached = false;
        //countDown = 30.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        // initialise time to 0 for each of the planet
        // create a text to reflect the timer
        // timer is linearly related to the size of the planet
        // larger planets larger timer
        planetInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isShipAttached)
        {
            timerRun();
        }
        else
        {
            countDown = 30.0f; // rst countdown
        }
    }
}
