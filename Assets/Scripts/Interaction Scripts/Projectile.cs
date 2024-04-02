using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private DamageData  m_damageData;
    private float       m_speed;
    private float       m_range;
    private Vector3     m_targetPosition;

    public void Set(DamageData damageData, Vector3 targetPosition, float speed, float range, float radius)
    {
        m_damageData = damageData;
        m_speed = speed;
        m_range = range;

        var c2d = gameObject.GetComponent<CircleCollider2D>();
        c2d.radius = radius;
        c2d.excludeLayers = damageData.ignoreLayer;
        m_targetPosition = targetPosition;
    }

    void Update()
    {
        transform.Translate((m_targetPosition - transform.position) * Time.deltaTime * m_speed);
        if(Vector3.Distance(transform.position, m_targetPosition) > m_range) Destroy(gameObject);
    }
}
