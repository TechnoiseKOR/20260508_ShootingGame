# Ch01_Setup_09_EnemySpawnRandomTime_REVIEW

## 리뷰 대상

이번 리뷰 대상은 EnemyManager의 적 생성 시간 랜덤화 패치다.

패치 요약:

- `EnemyManager.cs`에 `minTime`, `maxTime` 추가
- `Start()`에서 최초 적 생성 시간을 랜덤으로 설정
- 적 생성 후 다음 생성 시간도 다시 랜덤으로 설정

## 전체 평가

이번 작업은 EnemyManager 단계 이후 자연스러운 개선 작업이다.

이전에는 모든 EnemyManager가 같은 `createTime` 기준으로 적을 생성했다.  
이번 변경으로 각 EnemyManager의 생성 타이밍이 달라지고, 적이 더 불규칙하게 등장할 수 있다.

게임 느낌을 조금 더 자연스럽게 만드는 좋은 단계다.

## 좋은 점

### 1. 기존 구조를 크게 바꾸지 않았다

이번 변경은 기존 EnemyManager 구조를 유지한다.

기존 흐름에 랜덤 시간 설정만 추가했기 때문에,
학생들이 이전 코드와 비교하면서 차이를 이해하기 쉽다.

### 2. Start를 활용했다

비어 있던 `Start()`에 최초 생성 시간을 설정하는 코드가 들어갔다.

```csharp
createTime = UnityEngine.Random.Range(minTime, maxTime);
```

이를 통해 `Start()`가 게임 시작 시 한 번 실행된다는 점을 설명하기 좋다.

### 3. 적 생성 후 다음 시간도 다시 정한다

적을 한 번 생성한 뒤에도 다음 생성 시간을 다시 랜덤으로 정한다.

이 구조 덕분에 생성 간격이 계속 같은 값으로 반복되지 않는다.

### 4. Random.Range를 설명하기 좋다

이번 단계는 Unity의 `Random.Range`를 처음 소개하기 좋다.

최소값과 최대값 사이에서 값을 뽑아 게임에 변화감을 줄 수 있다는 점을 보여준다.

## 확인이 필요한 점

### 1. Play 모드에서 생성 간격 확인

Play 모드에서 적이 생성되는 간격을 확인해야 한다.

확인할 내용:

- 적이 계속 생성되는지
- 적 생성 간격이 매번 조금씩 다른지
- 여러 EnemyManager가 같은 순간에만 생성하지 않는지

### 2. minTime, maxTime 값 확인

현재 범위는 다음과 같다.

```csharp
float minTime = 1.0f;
float maxTime = 5.0f;
```

수업 중에는 이 값을 바꿔보며 적 생성 속도가 어떻게 달라지는지 확인할 수 있다.

### 3. createTime 값의 의미

현재 `createTime`은 public 필드지만 실행 중 랜덤 값으로 바뀐다.

Inspector에 보이는 값과 실제 실행 중 값의 관계를 학생들이 헷갈릴 수 있다.

수업에서는 다음처럼 설명하면 좋다.

- `createTime`은 이번에 기다릴 시간이다.
- `minTime`, `maxTime`은 랜덤 시간을 뽑을 범위다.
- 적이 생성될 때마다 `createTime`이 새로 정해진다.

## 개선하면 좋은 점

### 1. minTime과 maxTime을 Inspector에 노출하기

현재 `minTime`, `maxTime`은 Inspector에서 보이지 않는다.

나중에 값을 쉽게 조정하려면 아래처럼 바꿀 수 있다.

```csharp
[SerializeField] private float minTime = 1.0f;
[SerializeField] private float maxTime = 5.0f;
```

다만 초반 수업에서는 코드에서 직접 바꿔보는 방식도 괜찮다.

### 2. minTime이 maxTime보다 큰 경우 처리

현재는 `minTime`과 `maxTime` 값이 정상이라는 전제로 작성되어 있다.

나중에 안정성을 높이려면 `minTime <= maxTime`인지 확인하는 처리를 넣을 수 있다.

하지만 지금 단계에서는 필요하지 않다.

### 3. 적 생성 위치 랜덤화

현재는 EnemyManager가 배치된 위치에서 적이 생성된다.

다음 개선으로는 위치를 랜덤으로 정할 수도 있다.

다만 지금 프로젝트 흐름에서는 여러 EnemyManager를 배치하는 방식이 더 직관적이다.

## 다음 단계 제안

다음 단계는 생성된 오브젝트 정리 기능이 적절하다.

추천 다음 작업:

```text
Ch01_Setup_10_DestroyZone
```

내용:

- 화면 밖으로 나간 Bullet과 Enemy를 삭제하는 영역 만들기
- DestroyZone 오브젝트 추가
- Trigger 또는 Collision으로 오브젝트 제거

또는 더 단순하게 각 오브젝트가 일정 범위를 벗어나면 스스로 삭제하게 할 수도 있다.

```text
Ch01_Setup_10_RemoveOutOfScreen
```

내용:

- Bullet이 위쪽 경계를 넘으면 삭제
- Enemy가 아래쪽 경계를 넘으면 삭제

## 최종 결론

이번 패치는 EnemyManager에 랜덤성을 추가하는 좋은 개선 단계다.

코드 변경이 작고,
이전 EnemyManager 흐름을 유지하기 때문에 수업 난이도도 적절하다.

이제 게임 화면에 적이 더 자연스러운 간격으로 등장하게 되었고,
다음 단계에서는 계속 생성되는 오브젝트를 정리하는 기능으로 넘어가면 좋다.
