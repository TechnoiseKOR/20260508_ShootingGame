# Ch01_Setup_16_BulletVisualMissile_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 이전 단계에서 import한 미사일 에셋을 사용해 Bullet 프리팹의 외형을 변경하고, 충돌 박스를 조정했다.

주요 변경은 다음과 같다.

1. Bullet 프리팹의 기본 큐브 MeshFilter / MeshRenderer 제거
2. Bullet 프리팹 아래에 `RMB_08` 미사일 프리팹 인스턴스 추가
3. Bullet 프리팹 Transform 기준값 정리
4. Bullet BoxCollider 크기와 중심 조정
5. SampleScene에 Bullet 프리팹 인스턴스 추가
6. 기존 Bullet 이동 스크립트는 유지

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Prefabs/Bullet.prefab` | Bullet 큐브 외형 제거, RMB_08 자식 프리팹 추가, Collider 조정 |
| `Assets/Scenes/SampleScene.unity` | Bullet 프리팹 인스턴스 추가 |

## Bullet 프리팹 변경 분석

`Assets/Prefabs/Bullet.prefab`에서 기존 큐브 외형에 해당하는 컴포넌트가 제거되었다.

제거된 컴포넌트:

- MeshFilter
- MeshRenderer

Bullet 부모 오브젝트에는 기존 기능 컴포넌트가 유지된다.

유지되는 주요 컴포넌트:

- Transform
- BoxCollider
- Bullet 스크립트
- Bullet 레이어

이제 Bullet 부모 오브젝트는 게임 기능과 충돌 처리를 담당하고, 실제 보이는 외형은 자식 미사일 프리팹이 담당한다.

## Bullet Transform 변경 분석

Bullet 프리팹의 Transform 값이 정리되었다.

```text
기존 Local Position: x 0, y 1.5, z 0
변경 Local Position: x 0, y 0, z 0

기존 Local Scale: x 0.25, y 0.6, z 1
변경 Local Scale: x 1, y 1, z 1
```

이 변경은 Bullet 부모 오브젝트의 기준 Transform을 기본값에 가깝게 정리하고, 실제 크기 표현은 자식 미사일 외형과 BoxCollider 조정으로 처리하는 방식이다.

## BoxCollider 변경 분석

Bullet의 BoxCollider 크기와 중심이 조정되었다.

```text
기존 Size:   x 1, y 1, z 1
변경 Size:   x 0.22, y 0.6, z 1

기존 Center: x 0, y 0, z 0
변경 Center: x 0, y -0.06, z 0
```

이 변경으로 Bullet의 충돌 범위가 큐브 기준이 아니라 미사일 외형에 더 가까운 세로형 박스로 바뀌었다.

## 미사일 외형 추가 분석

Bullet 프리팹 아래에 `RMB_08` 프리팹 인스턴스가 자식으로 추가되었다.

구조는 다음과 비슷하다.

```text
Bullet
└─ RMB_08
```

`RMB_08`의 주요 Transform 조정은 다음과 같다.

| 항목 | 값 |
|---|---|
| Scale | `0.06, 0.06, 0.06` |
| Local Position | `0, 0, 0` |
| Local Rotation | `0, 0, 0` |

즉, Bullet의 이동과 충돌은 부모 Bullet이 담당하고, 화면에 보이는 외형은 자식 RMB_08이 담당한다.

## SampleScene 변경 분석

`Assets/Scenes/SampleScene.unity`에는 Bullet 프리팹 인스턴스가 새로 추가되었다.

이 인스턴스는 Bullet 프리팹의 변경 사항을 씬에서 확인하기 위한 배치로 볼 수 있다.

추가된 Bullet 인스턴스에도 Collider 조정값이 반영되어 있다.

```text
BoxCollider Size: x 0.22, y 0.6
BoxCollider Center.y: -0.06
```

## 유지된 기능

이번 패치에서는 C# 스크립트가 수정되지 않았다.

따라서 아래 기능은 기존 구현을 그대로 사용한다.

- PlayerFire의 Bullet 생성
- Bullet의 위쪽 이동
- Enemy와의 충돌 처리
- DestroyZone 삭제 처리
- 레이어 기반 충돌 설정

이번 작업은 기능 변경이 아니라 Bullet 외형 교체와 충돌 박스 보정에 해당한다.

## 구현된 내용

이번 패치로 구현된 내용은 다음과 같다.

- Bullet 큐브 외형 제거
- Bullet에 RMB_08 미사일 외형 추가
- Bullet 부모 Transform 기준값 정리
- Bullet BoxCollider 크기 조정
- Bullet BoxCollider 중심 조정
- 씬에 확인용 Bullet 인스턴스 추가

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 점수
- 게임 오버
- UI
- 총알 발사 이펙트
- 폭발 이펙트
- 사운드
- 오브젝트 풀링
- 총알 종류 선택

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. Bullet 프리팹이 큐브가 아니라 RMB_08 미사일 외형으로 보이는지 확인
2. Bullet 프리팹에 Bullet 스크립트가 유지되는지 확인
3. Bullet 프리팹의 Layer가 Bullet인지 확인
4. Bullet BoxCollider가 미사일 외형과 크게 어긋나지 않는지 확인
5. Player가 발사하는 Bullet이 미사일 외형으로 생성되는지 확인
6. Bullet이 위쪽으로 이동하는지 확인
7. Bullet과 Enemy 충돌이 기존처럼 동작하는지 확인
8. Bullet이 DestroyZone에 닿으면 삭제되는지 확인
9. Console에 Missing Prefab 또는 Missing Script 오류가 없는지 확인

## 주의할 점

### 1. 씬에 Bullet 인스턴스가 추가되었다

이번 패치에서는 `SampleScene`에 Bullet 프리팹 인스턴스가 추가되었다.

이것이 확인용 배치라면 수업 중에는 괜찮지만, 최종 게임 흐름에서는 PlayerFire가 Bullet을 생성하므로 씬에 미리 놓인 Bullet이 꼭 필요한지 확인해야 한다.

### 2. 외형과 충돌 박스가 완전히 같지는 않을 수 있다

미사일 외형은 3D 모델이고, 충돌은 BoxCollider 하나로 처리한다.

따라서 실제 보이는 미사일과 충돌 범위가 완전히 일치하지 않을 수 있다.

초급 수업에서는 단순한 BoxCollider를 유지하는 것이 적절하다.

### 3. 모델 방향 확인 필요

RMB_08 프리팹의 회전값은 기본값으로 유지되어 있다.

Play 모드에서 미사일 앞쪽이 Bullet 이동 방향과 맞는지 확인해야 한다.

### 4. 에셋 참조 유지

Bullet 프리팹은 `BTM_Rockets_Missiles_Bombs` 에셋의 RMB_08 프리팹을 참조한다.

해당 에셋 폴더나 프리팹을 이동하거나 삭제하면 Bullet 외형 참조가 깨질 수 있다.

## 결론

이번 패치는 Bullet의 기본 큐브 외형을 미사일 모델로 교체하고, 충돌 박스를 미사일 외형에 맞게 조정한 단계다.

기존 Bullet 이동과 발사 코드는 그대로 유지하고 외형만 자식 프리팹으로 교체했기 때문에 구조가 안전하고 수업 흐름에도 적절하다.
