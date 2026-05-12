# Ch01_Setup_12_LayerCollisionMatrix

## 목표

게임 오브젝트에 레이어를 지정하고, 레이어별 충돌 여부를 설정한다.

이전 단계에서는 DestroyZone을 만들어 화면 밖으로 나간 오브젝트를 삭제했다.  
이번 단계에서는 Player, Bullet, Enemy, DestroyZone을 각각 다른 레이어로 나누고, 필요한 오브젝트끼리만 충돌하도록 정리한다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. Unity 레이어를 추가한다.
2. Player, Bullet, Enemy, DestroyZone에 알맞은 레이어를 지정한다.
3. Physics 설정에서 Layer Collision Matrix를 조정한다.
4. 불필요한 충돌은 꺼서 의도한 충돌만 발생하게 한다.
5. 코드 파일은 수정하지 않는다.

## 중요

- 이번 작업은 레이어와 레이어별 충돌 설정만 다룬다.
- 새 스크립트를 만들지 마.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- UI, 사운드, 이펙트는 만들지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 레이어 기준

아래 레이어를 사용한다.

| 레이어 번호 | 레이어 이름 | 적용 대상 |
|---|---|---|
| 6 | DestroyZone | DestroyZone_U, DestroyZone_D, DestroyZone_L, DestroyZone_R |
| 7 | Player | Player, FirePosition |
| 8 | Bullet | Bullet 프리팹 |
| 9 | Enemy | Enemy 프리팹 |

## 설정 기준

### Player

`Player` 오브젝트와 그 자식인 `FirePosition`은 Player 레이어로 설정한다.

### Bullet

`Bullet.prefab`은 Bullet 레이어로 설정한다.

### Enemy

`Enemy.prefab`은 Enemy 레이어로 설정한다.

### DestroyZone

상하좌우 DestroyZone 오브젝트는 DestroyZone 레이어로 설정한다.

## Layer Collision Matrix

Project Settings의 Physics 설정에서 레이어별 충돌 여부를 조정한다.

목표는 아래와 같다.

- Bullet과 Enemy는 충돌해야 한다.
- Enemy와 Player는 충돌해야 한다.
- Bullet과 DestroyZone은 감지되어야 한다.
- Enemy와 DestroyZone은 감지되어야 한다.
- 필요 없는 충돌은 꺼서 불필요한 충돌 처리를 줄인다.

정확한 충돌 조합은 Unity Editor에서 실제 동작을 확인하며 조정한다.

## Unity Editor에서 확인할 내용

작업 후 아래 내용을 확인한다.

- TagManager에 DestroyZone, Player, Bullet, Enemy 레이어가 있는지 확인
- Player가 Player 레이어인지 확인
- FirePosition이 Player 레이어인지 확인
- Bullet 프리팹이 Bullet 레이어인지 확인
- Enemy 프리팹이 Enemy 레이어인지 확인
- DestroyZone들이 DestroyZone 레이어인지 확인
- Physics의 Layer Collision Matrix가 의도대로 설정되었는지 확인

## Play 모드 확인

Play 모드에서 아래 내용을 확인한다.

- Player가 이동하는지 확인
- 총알이 발사되는지 확인
- Enemy가 생성되고 이동하는지 확인
- Bullet과 Enemy가 닿으면 사라지는지 확인
- Enemy와 Player가 닿으면 충돌 처리되는지 확인
- Bullet과 Enemy가 DestroyZone에 닿으면 삭제되는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- 파일 목록

주요 변경:
- 추가한 레이어
- 레이어를 적용한 오브젝트
- Layer Collision Matrix 변경

Unity Editor에서 확인할 내용:
- 레이어 설정 확인
- 충돌 매트릭스 확인
- Play 모드 충돌 동작 확인

아직 구현하지 않은 내용:
- 점수
- 게임 오버
- UI
- 사운드
- 이펙트
```
