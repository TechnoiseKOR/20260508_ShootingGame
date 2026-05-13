# Ch01_Setup_25_BestScoreSave_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 `PlayerPrefs`를 사용해 최고 점수를 저장하고, 게임 시작 시 저장된 최고 점수를 다시 불러오도록 변경되었다.

커밋 메시지:

```text
최고 점수 저장하고 불러오기
```

주요 변경은 다음과 같다.

1. `ScoreManager.cs`의 `Start()`에서 최고 점수 불러오기 추가
2. 불러온 최고 점수를 `bestScore` 변수에 저장
3. 불러온 최고 점수를 `BestScore` UI에 표시
4. `Enemy.cs`에서 최고 점수 갱신 시 PlayerPrefs에 저장

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Scripts/ScoreManager.cs` | 게임 시작 시 저장된 최고 점수 불러오기 |
| `Assets/Scripts/Enemy.cs` | 최고 점수 갱신 시 PlayerPrefs에 저장 |

## ScoreManager.cs 변경 분석

기존 `Start()`는 비어 있었다.

```csharp
void Start()
{

}
```

이번 패치에서는 `Start()`에 최고 점수 불러오기 로직이 추가되었다.

```csharp
// 목표 : 최고 점수를 불러와 bestScore 변수에 할당하고 화면에 표시한다.
// 순서 : 1. 최고 점수를 불러와 bestScore에 넣어주기
bestScore = PlayerPrefs.GetInt("Best Score", 0);
// 2. 최고 점수를 화면에 표시하기
bestScoreUI.text = "최고 점수 : " + bestScore;
```

이 코드는 게임이 시작될 때 저장된 최고 점수를 불러온다.

`PlayerPrefs.GetInt("Best Score", 0)`의 의미는 다음과 같다.

| 값 | 의미 |
|---|---|
| `"Best Score"` | 저장된 최고 점수를 찾을 때 사용하는 키 이름 |
| `0` | 저장된 값이 없을 때 사용할 기본값 |

즉, 이전에 저장된 최고 점수가 있으면 그 값을 불러오고, 없으면 0으로 시작한다.

## Enemy.cs 변경 분석

이전 단계에서는 현재 점수가 최고 점수를 넘으면 최고 점수 변수와 UI만 갱신했다.

기존 흐름:

```csharp
if( sm.currentScore > sm.bestScore )
{
    sm.bestScore = sm.currentScore;
    sm.bestScoreUI.text = "최고 점수 : " + sm.bestScore;
}
```

이번 패치에서는 최고 점수를 갱신한 뒤 PlayerPrefs에 저장하는 코드가 추가되었다.

```csharp
// 목표 : 최고 점수를 저장하고 싶다.
PlayerPrefs.SetInt("Best Score", sm.bestScore);
```

최종 흐름은 다음과 같다.

1. Enemy가 충돌한다.
2. 현재 점수가 1 증가한다.
3. 현재 점수 UI가 갱신된다.
4. 현재 점수가 최고 점수보다 큰지 확인한다.
5. 더 크면 `bestScore`를 현재 점수로 갱신한다.
6. BestScore UI를 갱신한다.
7. 새 최고 점수를 PlayerPrefs에 저장한다.

## PlayerPrefs 사용 분석

이번 패치에서는 Unity의 `PlayerPrefs`를 사용한다.

사용된 함수는 다음 2개다.

```csharp
PlayerPrefs.GetInt("Best Score", 0);
PlayerPrefs.SetInt("Best Score", sm.bestScore);
```

### GetInt

```csharp
PlayerPrefs.GetInt("Best Score", 0);
```

저장된 정수 값을 불러온다.

저장된 값이 없으면 기본값 0을 반환한다.

### SetInt

```csharp
PlayerPrefs.SetInt("Best Score", sm.bestScore);
```

현재 최고 점수를 `"Best Score"`라는 이름으로 저장한다.

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- 게임 시작 시 최고 점수 불러오기
- 불러온 최고 점수를 BestScore UI에 표시
- 최고 점수 갱신 시 PlayerPrefs에 저장
- Play 모드를 다시 시작해도 최고 점수 유지

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 최고 점수 초기화 버튼
- 최고 점수 저장 즉시 `PlayerPrefs.Save()` 호출
- ScoreManager의 `AddScore()` 함수화
- 충돌 대상 구분
- 게임 오버
- 재시작
- 점수 저장 데이터 관리 UI

현재 단계에서는 PlayerPrefs의 기본 사용법을 보여주는 것이 핵심이다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. Play 모드에서 Enemy를 잡아 현재 점수를 올린다.
2. 최고 점수가 현재 점수와 함께 갱신되는지 확인한다.
3. Play 모드를 종료한다.
4. 다시 Play 모드를 시작한다.
5. BestScore UI에 이전 최고 점수가 표시되는지 확인한다.
6. Console에 오류가 없는지 확인한다.

## 주의할 점

### 1. 키 이름이 정확히 같아야 한다

저장할 때와 불러올 때 키 이름이 같아야 한다.

현재 코드는 둘 다 다음 문자열을 사용한다.

```text
Best Score
```

키 이름이 한 글자라도 달라지면 저장된 값을 불러오지 못한다.

### 2. PlayerPrefs.Save()는 호출하지 않았다

현재는 `PlayerPrefs.SetInt()`만 호출한다.

Unity는 일반적으로 적절한 시점에 PlayerPrefs 값을 저장하지만, 즉시 저장을 명확히 하고 싶다면 나중에 다음 코드를 추가할 수 있다.

```csharp
PlayerPrefs.Save();
```

이번 단계에서는 초급 수업 흐름상 `SetInt`만 사용해도 충분하다.

### 3. 충돌 대상 구분은 아직 없다

현재 Enemy는 어떤 오브젝트와 충돌해도 점수가 증가할 수 있다.

따라서 Player와 충돌해도 점수가 오르고 최고 점수가 저장될 수 있다.

다음 단계에서 Bullet과 충돌했을 때만 점수가 오르도록 정리하는 것이 좋다.

### 4. ScoreManager 책임이 아직 분산되어 있다

현재 점수 증가와 UI 갱신은 `Enemy.cs`에서 직접 처리한다.

나중에는 `ScoreManager.AddScore()` 함수로 옮기면 구조가 더 깔끔해진다.

## 결론

이번 패치는 최고 점수를 PlayerPrefs에 저장하고, 게임 시작 시 다시 불러오는 단계다.

이제 최고 점수가 단순히 실행 중에만 유지되는 값이 아니라, Play 모드를 다시 시작해도 남아 있는 값이 되었다.

다음 단계에서는 ScoreManager 구조 정리 또는 충돌 대상 구분으로 이어가면 좋다.
