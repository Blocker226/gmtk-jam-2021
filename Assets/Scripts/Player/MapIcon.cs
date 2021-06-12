using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapIcon : MonoBehaviour
{
    //public Transform Object;
    public Transform CanvasManager;
    public GameObject GameObj;
    Camera _cam;
    [SerializeField]
    List<GameObject> knobs = new List<GameObject>();

    public void openKnobs()
    {
        if (knobs.Count == 0)
        {
            foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Planet"))
            {
                Vector3 pos = GO.transform.position;
                //Vector3 final = _cam.WorldToScreenPoint(pos);
                knobs.Add(Instantiate(GameObj, pos, Quaternion.identity, CanvasManager));
            }
        }
        else
        {
            foreach (GameObject Obj in knobs)
            {
                Obj.SetActive(true);
            }
        }
    }

    public void shutKnobs()
    {
        if (knobs.Count > 0)
        {
            foreach (GameObject Obj in knobs)
            {
                Obj.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
