using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rotate180 = true;
        var yAxisOnly = false;
        var m_mainCamera = Camera.main;
        if (m_mainCamera == null) return;
        if (rotate180)
        {
            if (yAxisOnly)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, (m_mainCamera.transform.rotation.eulerAngles + 180f * Vector3.up).y, transform.rotation.eulerAngles.z);
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(-m_mainCamera.transform.forward, m_mainCamera.transform.up);
            }
        }
        else
        {
            if (yAxisOnly)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, m_mainCamera.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
            else
            {
                transform.rotation = m_mainCamera.transform.rotation;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
