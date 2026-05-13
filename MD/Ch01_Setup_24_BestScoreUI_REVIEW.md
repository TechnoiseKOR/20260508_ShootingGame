# Ch01_Setup_24_BestScoreUI_REVIEW

## 리뷰 대상

이번 리뷰 대상은 최고 점수 UI 갱신 기능 추가 패치다.

패치 요약:

- `ScoreManager.cs`에 `bestScoreUI` 추가
- `ScoreManager.cs`에 `bestScore` 추가
- ScoreManager 오브젝트에 BestScore Text UI 연결
- Enemy 충돌 시 현재 점수가 최고 점수를 넘으면 최고 점수 갱신
- BestScore Text UI 갱신
- `Mat_Background.mat` 오프셋 변경

## 전체 평가

이번 작업은 이전 점수 UI 단계 이후 자연스러운 확장이다.

이전 단계에서는 현재 점수만 실제로 증가했고, 최고 점수 UI는 화면에 자리만 있었다.  
이번 변경으로 현재 점수가 최고 점수를 넘을 때 최고 점수 UI도 함께 올라가므로, 점수 표시가 더 완성된 형태가 되었다.

초급 수업에서 `if` 조건문을 활용해 “현재 값이 기존 최고값보다 크면 갱신한다”는 개념을 설명하기 좋다.

## 좋은 점

### 1. 이전 단계에서 만든 BestScore UI를 실제로 연결했다

이전 단계에서 Canvas 아래에 만들어둔 `BestScore` Text UI가 이번 단계에서 ScoreManager에 연결되었다.

이제 화면에 보이는 최고 점수 UI가 실제 값과 연결된다.

### 2. 최고 점수 갱신 조건이 단순하다

핵심 조건은 다음과 같다.

```csharp
if (sm.currentScore > sm.bestScore)
```

현재 점수가 최고 점수보다 커졌을 때만 최고 점수를 갱신한다.

초급자도 이해하기 쉬운 구조다.

### 3. 현재 점수와 최고 점수의 차이를 설명하기 좋다

이번 단계는 다음 개념을 설명하기 좋다.

- 현재 점수: 이번 플레이에서 얻은 점수
- 최고 점수: 지금까지 이번 실행 중 가장 높았던 점수

다만 아직 저장 기능은 없으므로 “게임을 다시 시작해도 유지되는 최고 점수”는 아니다.

### 4. 기존 점수 증가 흐름을 유지했다

기존의 현재 점수 증가 코드를 그대로 두고, 그 아래에 최고 점수 확인을 추가했다.

수업 흐름상 이전 코드에서 어떻게 기능이 확장되는지 보여주기 좋다.

## 확인이 필요한 점

### 1. BestScore 연결 확인

ScoreManager의 `bestScoreUI`에 BestScore Text가 연결되어 있어야 한다.

연결이 빠지면 최고 점수 갱신 시 오류가 발생할 수 있다.

### 2. Play 모드 최고 점수 확인

Play 모드에서 다음을 확인한다.

- Enemy를 잡으면 현재 점수가 올라가는지
- 현재 점수가 최고 점수를 넘으면 최고 점수도 올라가는지
- BestScore Text가 화면에 잘 보이는지
- Console에 오류가 없는지

### 3. 최고 점수 초기값 확인

현재 `bestScore`는 0으로 시작한다.

점수가 1이 되는 순간 최고 점수도 1로 갱신되는 것이 정상이다.

### 4. Mat_Background 변경 확인

이번 패치에 `Mat_Background.mat` 오프셋 변경이 포함되어 있다.

이 변경은 최고 점수 기능과 직접 관련이 없으므로, 의도된 변경인지 확인하면 좋다.

## 개선하면 좋은 점

### 1. 최고 점수 저장

현재 최고 점수는 게임 실행 중에만 유지된다.

나중에는 `PlayerPrefs`를 사용해서 게임을 다시 실행해도 최고 점수가 남아 있게 만들 수 있다.

예상 다음 기능:

```text
PlayerPrefs.GetInt("BestScore", 0)
PlayerPrefs.SetInt("BestScore", bestScore)
```

### 2. ScoreManager에 AddScore 함수 만들기

현재 Enemy가 ScoreManager의 값을 직접 바꾼다.

나중에는 다음처럼 ScoreManager 내부 함수로 정리하면 좋다.

```csharp
public void AddScore(int amount)
{
    currentScore += amount;
    currentScoreUI.text = "현재 점수 : " + currentScore;

    if (currentScore > bestScore)
    {
        bestScore = currentScore;
        bestScoreUI.text = "최고 점수 : " + bestScore;
    }
}
```

그럼 Enemy는 이렇게만 호출하면 된다.

```csharp
sm.AddScore(1);
```

이렇게 하면 점수 처리 책임이 ScoreManager 안으로 모인다.

### 3. 충돌 대상 구분

현재 Enemy는 어떤 오브젝트와 충돌해도 점수가 올라갈 수 있다.

게임 규칙상 Bullet과 충돌했을 때만 점수가 올라가는 편이 자연스럽다.

다음 단계에서 반드시 정리하면 좋다.

### 4. UI 초기 텍스트 세팅

현재 Text의 초기값은 씬에서 직접 설정한다.

나중에는 ScoreManager의 `Start()`에서 초기 UI를 세팅할 수 있다.

```csharp
currentScoreUI.text = "현재 점수 : " + currentScore;
bestScoreUI.text = "최고 점수 : " + bestScore;
```

## 다음 단계 제안

다음 단계는 점수 구조를 정리하는 것이 좋다.

추천 다음 작업:

```text
Ch01_Setup_25_ScoreManagerRefactor
```

내용:

- ScoreManager에 AddScore 함수 추가
- Enemy가 UI를 직접 수정하지 않도록 변경
- 점수 갱신 로직을 ScoreManager 안으로 이동

또는 최고 점수 저장으로 바로 이어갈 수 있다.

```text
Ch01_Setup_25_BestScoreSave
```

내용:

- PlayerPrefs로 최고 점수 저장
- 게임을 다시 시작해도 최고 점수 유지
- Start에서 저장된 최고 점수 불러오기

수업 흐름상으로는 먼저 `ScoreManagerRefactor`를 한 뒤 `BestScoreSave`를 진행하는 편이 더 깔끔하다.

## 최종 결론

이번 패치는 최고 점수 UI를 실제 점수 값과 연결한 좋은 확장 단계다.

현재 점수와 최고 점수의 개념을 분리해서 보여줄 수 있고, 조건문을 이용한 최고값 갱신을 설명하기 좋다.

다음 단계에서는 ScoreManager에 점수 처리 함수를 만들고, 충돌 대상 구분과 최고 점수 저장으로 이어가면 좋다.
