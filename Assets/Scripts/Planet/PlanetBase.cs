using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
/*
 * A neutral planet; does not effect the ship in anyway
 */
public class PlanetBase : MonoBehaviour
{
    [SerializeField]
    [Range(1, 5)]
    protected int size = 1; // change this for planet size
    [SerializeField]
    protected bool _isShipAttached = false;
    [SerializeField]
    protected CinemachineVirtualCamera vcam;

    protected void planetInit()
    {
        size = (int)transform.localScale.x;   
        GetComponent<CircleCollider2D>().isTrigger = true; // Planet trigger
        vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        //transform.localScale += planetSize; // change planet size
        //planetSize = new Vector3(size, size, size);
        //GetComponent<CircleCollider2D>().radius = Mathf.Log(size, 3); // Planet trigger size
    }

    protected void camera_ON()
    {
        vcam.enabled = true;
    }

    protected void camera_OFF()
    {
        vcam.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isShipAttached = true;
            //Debug.Log("Ship attached");
            //camera_ON();
            //Debug.Log("Base cam enabled");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isShipAttached = false;
            //Debug.Log("Ship departed");
            //camera_OFF();
            //Debug.Log("cam disabled");
        }
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
