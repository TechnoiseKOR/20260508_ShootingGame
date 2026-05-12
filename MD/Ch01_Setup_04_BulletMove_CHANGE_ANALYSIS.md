# Ch01_Setup_04_BulletMove_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 슈팅 게임에서 사용할 총알 오브젝트와 총알 이동 스크립트가 추가되었다.

주요 변경은 다음과 같다.

1. 기본 씬에 `Bullet` 오브젝트 추가
2. `Bullet` 오브젝트에 `Bullet` 스크립트 연결
3. `Assets/Scripts/Bullet.cs` 스크립트 추가
4. 기존 `PlayerMove` 컴포넌트의 `speed` 값이 씬에 직렬화됨

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Scenes/SampleScene.unity` | `Bullet` 오브젝트 추가 및 `Bullet` 스크립트 연결 |
| `Assets/Scripts/Bullet.cs` | 총알 이동 스크립트 추가 |
| `Assets/Scripts/Bullet.cs.meta` | `Bullet.cs` 메타 파일 추가 |

## 씬 변경 분석

`SampleScene.unity`에 `Bullet` GameObject가 추가되었다.

추가된 주요 컴포넌트는 다음과 같다.

- `Transform`
- `MeshFilter`
- `MeshRenderer`
- `BoxCollider`
- `Bullet`

`Bullet` 오브젝트의 Transform 값도 설정되어 있다.

- 위치: `{x: 0, y: 1.5, z: 0}`
- 크기: `{x: 0.25, y: 0.6, z: 1}`

즉, 총알은 플레이어보다 작고 세로로 긴 형태로 배치되어 있으며,
Play 모드에서 위로 움직이는지 확인할 수 있는 상태다.

## 스크립트 변경 분석

새 파일 `Assets/Scripts/Bullet.cs`가 추가되었다.

핵심 코드는 다음 흐름이다.

```csharp
Vector3 dir = Vector3.up;
transform.position += dir * speed * Time.deltaTime;
```

이 코드는 총알의 이동 방향을 `Vector3.up`으로 정하고,
매 프레임 현재 위치에 이동량을 더하는 방식이다.

## 이동 방식

이번 이동 방식은 `transform.position`을 직접 변경하는 방식이다.

플레이어 이동 단계와 마찬가지로,
초반 수업에서 가장 이해하기 쉬운 방식이다.

- `Vector3.up`: 위쪽 방향
- `speed`: 이동 속도
- `Time.deltaTime`: 프레임 속도 차이를 줄이기 위한 시간 보정

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- 총알 오브젝트 표시
- 총알 이동 스크립트 추가
- 총알이 위쪽으로 이동
- Inspector에서 총알 이동 속도 조절 가능

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 플레이어가 총알을 발사하는 기능
- 총알 프리팹화
- 총알 자동 삭제
- 적 생성
- 총알과 적 충돌
- 점수
- 게임 오버
- 사운드
- 이펙트

현재 단계에서는 총알 이동만 구현한 것이 적절하다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. `SampleScene`에 `Bullet` 오브젝트가 있는지 확인
2. `Bullet` 오브젝트에 `Bullet` 스크립트가 붙어 있는지 확인
3. `Bullet` 스크립트의 `speed` 값이 Inspector에 보이는지 확인
4. Play 모드에서 총알이 위쪽으로 이동하는지 확인
5. Console에 컴파일 오류가 없는지 확인

## 주의할 점

### 1. 총알 발사는 아직 없다

현재 총알은 씬에 직접 배치되어 있고,
Play 모드가 시작되면 스스로 위로 이동한다.

플레이어 위치에서 총알을 생성하는 기능은 다음 단계에서 구현하는 것이 좋다.

### 2. 총알 자동 삭제는 아직 없다

현재 총알은 계속 위로 이동한다.

화면 밖으로 나간 총알을 제거하는 기능은 아직 없다.

나중에 총알이 많이 생성되면 화면 밖 제거 또는 DestroyZone 같은 구조가 필요하다.

### 3. 빈 Start 메서드가 있다

`Start()` 메서드가 있지만 현재 아무 동작도 하지 않는다.

수업 초반에는 Unity 기본 스크립트 구조를 보여주는 용도로 둘 수 있지만,
나중에 코드 정리 단계에서는 제거해도 된다.

### 4. speed가 public 필드다

현재 `speed`는 `public float speed = 5.0f;`로 작성되어 있다.

초급 단계에서는 Inspector에서 바로 보이기 때문에 이해하기 쉽다.

이후 코드 스타일을 정리할 때는 다음처럼 바꿀 수 있다.

```csharp
[SerializeField] private float speed = 5.0f;
```

## 결론

이번 패치는 총알을 발사하기 전,
총알 오브젝트가 스스로 위로 이동하는 기능을 먼저 확인하는 단계로 적절하다.

플레이어 이동 다음 단계로 자연스럽고,
다음에는 플레이어가 버튼을 눌렀을 때 총알을 생성하는 기능으로 이어가기 좋다.
