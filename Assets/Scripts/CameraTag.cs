using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraTag : MonoBehaviour
{
    [SerializeField]
    string targetTag;

    CinemachineVirtualCamera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
        if (!_cam.Follow && GameObject.FindWithTag(targetTag))
        {
            _cam.Follow = GameObject.FindWithTag(targetTag).transform;
        }
    }
}
