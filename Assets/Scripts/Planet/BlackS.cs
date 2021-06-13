using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackS : MonoBehaviour
{
    [SerializeField]
    GameObject Newb;
    [SerializeField]
    bool launched = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!launched && Input.GetKey(KeyCode.Space))
        {
            Newb.GetComponent<Player.Player>().orbitSpeed = 2.0f;
            launched = true;
        }

        else if (!launched)
        {
            Newb.GetComponent<Player.Player>().orbitSpeed = .75f;
        }
    }
}
