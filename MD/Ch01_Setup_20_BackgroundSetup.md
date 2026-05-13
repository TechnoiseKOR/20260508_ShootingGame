# Ch01_Setup_20_BackgroundSetup

## 목표

이전 단계에서 추가한 Starfield 배경 에셋을 사용해서 게임 씬에 우주 배경을 적용하고, 배경이 천천히 움직이는 느낌을 만든다.

이번 단계에서는 배경 오브젝트와 배경 머티리얼을 만들고, 텍스처 오프셋을 이용해 배경이 스크롤되도록 구현한다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `Assets/Materials` 폴더가 없다면 생성한다.
2. `Assets/Materials/Mat_Background.mat` 머티리얼을 만든다.
3. Starfield 에셋의 배경 텍스처를 `Mat_Background`에 적용한다.
4. `Assets/Scripts/Background.cs` 스크립트를 만든다.
5. 기본 씬에 `Background` 오브젝트를 만든다.
6. `Background` 오브젝트에 `Mat_Background`를 적용한다.
7. `Background` 오브젝트에 `Background.cs`를 붙인다.
8. Play 모드에서 배경 텍스처가 천천히 움직이게 한다.

## 중요

- 이번 작업은 배경 적용과 배경 스크롤만 만든다.
- Player, Enemy, Bullet, DestroyZone 스크립트는 수정하지 마.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- UI, 사운드, 추가 이펙트는 만들지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### 배경 머티리얼

새 머티리얼을 만든다.

```text
Assets/Materials/Mat_Background.mat
```

이 머티리얼에는 Starfield 배경 텍스처를 연결한다.

예시 텍스처:

```text
Assets/StarfieldMaterials/Textures/SingleNeb/SingleNeb_07.png
```

### Background 오브젝트

`SampleScene`에 `Background` 오브젝트를 만든다.

배경은 게임 오브젝트들 뒤쪽에 보이도록 배치한다.

예시 기준:

```text
Position: x 0, y 0, z 1.5
Scale:    x 7, y 10, z 1
```

### Background.cs

`Background.cs`는 배경 머티리얼의 텍스처 오프셋을 계속 움직이게 한다.

```csharp
public Material bgMaterial;
public float scrollSpeed = 0.2f;

void Update()
{
    Vector2 direction = Vector2.up;
    bgMaterial.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
}
```

## Unity Editor에서 확인할 내용

- `Assets/Materials/Mat_Background.mat`이 생성되어 있는지 확인
- `Mat_Background`에 Starfield 텍스처가 연결되어 있는지 확인
- `SampleScene`에 `Background` 오브젝트가 있는지 확인
- `Background` 오브젝트에 `Mat_Background`가 적용되어 있는지 확인
- `Background` 오브젝트에 `Background.cs`가 붙어 있는지 확인
- `Background` 스크립트의 `bgMaterial`에 `Mat_Background`가 연결되어 있는지 확인
- Play 모드에서 배경이 천천히 움직이는지 확인

## 완료 후 보고 형식

```text
작업 완료

수정한 파일:
- 파일 목록

새로 만든 파일:
- 파일 목록

새로 만든 오브젝트:
- Background

주요 변경:
- 배경 머티리얼 생성
- 배경 오브젝트 추가
- 배경 스크롤 스크립트 추가

Unity Editor에서 확인할 내용:
- Background 오브젝트 위치 확인
- Mat_Background 텍스처 연결 확인
- Background 스크립트의 bgMaterial 연결 확인
- Play 모드에서 배경 스크롤 확인

아직 구현하지 않은 내용:
- 배경 반복 타일 조정
- 패럴랙스 배경
- 점수
- 게임 오버
- UI
```
