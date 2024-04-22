using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float       m_speed;
    private float       m_range;
    private Vector3     m_targetPosition;

    public void Set(Vector3 targetPosition, float speed, float range)
    {
        m_speed = speed;
        m_range = range;
        m_targetPosition = targetPosition;
    }

    void Update()
    {
        transform.Translate((m_targetPosition - transform.position) * Time.deltaTime * m_speed);
        if(Vector3.Distance(transform.position, m_targetPosition) > m_range) Destroy(gameObject);
    }
}
