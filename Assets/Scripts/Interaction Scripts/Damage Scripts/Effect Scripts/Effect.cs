using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public      float       m_duration;
    public      int         m_stack;
    public      EffectData  m_effectData;
    protected   Coroutine   m_coroutine;

    public void OnEnable()
    {
        m_duration = 0;
        m_stack = 0;
        m_coroutine = null;
    }

    public override bool Equals(object other)
    {
        if(other is not Effect) return false;
        return base.Equals(other);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(m_effectData.number, m_effectData.name);
    }

    private IEnumerator Run(Character character)
    {
        float effectTick = 0;
        while(m_duration < m_effectData.duriation)
        {
            m_duration += Time.fixedDeltaTime;
            effectTick += Time.fixedDeltaTime;
            if(effectTick > m_effectData.effectTick)
            {
                effectTick = 0;
                m_effectData.effectAction(character, m_duration, m_stack);
            }
            yield return null;
        }
        Termination();
    }
    public void Additional(Character character)
    {
        if(m_coroutine != null) m_coroutine = StartCoroutine(Run(character));
        if(m_effectData.isDuriationReset) m_effectData.duriation = 0;
        if(m_effectData.stack > m_stack) m_stack++;
    }
    public void Termination()
    {
        if(m_coroutine != null) StopCoroutine(m_coroutine);
    }
}
