using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Item
{
    public int              m_number    { get; set; }
    public string           m_name      { get; set; }
    public Sprite           m_img       { get; set; }
    public string           m_info      { get; set; }
    public System.Action    startAcion  { get; set; }
}
