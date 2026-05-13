# Ch01_Setup_19_ImportBackgroundAssets

## 목표

슈팅 게임의 우주 배경으로 사용할 Starfield 배경 에셋을 프로젝트에 추가한다.

이번 단계는 배경 에셋을 Unity 프로젝트에 import하는 단계다.  
기존 `SampleScene`의 배경을 실제로 교체하는 작업은 아직 진행하지 않는다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. Starfield 배경 에셋을 Unity 프로젝트에 추가한다.
2. 추가된 에셋 폴더 구조를 확인한다.
3. 배경 텍스처, 머티리얼, 예제 씬, 예제 스크립트가 정상적으로 들어왔는지 확인한다.
4. 기존 게임 씬과 게임 스크립트는 수정하지 않는다.
5. 에셋 추가 후 Unity Editor에서 import 오류가 없는지 확인한다.

## 중요

- 이번 작업은 배경 에셋 추가만 한다.
- `SampleScene`의 배경은 아직 바꾸지 마.
- 스크롤 배경 기능은 아직 만들지 마.
- 배경 애니메이션은 아직 만들지 마.
- Player, Enemy, Bullet, Effect 관련 스크립트는 수정하지 마.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 추가되는 에셋 기준

추가되는 에셋 폴더는 다음 경로를 기준으로 한다.

```text
Assets/StarfieldMaterials
```

주요 하위 폴더는 다음과 같다.

```text
Assets/StarfieldMaterials/DemoMaterials
Assets/StarfieldMaterials/README
Assets/StarfieldMaterials/Scenes
Assets/StarfieldMaterials/Scripts
Assets/StarfieldMaterials/Textures
```

## 확인할 에셋

### DemoMaterials

배경에 사용할 머티리얼이 들어 있는지 확인한다.

예:

```text
Nebu_far.mat
Starfield_01_Material.mat
Starfield_Overlay.mat
```

### README

에셋 안내 문서가 포함되어 있는지 확인한다.

```text
Readme.txt
```

### Scenes

에셋 패키지의 예제 씬과 lighting 설정이 포함되어 있는지 확인한다.

```text
DemoScene.unity
DemoSceneSettings.lighting
```

### Scripts

에셋 예제용 스크립트가 포함되어 있는지 확인한다.

```text
Move.cs
```

이번 단계에서는 이 스크립트를 게임 로직에 연결하지 않는다.

### Textures

배경에 사용할 우주 텍스처가 포함되어 있는지 확인한다.

#### Plain Starfields

```text
PlainStarfield_01.png
PlainStarfield_02.png
PlainStarfield_03.png
PlainStarfield_04.png
```

#### SingleNeb

```text
SingleNeb_01.png
SingleNeb_02.png
SingleNeb_03.png
SingleNeb_04.png
SingleNeb_05.png
SingleNeb_06.png
SingleNeb_07.png
SingleNeb_08.png
SingleNeb_09.png
SingleNeb_10.png
```

## Unity Editor에서 확인할 내용

작업 후 Unity Editor에서 아래 내용을 확인한다.

- `Assets/StarfieldMaterials` 폴더가 생성되었는지 확인
- 머티리얼과 텍스처가 정상적으로 보이는지 확인
- Console에 import 오류가 없는지 확인
- 기존 `SampleScene`이 의도치 않게 수정되지 않았는지 확인
- 에셋의 예제 씬과 현재 작업 씬을 구분한다

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

추가한 에셋 폴더:
- Assets/StarfieldMaterials

추가된 주요 리소스:
- DemoMaterials
- README
- Scenes
- Scripts
- Textures

수정한 기존 게임 파일:
- 없음

Unity Editor에서 확인할 내용:
- 에셋 import 오류가 없는지 확인
- 배경 텍스처와 머티리얼이 정상적으로 보이는지 확인
- 기존 게임 씬이 수정되지 않았는지 확인

아직 구현하지 않은 내용:
- SampleScene 배경 적용
- 스크롤 배경
- 배경 애니메이션
- 게임 배경과 이펙트 연결
```
