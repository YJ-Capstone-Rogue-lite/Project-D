using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour, IEquipableItem<Effect>
{
    public      float       m_duration;
    public      int         m_stack;
    public      EffectData  m_effectData;
    protected   Coroutine   m_coroutine;
    protected   Coroutine   m_effectCoroutine;

    public      int         m_number        { get; set; }
    public      string      m_name          { get; set; }
    public      Sprite      m_img           { get; set; }
    public      string      m_info          { get; set; }
    public      Action      startAcion      { get; set; }

    public Effect(Effect effect)
    {
        m_duration = 0;
        m_stack = 0;
        m_coroutine = null;
        m_effectData = effect.m_effectData;
    }

    public override bool Equals(object other)
    {
        if(other is not Effect) return false;
        return base.Equals(other);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(m_number, m_name);
    }

    private IEnumerator Run()
    {
        m_effectCoroutine = StartCoroutine(m_effectData.effectAction(ref m_duration, ref m_stack));
        while(m_duration < m_effectData.duriation)
        {
            m_duration += Time.fixedDeltaTime;
            yield return null;
        }
        Termination();
    }
    public void Additional()
    {
        if(m_coroutine != null) m_coroutine = StartCoroutine(Run());
        if(m_effectData.isDuriationReset) m_effectData.duriation = 0;
        if(m_effectData.stack > m_stack) m_stack++;
    }
    public void Termination()
    {
        if(m_effectCoroutine != null) StopCoroutine(m_effectCoroutine);
        if(m_coroutine != null) StopCoroutine(m_coroutine);
    }
}
