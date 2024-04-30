using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "FloorItemRateTable", menuName = "Scriptable Object/FloorItemRateTable")]
public class FloorItemTable : ScriptableObject
{
    [Serializable]
    public class Data
    {
        //public ItemData.RatingType ratingType;
        public float rate;
    }

    public Data[] datas;

   // public Dictionary<ItemData.RatingType, float> ratingMap = new Dictionary<ItemData.RatingType, float>();

    void Awake()
    {
        //foreach(var item in datas) ratingMap.Add(item.ratingType, item.rate);
    }
}
