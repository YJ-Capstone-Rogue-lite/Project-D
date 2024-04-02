using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EffectData
{
    public enum EffectType { BUFF, DEBUFF }
    public delegate IEnumerator EffectAction(ref float duriation, ref int stack);

    public int                  stack;
    public float                duriation;
    public bool                 isDuriationReset;
    public EffectAction         effectAction;
}
