# Ch01_Setup_17_ImportEffectAssets

## 목표

슈팅 게임에서 충돌, 폭발, 피격 연출에 사용할 Cartoon FX Remaster 에셋을 프로젝트에 추가한다.

이번 단계는 에셋스토어에서 받은 이펙트 에셋을 프로젝트에 import하는 단계다.  
Bullet과 Enemy 충돌 시 이펙트를 실제로 생성하는 기능은 아직 진행하지 않는다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. 에셋스토어에서 받은 Cartoon FX Remaster 에셋을 Unity 프로젝트에 추가한다.
2. 추가된 에셋 폴더 구조를 확인한다.
3. 그래픽 리소스, 머티리얼, 메시, 셰이더, 프리팹, 예제 씬, 예제 스크립트가 정상적으로 들어왔는지 확인한다.
4. 기존 게임 로직 스크립트는 수정하지 않는다.
5. 에셋 추가 후 Unity Editor에서 import 오류가 없는지 확인한다.
6. 기존 `SampleScene`이 의도치 않게 바뀌었는지 확인한다.

## 중요

- 이번 작업은 이펙트 에셋 추가만 한다.
- Bullet과 Enemy 충돌 시 이펙트를 생성하는 기능은 아직 만들지 마.
- Player 충돌 이펙트는 아직 만들지 마.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- UI, 사운드는 만들지 마.
- 기존 게임 스크립트는 수정하지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 추가되는 에셋 기준

추가되는 에셋 폴더는 다음 경로를 기준으로 한다.

```text
Assets/JMO Assets/Cartoon FX Remaster
```

주요 하위 폴더는 다음과 같다.

```text
Assets/JMO Assets/Cartoon FX Remaster/CFXR Assets
Assets/JMO Assets/Cartoon FX Remaster/CFXR Prefabs
Assets/JMO Assets/Cartoon FX Remaster/Demo Assets
Assets/JMO Assets/Welcome Screen
```

## 확인할 에셋

### CFXR Assets

이 폴더에는 이펙트 제작에 필요한 기본 리소스가 들어 있다.

예상 하위 폴더:

```text
Editor
Graphics
Meshes
Misc
Scripts
Shaders
```

확인할 내용:

- 이펙트용 텍스처와 머티리얼이 들어왔는지 확인
- 이펙트용 메시가 들어왔는지 확인
- CFXR 런타임 스크립트와 셰이더가 import 되었는지 확인
- Editor 전용 스크립트가 Editor 폴더 아래에 있는지 확인

### CFXR Prefabs

이 폴더에는 실제로 사용할 수 있는 이펙트 프리팹들이 들어 있다.

예상 카테고리:

```text
Eerie
Electric
Explosions
Fire
Ice
Impacts
Light
Liquids
Magic Misc
Misc
Nature
Sword Trails
Texts
```

이번 슈팅 게임에서는 우선 다음 카테고리를 나중에 사용할 수 있다.

```text
Explosions
Impacts
Fire
Electric
Misc
```

### Demo Assets와 예제 씬

에셋에는 예제 씬과 데모용 리소스가 함께 들어올 수 있다.

예:

```text
CFXRF Demo.unity
Demo Assets
Readme Cartoon FX Remaster FREE.html
```

이번 단계에서는 예제 씬을 게임 흐름에 연결하지 않는다.

## Unity Editor에서 확인할 내용

작업 후 Unity Editor에서 아래 내용을 확인한다.

- `Assets/JMO Assets/Cartoon FX Remaster` 폴더가 생성되었는지 확인
- CFXR Prefabs 폴더의 이펙트 프리팹들이 정상적으로 보이는지 확인
- Console에 import 오류나 컴파일 오류가 없는지 확인
- 기존 `SampleScene`이 의도치 않게 수정되지 않았는지 확인
- 에셋의 예제 씬과 현재 작업 씬을 구분한다.

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

추가한 에셋 폴더:
- Assets/JMO Assets/Cartoon FX Remaster

추가된 주요 리소스:
- CFXR Assets
- CFXR Prefabs
- Demo Assets
- Welcome Screen
- Readme

수정한 기존 게임 파일:
- 없음 또는 SampleScene 변경 여부 명시

Unity Editor에서 확인할 내용:
- 에셋 import 오류가 없는지 확인
- 이펙트 프리팹이 정상적으로 보이는지 확인
- 기존 게임 씬이 의도치 않게 수정되지 않았는지 확인

아직 구현하지 않은 내용:
- 충돌 이펙트 생성
- 폭발 이펙트 생성
- 점수
- 게임 오버
- UI
- 사운드
```
