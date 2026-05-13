# Ch01_Setup_23_ScoreUI

## 목표

Enemy를 잡을 때마다 현재 점수가 올라가고, 화면 왼쪽 위에 점수가 표시되도록 만든다.

이번 단계에서는 UI Canvas를 추가하고, 현재 점수를 표시하는 Text UI와 점수를 관리하는 ScoreManager를 만든다.  
최고 점수 UI도 화면에 배치하지만, 최고 점수 저장과 갱신 기능은 아직 만들지 않는다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `SampleScene`에 UI Canvas를 추가한다.
2. Canvas 아래에 현재 점수 Text UI를 추가한다.
3. Canvas 아래에 최고 점수 Text UI를 추가한다.
4. `Assets/Scripts/ScoreManager.cs` 스크립트를 만든다.
5. 씬에 `ScoreManager` 오브젝트를 추가한다.
6. ScoreManager에 현재 점수 UI를 연결한다.
7. Enemy가 충돌로 제거될 때 현재 점수를 1 증가시킨다.
8. 증가한 현재 점수를 화면에 표시한다.

## 중요

- 이번 작업은 현재 점수 UI와 점수 증가만 만든다.
- 최고 점수 UI는 화면에 배치만 한다.
- 최고 점수 저장 기능은 아직 만들지 마.
- PlayerPrefs는 아직 사용하지 마.
- 게임 오버 기능은 만들지 마.
- 재시작 기능은 만들지 마.
- 사운드, 이펙트, 배경 기능은 수정하지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### ScoreManager.cs

새 스크립트를 만든다.

```text
Assets/Scripts/ScoreManager.cs
```

필요한 네임스페이스:

```csharp
using UnityEngine;
using UnityEngine.UI;
```

필요한 필드:

```csharp
public Text currentScoreUI;
public int currentScore;
```

`currentScoreUI`는 현재 점수를 표시할 Text UI를 Inspector에서 연결하는 용도다.  
`currentScore`는 현재 점수 값을 저장하는 용도다.

이번 단계에서는 `Start()`와 `Update()`에 별도 로직을 넣지 않아도 된다.

### UI 구성

`SampleScene`에 Canvas를 만들고, Canvas 아래에 Text UI를 2개 만든다.

예시 이름:

```text
Canvas
├─ CurrentScore
└─ BestScore
```

`CurrentScore` Text:

```text
현재 점수 : 
```

`BestScore` Text:

```text
최고 점수 : 
```

예시 배치:

```text
CurrentScore: 왼쪽 위, x 30, y -30
BestScore:    왼쪽 위, x 30, y -70
```

### ScoreManager 오브젝트

씬에 빈 오브젝트를 만든다.

```text
ScoreManager
```

이 오브젝트에 `ScoreManager.cs`를 붙인다.

Inspector에서 다음 값을 연결한다.

```text
Current Score UI: CurrentScore Text
Current Score: 0
```

### Enemy.cs 변경

Enemy가 충돌했을 때 현재 점수를 증가시킨다.

예상 흐름:

```csharp
GameObject smObject = GameObject.Find("ScoreManager");
ScoreManager sm = smObject.GetComponent<ScoreManager>();

sm.currentScore++;
sm.currentScoreUI.text = "현재 점수 : " + sm.currentScore;
```

이후 기존처럼 폭발 이펙트를 생성하고 충돌한 오브젝트와 Enemy를 제거한다.

## Unity Editor에서 확인할 내용

작업 후 Unity Editor에서 아래 내용을 확인한다.

- Canvas가 있는지 확인
- CurrentScore Text가 Canvas 아래에 있는지 확인
- BestScore Text가 Canvas 아래에 있는지 확인
- EventSystem이 있는지 확인
- ScoreManager 오브젝트가 있는지 확인
- ScoreManager 컴포넌트의 `Current Score UI`에 CurrentScore Text가 연결되어 있는지 확인
- Play 모드에서 Enemy를 맞추면 현재 점수가 1씩 올라가는지 확인
- 최고 점수 UI는 아직 값이 갱신되지 않는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- 파일 목록

새로 만든 파일:
- Assets/Scripts/ScoreManager.cs

새로 만든 오브젝트:
- Canvas
- CurrentScore
- BestScore
- ScoreManager
- EventSystem

주요 변경:
- 현재 점수 UI 추가
- 최고 점수 UI 배치
- Enemy 충돌 시 현재 점수 1 증가
- 현재 점수 Text 갱신

Unity Editor에서 확인할 내용:
- ScoreManager의 Current Score UI 연결 확인
- Play 모드에서 Enemy를 잡을 때 현재 점수 증가 확인

아직 구현하지 않은 내용:
- 최고 점수 저장
- 최고 점수 갱신
- 게임 오버
- 재시작
- 점수 UI 꾸미기
```
