using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isShipAttached = true;
            collision.gameObject.GetComponent<Player.Player>().fuel++;
            //Debug.Log("Fuel ++");
            //camera_ON();
            //Debug.Log("Fuel Cam Enabled");
            // alternatively this can also be effected by the size of the planet
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isShipAttached = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
