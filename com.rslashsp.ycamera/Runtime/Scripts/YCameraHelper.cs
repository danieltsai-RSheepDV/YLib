using Cinemachine;
using UnityEngine;

public class YCameraHelper : MonoBehaviour
{
    [SerializeField] private GameObject NorCam;
    [SerializeField] private GameObject ZTargetCam;

    // Start is called before the first frame update
    void Start()
    {
        NorCam.SetActive(true);
        ZTargetCam.SetActive(false);
    }

    public void UpdateTarget(GameObject g, GameObject l)
    {
        NorCam.GetComponent<CinemachineVirtualCamera>().Follow = g.transform;
        NorCam.GetComponent<CinemachineVirtualCamera>().LookAt = l.transform;
        ZTargetCam.GetComponent<CinemachineVirtualCamera>().Follow = g.transform;
        ZTargetCam.GetComponent<CinemachineVirtualCamera>().LookAt = l.transform;
    }

    public void ZTarget(GameObject g)
    {
        if (g != null)
        {
            ZTargetCam.GetComponent<CinemachineVirtualCamera>().LookAt = g.transform;
            ZTargetCam.SetActive(true);
            NorCam.SetActive(false);
        }
    }

    public void Normal()
    {
        ZTargetCam.SetActive(false);
        NorCam.SetActive(true);
    }
}
