using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_follow_UI : MonoBehaviour
{
    public Slider hpBar; // UI Slider를 참조하기 위한 public 변수
    private Camera mainCamera; // 주 카메라에 대한 참조
    public Vector3 offset = new Vector3(0, 0, 0.8f); // 플레이어와 UI 사이의 오프셋 값

    void Start()
    {
        // 주 카메라에 대한 참조 획득
        mainCamera = Camera.main;

        // hpBar가 할당되지 않은 경우 경고 출력 및 스크립트 비활성화
        if (hpBar == null)
        {
            Debug.LogError("hpBar is not assigned!");
            enabled = false; // 스크립트 비활성화
        }
    }

    void Update()
    {
        // hpBar와 mainCamera가 할당되어 있는지 확인
        if (hpBar != null && mainCamera != null)
        {
            // 플레이어 위치에 따른 UI의 스크린 좌표 계산
            Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position + offset);

            // UI의 위치를 화면 경계 내에 유지하도록 제한
            screenPos.x = Mathf.Clamp(screenPos.x, 0, Screen.width);
            screenPos.y = Mathf.Clamp(screenPos.y, 0, Screen.height);

            // UI의 위치 설정
            hpBar.transform.position = screenPos;
        }
    }
}
