# Ch01_Setup_24_BestScoreUI

## 목표

현재 점수가 최고 점수를 넘었을 때 최고 점수 UI가 갱신되도록 만든다.

이전 단계에서는 현재 점수가 올라가고, 현재 점수 UI가 갱신되었다.  
이번 단계에서는 이미 배치되어 있던 `BestScore` UI를 실제 점수 값과 연결하고, 현재 점수가 최고 점수보다 커지면 최고 점수도 함께 갱신한다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `Assets/Scripts/ScoreManager.cs`를 수정한다.
2. 최고 점수 UI를 저장할 변수를 추가한다.
3. 최고 점수 값을 저장할 변수를 추가한다.
4. `SampleScene`의 ScoreManager 오브젝트에 BestScore Text UI를 연결한다.
5. Enemy가 충돌로 제거될 때 현재 점수가 최고 점수를 넘었는지 확인한다.
6. 현재 점수가 최고 점수보다 크면 최고 점수 값을 갱신한다.
7. 최고 점수 UI Text를 갱신한다.

## 중요

- 이번 작업은 최고 점수 UI 갱신만 만든다.
- PlayerPrefs 저장 기능은 아직 만들지 마.
- 게임을 껐다 켜도 최고 점수를 유지하는 기능은 아직 만들지 마.
- 게임 오버 기능은 만들지 마.
- 재시작 기능은 만들지 마.
- 사운드, 이펙트, 배경 기능은 수정하지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### ScoreManager.cs

기존 ScoreManager에는 현재 점수 UI와 현재 점수 값이 있다.

```csharp
public Text currentScoreUI;
public int currentScore;
```

이번 단계에서는 최고 점수 UI와 최고 점수 값을 추가한다.

```csharp
public Text bestScoreUI;
public int bestScore;
```

각 필드의 역할은 다음과 같다.

| 필드 | 역할 |
|---|---|
| `currentScoreUI` | 현재 점수를 표시하는 Text UI |
| `currentScore` | 현재 점수 값 |
| `bestScoreUI` | 최고 점수를 표시하는 Text UI |
| `bestScore` | 최고 점수 값 |

### SampleScene 설정

ScoreManager 오브젝트의 Inspector에서 다음을 연결한다.

```text
Current Score UI: CurrentScore Text
Current Score: 0
Best Score UI: BestScore Text
Best Score: 0
```

### Enemy.cs 변경

Enemy가 충돌했을 때 현재 점수를 증가시킨 뒤, 최고 점수도 확인한다.

예상 코드 흐름:

```csharp
sm.currentScore++;
sm.currentScoreUI.text = "현재 점수 : " + sm.currentScore;

if (sm.currentScore > sm.bestScore)
{
    sm.bestScore = sm.currentScore;
    sm.bestScoreUI.text = "최고 점수 : " + sm.bestScore;
}
```

이 코드는 현재 점수가 최고 점수보다 커졌을 때만 최고 점수 UI를 갱신한다.

## Unity Editor에서 확인할 내용

작업 후 Unity Editor에서 아래 내용을 확인한다.

- ScoreManager 오브젝트에 `bestScoreUI` 필드가 보이는지 확인
- `bestScoreUI`에 BestScore Text가 연결되어 있는지 확인
- `bestScore` 초기값이 0인지 확인
- Play 모드에서 Enemy를 잡으면 현재 점수가 올라가는지 확인
- 현재 점수가 최고 점수를 넘으면 최고 점수 UI도 같이 올라가는지 확인
- Console에 오류가 없는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- 파일 목록

주요 변경:
- ScoreManager에 최고 점수 UI 추가
- ScoreManager에 최고 점수 값 추가
- 현재 점수가 최고 점수를 넘으면 최고 점수 UI 갱신

Unity Editor에서 확인할 내용:
- ScoreManager의 Best Score UI 연결 확인
- Play 모드에서 현재 점수와 최고 점수 갱신 확인

아직 구현하지 않은 내용:
- 최고 점수 저장
- PlayerPrefs
- 게임 오버
- 재시작
- 점수 구조 정리
```
