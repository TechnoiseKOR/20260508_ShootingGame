# Ch01_Setup_16_BulletVisualMissile

## 목표

이전 단계에서 추가한 미사일 에셋을 사용해서 Bullet의 외형을 미사일 모델로 바꾼다.

이번 단계에서는 기존 Bullet 이동, 발사, 충돌 로직은 유지한다.  
기본 큐브처럼 보이던 Bullet의 겉모습만 미사일 프리팹을 이용해 바꾸고, 외형에 맞게 충돌 박스를 조정하는 것이 목표다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. 기존 Bullet 프리팹의 기본 큐브 MeshFilter와 MeshRenderer를 제거한다.
2. Bullet 프리팹 아래에 미사일 프리팹을 자식으로 추가한다.
3. 미사일 외형으로 사용할 프리팹의 크기와 회전을 조정한다.
4. Bullet 프리팹의 BoxCollider 크기와 중심을 미사일 외형에 맞게 조정한다.
5. 기존 Bullet 스크립트와 Bullet 레이어 설정은 유지한다.
6. Play 모드 확인을 위해 필요하다면 씬에 Bullet 프리팹 인스턴스를 배치한다.

## 중요

- 이번 작업은 Bullet 외형 변경과 충돌 박스 조정만 진행한다.
- Bullet.cs는 수정하지 마.
- PlayerFire.cs는 수정하지 마.
- Player, Enemy, EnemyManager, DestroyZone 스크립트는 수정하지 마.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- UI, 사운드, 이펙트는 만들지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### Bullet 프리팹 구조

Bullet 프리팹은 기존 게임 기능을 담당하는 부모 오브젝트로 유지한다.

Bullet 부모 오브젝트에는 기존처럼 아래 요소가 유지되어야 한다.

- BoxCollider
- Bullet 스크립트
- Bullet 레이어

기본 큐브 외형만 제거하고, 미사일 프리팹을 Bullet의 자식으로 둔다.

예시:

```text
Bullet
└─ RMB_08
```

### 미사일 외형

미사일 외형으로는 `BTM_Rockets_Missiles_Bombs` 에셋의 RMB 프리팹 중 하나를 사용한다.

예시:

```text
Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs/Prefabs/Blue/RMB_08.prefab
```

미사일 외형은 Bullet 크기에 맞게 작게 조정한다.

예시 기준:

```text
Scale: 0.06, 0.06, 0.06
```

### Bullet 충돌 박스

미사일 외형에 맞게 Bullet의 BoxCollider를 조정한다.

예시 기준:

```text
Size:   x 0.22, y 0.6, z 1
Center: x 0,    y -0.06, z 0
```

이 값은 미사일 외형과 실제 충돌 범위가 너무 어긋나지 않도록 하기 위한 기준이다.

## Unity Editor에서 확인할 내용

작업 후 Unity Editor에서 아래 내용을 확인한다.

- Bullet 프리팹이 더 이상 큐브로 보이지 않는지 확인
- Bullet 아래에 RMB_08 미사일 외형이 보이는지 확인
- Bullet 스크립트가 유지되는지 확인
- Bullet 레이어가 유지되는지 확인
- BoxCollider 크기와 중심이 미사일 외형과 크게 어긋나지 않는지 확인
- Play 모드에서 Player가 발사한 Bullet이 미사일 외형으로 보이는지 확인
- Bullet이 위쪽으로 이동하는지 확인
- Bullet과 Enemy 충돌이 기존처럼 동작하는지 확인
- DestroyZone에 닿으면 Bullet이 삭제되는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- 파일 목록

주요 변경:
- Bullet 외형 변경
- Bullet 충돌 박스 조정
- 기존 이동/발사/충돌 로직 유지

Unity Editor에서 확인할 내용:
- Bullet 미사일 외형 확인
- Bullet Collider 크기 확인
- Play 모드에서 발사, 이동, 충돌, 삭제 확인

아직 구현하지 않은 내용:
- 점수
- 게임 오버
- UI
- 사운드
- 이펙트
```
