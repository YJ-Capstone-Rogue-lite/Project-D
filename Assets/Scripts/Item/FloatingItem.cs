using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float floatSpeed; // 위아래로 움직이는 속도
    public float floatAmplitude; // 움직이는 범위

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        floatSpeed = 10f;
        floatAmplitude = 0.01f;
    }

    void Update()
    {
        // 위아래로 움직이도록 포지션을 조정
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
