using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
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
    protected float orbitSpd;
    [SerializeField]
    protected TextMeshProUGUI _Tutorial;
    [SerializeField]
    [TextArea]
    protected string _TutorialWords;

    protected void planetInit()
    {
        size = (int)transform.localScale.x;   
        GetComponent<CircleCollider2D>().isTrigger = true; // Planet trigger
        vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        //transform.localScale += planetSize; // change planet size
        //planetSize = new Vector3(size, size, size);
        //GetComponent<CircleCollider2D>().radius = Mathf.Log(size, 3); // Planet trigger size
    }

    protected void orbitReg(Collider2D a)
    {
        a.gameObject.GetComponent<Player.Player>().orbitSpeed -= size / 12;
    }

    protected void camera_ON()
    {
        vcam.enabled = true;
        //vcam.m_Lens.OrthographicSize += size*2;
    }

    protected void camera_OFF()
    {
        vcam.enabled = false;
        //vcam.m_Lens.OrthographicSize -= size * 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isShipAttached = true;
            orbitSpd = collision.gameObject.GetComponent<Player.Player>().orbitSpeed;
            //Debug.Log("Base Orbit Speed: " + orbitSpd.ToString());
            orbitReg(collision);
            if (_Tutorial)
            {
                _Tutorial.text = _TutorialWords;
            }
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
            collision.gameObject.GetComponent<Player.Player>().orbitSpeed = orbitSpd;
            //Debug.Log("Base Orbit Speed: " + orbitSpd.ToString());
            if (_Tutorial)
            {
                _Tutorial.text = "";
            }
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
