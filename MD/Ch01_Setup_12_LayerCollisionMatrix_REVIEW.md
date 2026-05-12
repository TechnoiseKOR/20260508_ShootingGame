# Ch01_Setup_12_LayerCollisionMatrix_REVIEW

## 리뷰 대상

이번 리뷰 대상은 레이어 추가 및 레이어별 충돌 여부 설정 패치다.

패치 요약:

- `DestroyZone`, `Player`, `Bullet`, `Enemy` 레이어 추가
- Bullet 프리팹을 Bullet 레이어로 설정
- Enemy 프리팹을 Enemy 레이어로 설정
- Player와 FirePosition을 Player 레이어로 설정
- DestroyZone 오브젝트들을 DestroyZone 레이어로 설정
- Physics Layer Collision Matrix 변경

## 전체 평가

이번 작업은 DestroyZone과 충돌 처리를 만든 이후에 적절한 정리 단계다.

지금까지는 대부분의 오브젝트가 Default 레이어에 있었다.  
이 상태에서는 어떤 오브젝트끼리 충돌할지 세밀하게 관리하기 어렵다.

이번 패치로 Player, Bullet, Enemy, DestroyZone을 레이어로 나누었기 때문에, 앞으로 충돌 규칙을 관리하기 쉬워졌다.

## 좋은 점

### 1. 주요 오브젝트 역할이 레이어로 분리되었다

이제 각 오브젝트가 다음처럼 구분된다.

| 역할 | 레이어 |
|---|---|
| 삭제 영역 | DestroyZone |
| 플레이어 | Player |
| 총알 | Bullet |
| 적 | Enemy |

이렇게 나누면 Unity Physics 설정에서 충돌 여부를 한눈에 관리할 수 있다.

### 2. 프리팹에 레이어를 적용했다

Bullet과 Enemy는 계속 생성되는 오브젝트다.

프리팹에 레이어를 적용했기 때문에, 새로 생성되는 Bullet과 Enemy에도 같은 레이어가 적용된다.

씬에 있는 개별 오브젝트만 바꾸는 것보다 좋은 방식이다.

### 3. Layer Collision Matrix를 사용했다

코드에서 모든 충돌을 검사하기 전에, Physics 설정에서 불필요한 충돌을 줄일 수 있다.

이 방식은 나중에 오브젝트가 많아질 때도 유용하다.

### 4. 이후 게임 규칙 구현을 위한 준비가 되었다

다음 단계에서 아래 기능을 만들 때 더 정리된 상태로 진행할 수 있다.

- Bullet과 Enemy 충돌
- Enemy와 Player 충돌
- DestroyZone에서 Bullet과 Enemy 삭제
- 점수 증가
- 게임 오버

## 확인이 필요한 점

### 1. Layer Collision Matrix 직접 확인

`ProjectSettings/DynamicsManager.asset`에는 Layer Collision Matrix가 긴 값으로 저장된다.

이 값은 diff만 보고 의미를 판단하기 어렵다.

Unity Editor에서 다음 경로를 직접 확인하는 것이 좋다.

```text
Edit > Project Settings > Physics > Layer Collision Matrix
```

확인할 내용:

- Bullet과 Enemy가 충돌하는지
- Enemy와 Player가 충돌하는지
- DestroyZone이 Bullet과 Enemy를 감지하는지
- 필요 없는 충돌이 꺼져 있는지

### 2. Play 모드 동작 확인

Play 모드에서 실제 충돌 동작을 확인해야 한다.

확인할 내용:

- 총알이 적에 닿으면 기존처럼 충돌 처리되는지
- 적이 Player에 닿으면 충돌 처리되는지
- 총알이 DestroyZone에 닿으면 삭제되는지
- 적이 DestroyZone에 닿으면 삭제되는지

### 3. 레이어 적용 누락 확인

새로 생성되는 오브젝트는 프리팹의 레이어를 따른다.

따라서 다음 프리팹 설정이 중요하다.

- Bullet.prefab: Bullet 레이어
- Enemy.prefab: Enemy 레이어

Prefab 설정이 빠져 있으면 생성된 오브젝트가 Default 레이어로 남을 수 있다.

## 주의할 점

### 1. 코드 변경은 없다

이번 패치에서는 C# 코드가 바뀌지 않았다.

즉, 충돌 처리의 실제 결과는 기존 `Enemy.cs`와 `DestroyZone.cs` 동작을 그대로 따른다.

이번 작업은 코드 기능 추가가 아니라, 물리 충돌 설정을 정리하는 단계다.

### 2. Layer 번호를 임의로 바꾸면 안 된다

현재 기준은 다음과 같다.

| 번호 | 레이어 |
|---|---|
| 6 | DestroyZone |
| 7 | Player |
| 8 | Bullet |
| 9 | Enemy |

이 번호는 씬과 프리팹에 저장되어 있다.

나중에 TagManager에서 레이어 순서를 바꾸면 기존 오브젝트의 레이어 의미가 달라질 수 있으므로 주의해야 한다.

### 3. DestroyZone은 여전히 모든 오브젝트를 삭제한다

`DestroyZone.cs`는 들어온 오브젝트를 모두 삭제한다.

```csharp
Destroy(other.gameObject);
```

Layer Collision Matrix로 어느 정도 감지 대상을 제한할 수 있지만, 더 안전하게 하려면 나중에 코드에서도 삭제 대상을 확인하는 것이 좋다.

## 개선하면 좋은 점

### 1. 레이어 매트릭스 표로 문서화

수업용 프로젝트에서는 어떤 레이어끼리 충돌하는지 표로 정리해두면 좋다.

예:

| 충돌 조합 | 사용 여부 |
|---|---|
| Bullet - Enemy | 사용 |
| Enemy - Player | 사용 |
| Bullet - DestroyZone | 사용 |
| Enemy - DestroyZone | 사용 |
| Player - DestroyZone | 사용 안 함 |

### 2. DestroyZone 코드에 대상 확인 추가

나중에는 DestroyZone에서 Bullet과 Enemy만 삭제하도록 바꿀 수 있다.

예:

```csharp
if (other.gameObject.layer == LayerMask.NameToLayer("Bullet") ||
    other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
{
    Destroy(other.gameObject);
}
```

다만 현재 단계에서는 Layer Collision Matrix를 배우는 것이 목적이므로, 코드를 단순하게 유지해도 된다.

## 다음 단계 제안

다음 단계는 충돌 결과를 게임 규칙으로 연결하는 작업이 좋다.

추천 다음 작업:

```text
Ch01_Setup_13_CollisionRules
```

내용:

- Bullet과 Enemy 충돌 구분
- Enemy와 Player 충돌 구분
- 충돌 대상에 따라 다른 동작 처리

그다음 단계에서 점수와 게임 오버로 이어갈 수 있다.

```text
Ch01_Setup_14_ScoreBasic
Ch01_Setup_15_GameOverBasic
```

## 최종 결론

이번 패치는 오브젝트 충돌 관리를 위한 중요한 설정 단계다.

Player, Bullet, Enemy, DestroyZone을 레이어로 나누고 Layer Collision Matrix를 조정했기 때문에, 이후 점수와 게임 오버 같은 게임 규칙을 더 안정적으로 연결할 수 있다.

다음 단계에서는 충돌 대상에 따라 다른 처리를 하는 코드로 이어가면 좋다.
