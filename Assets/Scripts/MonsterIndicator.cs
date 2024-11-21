using UnityEngine;
using UnityEngine.UI;

public class MonsterIndicator : MonoBehaviour
{
    public Camera main_camera;

    private Vector2 camera_vec; // Player 벡터
    private Vector2 camera_screen_vec; // Screen 상의 Player 벡터

    private float angleRU; // 우측상단 대각선 각도

    private float screenHalfHeight = 0.5f; // 화면 높이 절반
    private float screenHalfWidth = 0.5f; // 화면 폭 절반

    void Start()
    {
        camera_screen_vec = new Vector2(Screen.width / 2, Screen.height / 2);
        camera_vec = Camera.main.WorldToViewportPoint(transform.position); // 0f ~ 1f

        Vector2 vecRU = new Vector2(Screen.width, Screen.height) - camera_screen_vec;
        vecRU = vecRU.normalized;
        angleRU = Vector2.Angle(vecRU, Vector2.up);
    }

    public void DrawIndicator(GameObject obj, GameObject indicatorObj)
    {
        Image indicator = indicatorObj.GetComponent<Image>();

        Vector2 objScreenVec = Camera.main.WorldToScreenPoint(obj.transform.position);
        Vector2 objVec = Camera.main.WorldToViewportPoint(obj.transform.position); // 0f ~ 1f

        Vector2 targetVec = objScreenVec - camera_screen_vec;
        targetVec = targetVec.normalized;


        float targetAngle = Vector2.Angle(targetVec, Vector2.up); // 0 ~ 180
        int sign = Vector3.Cross(targetVec, Vector2.up).z < 0 ? -1 : 1;
        targetAngle *= sign; // -180 ~ 180

        indicator.rectTransform.rotation = Quaternion.Euler(0, 0, targetAngle); // 회전 적용

        float xPrime = objVec.x - screenHalfHeight;
        float yPrime = objVec.y - screenHalfWidth;

        float anchorMinX;
        float anchorMinY;
        float anchorMaxX;
        float anchorMaxY;

        if (-angleRU < targetAngle && angleRU >= targetAngle) // UP 쪽에 있을 때
        {
            anchorMinY = 0.84f;
            anchorMaxY = 0.84f;
            // y anchor 지정

            float posX = (Mathf.Abs(xPrime) * screenHalfHeight) / yPrime;

            if (xPrime > 0) // Right
            {
                anchorMinX = screenHalfWidth + posX;
                anchorMaxX = screenHalfWidth + posX;

                if (anchorMinX > 0.865f) anchorMinX = 0.865f;
                if (anchorMaxX > 0.865f) anchorMaxX = 0.865f;
                // 이미지가 넘어가는 걸 방지
            }
            else // Left
            {
                anchorMinX = screenHalfWidth - posX;
                anchorMaxX = screenHalfWidth - posX;

                if (anchorMinX < 0.135f) anchorMinX = 0.135f;
                if (anchorMaxX < 0.135f) anchorMaxX = 0.135f;
                // 이미지가 넘어가는 걸 방지
            }

            indicator.rectTransform.anchorMin = new Vector2(anchorMinX, anchorMinY);
            indicator.rectTransform.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
            indicator.rectTransform.rotation = Quaternion.Euler(0, 0, -targetAngle); // 회전 적용

            // indicator의 anchor 지정
        }

        else if (angleRU < targetAngle && 180 - angleRU >= targetAngle) // RIGHT 쪽에 있을 떄
        {
            anchorMinX = 0.865f;
            anchorMaxX = 0.865f;
            // x anchor 지정

            float posY = (screenHalfWidth * Mathf.Abs(yPrime)) / xPrime;

            if (yPrime > 0) // Up
            {
                anchorMinY = screenHalfHeight + posY;
                anchorMaxY = screenHalfHeight + posY;

                if (anchorMinY > 0.84f) anchorMinY = 0.84f;
                if (anchorMaxY > 0.84f) anchorMaxY = 0.84f;
                // 이미지가 넘어가는 걸 방지
            }
            else // Down
            {
                anchorMinY = screenHalfHeight - posY;
                anchorMaxY = screenHalfHeight - posY;

                if (anchorMinY < 0.14f) anchorMinY = 0.14f;
                if (anchorMaxY < 0.14f) anchorMaxY = 0.14f;
                // 이미지가 넘어가는 걸 방지
            }

            indicator.rectTransform.anchorMin = new Vector2(anchorMinX, anchorMinY);
            indicator.rectTransform.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
            indicator.rectTransform.rotation = Quaternion.Euler(0, 0, -targetAngle); // 회전 적용

            // indicator의 anchor 지정
        }
        else if ((180 - angleRU < targetAngle && 180 > targetAngle)
            || (-180 <= targetAngle && angleRU - 180 >= targetAngle)) // DOWN 쪽에 있을 때
        {
            anchorMinY = 0.16f;
            anchorMaxY = 0.16f;

            float posX = (Mathf.Abs(xPrime) * screenHalfHeight) / -yPrime;

            if (xPrime > 0) // Right
            {
                anchorMinX = screenHalfWidth + posX;
                anchorMaxX = screenHalfWidth + posX;

                if (anchorMinX > 0.865f) anchorMinX = 0.865f;
                if (anchorMaxX > 0.865f) anchorMaxX = 0.865f;
            }
            else // Left
            {
                anchorMinX = screenHalfWidth - posX;
                anchorMaxX = screenHalfWidth - posX;

                if (anchorMinX < 0.135f) anchorMinX = 0.135f;
                if (anchorMaxX < 0.135f) anchorMaxX = 0.135f;
            }

            indicator.rectTransform.anchorMin = new Vector2(anchorMinX, anchorMinY);
            indicator.rectTransform.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
            indicator.rectTransform.rotation = Quaternion.Euler(0, 0, -targetAngle); // 회전 적용

        }
        else if (angleRU - 180 < targetAngle && -angleRU >= targetAngle) // LEFT 쪽에 있을 때
        {
            anchorMinX = 0.135f;
            anchorMaxX = 0.135f;

            float posY = (screenHalfWidth * Mathf.Abs(yPrime)) / -xPrime;

            if (yPrime > 0) // Up
            {
                anchorMinY = screenHalfWidth + posY;
                anchorMaxY = screenHalfWidth + posY;

                if (anchorMinY > 0.84f) anchorMinY = 0.84f;
                if (anchorMaxY > 0.84f) anchorMaxY = 0.84f;
            }
            else // Down
            {
                anchorMinY = screenHalfWidth - posY;
                anchorMaxY = screenHalfWidth - posY;

                if (anchorMinY < 0.14f) anchorMinY = 0.14f;
                if (anchorMaxY < 0.14f) anchorMaxY = 0.14f;
            }
            indicator.rectTransform.anchorMin = new Vector2(anchorMinX, anchorMinY);
            indicator.rectTransform.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
            indicator.rectTransform.rotation = Quaternion.Euler(0, 0, -targetAngle); // 회전 적용

        }

        indicator.rectTransform.anchoredPosition = new Vector3(0, 0);
        // 위에서 지정한 anchor로 이동
    }

}
