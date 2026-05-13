# Ch01_Setup_23_ScoreUI_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 현재 점수와 최고 점수 UI가 추가되고, Enemy가 충돌로 제거될 때 현재 점수가 증가하도록 변경되었다.

커밋 메시지:

```text
현재점수, 최고점수 UI 추가

현재 점수 증가 적용
```

주요 변경은 다음과 같다.

1. `SampleScene`에 Canvas 추가
2. Canvas 아래에 `CurrentScore` Text UI 추가
3. Canvas 아래에 `BestScore` Text UI 추가
4. EventSystem 추가
5. `ScoreManager` 오브젝트 추가
6. `ScoreManager.cs` 스크립트 추가
7. Enemy 충돌 시 ScoreManager를 찾아 현재 점수 증가
8. 현재 점수 UI 텍스트 갱신
9. `Mat_Background.mat` 텍스처 오프셋 변경

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Scenes/SampleScene.unity` | Canvas, CurrentScore, BestScore, EventSystem, ScoreManager 오브젝트 추가 |
| `Assets/Scripts/ScoreManager.cs` | 현재 점수와 현재 점수 UI 참조를 가진 스크립트 추가 |
| `Assets/Scripts/ScoreManager.cs.meta` | ScoreManager 스크립트 메타 파일 추가 |
| `Assets/Scripts/Enemy.cs` | Enemy 충돌 시 현재 점수 증가 및 UI 갱신 |
| `Assets/Materials/Mat_Background.mat` | 배경 텍스처 오프셋 값 변경 |

## UI 추가 분석

`SampleScene`에 Canvas가 추가되었다.

Canvas에는 기본 UI 구성을 위한 컴포넌트가 포함된다.

- Canvas
- CanvasScaler
- GraphicRaycaster
- RectTransform

Canvas 아래에는 Text UI 2개가 추가되었다.

```text
Canvas
├─ CurrentScore
└─ BestScore
```

### CurrentScore

`CurrentScore`는 현재 점수를 표시하기 위한 Text UI다.

초기 텍스트는 다음과 같다.

```text
현재 점수 : 
```

주요 배치값은 다음과 같다.

```text
Anchor: 왼쪽 위
Anchored Position: x 30, y -30
Size: x 160, y 30
Font Size: 26
```

### BestScore

`BestScore`는 최고 점수를 표시하기 위한 Text UI다.

초기 텍스트는 다음과 같다.

```text
최고 점수 : 
```

주요 배치값은 다음과 같다.

```text
Anchor: 왼쪽 위
Anchored Position: x 30, y -70
Size: x 160, y 30
Font Size: 26
```

현재 패치에서는 BestScore UI가 배치되었지만, 최고 점수 값 저장이나 갱신 로직은 아직 구현되지 않았다.

## EventSystem 추가 분석

UI 입력 처리를 위한 EventSystem이 추가되었다.

현재 프로젝트는 Input System을 사용하므로 EventSystem에는 `InputSystemUIInputModule`이 연결되어 있다.

이번 단계의 Text UI는 클릭이나 입력이 필요한 UI는 아니지만, Unity가 Canvas UI를 추가할 때 EventSystem을 함께 생성한 것으로 볼 수 있다.

## ScoreManager 오브젝트 분석

씬에 `ScoreManager` 오브젝트가 추가되었다.

ScoreManager 오브젝트에는 `ScoreManager.cs` 스크립트가 연결되어 있다.

Inspector 연결 상태는 다음과 같다.

```text
currentScoreUI: CurrentScore Text
currentScore: 0
```

ScoreManager 오브젝트의 Transform 위치는 게임플레이에 직접 영향을 주지 않는다.  
이 오브젝트는 점수 데이터를 보관하고 UI 참조를 가진 관리용 오브젝트다.

## ScoreManager.cs 분석

새 파일 `Assets/Scripts/ScoreManager.cs`가 추가되었다.

코드는 다음 구조를 가진다.

```csharp
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text currentScoreUI;
    public int currentScore;

    void Start()
    {

    }

    void Update()
    {

    }
}
```

역할은 단순하다.

- `currentScoreUI`: 현재 점수를 표시할 Text UI
- `currentScore`: 현재 점수 값

현재 단계에서는 `Start()`와 `Update()`에 별도 로직이 없다.

## Enemy.cs 변경 분석

`Enemy.cs`의 `OnCollisionEnter`에 점수 증가 코드가 추가되었다.

추가된 흐름은 다음과 같다.

```csharp
GameObject smObject = GameObject.Find("ScoreManager");
ScoreManager sm = smObject.GetComponent<ScoreManager>();

sm.currentScore++;
sm.currentScoreUI.text = "현재 점수 : " + sm.currentScore;
```

즉, Enemy가 다른 오브젝트와 충돌하면 다음 순서로 동작한다.

1. 씬에서 이름이 `ScoreManager`인 오브젝트를 찾는다.
2. 그 오브젝트에서 `ScoreManager` 컴포넌트를 가져온다.
3. 현재 점수를 1 증가시킨다.
4. 현재 점수 UI 텍스트를 갱신한다.
5. 기존처럼 폭발 이펙트를 생성한다.
6. 충돌한 오브젝트와 Enemy 자신을 제거한다.

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- 현재 점수 UI 표시
- 최고 점수 UI 배치
- ScoreManager 추가
- Enemy 충돌 시 현재 점수 증가
- 현재 점수 Text 갱신
- Unity UI Canvas 구성 추가

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 최고 점수 저장
- 최고 점수 갱신
- PlayerPrefs 사용
- 게임 오버
- 점수 초기화 규칙
- 점수 증가 대상 구분
- UI 디자인 개선
- TextMeshPro 전환

## Mat_Background 변경 분석

`Assets/Materials/Mat_Background.mat`도 함께 수정되었다.

변경 내용은 배경 텍스처 오프셋이다.

```text
_BaseMap m_Offset.y: 1.174033 → 6.6824718
_MainTex m_Offset.y: 1.174033 → 3.4646902
```

이 변경은 배경 스크롤을 Play 모드에서 확인한 뒤 머티리얼 값이 저장된 것으로 보인다.

이번 패치의 핵심은 점수 UI와 점수 증가이므로, 배경 머티리얼 오프셋 변경은 의도한 변경인지 확인하는 것이 좋다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. Canvas가 생성되어 있는지 확인
2. CurrentScore Text가 Canvas 아래에 있는지 확인
3. BestScore Text가 Canvas 아래에 있는지 확인
4. CurrentScore와 BestScore가 화면 왼쪽 위에 보이는지 확인
5. ScoreManager 오브젝트가 있는지 확인
6. ScoreManager의 `currentScoreUI`에 CurrentScore Text가 연결되어 있는지 확인
7. Enemy를 Bullet으로 맞추면 현재 점수가 1씩 올라가는지 확인
8. Enemy가 Player와 충돌했을 때도 점수가 올라가는지 확인
9. Console에 NullReferenceException이 없는지 확인
10. `Mat_Background.mat` 오프셋 변경이 의도된 것인지 확인

## 주의할 점

### 1. 충돌 대상 구분이 없다

현재 Enemy는 어떤 오브젝트와 충돌해도 점수가 증가한다.

즉, Bullet과 충돌해도 점수가 오르고, Player와 충돌해도 점수가 오를 수 있다.

다음 단계에서는 충돌한 대상이 Bullet인지 확인한 뒤 점수를 올리는 방식으로 개선하는 것이 좋다.

### 2. GameObject.Find 사용

현재 점수 증가 시마다 다음 코드로 ScoreManager를 찾는다.

```csharp
GameObject smObject = GameObject.Find("ScoreManager");
```

초급 단계에서는 이해하기 쉽고 괜찮다.

하지만 Enemy가 자주 충돌하면 매번 Find를 호출하므로, 나중에는 캐싱하거나 static 접근, GameManager 구조로 정리할 수 있다.

### 3. Null 체크가 없다

ScoreManager 오브젝트가 없거나 `currentScoreUI` 연결이 빠져 있으면 오류가 날 수 있다.

수업에서는 Inspector 연결을 확인하는 것을 강조하면 좋다.

### 4. BestScore는 아직 표시만 한다

BestScore Text는 추가되었지만 최고 점수 값은 아직 갱신되지 않는다.

현재 단계에서는 UI 자리만 만든 것으로 보는 것이 맞다.

## 결론

이번 패치는 현재 점수 UI와 점수 증가 기능을 추가한 게임 규칙 단계다.

Enemy를 잡을 때 현재 점수가 올라가므로, 플레이 목표가 조금 더 명확해졌다.

다음 단계에서는 충돌 대상 구분, 최고 점수 갱신, 게임 오버 처리로 이어가면 좋다.
