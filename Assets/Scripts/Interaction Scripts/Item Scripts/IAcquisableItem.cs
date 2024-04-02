using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAcquisableItem : Item
{
    public void Acquir()
    {
        if(startAcion != null) startAcion();
    }
}
