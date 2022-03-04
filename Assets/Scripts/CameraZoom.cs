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
    public CinemachineConfiner cinemachineConfiner;
    private Transform playerTransform;
    private void Start()
    {
        playerTransform = vcam.m_LookAt;
    }
    public void Scroll(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();
        if (value > 0 && vcam.m_Lens.FieldOfView > 60)
        {
            if (vcam.Priority==0)
            {
                vcam.Priority = 2;
            }
            vcam.m_Lens.FieldOfView -= value*Time.deltaTime * zoomingSpeed;
            if (vcam.m_Lens.FieldOfView < 60)
            {
                vcam.m_Lens.FieldOfView = 60;
            }
        }
        else if (value < 0 && vcam.m_Lens.FieldOfView < 80)
        {
            vcam.m_Lens.FieldOfView -= value*Time.deltaTime * zoomingSpeed;
            if (vcam.m_Lens.FieldOfView > 80)
            {
                vcam.m_Lens.FieldOfView = 80;
                vcam.Priority= 0;
            }
        }
    }

    private void Update()
    {
        if (cinemachineConfiner.CameraWasDisplaced(vcam))
        {
            vcam.m_LookAt = null;
        }
        else
        {
            vcam.m_LookAt = playerTransform; 
        }
        
    }
}
