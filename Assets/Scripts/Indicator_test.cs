using UnityEngine;

public class Indicator_test : MonoBehaviour
{
    // 몬스터 인디케이터를 그리는 MonsterIndicator 스크립트 참조
    public MonsterIndicator monsterIndicator;

    // 인디케이터 객체의 프리팹
    public GameObject indicatorObj;

    // 인디케이터 객체들이 배치될 캔버스
    public GameObject indicatorCanvas;

    // 생성된 실제 인디케이터 객체를 저장할 변수
    private GameObject myIndicatorObj;

    // 인디케이터가 현재 표시되고 있는지 여부를 추적하는 플래그
    private bool hasIndicator = false;

    // 메인 카메라 객체, 뷰포트 계산에 사용
    private Camera mainCamera;

    void Start()
    {
        // 시작 시 메인 카메라 참조를 가져옴
        mainCamera = Camera.main;

        // 만약 indicatorCanvas가 지정되지 않았다면, "Canvas"라는 이름으로 찾아서 할당
        if (indicatorCanvas == null)
            indicatorCanvas = GameObject.Find("Canvas");

        // Canvas가 여전히 null이면 오류 메시지 출력
        if (indicatorCanvas == null)
            Debug.LogError("IndicatorCanvas를 찾을 수 없습니다. Canvas가 존재하거나 이름이 'Canvas'인지 확인하세요.");
    }

    void Update()
    {
        // 만약 monsterIndicator가 null이면, 첫 번째로 발견되는 MonsterIndicator 객체를 찾아 할당
        if (monsterIndicator == null)
            monsterIndicator = FindFirstObjectByType<MonsterIndicator>();

        // mainCamera가 null이면, 다시 메인 카메라를 찾음
        if (mainCamera == null)
            mainCamera = Camera.main;

        // 인디케이터 갱신 함수 호출
        UpdateIndicator();
    }

    private void UpdateIndicator()
    {
        // 인디케이터를 표시해야 하는지 여부를 판단
        bool shouldShowIndicator = ShouldShowIndicator();

        // 인디케이터를 표시해야 하는데 아직 인디케이터가 없다면 생성
        if (shouldShowIndicator && !hasIndicator)
        {
            CreateIndicator();
        }
        // 인디케이터를 표시하지 않아야 하는데 인디케이터가 있다면 제거
        else if (!shouldShowIndicator && hasIndicator)
        {
            DestroyIndicator();
        }

        // 인디케이터가 존재하면 MonsterIndicator를 사용해 그리기
        if (hasIndicator)
        {
            monsterIndicator.DrawIndicator(gameObject, myIndicatorObj);
        }
    }

    // 인디케이터를 표시할지 여부를 판단하는 함수
    private bool ShouldShowIndicator()
    {
        // 메인 카메라가 없으면 표시하지 않음
        if (!mainCamera) return false;

        // 몬스터와 카메라 간의 방향 벡터 계산
        Vector3 directionToMonster = transform.position - mainCamera.transform.position;

        // 몬스터가 카메라 앞에 있는지 확인 (전방 방향을 기준으로 판단)
        bool isInFront = Vector3.Dot(mainCamera.transform.forward, directionToMonster) > 0;

        // 만약 카메라 앞에 없으면 인디케이터를 표시하지 않음
        if (!isInFront) return false;

        // 월드 좌표를 뷰포트 좌표로 변환하여 화면 밖에 있는지 확인
        Vector2 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);

        // 뷰포트 좌표가 화면 밖에 있으면 인디케이터를 표시
        return viewportPoint.x <= 0 || viewportPoint.x >= 1 ||
               viewportPoint.y <= 0 || viewportPoint.y >= 1;
    }

    // 인디케이터 객체를 생성하는 함수
    private void CreateIndicator()
    {
        // 인디케이터 객체를 생성하고 캔버스의 자식으로 설정
        myIndicatorObj = Instantiate(indicatorObj);
        myIndicatorObj.transform.SetParent(indicatorCanvas.transform, false);

        // 인디케이터가 존재함을 나타내는 플래그 설정
        hasIndicator = true;
    }

    // 생성된 인디케이터 객체를 제거하는 함수
    private void DestroyIndicator()
    {
        // 만약 인디케이터 객체가 존재하면 삭제
        if (myIndicatorObj != null)
            Destroy(myIndicatorObj);

        // 인디케이터가 없음을 나타내는 플래그 설정
        hasIndicator = false;
    }

    // 이 객체가 삭제될 때 호출되는 함수
    private void OnDestroy()
    {
        // 만약 인디케이터 객체가 존재하면 삭제
        if (myIndicatorObj != null)
            Destroy(myIndicatorObj);
    }
}
