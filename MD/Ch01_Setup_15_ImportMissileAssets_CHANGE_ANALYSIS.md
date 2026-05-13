# Ch01_Setup_15_ImportMissileAssets_CHANGE_ANALYSIS

## 변경 개요

이번 커밋에서는 에셋스토어에서 받은 로켓/미사일/폭탄 에셋이 프로젝트에 추가되었다.

커밋 메시지:

```text
미사일 에셋 추가
```

이번 단계는 코드 기능 구현이 아니라 리소스 import 단계다.

## 추가된 최상위 폴더

새로 추가된 에셋의 기준 폴더는 다음과 같다.

```text
Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs
```

## 추가된 파일 구조

추가된 주요 하위 폴더는 다음과 같다.

| 폴더 | 내용 |
|---|---|
| `Materials` | 로켓/미사일 색상 팔레트 머티리얼 |
| `Models` | RMB 계열 FBX 모델 |
| `Prefabs` | 색상별 로켓/미사일 프리팹 |
| `Scenes` | 에셋 예제 씬 |
| `Scripts` | 에셋 예제 스크립트 |
| `Textures` | 색상 팔레트 텍스처 |

또한 에셋 문서 PDF가 포함되어 있다.

```text
Documentation - Rockets, Missiles and Bombs Pack.pdf
```

## Materials 폴더 분석

추가된 주요 머티리얼은 다음과 같다.

```text
Backdrop.mat
RocketsPalletteBlank.mat
RocketsPalletteBlue.mat
RocketsPalletteGreen.mat
RocketsPalletteGrey.mat
RocketsPalletteOrange.mat
RocketsPallettePink.mat
RocketsPalletteRed.mat
RocketsPalletteYellow.mat
```

각 머티리얼의 `.meta` 파일도 함께 추가되었다.

이 머티리얼들은 로켓/미사일 프리팹의 색상 변형에 사용되는 것으로 볼 수 있다.

## Models 폴더 분석

`Models` 폴더에는 `RMB_01.fbx`부터 `RMB_41.fbx`까지의 FBX 모델이 추가되었다.

예:

```text
RMB_01.fbx
RMB_02.fbx
RMB_03.fbx
...
RMB_41.fbx
```

각 FBX 모델의 `.meta` 파일도 함께 추가되었다.

이 모델들은 이후 Bullet 외형을 미사일 또는 로켓 형태로 바꿀 때 사용할 수 있는 후보 리소스다.

## Prefabs 폴더 분석

`Prefabs` 폴더에는 색상별 하위 폴더가 추가되었다.

```text
Blue
Green
Grey
Orange
Pink
Red
Yellow
```

각 색상 폴더에는 `RMB_01.prefab`부터 `RMB_41.prefab`까지의 프리팹이 포함된다.

즉, 같은 형태의 로켓/미사일이라도 색상별 프리팹을 선택할 수 있다.

## Textures 폴더 분석

`Textures` 폴더에는 색상 팔레트 PNG 파일이 추가되었다.

예:

```text
RocketsPalletteBlue.png
RocketsPalletteGreen.png
RocketsPalletteGrey.png
RocketsPalletteOrange.png
RocketsPallettePink.png
RocketsPalletteRed.png
RocketsPalletteYellow.png
```

각 텍스처의 `.meta` 파일도 함께 추가되었다.

## Scenes 폴더 분석

추가된 예제 씬은 다음과 같다.

```text
DemoScene_RMB.unity
```

이 씬은 에셋 패키지에서 제공하는 미사일/로켓 프리팹 확인용 예제 씬으로 볼 수 있다.

현재 수업 프로젝트의 기준 씬은 기존 `SampleScene`이므로, 이번 단계에서는 예제 씬을 게임 흐름에 연결하지 않는다.

## Scripts 폴더 분석

추가된 예제 스크립트는 다음과 같다.

```text
RocketRotator.cs
```

이 스크립트는 에셋 예제 씬에서 미사일을 회전시켜 보여주는 용도일 가능성이 높다.

현재 프로젝트의 게임 로직인 `Bullet.cs`, `PlayerFire.cs`, `Enemy.cs`와는 별도로 둔다.

## 기존 게임 파일 변경 여부

이번 목록 기준으로 기존 게임 구현 파일은 수정되지 않았다.

수정되지 않은 주요 파일:

```text
Assets/Scenes/SampleScene.unity
Assets/Prefabs/Bullet.prefab
Assets/Prefabs/Enemy.prefab
Assets/Scripts/PlayerMove.cs
Assets/Scripts/PlayerFire.cs
Assets/Scripts/Bullet.cs
Assets/Scripts/Enemy.cs
Assets/Scripts/EnemyManager.cs
Assets/Scripts/DestroyZone.cs
```

따라서 이번 커밋은 기능 구현이 아니라 순수 에셋 추가로 보는 것이 맞다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. `Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs` 폴더가 정상적으로 보이는지 확인
2. `Models` 폴더의 RMB FBX 모델들이 정상적으로 import 되었는지 확인
3. `Prefabs`의 색상별 RMB 프리팹들이 정상적으로 열리는지 확인
4. 머티리얼과 텍스처가 Missing 상태가 아닌지 확인
5. Console에 import 오류가 없는지 확인
6. 기존 `SampleScene`이 수정되지 않았는지 확인
7. 에셋 예제 스크립트가 기존 게임 스크립트와 이름 충돌을 일으키지 않는지 확인

## 주의할 점

### 1. 에셋 패키지의 예제 스크립트

이번 에셋에는 `RocketRotator.cs`가 포함되어 있다.

이 스크립트는 에셋 예제용일 가능성이 높으므로,
현재 게임의 Bullet 이동 로직에 바로 연결하지 않는 것이 좋다.

### 2. 예제 씬

`DemoScene_RMB.unity` 예제 씬이 추가되었다.

이 씬은 에셋 확인용이고, 현재 수업 프로젝트의 작업 씬은 기존 `SampleScene`이다.

실수로 예제 씬에서 작업을 이어가지 않도록 주의해야 한다.

### 3. 에셋 수가 많다

색상별 프리팹과 41개 모델이 포함되어 있어 파일 수가 많다.

이런 에셋 import 단계는 전체 patch 대신 파일 목록과 요약으로 관리하는 것이 적절하다.

### 4. Bullet 외형 교체는 다음 단계에서 진행

이번 커밋은 에셋 추가만 했다.

Bullet 프리팹을 미사일 모델로 바꾸는 작업은 다음 단계에서 별도로 진행하는 것이 좋다.

## 결론

이번 커밋은 총알 외형으로 사용할 수 있는 미사일/로켓 리소스를 프로젝트에 추가하는 준비 단계다.

기존 게임 로직이나 씬을 수정하지 않고 에셋만 추가했기 때문에 변경 범위가 명확하다.

다음 단계에서는 이 에셋 중 하나를 선택해 기존 Bullet 프리팹의 외형으로 적용하면 된다.
