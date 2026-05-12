# Ch01_Setup_06_EnemyMove_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 슈팅 게임에서 사용할 적 오브젝트와 적 이동 스크립트가 추가되었다.

주요 변경은 다음과 같다.

1. 기본 씬에 `Enemy` 오브젝트 추가
2. `Enemy` 오브젝트에 `Enemy` 스크립트 연결
3. `Assets/Scripts/Enemy.cs` 스크립트 추가
4. `Enemy.cs.meta` 파일 추가

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Scenes/SampleScene.unity` | `Enemy` 오브젝트 추가 및 `Enemy` 스크립트 연결 |
| `Assets/Scripts/Enemy.cs` | 적 이동 스크립트 추가 |
| `Assets/Scripts/Enemy.cs.meta` | `Enemy.cs` 메타 파일 추가 |

## 씬 변경 분석

`SampleScene.unity`에 `Enemy` GameObject가 추가되었다.

추가된 주요 컴포넌트는 다음과 같다.

- `Transform`
- `MeshFilter`
- `MeshRenderer`
- `BoxCollider`
- `Enemy`

`Enemy` 오브젝트는 씬 루트에 추가되었고, 위치는 화면 위쪽에서 내려오는 것을 확인할 수 있도록 설정되어 있다.

주요 Transform 값은 다음과 같다.

```text
Position: x 0, y 4, z 0
Scale: x 1, y 1, z 1
```

## 스크립트 변경 분석

새 파일 `Assets/Scripts/Enemy.cs`가 추가되었다.

핵심 코드는 다음 흐름이다.

```csharp
Vector3 dir = Vector3.down;
transform.position += dir * speed * Time.deltaTime;
```

이 코드는 적의 이동 방향을 `Vector3.down`으로 정하고,
매 프레임 현재 위치에 이동량을 더하는 방식이다.

## 이동 방식

이번 이동 방식은 `transform.position`을 직접 변경하는 방식이다.

이전 단계의 총알 이동과 거의 같은 구조이고,
방향만 반대다.

| 오브젝트 | 이동 방향 |
|---|---|
| Bullet | `Vector3.up` |
| Enemy | `Vector3.down` |

이렇게 구성하면 학생들이 총알과 적의 차이를 쉽게 비교할 수 있다.

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- 적 오브젝트 표시
- 적 이동 스크립트 추가
- 적이 아래쪽으로 이동
- Inspector에서 적 이동 속도 조절 가능

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 적 프리팹화
- 적 자동 생성
- 적 자동 삭제
- 총알과 적 충돌
- 플레이어와 적 충돌
- 점수
- 게임 오버
- 사운드
- 이펙트

현재 단계에서는 적 이동만 구현한 것이 적절하다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. `SampleScene`에 `Enemy` 오브젝트가 있는지 확인
2. `Enemy` 오브젝트에 `Enemy` 스크립트가 붙어 있는지 확인
3. `Enemy` 스크립트의 `speed` 값이 Inspector에 보이는지 확인
4. Play 모드에서 적이 아래쪽으로 이동하는지 확인
5. Console에 컴파일 오류가 없는지 확인

## 주의할 점

### 1. 적 자동 생성은 아직 없다

현재 적은 씬에 직접 배치되어 있다.

Play 모드가 시작되면 씬에 있는 적이 아래로 내려오는 구조다.

여러 적을 일정 시간마다 생성하는 기능은 다음 단계 이후에 구현하는 것이 좋다.

### 2. 적 자동 삭제는 아직 없다

현재 적은 계속 아래쪽으로 이동한다.

화면 밖으로 나간 적을 제거하는 기능은 아직 없다.

나중에 적이 계속 생성되면 화면 밖 제거 또는 DestroyZone 같은 구조가 필요하다.

### 3. 충돌 처리는 아직 없다

적에는 `BoxCollider`가 있지만,
총알과 부딪히거나 플레이어와 부딪혔을 때의 처리는 아직 없다.

충돌 처리는 이후 단계에서 별도로 추가하면 된다.

### 4. 빈 Start 메서드가 있다

`Start()` 메서드가 있지만 현재 아무 동작도 하지 않는다.

수업 초반에는 Unity 기본 스크립트 구조를 보여주는 용도로 둘 수 있지만,
나중에 코드 정리 단계에서는 제거해도 된다.

### 5. speed가 public 필드다

현재 `speed`는 `public float speed = 5.0f;`로 작성되어 있다.

초급 단계에서는 Inspector에서 바로 보이기 때문에 이해하기 쉽다.

이후 코드 스타일을 정리할 때는 다음처럼 바꿀 수 있다.

```csharp
[SerializeField] private float speed = 5.0f;
```

## 결론

이번 패치는 적 오브젝트가 아래로 이동하는 기능을 먼저 확인하는 단계로 적절하다.

총알 이동과 반대 방향의 자동 이동을 구현하면서,
이후 총알과 적 충돌 처리로 자연스럽게 이어질 수 있다.
