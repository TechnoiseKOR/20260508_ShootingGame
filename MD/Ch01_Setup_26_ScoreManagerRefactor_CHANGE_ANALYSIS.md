# Ch01_Setup_26_ScoreManagerRefactor_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 점수 계산과 UI 갱신, 최고 점수 저장 로직을 Enemy에서 ScoreManager로 옮겼다.

첫 번째 패치에서는 Enemy가 직접 처리하던 점수 로직을 ScoreManager의 함수로 이동했고, 추가 패치에서는 `SetScore(int value)`가 전달받은 값을 실제로 사용하도록 수정되었다.

커밋 메시지 흐름:

```text
점수 계산과 UI 갱신을 에너미에서 ScoreManager로 옮기기
점수 계산과 UI 갱신을 에너미에서 ScoreManager로 옮기기 빠진 부분 수정
```

주요 변경은 다음과 같다.

1. `ScoreManager.cs`의 `currentScore`를 private으로 변경
2. `ScoreManager.cs`의 `bestScore`를 private으로 변경
3. ScoreManager에 `SetScore(int value)` 함수 추가
4. ScoreManager에 `GetScore()` 함수 추가
5. 현재 점수 UI 갱신 로직을 ScoreManager로 이동
6. 최고 점수 갱신 로직을 ScoreManager로 이동
7. 최고 점수 저장 로직을 ScoreManager로 이동
8. Enemy는 ScoreManager의 함수만 호출하도록 변경
9. `SetScore(int value)`가 매개변수 `value`를 사용하도록 수정

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Scripts/Enemy.cs` | 점수/UI/저장 직접 처리 제거, ScoreManager 함수 호출로 변경 |
| `Assets/Scripts/ScoreManager.cs` | 점수 필드 private 변경, SetScore/GetScore 함수 추가, 점수 처리 로직 이동 |

## ScoreManager.cs 변경 분석

### 점수 필드 private 변경

기존에는 현재 점수와 최고 점수가 public이었다.

```csharp
public int currentScore;
public int bestScore;
```

변경 후에는 private으로 바뀌었다.

```csharp
private int currentScore;
private int bestScore;
```

이 변경으로 다른 스크립트가 점수 값을 직접 바꾸지 못하게 되었다.

점수 값은 ScoreManager 내부 함수로만 변경하도록 유도된다.

### UI 필드는 public 유지

UI 연결 필드는 그대로 public이다.

```csharp
public Text currentScoreUI;
public Text bestScoreUI;
```

이 필드들은 Unity Inspector에서 Text UI를 연결해야 하므로 public으로 유지된 것으로 볼 수 있다.

## SetScore 함수 분석

새 함수 `SetScore(int value)`가 추가되었다.

최종 수정 후 핵심 구조는 다음과 같다.

```csharp
public void SetScore(int value)
{
    currentScore = value;
    currentScoreUI.text = "현재 점수 : " + currentScore;

    if( currentScore > bestScore )
    {
        bestScore = currentScore;
        bestScoreUI.text = "최고 점수 : " + bestScore;
        PlayerPrefs.SetInt("Best Score", bestScore);
    }
}
```

이 함수 안으로 다음 로직이 이동했다.

- 현재 점수 설정
- 현재 점수 UI 갱신
- 최고 점수 비교
- 최고 점수 갱신
- 최고 점수 UI 갱신
- 최고 점수 PlayerPrefs 저장

즉, 점수 처리 책임이 Enemy에서 ScoreManager로 이동했다.

## 추가 수정 패치 분석

처음 리팩터링 패치에서는 `SetScore(int value)`가 매개변수를 받았지만 실제로는 사용하지 않고 있었다.

초기 형태:

```csharp
public void SetScore(int value)
{
    currentScore++;
}
```

추가 패치에서 아래처럼 수정되었다.

```csharp
public void SetScore(int value)
{
    currentScore = value;
}
```

이 수정으로 함수 이름과 동작이 더 자연스럽게 맞아졌다.

Enemy에서 다음처럼 호출하면:

```csharp
sm.SetScore(sm.GetScore() + 1);
```

`GetScore() + 1`로 계산한 값이 실제 currentScore에 들어간다.

## GetScore 함수 분석

새 함수 `GetScore()`가 추가되었다.

```csharp
public int GetScore()
{
    return currentScore;
}
```

이 함수는 외부에서 현재 점수 값을 읽을 때 사용한다.

`currentScore`가 private이 되었기 때문에, 외부 스크립트는 이 함수를 통해서만 현재 점수를 가져올 수 있다.

## Enemy.cs 변경 분석

기존 Enemy는 충돌 시 ScoreManager의 필드와 UI를 직접 수정했다.

기존 Enemy의 역할:

```text
currentScore 증가
currentScoreUI 갱신
bestScore 비교
bestScore 갱신
bestScoreUI 갱신
PlayerPrefs 저장
```

이번 패치에서는 이 코드가 제거되고, ScoreManager 함수 호출로 변경되었다.

```csharp
GameObject smObject = GameObject.Find("ScoreManager");
ScoreManager sm = smObject.GetComponent<ScoreManager>();

sm.SetScore(sm.GetScore() + 1);
```

이제 Enemy는 ScoreManager를 찾고, 점수 처리를 요청하는 역할만 한다.

## 리팩터링 전후 비교

### 변경 전

```text
Enemy
├─ ScoreManager 찾기
├─ currentScore 직접 증가
├─ currentScoreUI 직접 갱신
├─ bestScore 직접 비교
├─ bestScore 직접 갱신
├─ bestScoreUI 직접 갱신
└─ PlayerPrefs 직접 저장
```

### 변경 후

```text
Enemy
├─ ScoreManager 찾기
└─ ScoreManager.SetScore(현재 점수 + 1) 호출

ScoreManager
├─ currentScore 설정
├─ currentScoreUI 갱신
├─ bestScore 비교
├─ bestScore 갱신
├─ bestScoreUI 갱신
└─ PlayerPrefs 저장
```

역할 분리가 더 좋아졌다.

## 구현된 기능

이번 패치로 구현된 내용은 다음과 같다.

- 점수 값 은닉화
- 현재 점수 읽기 함수 추가
- 점수 설정 함수 추가
- 점수 UI 갱신 책임을 ScoreManager로 이동
- 최고 점수 저장 책임을 ScoreManager로 이동
- Enemy의 점수 처리 코드 단순화
- `SetScore(int value)`가 전달받은 값을 실제 점수에 반영하도록 수정

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 충돌 대상 구분
- Bullet과 충돌했을 때만 점수 증가
- ScoreManager 오브젝트 캐싱
- `AddScore(int amount)` 형태의 명확한 점수 증가 함수
- Null 체크
- PlayerPrefs.Save()
- 게임 오버
- 재시작
- 최고 점수 초기화 버튼

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. ScoreManager 오브젝트에 `currentScoreUI`가 연결되어 있는지 확인
2. ScoreManager 오브젝트에 `bestScoreUI`가 연결되어 있는지 확인
3. Play 모드에서 Enemy를 잡으면 현재 점수가 1씩 올라가는지 확인
4. 현재 점수가 최고 점수를 넘으면 최고 점수 UI도 갱신되는지 확인
5. Play 모드를 다시 시작해도 최고 점수가 유지되는지 확인
6. Console에 오류가 없는지 확인

## 주의할 점

### 1. SetScore라는 이름은 현재 구조에서는 동작과 맞는다

최종 수정 후 `SetScore(int value)`는 전달받은 값으로 `currentScore`를 설정한다.

```csharp
currentScore = value;
```

따라서 함수 이름과 동작은 일치한다.

다만 현재 Enemy는 점수 증가를 위해 다음처럼 호출한다.

```csharp
sm.SetScore(sm.GetScore() + 1);
```

나중에는 더 직관적으로 `AddScore(int amount)` 함수를 추가하는 것이 좋다.

### 2. GameObject.Find는 아직 남아 있다

Enemy는 여전히 충돌할 때마다 ScoreManager를 이름으로 찾는다.

```csharp
GameObject smObject = GameObject.Find("ScoreManager");
```

초급 단계에서는 이해하기 쉽지만, 나중에는 캐싱하거나 GameManager 구조로 바꿀 수 있다.

### 3. 충돌 대상 구분은 아직 없다

현재 Enemy는 어떤 오브젝트와 충돌해도 점수가 올라갈 수 있다.

Bullet과 충돌했을 때만 점수가 오르도록 하려면 다음 단계에서 충돌 대상을 구분해야 한다.

### 4. Null 체크가 없다

ScoreManager 오브젝트나 UI 연결이 빠져 있으면 오류가 날 수 있다.

이번 단계에서는 Inspector 연결 확인이 중요하다.

## 결론

이번 패치는 점수 처리 책임을 Enemy에서 ScoreManager로 옮긴 리팩터링 단계다.

Enemy는 점수 처리 세부 내용을 몰라도 되고, ScoreManager가 현재 점수, 최고 점수, UI 갱신, 저장을 담당하게 되었다.

추가 수정으로 `SetScore(int value)`가 매개변수를 실제로 사용하게 되어 함수 동작도 더 자연스러워졌다.
