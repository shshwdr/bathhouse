using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public Vector3 m_EulerAngleVelocity = new Vector3(0, 100, 0);
    public float scaleSpeed = -1; 
    public float scaleMax = 3;

    List<HPObjectController> enemies = new List<HPObjectController>();

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach(var enemy in enemies)
            {
                enemy.getDamage();
            }
        }
        if (scaleSpeed > 0)
        {

            float scaleValue = Mathf.PingPong(Time.time * scaleSpeed, scaleMax);
            transform.localScale = new Vector3(scaleValue, transform.localScale.y, scaleValue);
        }
    }
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        m_Rigidbody.MovePosition(transform.parent.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter");
        var hpObject = other.GetComponent<HPObjectController>();
        if (hpObject)
        {
            enemies.Add(hpObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var hpObject = other.GetComponent<HPObjectController>();
        if (hpObject)
        {
            enemies.Remove(hpObject);
        }
    }
}
