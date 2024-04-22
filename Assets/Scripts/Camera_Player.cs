using UnityEngine;

public class Camera_Player : MonoBehaviour
{
<<<<<<< HEAD
    public float cameraSpeed = 6.0f; // 카메라가 플레이어를 따라가는 속도
    public float maxDistanceFromPlayer = 3f; // 플레이어로부터 최대 거리
=======
    public float cameraSpeed = 5.0f; // 카메라가 플레이어를 따라가는 속도
    public float maxDistanceFromPlayer = 2f; // 플레이어로부터 최대 거리
>>>>>>> System

    public GameObject player; // 플레이어 GameObject
    Vector3 mousePos;
    Vector3 direction;
    float distance;
    private void Update()
    {
        // 마우스의 현재 위치를 월드 좌표로 변환
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -10f; // 카메라의 z 축 위치를 고정시킴

        // 플레이어와 마우스 사이의 거리를 계산
        direction = mousePos - player.transform.position;
        distance = direction.magnitude;

        
    }
<<<<<<< HEAD
    private void FixedUpdate()
=======
    private void LateUpdate()
>>>>>>> System
    {
        // 만약 플레이어와 마우스 사이의 거리가 최대 거리보다 크다면
        if (distance > maxDistanceFromPlayer)
        {
            // 플레이어와 마우스 사이의 방향으로 카메라를 이동
            Vector3 targetPosition = player.transform.position + direction.normalized * maxDistanceFromPlayer;
<<<<<<< HEAD
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime * 0.4f);
=======
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime * 0.1f);
>>>>>>> System
        }
        else
        {
            // 카메라가 플레이어와 마우스 사이에 위치할 때는 플레이어를 중심으로 이동하지 않음
            transform.position = Vector3.Lerp(transform.position, new Vector3(mousePos.x, mousePos.y, transform.position.z), cameraSpeed * Time.deltaTime * 0.1f);
        }
    }
}
