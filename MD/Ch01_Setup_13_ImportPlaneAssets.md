# Ch01_Setup_13_ImportPlaneAssets

## 목표

슈팅 게임에서 사용할 비행기 외형 리소스를 프로젝트에 추가한다.

이번 단계는 에셋스토어에서 받은 비행기 에셋을 프로젝트에 import하는 단계다.  
플레이어, 적, 총알의 실제 외형 교체는 아직 진행하지 않는다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. 에셋스토어에서 받은 비행기 에셋을 Unity 프로젝트에 추가한다.
2. 추가된 에셋 폴더 구조를 확인한다.
3. 비행기 모델, 머티리얼, 이미지, 프리팹, 예제 씬이 정상적으로 들어왔는지 확인한다.
4. 기존 게임 씬과 스크립트는 수정하지 않는다.
5. 에셋 추가 후 Unity Editor에서 import 오류가 없는지 확인한다.

## 중요

- 이번 작업은 에셋 추가만 한다.
- 기존 플레이어 외형은 아직 바꾸지 마.
- 기존 적 외형은 아직 바꾸지 마.
- 기존 총알 외형은 아직 바꾸지 마.
- SampleScene은 수정하지 마.
- 기존 스크립트는 수정하지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 추가되는 에셋 기준

추가되는 에셋 폴더는 다음 경로를 기준으로 한다.

```text
Assets/AwesomeCartoonPlanes
```

주요 하위 폴더는 다음과 같다.

```text
Assets/AwesomeCartoonPlanes/Images
Assets/AwesomeCartoonPlanes/Materials
Assets/AwesomeCartoonPlanes/Models
Assets/AwesomeCartoonPlanes/Prefabs
Assets/AwesomeCartoonPlanes/Scenes
Assets/AwesomeCartoonPlanes/Scripts
```

## 확인할 에셋

### 이미지

비행기 텍스처와 프로펠러 이미지가 포함되어 있는지 확인한다.

```text
Plane1_2048x2048.psd
Plane2_2048x2048.psd
Plane3_2048x2048.psd
Plane4_2048x2048.psd
Plane_UV_2048x2048.psd
Prop1.psd
Prop1Blur.psd
```

### 머티리얼

비행기와 프로펠러 머티리얼이 포함되어 있는지 확인한다.

```text
Plane1_2048x2048.mat
Plane2_2048x2048.mat
Plane3_2048x2048.mat
Plane4_2048x2048.mat
Prop1.mat
Prop1Blur.mat
```

### 모델

비행기와 프로펠러 모델이 포함되어 있는지 확인한다.

```text
Plane1.fbx
Prop1.fbx
Prop1Blur.fbx
```

### 프리팹

비행기 프리팹이 포함되어 있는지 확인한다.

```text
Plane1.prefab
Plane2.prefab
Plane3.prefab
Plane4.prefab
```

### 예제 씬과 스크립트

에셋 패키지에 포함된 예제 씬과 예제 스크립트가 함께 들어올 수 있다.

```text
Planes.unity
Plane.cs
RotateCamera.cs
```

이번 단계에서는 이 파일들을 분석하거나 게임 로직에 연결하지 않는다.  
단순히 에셋 import 결과로 포함된 파일로 둔다.

## Unity Editor에서 확인할 내용

작업 후 Unity Editor에서 아래 내용을 확인한다.

- `Assets/AwesomeCartoonPlanes` 폴더가 생성되었는지 확인
- 비행기 프리팹들이 정상적으로 보이는지 확인
- 모델과 머티리얼이 깨지지 않았는지 확인
- Console에 import 오류가 없는지 확인
- 기존 `SampleScene`이 의도치 않게 수정되지 않았는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

추가한 에셋 폴더:
- Assets/AwesomeCartoonPlanes

추가된 주요 리소스:
- Images
- Materials
- Models
- Prefabs
- Scenes
- Scripts

수정한 기존 게임 파일:
- 없음

Unity Editor에서 확인할 내용:
- 에셋 import 오류가 없는지 확인
- 비행기 프리팹이 정상적으로 보이는지 확인
- 기존 게임 씬이 수정되지 않았는지 확인

아직 구현하지 않은 내용:
- 플레이어 외형 교체
- 적 외형 교체
- 총알 외형 교체
- 에셋을 게임 로직에 연결
```
