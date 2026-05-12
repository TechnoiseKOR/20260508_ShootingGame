# Ch01_Setup_10_EnemyRandomTargetMove

## 목표

Enemy가 항상 아래쪽으로만 이동하지 않고, 일정 확률로 Player 방향을 향해 이동하게 만든다.

이전 단계에서는 EnemyManager가 Enemy를 일정 시간마다 생성했고, 생성된 Enemy는 항상 아래쪽으로 이동했다.  
이번 단계에서는 Enemy가 생성될 때 이동 방향을 한 번 정하고, 일부 Enemy는 Player 방향으로 이동하게 한다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `Assets/Scripts/Enemy.cs`만 수정한다.
2. Enemy의 이동 방향을 저장할 변수를 추가한다.
3. Enemy가 생성될 때 랜덤 값을 뽑는다.
4. 일정 확률로 Player 오브젝트를 찾는다.
5. Player를 찾았다면 Player 방향을 계산해서 이동 방향으로 사용한다.
6. Player를 찾지 못했거나 조건에 맞지 않으면 기존처럼 아래쪽으로 이동한다.
7. Update에서는 Start에서 정한 방향으로 계속 이동한다.

## 중요

- 이번 작업은 Enemy 이동 방향 랜덤화만 만든다.
- EnemyManager는 수정하지 마.
- Enemy 프리팹은 수정하지 마.
- PlayerMove, PlayerFire, Bullet은 수정하지 마.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- UI, 사운드, 이펙트는 만들지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### Enemy.cs

기존 Enemy는 Update에서 매번 아래 방향을 사용했다.

```csharp
Vector3 dir = Vector3.down;
transform.position += dir * speed * Time.deltaTime;
```

이번 단계에서는 이동 방향을 필드로 만든다.

```csharp
Vector3 dir;
```

그리고 Start에서 방향을 한 번 정한다.

### 랜덤 방향 선택

Enemy가 생성될 때 랜덤 값을 뽑는다.

```csharp
int randValue = UnityEngine.Random.Range(0, 10);
```

예를 들어 `randValue`가 3보다 작으면 Player 방향으로 이동하게 한다.

```csharp
if (randValue < 3)
{
    GameObject target = GameObject.Find("Player");

    if (target != null)
    {
        dir = target.transform.position - transform.position;
        dir.Normalize();
        return;
    }
}
```

조건에 맞지 않거나 Player를 찾지 못하면 아래 방향으로 이동한다.

```csharp
dir = Vector3.down;
```

### Update 이동

Update에서는 Start에서 정해진 방향을 사용한다.

```csharp
transform.position += dir * speed * Time.deltaTime;
```

## 확인 기준

Play 모드에서 다음을 확인한다.

- 대부분의 Enemy는 아래쪽으로 이동하는지 확인
- 일부 Enemy는 Player가 있던 방향으로 이동하는지 확인
- Enemy가 생성될 때마다 이동 방향이 달라질 수 있는지 확인
- Console에 오류가 없는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- Assets/Scripts/Enemy.cs

주요 변경:
- Enemy가 생성될 때 이동 방향을 한 번 정하도록 수정
- 일부 Enemy는 Player 방향으로 이동
- 나머지 Enemy는 기존처럼 아래쪽으로 이동

Unity Editor에서 확인할 내용:
- Play 모드에서 Enemy 이동 방향이 달라지는지 확인
- Player 오브젝트 이름이 Player인지 확인

아직 구현하지 않은 내용:
- 점수
- 게임 오버
- UI
- 사운드
- 이펙트
```
