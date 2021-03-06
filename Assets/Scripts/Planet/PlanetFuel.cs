using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
 * A carbon-filled planet; Will prove to be useful when refuelling your lifecraft...
 */
public class PlanetFuel : PlanetBase
{
    // Start is called before the first frame update
    void Start()
    {
        // player.getcomponent some value, increase fuel by X units.
        // should I tie the fuel increment to the size of the planet?
        planetInit();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            orbitSpd = collision.gameObject.GetComponent<Player.Player>().orbitSpeed;
            //Debug.Log("Fuel Orbit Speed: " + orbitSpd.ToString());
            _isShipAttached = true;
            if(collision.gameObject.GetComponent<Player.Player>().fuel <= 3)
            {
                collision.gameObject.GetComponent<Player.Player>().fuel += 2;
            }
            else
            {
                collision.gameObject.GetComponent<Player.Player>().fuel++;
            }
            //Debug.Log("Fuel ++");
            //camera_ON();
            //Debug.Log("Fuel Cam Enabled");
            // alternatively this can also be effected by the size of the planet
            //collision.gameObject.GetComponent<Player.Player>().orbitSpeed /= (float)size;
            orbitReg(collision);
        }
        if (_Tutorial)
        {
            _Tutorial.text = _TutorialWords;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isShipAttached = false;
            collision.gameObject.GetComponent<Player.Player>().orbitSpeed = orbitSpd;
            //Debug.Log("Fuel Orbit Speed: " + orbitSpd.ToString());
            //camera_OFF();
            if (_Tutorial)
            {
                _Tutorial.text = "";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
