using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float zoomingSpeed;
    private float currentFov;
    public void Scroll(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();
        if (value > 0 && vcam.m_Lens.FieldOfView > 60)
        {
            vcam.m_Lens.FieldOfView -= value*Time.deltaTime * zoomingSpeed;
            var aim = vcam.GetCinemachineComponent<CinemachinePOV>();
        }
        else if (value < 0 && vcam.m_Lens.FieldOfView < 120)
        {
            vcam.m_Lens.FieldOfView -= value*Time.deltaTime * zoomingSpeed;
        }

    }
}
