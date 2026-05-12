# Ch01_Setup_08_EnemyManager

## 목표

적 오브젝트를 프리팹으로 만들고, 일정 시간마다 적을 생성하는 EnemyManager를 만든다.

이전 단계에서는 씬에 직접 배치된 Enemy가 아래로 이동하고 충돌하는 것까지 확인했다.  
이번 단계에서는 Enemy를 프리팹으로 만들고, EnemyManager가 필요한 위치에서 Enemy를 생성하게 한다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. 기존 Enemy 오브젝트를 프리팹으로 만든다.
2. `Assets/Prefabs/Enemy.prefab`을 생성한다.
3. 씬에 직접 배치되어 있던 Enemy 오브젝트는 제거한다.
4. `Assets/Scripts/EnemyManager.cs` 스크립트를 만든다.
5. 씬에 EnemyManager 오브젝트를 만든다.
6. EnemyManager가 일정 시간마다 Enemy 프리팹을 생성하게 한다.
7. 필요하다면 EnemyManager를 여러 위치에 배치해서 여러 지점에서 적이 나오게 한다.

## 중요

- 이번 작업은 Enemy 프리팹과 EnemyManager를 이용한 적 생성만 만든다.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- UI는 만들지 마.
- 사운드나 이펙트는 만들지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### Enemy 프리팹

Enemy는 앞으로 여러 번 생성될 오브젝트이므로 프리팹으로 만든다.

- 프리팹 위치: `Assets/Prefabs/Enemy.prefab`
- 기존 Enemy 스크립트와 Collider, Rigidbody 설정을 유지한다.
- 씬에 직접 놓여 있던 Enemy는 제거한다.
- 앞으로는 EnemyManager가 Enemy 프리팹을 생성한다.

### EnemyManager 오브젝트

씬에 `EnemyManager` 오브젝트를 만든다.

EnemyManager는 적이 생성되는 위치 역할을 한다.

- EnemyManager의 위치에 Enemy가 생성된다.
- 여러 위치에서 적을 만들고 싶다면 EnemyManager를 여러 개 배치해도 된다.
- 각 EnemyManager는 같은 Enemy 프리팹을 참조할 수 있다.

### EnemyManager 스크립트

`EnemyManager.cs`는 아래 기능만 담당한다.

- 시간이 흐르는 것을 계산한다.
- 일정 시간이 지나면 Enemy 프리팹을 생성한다.
- 생성한 Enemy를 EnemyManager의 위치에 배치한다.
- 생성 후 시간을 다시 0으로 초기화한다.

예상 코드 흐름:

```csharp
currentTime += Time.deltaTime;

if (currentTime > createTime)
{
    GameObject enemy = Instantiate(enemyFactory);
    enemy.transform.position = transform.position;
    currentTime = 0.0f;
}
```

## Unity Editor 설정

작업 후 Unity Editor에서 다음 연결이 필요하다.

- `Assets/Prefabs/Enemy.prefab`이 생성되어 있어야 한다.
- EnemyManager 오브젝트에 `EnemyManager` 스크립트가 붙어 있어야 한다.
- EnemyManager의 `enemyFactory`에 `Enemy.prefab`이 연결되어 있어야 한다.
- EnemyManager의 `createTime` 값이 적절하게 설정되어 있어야 한다.
- Play 모드에서 일정 시간마다 Enemy가 생성되는지 확인한다.

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- 파일 목록

새로 만든 파일:
- 파일 목록

새로 만든 프리팹:
- 프리팹 이름

새로 만든 오브젝트:
- 오브젝트 이름

Unity Editor에서 확인할 내용:
- Enemy 프리팹 생성 확인
- EnemyManager 스크립트 연결 확인
- EnemyManager의 enemyFactory 연결 확인
- Play 모드에서 적이 일정 시간마다 생성되는지 확인

아직 구현하지 않은 내용:
- 점수
- 게임 오버
- UI
- 사운드
- 이펙트
```
