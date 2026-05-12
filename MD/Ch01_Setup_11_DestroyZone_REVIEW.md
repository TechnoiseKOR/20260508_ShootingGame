# Ch01_Setup_11_DestroyZone_REVIEW

## 리뷰 대상

이번 리뷰 대상은 DestroyZone과 Trigger 감지 구현 패치다.

패치 요약:

- `Assets/Scripts/DestroyZone.cs` 추가
- 씬에 `DestroyZone_U`, `DestroyZone_D`, `DestroyZone_L`, `DestroyZone_R` 추가
- 각 DestroyZone에 Trigger BoxCollider 추가
- 각 DestroyZone에 Kinematic Rigidbody 추가
- Trigger 영역에 들어온 오브젝트 삭제

## 전체 평가

이번 작업은 EnemyManager와 PlayerFire 이후에 꼭 필요한 정리 단계로 적절하다.

현재 게임에서는 Bullet과 Enemy가 계속 생성된다.  
이 오브젝트들이 화면 밖으로 나가도 사라지지 않으면 Hierarchy에 계속 쌓이고, 게임이 점점 무거워질 수 있다.

DestroyZone을 추가해서 화면 밖 오브젝트를 삭제한 것은 좋은 흐름이다.

## 좋은 점

### 1. 화면 밖 오브젝트 정리 구조가 생겼다

이제 Bullet과 Enemy가 화면 밖으로 이동하면 DestroyZone에 닿아 삭제될 수 있다.

계속 생성되는 오브젝트를 정리하는 기본 구조가 만들어졌다.

### 2. Trigger 개념을 설명하기 좋다

DestroyZone은 Collider를 가지고 있지만 `Is Trigger`가 켜져 있다.

즉, 물리적으로 막는 벽이 아니라 감지 영역으로 사용된다.

학생들에게 Collision과 Trigger의 차이를 설명하기 좋은 단계다.

### 3. 상하좌우 경계가 모두 만들어졌다

DestroyZone이 위, 아래, 왼쪽, 오른쪽에 배치되어 있다.

특히 이번 프로젝트에서는 Enemy가 아래로 내려오거나 Player 방향으로 비스듬히 이동할 수 있으므로, 좌우 경계까지 만든 점이 좋다.

### 4. 코드가 단순하다

DestroyZone 코드는 매우 단순하다.

```csharp
private void OnTriggerEnter(Collider other)
{
    Destroy(other.gameObject);
}
```

초급자가 Trigger 감지를 이해하기에 적절하다.

## 확인이 필요한 점

### 1. Trigger 설정 확인

각 DestroyZone의 BoxCollider에서 `Is Trigger`가 켜져 있어야 한다.

켜져 있지 않으면 오브젝트를 감지하지 못하거나 물리 충돌처럼 동작할 수 있다.

### 2. Rigidbody 설정 확인

DestroyZone에는 Kinematic Rigidbody가 붙어 있다.

확인할 내용:

- Rigidbody가 있는지
- Is Kinematic이 켜져 있는지
- DestroyZone이 중력으로 움직이지 않는지

### 3. Play 모드 삭제 확인

Play 모드에서 다음을 확인한다.

- Bullet이 위쪽으로 이동하다가 DestroyZone_U에 닿으면 사라지는지
- Enemy가 아래쪽으로 이동하다가 DestroyZone_D에 닿으면 사라지는지
- Player 방향으로 비스듬히 이동한 Enemy가 좌우 DestroyZone에 닿으면 사라지는지
- Console에 오류가 없는지

### 4. DestroyZone이 화면에 보이는지 확인

현재 DestroyZone은 MeshRenderer가 포함된 오브젝트다.

Game View에서 보인다면 수업 중에는 설명용으로 괜찮지만, 실제 게임처럼 보이게 하려면 Renderer를 끄는 것이 좋다.

## 개선하면 좋은 점

### 1. 삭제 대상 구분

현재 DestroyZone은 들어온 모든 오브젝트를 삭제한다.

초반에는 단순해서 좋지만, 나중에는 삭제 대상만 구분하는 것이 안전하다.

예:

- Bullet 삭제
- Enemy 삭제
- Player는 삭제하지 않기

이를 위해 Tag 또는 Layer를 사용할 수 있다.

### 2. DestroyZone Renderer 끄기

DestroyZone은 게임 플레이 중 보이지 않는 편이 자연스럽다.

나중에 시각적으로 정리할 때는 MeshRenderer를 끄거나, 반투명 머티리얼을 사용해도 된다.

수업 중에는 Trigger 영역을 보여주기 위해 잠시 보이게 두는 것도 좋다.

### 3. 빈 Start와 Update 정리

현재 `DestroyZone.cs`에는 비어 있는 `Start()`와 `Update()`가 있다.

실제 기능에는 필요 없으므로, 나중에 코드 정리 단계에서 제거할 수 있다.

### 4. Use Gravity 끄기

DestroyZone의 Rigidbody는 Kinematic이라 중력 영향은 받지 않는다.

그래도 설정을 더 명확히 하려면 Use Gravity를 꺼두는 것이 좋다.

## 다음 단계 제안

다음 단계는 게임 규칙을 만드는 방향이 좋다.

추천 다음 작업:

```text
Ch01_Setup_12_CollisionRules
```

내용:

- Bullet과 Enemy 충돌 구분
- Player와 Enemy 충돌 구분
- DestroyZone 삭제 대상 구분

또는 바로 점수로 넘어갈 수도 있다.

```text
Ch01_Setup_12_ScoreBasic
```

내용:

- Bullet이 Enemy를 맞추면 점수 증가
- 점수 값을 화면 또는 Console로 확인

수업 흐름상 먼저 충돌 대상을 구분한 뒤 점수와 게임 오버를 연결하는 것이 더 안전하다.

## 최종 결론

이번 패치는 계속 생성되는 Bullet과 Enemy를 정리하는 중요한 단계다.

DestroyZone을 통해 화면 밖 오브젝트를 삭제할 수 있게 되었고, Trigger 개념을 배우기에도 적절하다.

다음 단계에서는 삭제 대상과 충돌 대상을 구분해서 점수와 게임 오버 같은 게임 규칙으로 이어가면 좋다.
