using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EffectData
{
    public enum Type { BUFF, DEBUFF }

    public int                  number;
    public string               name;
    public Type                 type;
    public int                  stack;
    public float                duriation;
    public bool                 isDuriationReset;
    public Action<Character, float, int>   effectAction;
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
                type = EffectData.Type.BUFF,
                stack = 1,
                duriation = 10,
                isDuriationReset = false,
                effectAction = (c, d, s)=>{
                    Debug.Log("Effect A On");
                    var temp = new DamageData();
                    temp.Set(10 * s);
                    c.Heal(temp);
                },
                effectTick = 1f
            },
            new(){
                number = 2,
                name = "B",
                type = EffectData.Type.DEBUFF,
                stack = 3,
                duriation = 1,
                isDuriationReset = false,
                effectAction = (c, d, s)=>{
                    Debug.Log("Effect B On");
                    var temp = new DamageData();
                    temp.Set(10 * s);
                    c.Damaged(temp);
                },
                effectTick = 0.2f
            }
        };
    }
}