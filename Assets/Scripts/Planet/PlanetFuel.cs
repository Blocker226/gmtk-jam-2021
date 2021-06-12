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
        _isShipAttached = true;
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().fuel++;
            Debug.Log("Fuel ++");
            // alternatively this can also be effected by the size of the planet
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
