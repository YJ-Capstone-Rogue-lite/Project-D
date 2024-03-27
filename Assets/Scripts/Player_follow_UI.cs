using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_follow_UI : MonoBehaviour
{
    public Slider hpBar; // UI Slider�� �����ϱ� ���� public ����
    private Camera mainCamera; // �� ī�޶� ���� ����
    public Vector3 offset = new Vector3(0, 0, 0.8f); // �÷��̾�� UI ������ ������ ��

    void Start()
    {
        // �� ī�޶� ���� ���� ȹ��
        mainCamera = Camera.main;

        // hpBar�� �Ҵ���� ���� ��� ��� ��� �� ��ũ��Ʈ ��Ȱ��ȭ
        if (hpBar == null)
        {
            Debug.LogError("hpBar is not assigned!");
            enabled = false; // ��ũ��Ʈ ��Ȱ��ȭ
        }
    }

    void Update()
    {
        // hpBar�� mainCamera�� �Ҵ�Ǿ� �ִ��� Ȯ��
        if (hpBar != null && mainCamera != null)
        {
            // �÷��̾� ��ġ�� ���� UI�� ��ũ�� ��ǥ ���
            Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position + offset);

            // UI�� ��ġ�� ȭ�� ��� ���� �����ϵ��� ����
            screenPos.x = Mathf.Clamp(screenPos.x, 0, Screen.width);
            screenPos.y = Mathf.Clamp(screenPos.y, 0, Screen.height);

            // UI�� ��ġ ����
            hpBar.transform.position = screenPos;
        }
    }
}
