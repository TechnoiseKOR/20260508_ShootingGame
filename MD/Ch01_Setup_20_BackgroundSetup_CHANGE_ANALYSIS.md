# Ch01_Setup_20_BackgroundSetup_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 Starfield 배경 에셋을 실제 게임 씬에 적용하고, 배경이 천천히 움직이도록 하는 기능이 추가되었다.

주요 변경은 다음과 같다.

1. `Assets/Materials` 폴더 추가
2. `Mat_Background.mat` 배경 머티리얼 추가
3. `SampleScene`에 `Background` 오브젝트 추가
4. `Background` 오브젝트에 배경 머티리얼 적용
5. `Background.cs` 스크립트 추가
6. `Background` 오브젝트에 `Background.cs` 연결
7. `SingleNeb_07.png.meta` import 설정 갱신

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Materials.meta` | Materials 폴더 메타 파일 추가 |
| `Assets/Materials/Mat_Background.mat` | 배경용 머티리얼 추가 |
| `Assets/Materials/Mat_Background.mat.meta` | 배경 머티리얼 메타 파일 추가 |
| `Assets/Scenes/SampleScene.unity` | Background 오브젝트 추가 및 머티리얼/스크립트 연결 |
| `Assets/Scripts/Background.cs` | 배경 스크롤 스크립트 추가 |
| `Assets/Scripts/Background.cs.meta` | Background 스크립트 메타 파일 추가 |
| `Assets/StarfieldMaterials/Textures/SingleNeb/SingleNeb_07.png.meta` | 텍스처 import 설정 갱신 |

## Mat_Background 머티리얼 분석

새 머티리얼 `Assets/Materials/Mat_Background.mat`이 추가되었다.

이 머티리얼은 URP Lit 계열 셰이더를 사용하며, BaseMap과 MainTex에 Starfield 텍스처가 연결되어 있다.

연결된 텍스처는 다음 파일이다.

```text
Assets/StarfieldMaterials/Textures/SingleNeb/SingleNeb_07.png
```

즉, 이번 배경은 StarfieldMaterials 에셋의 성운 배경 텍스처를 사용하는 구조다.

## SampleScene 변경 분석

`SampleScene.unity`에 새 루트 오브젝트 `Background`가 추가되었다.

추가된 주요 컴포넌트는 다음과 같다.

- Transform
- MeshFilter
- MeshRenderer
- MeshCollider
- Background 스크립트

Background의 Transform 값은 다음과 같다.

```text
Position: x 0, y 0, z 1.5
Scale:    x 7, y 10, z 1
```

Background의 MeshRenderer에는 `Mat_Background`가 연결되어 있다.

Background 스크립트 필드 값은 다음과 같다.

```text
bgMaterial: Mat_Background
scrollSpeed: 0.2
```

## Background.cs 분석

새 파일 `Assets/Scripts/Background.cs`가 추가되었다.

핵심 코드는 다음과 같다.

```csharp
public Material bgMaterial;
public float scrollSpeed = 0.2f;

void Update()
{
    Vector2 direction = Vector2.up;
    bgMaterial.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
}
```

이 코드는 매 프레임 배경 머티리얼의 `mainTextureOffset` 값을 위쪽 방향으로 조금씩 이동시킨다.

그 결과 배경 텍스처가 움직이는 것처럼 보인다.

## 텍스처 메타 변경 분석

`SingleNeb_07.png.meta`가 갱신되었다.

주요 변화는 Unity 최신 버전의 TextureImporter 직렬화 형식에 맞춰 import 설정이 갱신된 것으로 보인다.

예를 들어 다음 항목들이 추가되거나 변경되었다.

- `AssetOrigin`
- `serializedVersion: 13`
- `internalIDToNameTable`
- `externalObjects`
- 플랫폼별 texture import 설정
- wrapU, wrapV, wrapW 설정

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- 게임 씬에 우주 배경 오브젝트 추가
- 배경 전용 머티리얼 생성
- Starfield 텍스처를 배경 머티리얼에 적용
- 배경 텍스처 오프셋 스크롤 구현
- Inspector에서 배경 머티리얼과 스크롤 속도 조절 가능

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 배경 2중 스크롤
- 패럴랙스 배경
- 배경 반복 타일 세부 조정
- 배경 전용 레이어 설정
- 점수
- 게임 오버
- UI
- 사운드

## 확인할 점

1. `Assets/Materials/Mat_Background.mat`이 있는지 확인
2. `Mat_Background`에 `SingleNeb_07.png`가 연결되어 있는지 확인
3. `SampleScene`에 `Background` 오브젝트가 있는지 확인
4. `Background` 오브젝트에 `Mat_Background`가 적용되어 있는지 확인
5. `Background` 오브젝트에 `Background.cs`가 붙어 있는지 확인
6. `Background` 스크립트의 `bgMaterial`에 `Mat_Background`가 연결되어 있는지 확인
7. Play 모드에서 배경이 천천히 움직이는지 확인
8. Player, Enemy, Bullet이 배경보다 앞에 잘 보이는지 확인
9. Console에 오류가 없는지 확인

## 주의할 점

### 1. bgMaterial 연결이 빠지면 오류가 날 수 있다

현재 `Background.cs`는 `bgMaterial`이 연결되어 있다는 전제로 작성되어 있다.

```csharp
bgMaterial.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
```

Inspector에서 `bgMaterial`이 비어 있으면 NullReferenceException이 발생할 수 있다.

### 2. 머티리얼 오프셋을 직접 변경한다

현재 코드는 머티리얼의 `mainTextureOffset`을 직접 변경한다.

이 방식은 이해하기 쉽고 수업용으로 적절하다.

다만 같은 머티리얼을 여러 오브젝트가 공유하면 모든 오브젝트의 텍스처 오프셋이 함께 움직일 수 있다.

이번 단계에서는 배경 전용 머티리얼을 만들었기 때문에 문제가 적다.

### 3. Background에 MeshCollider가 있다

Background 오브젝트에는 MeshCollider가 포함되어 있다.

배경은 충돌이 필요 없는 오브젝트이므로 나중에 충돌 문제가 생기면 MeshCollider를 제거해도 된다.

## 결론

이번 패치는 Starfield 배경 텍스처를 실제 게임 씬에 적용하고, 텍스처 오프셋으로 스크롤 효과를 구현한 단계다.

기존 Player, Bullet, Enemy 로직은 건드리지 않고 배경만 추가했기 때문에 변경 범위가 명확하다.
