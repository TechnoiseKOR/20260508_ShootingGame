# Ch01_Setup_15_ImportMissileAssets

## 목표

슈팅 게임에서 총알 또는 미사일 외형으로 사용할 로켓/미사일 에셋을 프로젝트에 추가한다.

이번 단계는 에셋스토어에서 받은 미사일 에셋을 프로젝트에 import하는 단계다.  
기존 Bullet 프리팹의 외형 교체는 아직 진행하지 않는다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. 에셋스토어에서 받은 로켓/미사일 에셋을 Unity 프로젝트에 추가한다.
2. 추가된 에셋 폴더 구조를 확인한다.
3. 미사일 모델, 머티리얼, 텍스처, 프리팹, 예제 씬, 문서가 정상적으로 들어왔는지 확인한다.
4. 기존 게임 씬과 게임 스크립트는 수정하지 않는다.
5. 에셋 추가 후 Unity Editor에서 import 오류가 없는지 확인한다.

## 중요

- 이번 작업은 에셋 추가만 한다.
- 기존 Bullet 외형은 아직 바꾸지 마.
- 기존 Player 외형은 바꾸지 마.
- 기존 Enemy 외형은 바꾸지 마.
- SampleScene은 수정하지 마.
- 기존 게임 스크립트는 수정하지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 추가되는 에셋 기준

추가되는 에셋 폴더는 다음 경로를 기준으로 한다.

```text
Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs
```

주요 하위 폴더는 다음과 같다.

```text
Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs/Materials
Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs/Models
Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs/Prefabs
Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs/Scenes
Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs/Scripts
Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs/Textures
```

## 확인할 에셋

### 문서

에셋 패키지 문서가 포함되어 있는지 확인한다.

```text
Documentation - Rockets, Missiles and Bombs Pack.pdf
```

### 머티리얼

로켓/미사일 색상 팔레트 머티리얼이 포함되어 있는지 확인한다.

예:

```text
RocketsPalletteBlue.mat
RocketsPalletteGreen.mat
RocketsPalletteGrey.mat
RocketsPalletteOrange.mat
RocketsPallettePink.mat
RocketsPalletteRed.mat
RocketsPalletteYellow.mat
```

### 모델

미사일/로켓 FBX 모델이 포함되어 있는지 확인한다.

예:

```text
RMB_01.fbx
RMB_02.fbx
...
RMB_41.fbx
```

### 프리팹

색상별 프리팹 폴더가 포함되어 있는지 확인한다.

예:

```text
Prefabs/Blue
Prefabs/Green
Prefabs/Grey
Prefabs/Orange
Prefabs/Pink
Prefabs/Red
Prefabs/Yellow
```

각 색상 폴더 안에는 `RMB_01.prefab`부터 `RMB_41.prefab`까지 포함될 수 있다.

### 예제 씬과 예제 스크립트

에셋 패키지에 포함된 예제 씬과 예제 스크립트가 함께 들어올 수 있다.

```text
DemoScene_RMB.unity
RocketRotator.cs
```

이번 단계에서는 이 파일들을 게임 로직에 연결하지 않는다.  
단순히 에셋 import 결과로 포함된 파일로 둔다.

## Unity Editor에서 확인할 내용

작업 후 Unity Editor에서 아래 내용을 확인한다.

- `Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs` 폴더가 생성되었는지 확인
- 미사일 프리팹들이 정상적으로 보이는지 확인
- 모델과 머티리얼이 Missing 상태가 아닌지 확인
- Console에 import 오류가 없는지 확인
- 기존 `SampleScene`이 의도치 않게 수정되지 않았는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

추가한 에셋 폴더:
- Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs

추가된 주요 리소스:
- Documentation
- Materials
- Models
- Prefabs
- Scenes
- Scripts
- Textures

수정한 기존 게임 파일:
- 없음

Unity Editor에서 확인할 내용:
- 에셋 import 오류가 없는지 확인
- 미사일 프리팹이 정상적으로 보이는지 확인
- 기존 게임 씬이 수정되지 않았는지 확인

아직 구현하지 않은 내용:
- Bullet 외형 교체
- 미사일 발사 이펙트
- 폭발 이펙트
- 에셋을 게임 로직에 연결
```
