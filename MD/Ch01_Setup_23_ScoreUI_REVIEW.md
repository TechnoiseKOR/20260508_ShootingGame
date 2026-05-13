# Ch01_Setup_23_ScoreUI_REVIEW

## 리뷰 대상

이번 리뷰 대상은 현재 점수와 최고 점수 UI 추가, Enemy 충돌 시 현재 점수 증가 패치다.

패치 요약:

- Canvas 추가
- CurrentScore Text UI 추가
- BestScore Text UI 추가
- EventSystem 추가
- ScoreManager 오브젝트 추가
- `ScoreManager.cs` 추가
- Enemy 충돌 시 현재 점수 증가
- 현재 점수 UI 텍스트 갱신
- `Mat_Background.mat` 오프셋 변경

## 전체 평가

이번 작업은 시각/청각 요소가 갖춰진 뒤 게임 규칙을 추가하는 첫 단계로 적절하다.

이전까지는 Enemy를 맞추면 폭발하고 사라지지만, 플레이 결과가 숫자로 남지 않았다.  
이번 변경으로 Enemy를 잡을 때마다 현재 점수가 올라가므로, 플레이 목적이 더 명확해진다.

UI Canvas와 Text를 사용해 점수를 보여주는 방식도 초급 수업에 적절하다.

## 좋은 점

### 1. 점수 UI가 화면에 추가되었다

Canvas 아래에 다음 UI가 추가되었다.

```text
CurrentScore
BestScore
```

현재 점수와 최고 점수 자리를 미리 만들어 두었기 때문에, 이후 최고 점수 기능으로 확장하기 좋다.

### 2. ScoreManager가 별도 오브젝트로 분리되었다

점수 값을 Enemy 안에 직접 저장하지 않고, `ScoreManager` 오브젝트를 따로 만들었다.

이 구조는 이후 점수 표시, 최고 점수 저장, 게임 오버 처리로 확장하기 좋다.

### 3. Enemy를 잡을 때 즉시 UI가 갱신된다

Enemy 충돌 시 다음 코드로 점수가 올라가고 UI가 바로 바뀐다.

```csharp
sm.currentScore++;
sm.currentScoreUI.text = "현재 점수 : " + sm.currentScore;
```

학생들이 Play 모드에서 결과를 바로 확인할 수 있어 학습 효과가 좋다.

### 4. Unity UI의 기본 구조를 경험할 수 있다

이번 단계에서 Canvas, Text, EventSystem이 추가되었다.

수업에서 Unity UI의 기본 구성을 설명하기 좋은 시점이다.

## 확인이 필요한 점

### 1. CurrentScore 연결 확인

ScoreManager의 `currentScoreUI` 필드에 CurrentScore Text가 연결되어 있어야 한다.

연결이 빠지면 점수 증가 시 오류가 발생할 수 있다.

### 2. Play 모드 점수 증가 확인

Play 모드에서 다음을 확인한다.

- Enemy를 Bullet으로 맞추면 현재 점수가 올라가는지
- 점수가 1씩 증가하는지
- CurrentScore Text가 화면에 잘 보이는지
- Console에 오류가 없는지

### 3. Enemy와 Player 충돌 확인

현재 Enemy는 충돌 대상을 구분하지 않는다.

따라서 Enemy가 Player와 충돌해도 점수가 올라갈 수 있다.

이 동작은 현재 단계에서는 단순해서 이해하기 쉽지만, 게임 규칙상으로는 이후 수정이 필요하다.

### 4. BestScore 상태 확인

BestScore UI는 화면에 배치되었지만 아직 값이 갱신되지 않는다.

수업에서 “이번에는 자리만 만들고, 다음에 최고 점수를 연결한다”고 설명하면 좋다.

## 개선하면 좋은 점

### 1. 충돌 대상 구분

가장 먼저 개선할 부분은 충돌 대상 구분이다.

현재는 Enemy가 어떤 오브젝트와 충돌해도 점수가 올라간다.

다음 단계에서는 Bullet과 충돌했을 때만 점수를 올리도록 바꾸는 것이 좋다.

예상 방향:

```csharp
if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
{
    // 점수 증가
}
```

또는 태그를 사용할 수도 있다.

### 2. ScoreManager에 AddScore 함수 만들기

현재 Enemy가 ScoreManager의 필드에 직접 접근한다.

```csharp
sm.currentScore++;
sm.currentScoreUI.text = "현재 점수 : " + sm.currentScore;
```

나중에는 ScoreManager 안에 함수를 만들면 역할이 더 명확해진다.

```csharp
public void AddScore(int amount)
{
    currentScore += amount;
    currentScoreUI.text = "현재 점수 : " + currentScore;
}
```

그러면 Enemy는 다음처럼 호출할 수 있다.

```csharp
sm.AddScore(1);
```

### 3. BestScore 구현

BestScore UI가 이미 있으므로, 다음 단계에서 최고 점수 값을 갱신하거나 저장할 수 있다.

예상 기능:

- 현재 점수가 최고 점수보다 크면 최고 점수 갱신
- 게임 종료 후 최고 점수 유지
- PlayerPrefs로 저장

### 4. Mat_Background 오프셋 정리

이번 패치에도 `Mat_Background.mat` 오프셋 변경이 포함되어 있다.

배경 스크롤 실행 후 머티리얼 오프셋이 저장된 것으로 보인다.

필요하면 다음 정리 단계에서 오프셋을 0으로 되돌리거나, 런타임에만 오프셋이 변경되도록 처리하면 좋다.

### 5. TextMeshPro는 나중에 선택

현재는 Unity 기본 `Text`를 사용했다.

초급 단계에서는 충분하다.

나중에 UI 품질을 올리고 싶으면 TextMeshPro로 전환할 수 있다.

## 다음 단계 제안

다음 단계는 점수 구조를 조금 정리하는 것이 좋다.

추천 다음 작업:

```text
Ch01_Setup_24_ScoreRules
```

내용:

- Bullet과 충돌했을 때만 점수 증가
- ScoreManager에 AddScore 함수 만들기
- Enemy.cs에서 직접 UI를 수정하지 않도록 정리

그 다음 단계에서 최고 점수를 구현할 수 있다.

```text
Ch01_Setup_25_BestScore
```

내용:

- 최고 점수 값 추가
- 현재 점수가 최고 점수를 넘으면 갱신
- BestScore UI 갱신
- 필요하면 PlayerPrefs로 저장

또는 게임 오버 흐름을 먼저 만들 수도 있다.

```text
Ch01_Setup_25_GameOverBasic
```

## 최종 결론

이번 패치는 현재 점수 UI와 점수 증가 기능을 추가한 중요한 게임 규칙 단계다.

Enemy를 잡으면 점수가 오르기 때문에 게임 목표가 생겼고, UI 표시도 화면에 적용되었다.

다음 단계에서는 충돌 대상 구분과 ScoreManager 함수화를 통해 점수 구조를 더 안정적으로 정리하면 좋다.
