# Ch01_Setup_08_EnemyManager_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 Enemy를 프리팹으로 만들고, EnemyManager를 이용해 적을 일정 시간마다 생성하는 기능이 추가되었다.

주요 변경은 다음과 같다.

1. `Assets/Prefabs/Enemy.prefab` 추가
2. 기존 씬에 직접 배치되어 있던 Enemy 오브젝트 제거
3. `EnemyManager.cs` 스크립트 추가
4. 씬에 EnemyManager 오브젝트 여러 개 추가
5. 각 EnemyManager에 Enemy 프리팹 연결
6. 일정 시간마다 Enemy를 생성하는 로직 추가

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Prefabs/Enemy.prefab` | Enemy 프리팹 추가 |
| `Assets/Prefabs/Enemy.prefab.meta` | Enemy 프리팹 메타 파일 추가 |
| `Assets/Scenes/SampleScene.unity` | 씬 Enemy 제거, EnemyManager 오브젝트 추가 |
| `Assets/Scripts/EnemyManager.cs` | 적 생성 매니저 스크립트 추가 |
| `Assets/Scripts/EnemyManager.cs.meta` | EnemyManager 스크립트 메타 파일 추가 |

## Enemy 프리팹 변경 분석

`Assets/Prefabs/Enemy.prefab`이 새로 추가되었다.

Enemy 프리팹에는 다음 컴포넌트가 포함되어 있다.

- `Transform`
- `MeshFilter`
- `MeshRenderer`
- `BoxCollider`
- `Enemy`
- `Rigidbody`

이전 단계에서 씬에 직접 배치했던 Enemy 오브젝트 구성을 프리팹으로 분리한 것이다.

프리팹의 주요 설정은 다음과 같다.

- 위치: `{x: 0, y: 4, z: 0}`
- 크기: `{x: 1, y: 1, z: 1}`
- `Enemy` 스크립트의 `speed`: `5`
- Rigidbody의 `Use Gravity`: 꺼짐

이제 Enemy는 씬에 미리 하나만 놓는 방식이 아니라, 필요할 때 생성하는 방식으로 바뀌었다.

## 씬 변경 분석

`SampleScene.unity`에서 기존 루트 Enemy 오브젝트가 제거되었다.

대신 EnemyManager 오브젝트들이 씬에 추가되었다.

패치에서 확인되는 EnemyManager 위치는 다음과 같다.

| 오브젝트 | 위치 |
|---|---|
| `EnemyManager` | `{x: 0, y: 6, z: 0}` |
| `EnemyManager (1)` | `{x: 3, y: 6, z: 0}` |
| `EnemyManager (2)` | `{x: 6, y: 6, z: 0}` |
| `EnemyManager (3)` | `{x: -3, y: 6, z: 0}` |
| `EnemyManager (4)` | `{x: -6, y: 6, z: 0}` |

즉, 화면 위쪽 여러 위치에서 적이 생성되도록 구성한 것으로 볼 수 있다.

각 EnemyManager에는 다음 값이 연결되어 있다.

- `createTime`: `1`
- `enemyFactory`: `Assets/Prefabs/Enemy.prefab`

## EnemyManager.cs 변경 분석

새 파일 `Assets/Scripts/EnemyManager.cs`가 추가되었다.

핵심 코드는 다음 흐름이다.

```csharp
currentTime += Time.deltaTime;

if (currentTime > createTime)
{
    GameObject enemy = Instantiate(enemyFactory);
    enemy.transform.position = transform.position;
    currentTime = 0.0f;
}
```

이 코드는 시간이 흐르다가 `createTime`보다 커지면 Enemy 프리팹을 생성하고, 생성된 Enemy를 EnemyManager 위치로 옮긴다.

그 뒤 `currentTime`을 0으로 초기화하여 다음 적 생성 시간을 다시 계산한다.

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- Enemy 프리팹 생성
- EnemyManager 스크립트 추가
- 일정 시간마다 Enemy 생성
- 여러 위치에서 Enemy 생성
- 기존 Enemy 이동 및 충돌 기능 재사용

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 적 생성 위치 무작위화
- 적 생성 간격 난이도 조정
- 화면 밖으로 나간 적 삭제
- 총알과 적 충돌 대상 구분
- 점수 증가
- 플레이어 체력
- 게임 오버
- 사운드
- 이펙트
- 오브젝트 풀링

현재 단계에서는 EnemyManager로 적을 반복 생성하는 기본 구조를 만든 것이 적절하다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. `Assets/Prefabs/Enemy.prefab`이 있는지 확인
2. Enemy 프리팹에 Enemy 스크립트, BoxCollider, Rigidbody가 있는지 확인
3. EnemyManager 오브젝트들이 씬에 있는지 확인
4. 각 EnemyManager의 `enemyFactory`에 Enemy 프리팹이 연결되어 있는지 확인
5. `createTime` 값이 `1`로 설정되어 있는지 확인
6. Play 모드에서 일정 시간마다 적이 생성되는지 확인
7. 생성된 Enemy가 아래로 이동하는지 확인
8. Console에 오류가 없는지 확인

## 주의할 점

### 1. EnemyManager가 여러 개 있다

이번 패치에서는 EnemyManager가 여러 개 배치되어 있다.

이 구조는 여러 위치에서 적을 동시에 생성하기 쉽다는 장점이 있다.

다만 오브젝트 이름이 `EnemyManager (1)`, `EnemyManager (2)`처럼 자동 복제 이름으로 되어 있어, 나중에 수업용으로 정리할 때는 이름을 더 명확하게 바꿀 수도 있다.

예:

```text
EnemyManager_Left
EnemyManager_Center
EnemyManager_Right
```

### 2. FirePosition이 다시 정리되어 있다

패치 과정에서 `FirePosition` 오브젝트가 다시 등장한다.

최종적으로는 Player의 자식으로 `FirePosition`이 유지되어야 한다.

PlayerFire의 `firePosition` 참조가 깨지지 않았는지 Unity Editor에서 반드시 확인해야 한다.

### 3. 생성된 Enemy 자동 삭제가 없다

EnemyManager가 일정 시간마다 Enemy를 계속 생성한다.

하지만 화면 밖으로 내려간 Enemy를 삭제하는 기능은 아직 없다.

시간이 지나면 Hierarchy에 Enemy가 계속 쌓일 수 있으므로, 다음 단계에서 삭제 처리가 필요하다.

### 4. 모든 EnemyManager가 같은 간격으로 생성한다

모든 EnemyManager의 `createTime`이 `1`이다.

따라서 여러 위치에서 동시에 Enemy가 생성될 수 있다.

처음 확인용으로는 좋지만, 나중에 난이도를 조정하려면 위치별 시간 차이나 랜덤 생성이 필요할 수 있다.

## 결론

이번 패치는 Enemy를 프리팹화하고, EnemyManager를 통해 반복 생성하는 구조를 만든 단계다.

이제 게임 화면에 Enemy가 계속 등장하는 기본 슈팅 게임 흐름이 만들어졌다.

다음 단계에서는 화면 밖으로 나간 Enemy와 Bullet을 삭제하는 기능을 추가하는 것이 좋다.
