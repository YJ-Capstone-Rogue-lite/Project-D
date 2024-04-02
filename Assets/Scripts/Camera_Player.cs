using UnityEngine;

public class Camera_Player : MonoBehaviour
{
    public float cameraSpeed = 5.0f; // ī�޶� �÷��̾ ���󰡴� �ӵ�
    public float maxDistanceFromPlayer = 2f; // �÷��̾�κ��� �ִ� �Ÿ�

    public GameObject player; // �÷��̾� GameObject

    private void Update()
    {
        // ���콺�� ���� ��ġ�� ���� ��ǥ�� ��ȯ
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -10f; // ī�޶��� z �� ��ġ�� ������Ŵ

        // �÷��̾�� ���콺 ������ �Ÿ��� ���
        Vector3 direction = mousePos - player.transform.position;
        float distance = direction.magnitude;

        // ���� �÷��̾�� ���콺 ������ �Ÿ��� �ִ� �Ÿ����� ũ�ٸ�
        if (distance > maxDistanceFromPlayer)
        {
            // �÷��̾�� ���콺 ������ �������� ī�޶� �̵�
            Vector3 targetPosition = player.transform.position + direction.normalized * maxDistanceFromPlayer;
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        }
        else
        {
            // ī�޶� �÷��̾�� ���콺 ���̿� ��ġ�� ���� �÷��̾ �߽����� �̵����� ����
            transform.position = Vector3.Lerp(transform.position, new Vector3(mousePos.x, mousePos.y, transform.position.z), cameraSpeed * Time.deltaTime);
        }
    }
}
