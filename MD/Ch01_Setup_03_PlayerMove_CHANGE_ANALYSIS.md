# Ch01_Setup_03_PlayerMove_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 슈팅 게임의 첫 번째 실제 기능으로 플레이어 이동 기능이 추가되었다.

주요 변경은 다음과 같다.

1. 기본 씬에 `Player` 오브젝트 추가
2. `Assets/Scripts` 폴더 추가
3. `PlayerMove.cs` 스크립트 추가
4. `Player` 오브젝트에 `PlayerMove` 스크립트 연결

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Scenes/SampleScene.unity` | `Player` 오브젝트 추가 및 `PlayerMove` 스크립트 연결 |
| `Assets/Scripts.meta` | `Assets/Scripts` 폴더 메타 파일 추가 |
| `Assets/Scripts/PlayerMove.cs` | 플레이어 이동 스크립트 추가 |
| `Assets/Scripts/PlayerMove.cs.meta` | `PlayerMove.cs` 메타 파일 추가 |

## 씬 변경 분석

`SampleScene.unity`에 `Player` GameObject가 추가되었다.

추가된 주요 컴포넌트는 다음과 같다.

- `Transform`
- `MeshFilter`
- `MeshRenderer`
- `BoxCollider`
- `PlayerMove`

즉, 플레이어는 우선 눈에 보이는 3D 오브젝트로 만들어졌고,
나중에 충돌 처리를 확장할 수 있도록 `BoxCollider`도 포함되어 있다.

또한 씬 루트 목록에 `Player`의 Transform이 추가되어,
씬에 실제 오브젝트로 배치된 상태다.

## 스크립트 변경 분석

새 파일 `Assets/Scripts/PlayerMove.cs`가 추가되었다.

핵심 코드는 다음 흐름이다.

```csharp
float h = Input.GetAxis("Horizontal");
float v = Input.GetAxis("Vertical");

Vector3 dir = new Vector3(h, v, 0.0f);
transform.position = transform.position + dir * speed * Time.deltaTime;
```

이 코드는 Unity의 기본 입력 축인 `Horizontal`, `Vertical` 값을 읽어서
플레이어의 위치를 매 프레임 이동시킨다.

## 이동 방식

이번 이동 방식은 `transform.position`을 직접 변경하는 방식이다.

초반 수업에서는 가장 이해하기 쉬운 방식이다.

- `Horizontal`: 좌우 입력
- `Vertical`: 상하 입력
- `speed`: 이동 속도
- `Time.deltaTime`: 컴퓨터 성능에 따른 속도 차이를 줄이기 위한 시간 보정

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- 플레이어 오브젝트 표시
- 키보드 입력 읽기
- 입력 방향으로 플레이어 이동
- Inspector에서 이동 속도 조절 가능

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 화면 밖 이동 제한
- 총알 발사
- 적 생성
- 적 이동
- 충돌 처리
- 점수
- 게임 오버
- 사운드
- 이펙트

현재 단계에서는 플레이어 이동만 구현한 것이 적절하다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. `SampleScene`에 `Player` 오브젝트가 있는지 확인
2. `Player` 오브젝트에 `PlayerMove` 스크립트가 붙어 있는지 확인
3. `PlayerMove`의 `speed` 값이 Inspector에 보이는지 확인
4. Play 모드에서 방향키 또는 WASD로 움직이는지 확인
5. Console에 컴파일 오류가 없는지 확인

## 주의할 점

### 1. 화면 밖 이동 제한은 아직 없다

현재 코드는 입력 방향으로 계속 이동한다.

따라서 플레이어가 화면 밖으로 나갈 수 있다.

화면 제한은 다음 단계에서 추가하면 된다.

### 2. 빈 Start 메서드가 있다

`Start()` 메서드가 있지만 현재 아무 동작도 하지 않는다.

수업 초반에는 Unity가 자동으로 만든 구조를 보여주는 용도로 둘 수 있지만,
나중에 코드 정리 단계에서는 제거해도 된다.

### 3. speed가 public 필드다

현재 `speed`는 `public float speed = 5.0f;`로 작성되어 있다.

초급 단계에서는 Inspector에 바로 보이기 때문에 이해하기 쉽다.

다만 이후 코드 정리 단계에서는 다음처럼 바꿀 수 있다.

```csharp
[SerializeField] private float speed = 5.0f;
```

## 결론

이번 패치는 플레이어 이동을 처음 구현하는 단계로 적절하다.

오브젝트 생성, 스크립트 작성, 스크립트 연결, Play 모드 확인까지 이어질 수 있는
가장 기본적인 Unity 기능 구현 흐름을 담고 있다.
