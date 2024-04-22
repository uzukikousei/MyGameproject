using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // 切割进度条视角
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }
    [SerializeField]private Mode mode;
    private void Update()
    {
        switch (mode)
        {
                case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
                case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.forward;
                transform.LookAt(transform.position + dirFromCamera);
                break;
                case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
                case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
