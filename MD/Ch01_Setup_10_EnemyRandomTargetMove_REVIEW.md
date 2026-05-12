# Ch01_Setup_10_EnemyRandomTargetMove_REVIEW

## 리뷰 대상

이번 리뷰 대상은 Enemy가 랜덤 확률로 Player 방향을 향해 이동하도록 수정한 패치다.

패치 요약:

- `Enemy.cs`에 이동 방향 필드 `dir` 추가
- `Start()`에서 랜덤 값을 뽑아 이동 방향 결정
- 약 30% 확률로 Player 방향을 계산
- 나머지는 기존처럼 아래 방향으로 이동
- `Update()`에서는 Start에서 정한 방향으로 계속 이동

## 전체 평가

이번 작업은 EnemyManager로 적을 반복 생성한 이후 자연스러운 개선 단계다.

이전에는 모든 Enemy가 항상 아래쪽으로만 이동했다.  
이번에는 일부 Enemy가 Player 방향으로 이동하기 때문에 게임 화면의 변화가 커진다.

학생들이 랜덤 값, 조건문, 방향 벡터를 함께 복습하기 좋은 단계다.

## 좋은 점

### 1. 변경 범위가 작다

이번 패치는 `Enemy.cs` 하나만 수정한다.

EnemyManager, Enemy 프리팹, 씬 구조를 바꾸지 않고 Enemy의 이동 방식만 개선했기 때문에 변경 범위가 적절하다.

### 2. 기존 이동 구조를 확장했다

기존에는 아래 방향만 사용했다.

```csharp
Vector3.down
```

이번에는 조건에 따라 Player 방향 또는 아래 방향을 선택한다.

이렇게 기존 구조를 조금씩 확장하는 방식은 초급자 수업에 적합하다.

### 3. 방향 벡터 개념을 설명하기 좋다

이번 코드에는 방향 계산이 들어간다.

```csharp
dir = target.transform.position - transform.position;
dir.Normalize();
```

이 코드는 “목표 위치 - 내 위치 = 목표를 향하는 방향”이라는 개념을 설명하기 좋다.

### 4. 랜덤 조건을 이해하기 쉽다

```csharp
int randValue = UnityEngine.Random.Range(0, 10);

if (randValue < 3)
```

이 구조는 약 30% 확률을 만들기 쉽고,
학생들이 숫자를 바꿔보면서 확률 변화를 확인하기 좋다.

## 확인이 필요한 점

### 1. Player 오브젝트 이름 확인

`GameObject.Find("Player")`를 사용하므로,
씬에 있는 플레이어 오브젝트 이름이 정확히 `Player`여야 한다.

이름이 바뀌면 target이 null이 되고,
Enemy는 기본 아래 방향으로 이동한다.

### 2. Play 모드 이동 확인

Play 모드에서 다음을 확인한다.

- Enemy가 계속 생성되는지
- 일부 Enemy가 Player 방향으로 이동하는지
- 모든 Enemy가 Player만 따라오지는 않는지
- 아래로 내려오는 Enemy도 섞여 있는지

### 3. 생성 순간 방향만 사용한다는 점 확인

이번 Enemy는 Player를 계속 추적하지 않는다.

Enemy가 생성될 때 Player 방향을 한 번 정하고,
그 방향으로 계속 이동한다.

Player가 이후에 움직여도 Enemy 방향은 바뀌지 않는다.

이 점을 수업에서 명확히 설명하면 좋다.

## 개선하면 좋은 점

### 1. 주석 정리

패치 안에 다음 주석이 있다.

```csharp
// 방향을 구하고 싶다. target - maxTime
```

`maxTime`은 여기서 의미가 맞지 않는다.

다음처럼 바꾸면 더 좋다.

```csharp
// 방향을 구한다. 목표 위치 - 내 위치
```

또는:

```csharp
// Player 방향을 구한다.
```

### 2. 확률 값을 변수로 분리

현재는 `randValue < 3`으로 직접 적혀 있다.

나중에 수업이 조금 진행되면 다음처럼 변수로 분리할 수 있다.

```csharp
int chaseChance = 3;
```

그러면 “10번 중 3번은 Player 방향”이라는 의미가 더 잘 드러난다.

### 3. GameObject.Find 개선

초급 단계에서는 `GameObject.Find("Player")`가 이해하기 쉽다.

하지만 나중에 구조를 정리할 때는 태그, 참조 연결, 또는 GameManager를 통해 Player를 찾는 방식으로 개선할 수 있다.

### 4. 추적 Enemy와 구분하기

이번 구현은 Player를 계속 따라가는 것이 아니라,
생성 순간의 Player 방향으로 이동하는 기능이다.

나중에 진짜 추적 Enemy를 만들고 싶다면 Update에서 매번 방향을 다시 계산해야 한다.

## 다음 단계 제안

다음 단계는 생성된 오브젝트 정리 기능이 적절하다.

추천 다음 작업:

```text
Ch01_Setup_11_DestroyZone
```

내용:

- 화면 밖으로 나간 Bullet과 Enemy 삭제
- DestroyZone 오브젝트 추가
- Trigger 또는 Collision으로 오브젝트 제거

또는 코드에서 위치를 보고 삭제하는 방식도 가능하다.

```text
Ch01_Setup_11_RemoveOutOfScreen
```

내용:

- Bullet이 위쪽 경계를 넘으면 삭제
- Enemy가 아래쪽 또는 화면 밖으로 나가면 삭제

## 최종 결론

이번 패치는 Enemy 이동 패턴에 변화를 주는 좋은 단계다.

변경 범위가 작고,
랜덤, 조건문, 방향 벡터를 한 번에 복습할 수 있어 수업용으로 적절하다.

다음 단계에서는 계속 생성되는 Bullet과 Enemy가 화면 밖에서 쌓이지 않도록 삭제 처리를 추가하는 것이 좋다.
