using System;
using UnityEngine;

[ExecuteInEditMode]
public class YCamera : MonoBehaviour
{
    private YCameraHelper _yCameraHelper;
    
    [SerializeField] private GameObject follow;
    private GameObject lookAt;

    private void Awake()
    {
        _yCameraHelper = GetComponentInChildren<YCameraHelper>();
        if(follow != null) _yCameraHelper.UpdateTarget(follow, follow);
    }

    private void Update()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            if (follow != null)
            {
                _yCameraHelper.UpdateTarget(follow, follow);
            }
        }
    }

    public void ZTarget()
    {
        _yCameraHelper.ZTarget(lookAt);
    }
}