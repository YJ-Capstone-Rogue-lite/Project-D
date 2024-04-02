using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Item
{
    public int      number    { get; set; }
    public string   name      { get; set; }
    public Sprite   img       { get; set; }
    public string   info      { get; set; }
}
