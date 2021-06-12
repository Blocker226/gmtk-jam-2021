using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * A neutral planet; does not effect the ship in anyway
 */
public class PlanetBase : MonoBehaviour
{
    [SerializeField]
    protected float size = 1.0f; // change this for planet size
    private Vector3 planetSize;
    [SerializeField]
    protected bool _isShipAttached = false;

    protected void planetInit()
    {
        planetSize = new Vector3(size, size, size);
        transform.localScale += planetSize; // change planet size
        GetComponent<CircleCollider2D>().isTrigger = true; // Planet trigger
        GetComponent<CircleCollider2D>().radius = Mathf.Log(size, 2); // Planet trigger size
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isShipAttached = true;
        Debug.Log("Ship attached");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isShipAttached = false;
        Debug.Log("Ship departed");
    }

    // Start is called before the first frame update
    void Start() // start methods are private
    {
        planetInit();
    }

    // Update is called once per frame
    void Update()
    {
        // neutral planet no need affect player
    }
}
