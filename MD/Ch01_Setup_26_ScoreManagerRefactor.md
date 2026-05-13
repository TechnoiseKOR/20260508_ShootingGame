# Ch01_Setup_26_ScoreManagerRefactor

## 목표

점수 계산, UI 갱신, 최고 점수 저장 로직을 Enemy에서 직접 처리하지 않고 ScoreManager가 담당하도록 정리한다.

이전 단계에서는 Enemy가 충돌할 때 현재 점수 증가, 현재 점수 UI 갱신, 최고 점수 갱신, 최고 점수 저장까지 직접 처리했다.  
이번 단계에서는 Enemy가 ScoreManager에게 점수 변경만 요청하고, 실제 점수 계산과 UI 갱신은 ScoreManager 안에서 처리하도록 바꾼다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `Assets/Scripts/ScoreManager.cs`를 수정한다.
2. `currentScore`와 `bestScore`를 외부에서 직접 바꾸지 못하도록 private으로 바꾼다.
3. 현재 점수를 설정하는 public 함수를 만든다.
4. 현재 점수를 가져오는 public 함수를 만든다.
5. 현재 점수 UI 갱신, 최고 점수 갱신, 최고 점수 저장 로직을 ScoreManager 함수 안으로 옮긴다.
6. `Assets/Scripts/Enemy.cs`를 수정한다.
7. Enemy는 ScoreManager의 점수 처리 함수를 호출만 하도록 바꾼다.

## 중요

- 이번 작업은 점수 처리 위치를 Enemy에서 ScoreManager로 옮기는 리팩터링만 진행한다.
- 점수 규칙 자체는 바꾸지 마.
- 충돌 대상 구분은 아직 하지 마.
- 게임 오버 기능은 만들지 마.
- 재시작 기능은 만들지 마.
- UI 디자인은 바꾸지 마.
- 사운드, 이펙트, 배경 기능은 수정하지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### ScoreManager.cs

기존 public 점수 값은 private으로 바꾼다.

```csharp
private int currentScore;
private int bestScore;
```

UI 연결 필드는 Inspector에서 연결해야 하므로 public으로 유지한다.

```csharp
public Text currentScoreUI;
public Text bestScoreUI;
```

현재 점수를 설정하는 함수를 만든다.

```csharp
public void SetScore(int value)
{
    currentScore = value;
    currentScoreUI.text = "현재 점수 : " + currentScore;

    if (currentScore > bestScore)
    {
        bestScore = currentScore;
        bestScoreUI.text = "최고 점수 : " + bestScore;
        PlayerPrefs.SetInt("Best Score", bestScore);
    }
}
```

현재 점수를 가져오는 함수도 만든다.

```csharp
public int GetScore()
{
    return currentScore;
}
```

### Enemy.cs

Enemy는 점수 값을 직접 바꾸거나 UI를 직접 수정하지 않는다.

기존처럼 ScoreManager 오브젝트를 찾은 뒤, ScoreManager 함수만 호출한다.

```csharp
GameObject smObject = GameObject.Find("ScoreManager");
ScoreManager sm = smObject.GetComponent<ScoreManager>();

sm.SetScore(sm.GetScore() + 1);
```

Enemy가 직접 하지 않아야 할 일:

```text
currentScore 직접 증가
currentScoreUI 직접 변경
bestScore 직접 변경
bestScoreUI 직접 변경
PlayerPrefs 직접 저장
```

이제 이런 처리는 ScoreManager 안에서 담당한다.

## Unity Editor에서 확인할 내용

작업 후 Unity Editor에서 아래 내용을 확인한다.

- ScoreManager 오브젝트에 `currentScoreUI`가 연결되어 있는지 확인
- ScoreManager 오브젝트에 `bestScoreUI`가 연결되어 있는지 확인
- Play 모드에서 Enemy를 잡으면 현재 점수가 올라가는지 확인
- 현재 점수가 최고 점수를 넘으면 최고 점수도 올라가는지 확인
- Play 모드를 다시 시작해도 최고 점수가 유지되는지 확인
- Console에 오류가 없는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- Assets/Scripts/ScoreManager.cs
- Assets/Scripts/Enemy.cs

주요 변경:
- 점수 값을 private으로 변경
- 점수 갱신 로직을 ScoreManager 함수로 이동
- Enemy는 ScoreManager 함수만 호출하도록 변경
- 최고 점수 저장 로직도 ScoreManager 안에서 처리

Unity Editor에서 확인할 내용:
- CurrentScore UI 연결 확인
- BestScore UI 연결 확인
- Play 모드에서 현재 점수와 최고 점수 갱신 확인
- Play 모드 재시작 후 최고 점수 유지 확인

아직 구현하지 않은 내용:
- 충돌 대상 구분
- 게임 오버
- 재시작
- 최고 점수 초기화 버튼
```
