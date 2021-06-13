using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapCam : MonoBehaviour
{
    [SerializeField]
    GameObject mapcam;
    [SerializeField]
    GameObject DoTweenRef;
    [SerializeField]
    KeyCode MapKey;
    CinemachineVirtualCamera _isEnabled;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Planet"))
        {
            mapcam.GetComponent<CinemachineTargetGroup>().AddMember(GO.transform, 1, 0);
            Debug.Log("Planet added");
        }
        _isEnabled = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(MapKey))
        {
            _isEnabled.enabled = true; // sets the cinemachine camera value
            GetComponent<MapIcon>().openKnobs(); // creates the knobs
        }

        else if (_isEnabled.enabled == true)
        {
            _isEnabled.enabled = false;
            GetComponent<MapIcon>().shutKnobs();
        }
    }
}
