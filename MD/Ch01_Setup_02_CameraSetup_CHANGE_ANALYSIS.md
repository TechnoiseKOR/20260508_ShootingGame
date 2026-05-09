# Ch01_Setup_02_CameraSetup_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 슈팅 게임 제작을 시작하기 전 기본 제작 환경 일부를 정리했다.

주요 변경은 다음 세 가지다.

1. `.gitignore`에 `.vsconfig` 제외 규칙 추가
2. 기본 씬의 카메라 Orthographic Size 변경
3. Unity 프로젝트 입력 처리 설정 변경

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `.gitignore` | `.vsconfig` 제외 규칙 추가 |
| `Assets/Scenes/SampleScene.unity` | Main Camera의 Orthographic Size 변경 |
| `ProjectSettings/ProjectSettings.asset` | 입력 처리 설정 변경 |

## `.gitignore` 변경 분석

```diff
+.vsconfig
```

`.vsconfig`가 Git 제외 대상에 추가되었다.

`.vsconfig`는 Visual Studio 관련 환경 설정 파일로 사용될 수 있다.  
이번 프로젝트는 Visual Studio Code와 Codex CLI 기준으로 진행하므로, 로컬 IDE 설정 파일이 Git에 포함되지 않도록 제외한 것으로 볼 수 있다.

기존에 `patch.diff`도 제외되어 있으므로, 임시 패치 파일과 로컬 개발 환경 파일이 커밋에 섞이는 것을 줄일 수 있다.

## `Assets/Scenes/SampleScene.unity` 변경 분석

```diff
-  orthographic size: 3.14
+  orthographic size: 5
```

기본 씬의 카메라 Orthographic Size가 `3.14`에서 `5`로 변경되었다.

이 변경은 카메라가 보여주는 세로 화면 범위를 넓히는 작업이다.

Orthographic Size가 커지면 화면에 더 넓은 영역이 보인다.  
슈팅 게임에서는 플레이어, 적, 총알이 움직일 공간을 확인해야 하므로, 초반 제작 환경 설정으로 적절한 변경이다.

## `ProjectSettings/ProjectSettings.asset` 변경 분석

```diff
-  activeInputHandler: 1
+  activeInputHandler: 2
```

Unity 프로젝트의 입력 처리 설정이 변경되었다.

패치 기준으로 `activeInputHandler` 값이 `1`에서 `2`로 바뀌었다.

이 설정은 Unity의 입력 처리 방식과 관련된 프로젝트 설정이다.  
수업에서는 단순한 입력 처리를 사용할 수 있으므로, 기존 Input과 새 Input System이 충돌하지 않도록 입력 처리 설정을 조정한 것으로 볼 수 있다.

## 구현 기능 여부

이번 패치에서는 실제 게임 기능이 추가되지 않았다.

아직 구현되지 않은 것:

- 플레이어 이동
- 총알 발사
- 적 생성
- 충돌 처리
- 점수
- 게임 오버

이번 단계는 실제 구현 전 제작 환경 설정 단계다.

## 확인할 점

Unity Editor에서 다음 내용을 확인하면 좋다.

1. `SampleScene`을 열었을 때 Main Camera가 Orthographic 상태인지 확인
2. Main Camera의 Orthographic Size가 `5`인지 확인
3. Game View에서 화면이 너무 좁거나 넓지 않은지 확인
4. Project Settings의 입력 처리 설정이 의도한 방식인지 확인
5. `.vsconfig`가 Git 변경 목록에 올라오지 않는지 확인

## 주의할 점

### 1. 씬 파일 변경

`SampleScene.unity`는 Unity 씬 파일이다.

씬 파일은 YAML 형태로 diff가 보이지만, Unity Editor에서 저장되는 파일이므로 수동 수정은 조심해야 한다.

이번 변경은 카메라 값 하나만 바뀐 작은 변경이라 범위가 적절하다.

### 2. 입력 처리 설정 변경

입력 처리 설정은 이후 플레이어 이동 구현에 영향을 줄 수 있다.

다음 단계에서 플레이어 이동을 구현할 때는 어떤 입력 방식을 사용할지 프롬프트에 명확히 적는 것이 좋다.

예:

```text
이번 기능에서는 Unity 기본 Input 방식을 사용해줘.
```

또는

```text
이번 기능에서는 현재 프로젝트 입력 설정에 맞춰 간단한 키보드 입력으로 구현해줘.
```

## 결론

이번 패치는 실제 게임 기능 구현 전,
카메라 시야와 입력 처리 환경을 정리하는 제작 환경 설정 작업이다.

변경 범위가 작고 명확하며,
다음 단계에서 플레이어 오브젝트나 플레이어 이동 기능을 구현하기 위한 준비 작업으로 적절하다.
