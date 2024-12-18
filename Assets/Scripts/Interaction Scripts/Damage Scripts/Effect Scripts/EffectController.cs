using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    private             HashSet<Effect>     m_effectList;
    private             bool                m_isInvalidation;
    [SerializeField]    private float       m_invalidationTime;
    private             float               m_invalidationTimer;

    void Start()
    {
        m_effectList = new();
        m_isInvalidation = false;
        m_invalidationTimer = 0;
    }
    public void Operation(Character character, Effect effect)
    {
        if(m_isInvalidation && effect == null) return;

        StartCoroutine(IvalidationTimer());
        Effect temp;
        if(!m_effectList.TryGetValue(effect, out temp))
        {
            m_effectList.Add(effect);
            temp = effect;
            temp.Additional(character);
        }
        else if(temp.m_effectData.stack > temp.m_stack) temp.StackUp();
    }
    public IEnumerator IvalidationTimer()
    {
        m_isInvalidation = true;
        while(m_invalidationTimer < m_invalidationTime)
        {
            m_invalidationTimer += Time.fixedDeltaTime;
            yield return null;
        }
        m_isInvalidation = false;
    }
}
