# Ch01_Setup_25_BestScoreSave

## 목표

최고 점수를 저장하고, 게임을 다시 시작해도 저장된 최고 점수를 불러오게 만든다.

이전 단계에서는 현재 점수가 최고 점수를 넘으면 `BestScore` UI가 갱신되었다.  
하지만 Play 모드를 다시 시작하면 최고 점수가 다시 0으로 초기화되었다.

이번 단계에서는 `PlayerPrefs`를 사용해서 최고 점수를 저장하고, 게임 시작 시 저장된 최고 점수를 다시 불러온다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `Assets/Scripts/ScoreManager.cs`를 수정한다.
2. 게임 시작 시 저장된 최고 점수를 불러온다.
3. 불러온 최고 점수를 `bestScore` 변수에 넣는다.
4. 불러온 최고 점수를 `BestScore` UI에 표시한다.
5. `Assets/Scripts/Enemy.cs`를 수정한다.
6. 현재 점수가 최고 점수를 넘었을 때 최고 점수를 저장한다.

## 중요

- 이번 작업은 최고 점수 저장과 불러오기만 만든다.
- 현재 점수 증가 방식은 기존 구조를 유지한다.
- ScoreManager 구조 리팩터링은 아직 하지 마.
- 충돌 대상 구분은 아직 하지 마.
- 게임 오버 기능은 만들지 마.
- 재시작 기능은 만들지 마.
- UI 디자인은 바꾸지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### ScoreManager.cs

`Start()`에서 저장된 최고 점수를 불러온다.

```csharp
bestScore = PlayerPrefs.GetInt("Best Score", 0);
bestScoreUI.text = "최고 점수 : " + bestScore;
```

의미:

- `"Best Score"`는 저장할 때와 불러올 때 같은 이름으로 사용한다.
- 저장된 값이 없으면 기본값 `0`을 사용한다.
- 불러온 값을 `bestScore`에 넣고 UI에 표시한다.

### Enemy.cs

현재 점수가 최고 점수를 넘었을 때 최고 점수를 저장한다.

```csharp
PlayerPrefs.SetInt("Best Score", sm.bestScore);
```

이 코드는 최고 점수가 갱신된 순간에만 실행되도록 한다.

예상 흐름:

```csharp
if (sm.currentScore > sm.bestScore)
{
    sm.bestScore = sm.currentScore;
    sm.bestScoreUI.text = "최고 점수 : " + sm.bestScore;
    PlayerPrefs.SetInt("Best Score", sm.bestScore);
}
```

## Unity Editor에서 확인할 내용

작업 후 Unity Editor에서 아래 내용을 확인한다.

- Play 모드에서 Enemy를 잡으면 현재 점수가 올라가는지 확인
- 현재 점수가 최고 점수를 넘으면 최고 점수 UI가 갱신되는지 확인
- Play 모드를 종료했다가 다시 시작해도 최고 점수가 유지되는지 확인
- Console에 오류가 없는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- Assets/Scripts/ScoreManager.cs
- Assets/Scripts/Enemy.cs

주요 변경:
- PlayerPrefs로 최고 점수 불러오기
- 최고 점수 갱신 시 PlayerPrefs에 저장
- 게임을 다시 시작해도 최고 점수 유지

Unity Editor에서 확인할 내용:
- Play 모드에서 최고 점수 갱신 확인
- Play 모드 재시작 후 최고 점수 유지 확인

아직 구현하지 않은 내용:
- ScoreManager 리팩터링
- 충돌 대상 구분
- 게임 오버
- 재시작
- 최고 점수 초기화 버튼
```
