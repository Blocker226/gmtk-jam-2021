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
    [SerializeField]
    List<GameObject> planetList = new List<GameObject>();

    public void openKnobs()
    {
        // If planets dont exist
        if (knobs.Count == 0)
        {
            foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Planet"))
            {
                planetList.Add(GO);
                Vector3 pos = GO.transform.position;
                Vector3 final = _cam.WorldToScreenPoint(pos);
                knobs.Add(Instantiate(GameObj, final, Quaternion.identity, CanvasManager));
            }

            foreach(GameObject GO in GameObject.FindGameObjectsWithTag("Finish"))
            {
                planetList.Add(GO);
                Vector3 pos = GO.transform.position;
                Vector3 final = _cam.WorldToScreenPoint(pos);
                knobs.Add(Instantiate(GameObj, final, Quaternion.identity, CanvasManager));
            }
        }
        else
        {
            for(int i = 0; i<knobs.Count; i++)
            {
                Vector3 pos = planetList[i].transform.position;
                Vector3 final = _cam.WorldToScreenPoint(pos);
                knobs[i].transform.position = final;
                knobs[i].SetActive(true);
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
