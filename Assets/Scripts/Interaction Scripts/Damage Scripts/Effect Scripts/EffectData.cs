using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EffectData
{
    public enum EffectType { BUFF, DEBUFF }

    public int                  number;
    public string               name;
    public int                  stack;
    public float                duriation;
    public bool                 isDuriationReset;
    public Action<float, int>   effectAction;
    public float                effectTick;
}

public static class EffectDatas
{
    public static readonly EffectData[] effectDatas;

    static EffectDatas()
    {
        effectDatas = new EffectData[]{
            new(){
                number = 1,
                name = "A",
                stack = 1,
                duriation = 10,
                isDuriationReset = false,
                effectAction = (d, s)=>{

                }
            }
        };
    }
}