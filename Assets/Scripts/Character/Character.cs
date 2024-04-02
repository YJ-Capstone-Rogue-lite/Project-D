using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    public      EffectController    effectController    { get; private set; }
    [SerializeField]
    protected   CharStateData       charStateData;
    protected   float               m_health;
    protected   float               m_sheild;
    protected   float               m_movementSpeed;
    protected   float               m_protectionTime;
    
    protected virtual void Start()
    {
        charStateData = new();
        m_health = charStateData.health;
        m_health = charStateData.shield;
        m_movementSpeed = 0;
        m_protectionTime = 0;
    }

    protected virtual void Damaged(DamageData damageData)
    {
        m_sheild -= damageData.damage;
        m_health -= m_sheild<0 ? -m_sheild : 0;
        if(m_sheild <= 0) m_sheild = 0;
        effectController.Operation(damageData.effect);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DamageObject"))
        {
            Damaged(collision.GetComponent<DamageData>());
            return;
        }
    }
}
