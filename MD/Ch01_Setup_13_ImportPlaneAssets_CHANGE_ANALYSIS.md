# Ch01_Setup_13_ImportPlaneAssets_CHANGE_ANALYSIS

## 변경 개요

이번 커밋에서는 에셋스토어에서 받은 비행기 에셋이 프로젝트에 추가되었다.

커밋 메시지:

```text
비행기 에셋 추가
```

이번 단계는 코드 기능 구현이 아니라 리소스 import 단계다.

## 추가된 최상위 폴더

새로 추가된 에셋의 기준 폴더는 다음과 같다.

```text
Assets/AwesomeCartoonPlanes
```

## 추가된 파일 구조

추가된 주요 하위 폴더는 다음과 같다.

| 폴더 | 내용 |
|---|---|
| `Assets/AwesomeCartoonPlanes/Images` | 비행기와 프로펠러 텍스처 이미지 |
| `Assets/AwesomeCartoonPlanes/Materials` | 비행기와 프로펠러 머티리얼 |
| `Assets/AwesomeCartoonPlanes/Models` | FBX 모델 |
| `Assets/AwesomeCartoonPlanes/Prefabs` | 비행기 프리팹 |
| `Assets/AwesomeCartoonPlanes/Scenes` | 에셋 예제 씬과 lighting 설정 |
| `Assets/AwesomeCartoonPlanes/Scripts` | 에셋 예제 스크립트 |

## Images 폴더 분석

추가된 이미지 파일은 다음과 같다.

```text
Plane1_2048x2048.psd
Plane2_2048x2048.psd
Plane3_2048x2048.psd
Plane4_2048x2048.psd
Plane_UV_2048x2048.psd
Prop1.psd
Prop1Blur.psd
```

각 파일의 `.meta`도 함께 추가되었다.

이 파일들은 비행기 모델에 적용되는 텍스처와 프로펠러 이미지로 볼 수 있다.

## Materials 폴더 분석

추가된 머티리얼 파일은 다음과 같다.

```text
Plane1_2048x2048.mat
Plane2_2048x2048.mat
Plane3_2048x2048.mat
Plane4_2048x2048.mat
Prop1.mat
Prop1Blur.mat
```

각 머티리얼 파일의 `.meta`도 함께 추가되었다.

이 머티리얼들은 비행기 프리팹이나 모델에 연결되어 있을 가능성이 높다.

## Models 폴더 분석

추가된 모델 파일은 다음과 같다.

```text
Plane1.fbx
Prop1.fbx
Prop1Blur.fbx
```

각 FBX 파일의 `.meta`도 함께 추가되었다.

비행기 본체와 프로펠러 모델 리소스로 볼 수 있다.

## Prefabs 폴더 분석

추가된 프리팹 파일은 다음과 같다.

```text
Plane1.prefab
Plane2.prefab
Plane3.prefab
Plane4.prefab
```

각 프리팹의 `.meta`도 함께 추가되었다.

이 프리팹들은 이후 플레이어 또는 적 외형으로 교체할 때 사용할 후보 리소스다.

## Scenes 폴더 분석

추가된 씬 관련 파일은 다음과 같다.

```text
Planes.unity
PlanesSettings.lighting
```

각 파일의 `.meta`도 함께 추가되었다.

이 파일들은 에셋 패키지에서 제공하는 예제 씬과 lighting 설정으로 보인다.

이번 단계에서는 기존 게임 씬인 `SampleScene`을 수정하지 않는다.

## Scripts 폴더 분석

추가된 스크립트 파일은 다음과 같다.

```text
Plane.cs
RotateCamera.cs
```

각 스크립트의 `.meta`도 함께 추가되었다.

이 스크립트들은 에셋 예제 씬에서 사용하는 샘플 스크립트일 가능성이 높다.

이번 프로젝트의 게임 로직과 바로 연결하지 않고, 에셋 패키지 구성 파일로만 둔다.

## 기존 게임 파일 변경 여부

이번 목록 기준으로 기존 게임 구현 파일은 수정되지 않았다.

수정되지 않은 주요 파일:

```text
Assets/Scenes/SampleScene.unity
Assets/Scripts/PlayerMove.cs
Assets/Scripts/PlayerFire.cs
Assets/Scripts/Bullet.cs
Assets/Scripts/Enemy.cs
Assets/Scripts/EnemyManager.cs
Assets/Scripts/DestroyZone.cs
ProjectSettings/TagManager.asset
ProjectSettings/DynamicsManager.asset
```

따라서 이번 커밋은 기능 구현이 아니라 순수 에셋 추가로 보는 것이 맞다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. `Assets/AwesomeCartoonPlanes` 폴더가 정상적으로 보이는지 확인
2. `Prefabs` 폴더의 `Plane1` ~ `Plane4` 프리팹이 정상적으로 열리는지 확인
3. 모델과 머티리얼이 Missing 상태가 아닌지 확인
4. Console에 import 오류가 없는지 확인
5. 기존 `SampleScene`이 수정되지 않았는지 확인
6. 에셋 예제 스크립트가 기존 게임 스크립트와 이름 충돌을 일으키지 않는지 확인

## 주의할 점

### 1. 에셋 패키지의 예제 스크립트

이번 에셋에는 다음 스크립트가 포함되어 있다.

```text
Plane.cs
RotateCamera.cs
```

이 스크립트들은 에셋 예제용일 가능성이 높다.

이 프로젝트의 `PlayerMove`, `Enemy`, `Bullet` 같은 수업용 스크립트와 역할이 다르므로, 바로 연결하지 않는 것이 좋다.

### 2. 예제 씬

`Planes.unity` 예제 씬이 추가되었다.

이 씬은 에셋 확인용으로 사용할 수 있지만, 현재 게임 제작 기준 씬은 기존 `SampleScene`이다.

실수로 예제 씬에서 작업을 이어가지 않도록 주의해야 한다.

### 3. 용량이 큰 파일

PSD와 FBX 파일이 포함되어 있어 patch 전체 용량이 커질 수 있다.

이런 에셋 import 단계는 전체 diff 대신 파일 목록과 요약으로 관리하는 것이 좋다.

### 4. 외형 교체는 다음 단계에서 진행

이번 커밋은 에셋 추가만 했다.

플레이어 또는 적을 비행기 프리팹으로 바꾸는 작업은 다음 단계에서 별도로 진행하는 것이 좋다.

## 결론

이번 커밋은 비행기 외형 리소스를 프로젝트에 추가하는 준비 단계다.

기존 게임 로직이나 씬을 수정하지 않고 에셋만 추가했기 때문에 변경 범위가 명확하다.

다음 단계에서는 이 에셋 중 어떤 프리팹을 플레이어 또는 적에 사용할지 정하고, 기존 오브젝트 외형을 교체하면 된다.
