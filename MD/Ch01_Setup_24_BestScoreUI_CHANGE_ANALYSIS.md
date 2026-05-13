# Ch01_Setup_24_BestScoreUI_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 최고 점수 UI와 최고 점수 값이 ScoreManager에 연결되고, Enemy 충돌 시 현재 점수가 최고 점수를 넘으면 최고 점수 UI가 갱신되도록 변경되었다.

커밋 메시지:

```text
최고점수 추가
```

주요 변경은 다음과 같다.

1. `ScoreManager.cs`에 `bestScoreUI` 필드 추가
2. `ScoreManager.cs`에 `bestScore` 필드 추가
3. `SampleScene`의 ScoreManager 오브젝트에 BestScore Text UI 연결
4. Enemy 충돌 시 현재 점수가 최고 점수를 넘었는지 확인
5. 최고 점수 갱신 시 BestScore Text UI 갱신
6. `Mat_Background.mat` 텍스처 오프셋 값 변경

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Scripts/ScoreManager.cs` | 최고 점수 UI와 최고 점수 값 필드 추가 |
| `Assets/Scripts/Enemy.cs` | 현재 점수가 최고 점수보다 크면 최고 점수 갱신 |
| `Assets/Scenes/SampleScene.unity` | ScoreManager에 BestScore Text UI 연결 |
| `Assets/Materials/Mat_Background.mat` | 배경 텍스처 오프셋 값 변경 |

## ScoreManager.cs 변경 분석

기존 ScoreManager에는 현재 점수 관련 필드만 있었다.

```csharp
public Text currentScoreUI;
public int currentScore;
```

이번 패치에서 최고 점수 관련 필드가 추가되었다.

```csharp
// 최고 점수 UI
public Text bestScoreUI;
// 최고 점수
public int bestScore;
```

이제 ScoreManager는 현재 점수와 최고 점수를 함께 관리할 수 있다.

| 필드 | 역할 |
|---|---|
| `currentScoreUI` | 현재 점수 Text UI |
| `currentScore` | 현재 점수 값 |
| `bestScoreUI` | 최고 점수 Text UI |
| `bestScore` | 최고 점수 값 |

현재 단계에서는 최고 점수를 저장하는 기능은 없다.  
따라서 Play 모드를 새로 시작하면 최고 점수는 다시 0부터 시작한다.

## SampleScene 변경 분석

`SampleScene.unity`에서 ScoreManager 컴포넌트에 다음 값이 추가 연결되었다.

```text
bestScoreUI: BestScore Text
bestScore: 0
```

즉, 이전 단계에서 Canvas 아래에 배치해둔 `BestScore` Text UI가 ScoreManager에 실제로 연결되었다.

이제 코드에서 `sm.bestScoreUI.text`를 변경하면 화면의 최고 점수 UI가 갱신된다.

## Enemy.cs 변경 분석

`Enemy.cs`의 `OnCollisionEnter` 안에 최고 점수 갱신 로직이 추가되었다.

기존 흐름은 다음과 같았다.

```csharp
sm.currentScore++;
sm.currentScoreUI.text = "현재 점수 : " + sm.currentScore;
```

이번 패치에서는 그 뒤에 다음 로직이 추가되었다.

```csharp
if( sm.currentScore > sm.bestScore )
{
    sm.bestScore = sm.currentScore;
    sm.bestScoreUI.text = "최고 점수 : " + sm.bestScore;
}
```

동작 순서는 다음과 같다.

1. Enemy가 충돌한다.
2. ScoreManager를 찾는다.
3. 현재 점수를 1 증가시킨다.
4. 현재 점수 UI를 갱신한다.
5. 현재 점수가 최고 점수보다 큰지 확인한다.
6. 현재 점수가 더 크면 최고 점수를 현재 점수로 바꾼다.
7. 최고 점수 UI를 갱신한다.
8. 기존처럼 폭발 이펙트를 생성하고 오브젝트를 제거한다.

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- 최고 점수 값 추가
- 최고 점수 UI 연결
- 현재 점수가 최고 점수를 넘을 때 최고 점수 갱신
- 최고 점수 Text UI 갱신

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 최고 점수 저장
- PlayerPrefs 사용
- 게임을 다시 시작해도 최고 점수 유지
- 게임 오버
- 재시작
- 충돌 대상 구분
- ScoreManager 함수화
- 점수 UI 초기 텍스트 자동 세팅

## Mat_Background 변경 분석

`Assets/Materials/Mat_Background.mat`도 함께 수정되었다.

변경 내용은 `_MainTex` 텍스처 오프셋 값이다.

```text
_MainTex m_Offset.y: 3.4646902 → 6.6824718
```

이 변경은 배경 스크롤 실행 후 머티리얼 상태가 저장된 것으로 보인다.

이번 패치의 핵심은 최고 점수 기능이므로, 배경 머티리얼 오프셋 변경은 의도된 변경인지 확인하는 것이 좋다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. ScoreManager 오브젝트에 `bestScoreUI` 필드가 보이는지 확인
2. `bestScoreUI`에 BestScore Text가 연결되어 있는지 확인
3. `bestScore` 초기값이 0인지 확인
4. Play 모드에서 Enemy를 잡으면 현재 점수가 올라가는지 확인
5. 현재 점수가 최고 점수를 넘으면 최고 점수도 올라가는지 확인
6. BestScore UI가 화면 왼쪽 위에 잘 보이는지 확인
7. Console에 NullReferenceException이 없는지 확인
8. `Mat_Background.mat` 오프셋 변경이 의도된 것인지 확인

## 주의할 점

### 1. 최고 점수 저장은 아직 없다

현재 최고 점수는 메모리 안에서만 유지된다.

즉, Play 모드를 종료하거나 게임을 다시 시작하면 최고 점수는 다시 0이 된다.

나중에 `PlayerPrefs`를 사용하면 최고 점수를 저장할 수 있다.

### 2. 충돌 대상 구분이 아직 없다

현재 Enemy는 어떤 오브젝트와 충돌해도 현재 점수와 최고 점수를 갱신할 수 있다.

Bullet과 충돌했을 때만 점수가 오르게 하려면 다음 단계에서 충돌 대상을 구분해야 한다.

### 3. ScoreManager 필드 직접 접근

Enemy가 ScoreManager의 필드에 직접 접근하고 있다.

```csharp
sm.currentScore++;
sm.currentScoreUI.text = "현재 점수 : " + sm.currentScore;
sm.bestScore = sm.currentScore;
sm.bestScoreUI.text = "최고 점수 : " + sm.bestScore;
```

초급 단계에서는 이해하기 쉽지만, 나중에는 ScoreManager에 `AddScore()` 함수를 만들어서 점수 처리 책임을 ScoreManager 안으로 옮기는 것이 좋다.

### 4. Null 체크가 없다

ScoreManager 오브젝트를 찾지 못하거나 UI 연결이 빠져 있으면 오류가 날 수 있다.

이번 단계에서는 Inspector 연결 확인이 중요하다.

## 결론

이번 패치는 이전 단계에서 배치만 되어 있던 BestScore UI를 실제 최고 점수 값과 연결한 단계다.

현재 점수가 최고 점수를 넘으면 최고 점수 UI도 함께 갱신되므로, 점수 시스템이 한 단계 완성도 있게 바뀌었다.

다음 단계에서는 충돌 대상 구분 또는 최고 점수 저장 기능으로 이어가면 좋다.
