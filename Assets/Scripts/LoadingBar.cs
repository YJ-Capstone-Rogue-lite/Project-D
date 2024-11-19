using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Image progessBar;

    void Update()
    {
        progessBar.fillAmount = GameManager.loadProgress;
        Debug.Log(GameManager.loadProgress);
    }
}
