# Ch01_Setup_26_ScoreManagerRefactor_REVIEW

## 리뷰 대상

이번 리뷰 대상은 점수 계산과 UI 갱신을 Enemy에서 ScoreManager로 옮기는 리팩터링 패치다.

리뷰에는 추가 수정 패치까지 포함한다.

패치 요약:

- `currentScore`를 private으로 변경
- `bestScore`를 private으로 변경
- ScoreManager에 `SetScore(int value)` 추가
- ScoreManager에 `GetScore()` 추가
- 현재 점수 UI 갱신 로직을 ScoreManager로 이동
- 최고 점수 갱신과 저장 로직을 ScoreManager로 이동
- Enemy는 ScoreManager 함수만 호출하도록 변경
- 추가 수정으로 `SetScore(int value)`가 전달받은 값을 사용하도록 변경

## 전체 평가

이번 작업은 점수 시스템을 한 단계 정리한 좋은 리팩터링이다.

이전까지 Enemy는 충돌 처리뿐 아니라 점수 증가, UI 갱신, 최고 점수 갱신, PlayerPrefs 저장까지 직접 담당했다.  
이번 변경으로 점수와 관련된 책임이 ScoreManager로 옮겨졌고, Enemy는 점수 증가 요청만 하게 되었다.

초급 수업에서 “기능은 동작하지만 코드 책임이 섞였을 때, 관리 클래스로 옮긴다”는 개념을 설명하기 좋은 단계다.

## 좋은 점

### 1. 점수 필드를 private으로 바꿨다

`currentScore`와 `bestScore`가 private으로 바뀌었다.

이제 다른 스크립트가 점수 값을 마음대로 바꾸기 어렵다.

점수 값은 ScoreManager의 함수를 통해 처리하는 구조가 되었다.

### 2. Enemy 코드가 단순해졌다

Enemy는 더 이상 UI나 PlayerPrefs를 직접 다루지 않는다.

변경 후 Enemy의 점수 관련 코드는 다음 정도로 줄었다.

```csharp
GameObject smObject = GameObject.Find("ScoreManager");
ScoreManager sm = smObject.GetComponent<ScoreManager>();
sm.SetScore(sm.GetScore() + 1);
```

Enemy는 충돌, 폭발, 제거를 담당하고, 점수 처리는 ScoreManager에게 맡기는 구조가 되었다.

### 3. 점수 UI 갱신과 최고 점수 저장이 ScoreManager로 모였다

ScoreManager가 다음을 담당한다.

```text
현재 점수 설정
현재 점수 UI 갱신
최고 점수 비교
최고 점수 UI 갱신
최고 점수 저장
```

점수 관련 코드가 한곳에 모였기 때문에 이후 수정이 쉬워진다.

### 4. 추가 수정으로 SetScore 동작이 자연스러워졌다

처음에는 `SetScore(int value)`가 `value`를 받지만 사용하지 않고 `currentScore++`만 했다.

추가 수정 후에는 아래처럼 바뀌었다.

```csharp
currentScore = value;
```

이제 `SetScore`라는 이름에 맞게 전달받은 값을 점수에 반영한다.

## 확인이 필요한 점

### 1. 현재 점수 증가 확인

Play 모드에서 Enemy를 잡을 때 현재 점수가 1씩 올라가는지 확인해야 한다.

Enemy에서 다음처럼 호출하고 있기 때문에:

```csharp
sm.SetScore(sm.GetScore() + 1);
```

정상이라면 점수가 이전 값보다 1 증가해야 한다.

### 2. 최고 점수 갱신 확인

현재 점수가 최고 점수를 넘으면 BestScore UI도 같이 갱신되어야 한다.

또한 PlayerPrefs 저장도 기존처럼 동작해야 한다.

### 3. Play 모드 재시작 확인

Play 모드를 종료했다가 다시 시작했을 때 이전 최고 점수가 표시되는지 확인한다.

이 부분은 `ScoreManager.Start()`의 PlayerPrefs 불러오기 로직에 의존한다.

### 4. UI 연결 확인

ScoreManager의 UI 연결이 유지되어야 한다.

- CurrentScore Text
- BestScore Text

둘 중 하나라도 빠지면 점수 갱신 시 오류가 날 수 있다.

## 개선하면 좋은 점

### 1. AddScore 함수 추가

현재 구조는 동작하지만 점수 증가를 표현하기에는 조금 우회적이다.

```csharp
sm.SetScore(sm.GetScore() + 1);
```

나중에는 다음처럼 바꾸면 더 직관적이다.

```csharp
public void AddScore(int amount)
{
    SetScore(currentScore + amount);
}
```

Enemy에서는 이렇게 호출할 수 있다.

```csharp
sm.AddScore(1);
```

이렇게 하면 “점수를 1 올린다”는 의미가 더 명확해진다.

### 2. GameObject.Find 줄이기

Enemy는 충돌할 때마다 ScoreManager를 찾는다.

```csharp
GameObject.Find("ScoreManager")
```

초급 단계에서는 괜찮지만, 나중에 적이 많아지면 비효율적일 수 있다.

다음 방식으로 개선할 수 있다.

- Start에서 한 번 찾아 캐싱
- ScoreManager를 static Instance로 만들기
- GameManager 구조 사용

### 3. 충돌 대상 구분

현재 Enemy는 어떤 오브젝트와 충돌해도 점수가 올라갈 수 있다.

다음 단계에서는 Bullet과 충돌했을 때만 점수가 오르도록 처리하는 것이 좋다.

예:

```csharp
if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
{
    sm.SetScore(sm.GetScore() + 1);
}
```

또는 이후 `AddScore(1)`로 바꿀 수 있다.

### 4. Null 체크 추가

현재는 ScoreManager와 UI 연결이 반드시 존재한다고 가정한다.

나중에 안정성을 높이려면 null 체크를 추가할 수 있다.

### 5. PlayerPrefs.Save() 추가 검토

현재 최고 점수 저장은 `PlayerPrefs.SetInt`만 사용한다.

즉시 저장을 더 명확히 하고 싶다면 나중에 아래 코드를 추가할 수 있다.

```csharp
PlayerPrefs.Save();
```

## 다음 단계 제안

다음 단계는 충돌 대상 구분이 가장 자연스럽다.

추천 다음 작업:

```text
Ch01_Setup_27_CollisionTargetCheck
```

내용:

- Enemy가 Bullet과 충돌했을 때만 점수 증가
- Enemy가 Player와 충돌했을 때는 점수 증가하지 않기
- 이후 게임 오버 기능을 넣기 위한 준비

또는 점수 API를 더 직관적으로 만들 수도 있다.

```text
Ch01_Setup_27_AddScoreFunction
```

내용:

- ScoreManager에 AddScore 함수 추가
- Enemy에서 `SetScore(GetScore() + 1)` 대신 `AddScore(1)` 호출
- 점수 증가 의도를 더 명확하게 정리

수업 흐름상은 충돌 대상 구분으로 가는 것이 더 좋다.

## 최종 결론

이번 패치는 점수 시스템의 책임을 ScoreManager로 모은 중요한 리팩터링 단계다.

Enemy 코드가 단순해졌고, 현재 점수, 최고 점수, UI 갱신, PlayerPrefs 저장이 ScoreManager 내부로 정리되었다.

추가 수정으로 `SetScore(int value)`가 매개변수를 실제로 사용하게 되었기 때문에 최종 구조도 더 자연스럽다.
