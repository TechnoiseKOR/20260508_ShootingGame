# Ch01_Setup_25_BestScoreSave_REVIEW

## 리뷰 대상

이번 리뷰 대상은 PlayerPrefs를 사용한 최고 점수 저장과 불러오기 패치다.

패치 요약:

- `ScoreManager.cs`의 `Start()`에서 저장된 최고 점수 불러오기
- 불러온 최고 점수를 `bestScore` 변수에 저장
- 불러온 최고 점수를 BestScore UI에 표시
- `Enemy.cs`에서 최고 점수 갱신 시 PlayerPrefs에 저장

## 전체 평가

이번 작업은 최고 점수 UI 단계 이후 자연스러운 확장이다.

이전까지는 현재 점수가 최고 점수를 넘으면 BestScore UI가 갱신되었지만, 게임을 다시 시작하면 최고 점수가 0으로 돌아갔다.  
이번 변경으로 최고 점수를 PlayerPrefs에 저장하고 다시 불러오기 때문에, 최고 점수 기능이 훨씬 완성도 있게 바뀌었다.

초급 수업에서 “값을 저장하고 다시 불러오는 방법”을 소개하기 좋은 단계다.

## 좋은 점

### 1. PlayerPrefs 사용 위치가 단순하다

이번 패치에서는 저장과 불러오기가 각각 명확한 위치에 들어갔다.

불러오기:

```csharp
bestScore = PlayerPrefs.GetInt("Best Score", 0);
bestScoreUI.text = "최고 점수 : " + bestScore;
```

저장하기:

```csharp
PlayerPrefs.SetInt("Best Score", sm.bestScore);
```

복잡한 저장 구조 없이 최고 점수 하나만 다루기 때문에 초급자에게 적절하다.

### 2. 기존 최고 점수 갱신 흐름을 유지했다

기존 흐름은 그대로 유지하고, 최고 점수가 갱신되는 순간에 저장만 추가했다.

```text
현재 점수 증가
→ 최고 점수보다 큰지 확인
→ 최고 점수 갱신
→ 최고 점수 UI 표시
→ 최고 점수 저장
```

기능이 단계적으로 확장되는 흐름이 좋다.

### 3. UI와 저장 값이 연결되었다

게임 시작 시 저장된 값을 불러와 바로 BestScore UI에 표시한다.

덕분에 Play 모드를 다시 시작해도 이전 최고 점수를 화면에서 확인할 수 있다.

## 확인이 필요한 점

### 1. 최고 점수 유지 확인

Play 모드에서 다음 순서로 확인하면 좋다.

1. Enemy를 잡아 최고 점수를 올린다.
2. Play 모드를 종료한다.
3. 다시 Play 모드를 시작한다.
4. BestScore UI에 이전 최고 점수가 표시되는지 확인한다.

### 2. BestScore UI 연결 확인

`bestScoreUI` 연결이 빠져 있으면 `Start()`에서 오류가 날 수 있다.

ScoreManager 오브젝트의 Inspector에서 BestScore Text가 연결되어 있는지 확인해야 한다.

### 3. 저장 키 확인

저장과 불러오기 모두 `"Best Score"` 키를 사용한다.

```csharp
PlayerPrefs.GetInt("Best Score", 0);
PlayerPrefs.SetInt("Best Score", sm.bestScore);
```

키 이름이 같기 때문에 정상적으로 연결된다.

### 4. Console 오류 확인

Play 모드 시작 시 `ScoreManager.Start()`가 실행되므로, UI 연결이 빠졌다면 바로 오류가 발생할 수 있다.

Console을 확인하는 것이 중요하다.

## 개선하면 좋은 점

### 1. PlayerPrefs.Save() 추가 검토

현재는 `SetInt`만 사용한다.

일반적으로 충분하지만, 저장 시점을 더 명확히 하고 싶다면 다음 코드를 추가할 수 있다.

```csharp
PlayerPrefs.Save();
```

초급 수업에서는 나중 단계로 미뤄도 된다.

### 2. ScoreManager에 함수로 정리

현재 Enemy가 점수와 최고 점수 갱신, UI 갱신, 저장을 직접 처리한다.

나중에는 ScoreManager에 아래 함수를 만들면 좋다.

```csharp
public void AddScore(int amount)
{
    currentScore += amount;
    currentScoreUI.text = "현재 점수 : " + currentScore;

    if (currentScore > bestScore)
    {
        bestScore = currentScore;
        bestScoreUI.text = "최고 점수 : " + bestScore;
        PlayerPrefs.SetInt("Best Score", bestScore);
    }
}
```

그러면 Enemy는 점수 처리 내용을 몰라도 된다.

```csharp
sm.AddScore(1);
```

### 3. 충돌 대상 구분

현재 Enemy는 어떤 오브젝트와 충돌해도 점수가 오를 수 있다.

다음 단계에서는 Bullet과 충돌했을 때만 점수 증가와 최고 점수 저장이 일어나도록 바꾸는 것이 좋다.

### 4. 최고 점수 초기화 기능

수업이나 테스트 중에는 최고 점수를 지우고 다시 확인하고 싶을 수 있다.

나중에 아래 기능을 만들 수 있다.

```csharp
PlayerPrefs.DeleteKey("Best Score");
```

다만 학생 수업 초반에는 아직 필요하지 않다.

## 다음 단계 제안

다음 단계는 점수 구조를 정리하는 것이 좋다.

추천 다음 작업:

```text
Ch01_Setup_26_ScoreManagerRefactor
```

내용:

- ScoreManager에 AddScore 함수 추가
- Enemy가 점수 UI와 PlayerPrefs를 직접 수정하지 않게 변경
- 점수 처리 책임을 ScoreManager로 모으기

또는 충돌 대상 구분을 먼저 진행해도 좋다.

```text
Ch01_Setup_26_CollisionTargetCheck
```

내용:

- Enemy가 Bullet과 충돌했을 때만 점수 증가
- Player와 충돌했을 때는 점수 증가하지 않도록 처리
- 이후 게임 오버 기능 준비

수업 흐름상은 `ScoreManagerRefactor`를 먼저 진행하고, 그 다음 `CollisionTargetCheck`로 가는 것이 깔끔하다.

## 최종 결론

이번 패치는 최고 점수를 저장하고 불러오는 기능을 추가한 중요한 단계다.

`PlayerPrefs`를 사용해 게임을 다시 시작해도 최고 점수가 유지되므로, 최고 점수 시스템의 기본 흐름이 완성되었다.

다음 단계에서는 ScoreManager에 점수 처리 함수를 만들고, Enemy가 점수 내부 구조를 직접 다루지 않도록 정리하면 좋다.
