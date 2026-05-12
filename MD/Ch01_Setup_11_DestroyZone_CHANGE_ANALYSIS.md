# Ch01_Setup_11_DestroyZone_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 화면 밖으로 나간 오브젝트를 삭제하기 위한 DestroyZone 기능이 추가되었다.

주요 변경은 다음과 같다.

1. `DestroyZone.cs` 스크립트 추가
2. 씬에 DestroyZone 오브젝트 4개 추가
3. 각 DestroyZone에 BoxCollider, Rigidbody, DestroyZone 스크립트 연결
4. BoxCollider를 Trigger로 설정
5. Trigger 영역에 들어온 오브젝트 삭제

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Scenes/SampleScene.unity` | DestroyZone 오브젝트 4개 추가 |
| `Assets/Scripts/DestroyZone.cs` | Trigger 감지 후 오브젝트 삭제 스크립트 추가 |
| `Assets/Scripts/DestroyZone.cs.meta` | DestroyZone 스크립트 메타 파일 추가 |

## 씬 변경 분석

`SampleScene.unity`에 DestroyZone 오브젝트들이 추가되었다.

추가된 DestroyZone은 다음과 같다.

| 오브젝트 | 위치 | 크기 |
|---|---|---|
| `DestroyZone_U` | `{x: 0, y: 15, z: 0}` | `{x: 30, y: 1, z: 1}` |
| `DestroyZone_D` | `{x: 0, y: -15, z: 0}` | `{x: 30, y: 1, z: 1}` |
| `DestroyZone_L` | `{x: -15, y: 0, z: 0}` | `{x: 1, y: 30, z: 1}` |
| `DestroyZone_R` | `{x: 15, y: 0, z: 0}` | `{x: 1, y: 30, z: 1}` |

이 배치는 화면 위, 아래, 왼쪽, 오른쪽 바깥쪽에 삭제 영역을 만드는 구조다.

## DestroyZone 오브젝트 구성

각 DestroyZone에는 다음 컴포넌트가 포함되어 있다.

- `Transform`
- `MeshFilter`
- `MeshRenderer`
- `BoxCollider`
- `Rigidbody`
- `DestroyZone`

중요한 설정은 다음과 같다.

### BoxCollider

DestroyZone의 `BoxCollider`는 Trigger로 설정되어 있다.

```text
m_IsTrigger: 1
```

즉, 물리적으로 막는 벽이 아니라, 들어온 오브젝트를 감지하는 영역으로 동작한다.

### Rigidbody

DestroyZone에는 Rigidbody가 추가되어 있고 Kinematic 상태로 설정되어 있다.

```text
m_IsKinematic: 1
```

DestroyZone은 움직이는 오브젝트가 아니라 감지 영역이므로, Kinematic Rigidbody를 사용하는 것이 자연스럽다.

## DestroyZone.cs 변경 분석

새 파일 `Assets/Scripts/DestroyZone.cs`가 추가되었다.

핵심 코드는 다음과 같다.

```csharp
private void OnTriggerEnter(Collider other)
{
    Destroy(other.gameObject);
}
```

이 코드는 다른 Collider가 DestroyZone의 Trigger 영역에 들어오면 해당 오브젝트를 삭제한다.

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- 화면 바깥쪽 삭제 영역 추가
- Trigger 기반 감지 처리
- DestroyZone에 들어온 오브젝트 제거
- 계속 생성되는 Bullet과 Enemy가 화면 밖에 쌓이지 않도록 정리

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 삭제 대상 구분
- 점수 처리
- 게임 오버 처리
- 충돌 이펙트
- 충돌 사운드
- DestroyZone 시각적 숨김 처리
- 오브젝트 풀링

현재 단계에서는 화면 밖 오브젝트 정리가 핵심이다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. 씬에 `DestroyZone_U`, `DestroyZone_D`, `DestroyZone_L`, `DestroyZone_R`이 있는지 확인
2. 각 DestroyZone에 `DestroyZone` 스크립트가 붙어 있는지 확인
3. 각 DestroyZone의 BoxCollider에서 `Is Trigger`가 켜져 있는지 확인
4. 각 DestroyZone의 Rigidbody가 Kinematic인지 확인
5. Play 모드에서 Bullet이 위쪽 DestroyZone에 닿으면 삭제되는지 확인
6. Enemy가 아래쪽 또는 옆쪽 DestroyZone에 닿으면 삭제되는지 확인
7. Console에 오류가 없는지 확인

## 주의할 점

### 1. 삭제 대상을 구분하지 않는다

현재 DestroyZone은 Trigger에 들어온 모든 오브젝트를 삭제한다.

```csharp
Destroy(other.gameObject);
```

따라서 Player가 DestroyZone에 닿아도 삭제될 수 있다.

현재 DestroyZone이 화면 바깥쪽에 있으므로 일반 플레이 상황에서는 큰 문제는 없지만, 나중에 안전하게 만들려면 태그나 레이어로 삭제 대상을 구분할 수 있다.

### 2. DestroyZone이 보일 수 있다

패치 기준으로 DestroyZone에는 MeshRenderer가 포함되어 있다.

따라서 Game View에서 DestroyZone이 보일 수 있다.

수업 중 Trigger 영역을 눈으로 확인하기에는 좋지만, 실제 게임 화면에서는 MeshRenderer를 끄거나 투명하게 만드는 것이 좋다.

### 3. 빈 Start와 Update 메서드가 있다

`DestroyZone.cs`에는 빈 `Start()`와 `Update()`가 들어 있다.

수업 초반에는 Unity 기본 스크립트 구조를 보여주는 용도로 둘 수 있지만, 나중에 정리할 때는 제거해도 된다.

### 4. Use Gravity가 켜져 있지만 Kinematic이다

DestroyZone의 Rigidbody는 `Is Kinematic`이 켜져 있으므로 중력 영향을 받지 않는다.

다만 설정을 더 명확히 하려면 Use Gravity를 꺼도 된다.

## 결론

이번 패치는 계속 생성되는 Bullet과 Enemy가 화면 밖에 남아 쌓이는 문제를 해결하는 단계다.

DestroyZone을 상하좌우에 배치하고 Trigger 감지로 오브젝트를 삭제하는 구조는 Unity의 Trigger 개념을 설명하기에도 좋다.

다음 단계에서는 삭제 대상 구분, 점수, 게임 오버 같은 게임 규칙을 추가하면 좋다.
