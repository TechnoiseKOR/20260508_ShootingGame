# Ch01_Setup_09_EnemySpawnRandomTime

## 목표

EnemyManager가 적을 항상 같은 시간마다 생성하지 않고, 랜덤한 시간 간격으로 생성하게 만든다.

이전 단계에서는 EnemyManager의 `createTime` 값이 고정되어 있어서 적이 일정한 간격으로 생성되었다.  
이번 단계에서는 적이 생성되는 시간을 최소 시간과 최대 시간 사이에서 랜덤으로 정하도록 바꾼다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `Assets/Scripts/EnemyManager.cs`를 수정한다.
2. 적 생성 최소 시간 값을 추가한다.
3. 적 생성 최대 시간 값을 추가한다.
4. 게임이 시작될 때 적 생성 시간을 랜덤으로 정한다.
5. 적을 한 번 생성한 뒤, 다음 적 생성 시간도 다시 랜덤으로 정한다.

## 중요

- 이번 작업은 적 생성 시간 랜덤화만 만든다.
- 새 EnemyManager 구조를 만들지 마.
- Enemy 프리팹은 수정하지 마.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- UI는 만들지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### EnemyManager.cs

기존 EnemyManager의 흐름은 유지한다.

기존 흐름:

```csharp
currentTime += Time.deltaTime;

if (currentTime > createTime)
{
    GameObject enemy = Instantiate(enemyFactory);
    enemy.transform.position = transform.position;
    currentTime = 0.0f;
}
```

이번 단계에서는 `createTime`을 고정값으로만 쓰지 않고, 랜덤으로 다시 설정한다.

필요한 값:

```csharp
float minTime = 1.0f;
float maxTime = 5.0f;
```

게임이 시작될 때:

```csharp
createTime = UnityEngine.Random.Range(minTime, maxTime);
```

적을 생성한 뒤:

```csharp
createTime = UnityEngine.Random.Range(minTime, maxTime);
```

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- Assets/Scripts/EnemyManager.cs

주요 변경:
- 적 생성 시간이 랜덤으로 바뀜
- 적 생성 후 다음 생성 시간도 다시 랜덤으로 설정됨

Unity Editor에서 확인할 내용:
- Play 모드에서 적 생성 간격이 매번 조금씩 달라지는지 확인

아직 구현하지 않은 내용:
- 점수
- 게임 오버
- UI
- 사운드
- 이펙트
```
