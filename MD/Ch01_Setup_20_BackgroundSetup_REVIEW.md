# Ch01_Setup_20_BackgroundSetup_REVIEW

## 리뷰 대상

이번 리뷰 대상은 배경 머티리얼 생성, 배경 오브젝트 추가, 배경 스크롤 구현 패치다.

패치 요약:

- `Assets/Materials` 폴더 추가
- `Assets/Materials/Mat_Background.mat` 추가
- `SampleScene`에 `Background` 오브젝트 추가
- `Background` 오브젝트에 `Mat_Background` 적용
- `Assets/Scripts/Background.cs` 추가
- `Background.cs`에서 머티리얼 텍스처 오프셋 스크롤 구현
- `SingleNeb_07.png.meta` import 설정 갱신

## 전체 평가

이번 작업은 배경 에셋 import 이후 자연스러운 적용 단계다.

지금까지 게임 오브젝트와 이펙트는 어느 정도 갖춰졌지만, 배경은 아직 실제 게임 느낌을 주기 어려웠다.  
이번 변경으로 우주 배경이 추가되고, 텍스처가 움직이면서 슈팅 게임 느낌이 더 강해졌다.

코드도 단순하게 `mainTextureOffset`을 이동시키는 방식이라 초급 수업에 적절하다.

## 좋은 점

### 1. 배경 전용 머티리얼을 만들었다

`Assets/Materials/Mat_Background.mat`을 새로 만들어 배경에 사용했다.

에셋에 포함된 머티리얼을 직접 수정하지 않고 프로젝트 전용 머티리얼을 만든 점이 좋다.

### 2. Background 오브젝트가 독립적으로 추가되었다

씬에 `Background`라는 별도 오브젝트를 만들었다.

덕분에 Player, Enemy, Bullet 같은 게임 오브젝트와 배경 역할이 분리된다.

### 3. Background.cs 코드가 이해하기 쉽다

배경 스크롤 코드는 매우 단순하다.

```csharp
Vector2 direction = Vector2.up;
bgMaterial.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
```

학생들이 이전에 배운 `P = P0 + vt` 이동 개념과 비슷하게 이해할 수 있다.

### 4. Inspector에서 조절 가능한 값이 있다

`bgMaterial`과 `scrollSpeed`가 public 필드로 되어 있어 Inspector에서 확인하고 조절하기 쉽다.

초급 단계에서는 이 방식이 직관적이다.

## 확인이 필요한 점

### 1. Play 모드 배경 스크롤 확인

Play 모드에서 다음을 확인해야 한다.

- 배경이 보이는지
- 배경 텍스처가 천천히 움직이는지
- 움직임이 너무 빠르거나 느리지 않은지
- Player, Enemy, Bullet이 배경에 묻히지 않고 잘 보이는지

### 2. bgMaterial 연결 확인

`Background` 컴포넌트의 `bgMaterial`에 `Mat_Background`가 연결되어 있어야 한다.

연결이 빠져 있으면 Play 모드에서 오류가 날 수 있다.

### 3. Background 위치 확인

Background의 위치는 다음과 같다.

```text
Position: x 0, y 0, z 1.5
Scale:    x 7, y 10, z 1
```

현재 카메라와 오브젝트 배치 기준에서 배경이 화면 뒤쪽에 잘 보이는지 확인해야 한다.

### 4. MeshCollider 필요 여부 확인

Background에는 MeshCollider가 포함되어 있다.

배경은 충돌이 필요 없는 오브젝트이므로, 실제 플레이 중 충돌에 영향을 주지 않는지 확인해야 한다.

필요 없다면 이후 정리 단계에서 제거하는 것이 좋다.

## 개선하면 좋은 점

### 1. Null 체크 추가

현재 `Background.cs`는 `bgMaterial`이 반드시 연결되어 있다고 가정한다.

나중에 안정성을 높이려면 다음처럼 null 체크를 추가할 수 있다.

```csharp
if (bgMaterial == null)
{
    return;
}
```

### 2. `[SerializeField] private`로 정리

현재 필드는 public이다.

초급 단계에서는 괜찮지만, 나중에 코드 스타일을 정리할 때는 아래처럼 바꿀 수 있다.

```csharp
[SerializeField] private Material bgMaterial;
[SerializeField] private float scrollSpeed = 0.2f;
```

### 3. MeshCollider 제거

배경에 충돌이 필요 없다면 MeshCollider를 제거해도 된다.

이렇게 하면 불필요한 충돌 계산과 혼동을 줄일 수 있다.

### 4. 배경 레이어 분리

나중에 배경 전용 레이어를 만들고 충돌 매트릭스에서 모든 충돌을 끄면 더 안전하다.

예:

```text
Layer: Background
```

### 5. 2중 배경 또는 패럴랙스

현재는 하나의 배경 머티리얼만 움직인다.

나중에 더 풍부한 느낌을 주려면 다음을 추가할 수 있다.

- 별 배경 1장
- 성운 배경 1장
- 서로 다른 속도의 스크롤
- 패럴랙스 효과

## 다음 단계 제안

다음 단계는 코드 안정성 또는 게임 규칙 추가 중 하나가 적절하다.

### 선택 1. 배경 정리

```text
Ch01_Setup_21_BackgroundCleanup
```

내용:

- Background의 MeshCollider 제거
- Background 레이어 분리
- bgMaterial null 체크 추가
- 스크롤 속도 조정

### 선택 2. 점수 기능 추가

```text
Ch01_Setup_21_ScoreBasic
```

내용:

- Bullet이 Enemy를 맞추면 점수 증가
- 점수를 Console 또는 UI Text로 표시
- 충돌 대상을 구분하는 기본 조건 추가

## 최종 결론

이번 패치는 우주 배경을 실제 게임 씬에 적용하고, 텍스처 오프셋으로 스크롤 효과를 만든 단계다.

기존 게임 로직을 건드리지 않고 배경만 추가했기 때문에 구조가 안전하고, 수업용으로도 이해하기 쉽다.
