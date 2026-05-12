# Ch01_Setup_11_DestroyZone

## 목표

화면 밖으로 나간 총알과 적이 계속 남지 않도록 DestroyZone을 만든다.

이전 단계까지는 Bullet과 Enemy가 계속 생성되거나 이동했지만, 화면 밖으로 나가도 자동으로 정리되지 않았다.  
이번 단계에서는 화면 바깥쪽에 DestroyZone을 배치하고, 그 영역에 들어온 오브젝트를 삭제한다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `Assets/Scripts/DestroyZone.cs` 스크립트를 만든다.
2. 씬 바깥쪽에 DestroyZone 오브젝트를 배치한다.
3. DestroyZone 오브젝트에는 Trigger로 동작하는 BoxCollider를 사용한다.
4. DestroyZone에 들어온 오브젝트를 삭제한다.
5. 위, 아래, 왼쪽, 오른쪽 경계에 DestroyZone을 배치한다.

## 중요

- 이번 작업은 화면 밖 오브젝트 삭제 기능만 만든다.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- UI는 만들지 마.
- 사운드나 이펙트는 만들지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### DestroyZone 오브젝트

DestroyZone은 화면 바깥쪽에 배치한다.

예시 이름:

- `DestroyZone_U`
- `DestroyZone_D`
- `DestroyZone_L`
- `DestroyZone_R`

각 DestroyZone은 화면 밖으로 나간 오브젝트를 감지하는 역할을 한다.

필요한 컴포넌트:

- BoxCollider
- Rigidbody
- DestroyZone 스크립트

Collider 설정:

- `Is Trigger`를 켠다.
- 영역을 넓게 만들어 화면 밖으로 나간 Bullet과 Enemy를 감지할 수 있게 한다.

Rigidbody 설정:

- Trigger 감지를 위해 사용한다.
- 움직이지 않는 영역이므로 Kinematic으로 설정한다.

### DestroyZone.cs

`DestroyZone.cs`는 아래 기능만 담당한다.

- 다른 Collider가 Trigger 영역에 들어왔는지 확인한다.
- 들어온 오브젝트를 삭제한다.

예상 코드 흐름:

```csharp
private void OnTriggerEnter(Collider other)
{
    Destroy(other.gameObject);
}
```

## Unity Editor 설정

작업 후 Unity Editor에서 다음을 확인한다.

- DestroyZone 오브젝트들이 씬 바깥쪽에 배치되어 있는지 확인
- BoxCollider의 Is Trigger가 켜져 있는지 확인
- DestroyZone 스크립트가 붙어 있는지 확인
- Rigidbody가 Kinematic으로 설정되어 있는지 확인
- Play 모드에서 Bullet이나 Enemy가 DestroyZone에 닿으면 사라지는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- 파일 목록

새로 만든 파일:
- 파일 목록

새로 만든 오브젝트:
- 오브젝트 목록

Unity Editor에서 확인할 내용:
- DestroyZone 위치 확인
- Is Trigger 설정 확인
- Play 모드에서 Bullet과 Enemy가 화면 밖에서 삭제되는지 확인

아직 구현하지 않은 내용:
- 점수
- 게임 오버
- UI
- 사운드
- 이펙트
```
