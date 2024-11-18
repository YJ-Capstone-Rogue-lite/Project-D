using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TempAllDel : MonoBehaviour
{
    public GameObject[] targetGo;

    public void DelGo()
    {
        foreach(var go in targetGo) Destroy(go);
    }
}
