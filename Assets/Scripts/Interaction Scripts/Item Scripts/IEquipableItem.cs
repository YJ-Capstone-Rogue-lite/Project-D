using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipableItem<T> : Item
{
    public T Acquir()
    {
        if(startAcion != null) startAcion();
        return (T)this;
    }
}
