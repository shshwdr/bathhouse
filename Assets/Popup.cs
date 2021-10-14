using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using DG.Tweening;

public class Popup : MonoBehaviour
{
    public TMP_Text textLabel;
    static public void createPopup(string text,Transform parent)
    {
        var prefab = Resources.Load<GameObject>("UI/popup");
        var go = Instantiate(prefab, parent);
        go.GetComponent<Popup>().textLabel.text = text;
        go.transform.DOJump(go.transform.position, 1, 1, 1);
        Destroy(go, 1);


        var rotate180 = true;
        var yAxisOnly = false;
        var m_mainCamera = Camera.main;
        if (m_mainCamera == null) return;
        if (rotate180)
        {
            if (yAxisOnly)
            {
                go.transform.rotation = Quaternion.Euler(go.transform.rotation.eulerAngles.x, (m_mainCamera.transform.rotation.eulerAngles + 180f * Vector3.up).y, go.transform.rotation.eulerAngles.z);
            }
            else
            {
                go.transform.rotation = Quaternion.LookRotation(-m_mainCamera.transform.forward, m_mainCamera.transform.up);
            }
        }
        else
        {
            if (yAxisOnly)
            {
                go.transform.rotation = Quaternion.Euler(go.transform.rotation.eulerAngles.x, m_mainCamera.transform.rotation.eulerAngles.y, go.transform.rotation.eulerAngles.z);
            }
            else
            {
                go.transform.rotation = m_mainCamera.transform.rotation;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
