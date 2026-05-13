# Ch01_Setup_18_EnemyExplosionEffect

## 목표

Enemy가 다른 오브젝트와 충돌했을 때 폭발 이펙트를 생성하게 만든다.

이전 단계에서는 Enemy가 충돌하면 상대 오브젝트와 Enemy 자신이 사라졌다.  
이번 단계에서는 사라지기 전에 충돌 위치에 폭발 이펙트를 하나 생성해서, 적이 파괴되는 느낌을 더한다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `Assets/Scripts/Enemy.cs`를 수정한다.
2. Enemy가 사용할 폭발 이펙트 프리팹 참조 변수를 추가한다.
3. Enemy가 다른 오브젝트와 충돌했을 때 폭발 이펙트를 생성한다.
4. 생성된 폭발 이펙트를 Enemy 위치에 배치한다.
5. 기존처럼 충돌한 상대 오브젝트와 Enemy 자신은 제거한다.
6. Enemy 프리팹에 폭발 이펙트 프리팹을 연결한다.

## 중요

- 이번 작업은 Enemy 충돌 시 폭발 이펙트 생성만 만든다.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- UI는 만들지 마.
- 사운드는 만들지 마.
- EnemyManager는 수정하지 마.
- Bullet, PlayerMove, PlayerFire, DestroyZone 스크립트는 수정하지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### Enemy.cs

Enemy가 폭발 이펙트 프리팹을 외부에서 받을 수 있도록 변수를 추가한다.

```csharp
public GameObject explosionFactory;
```

충돌이 시작되면 폭발 이펙트를 생성한다.

```csharp
GameObject explosion = Instantiate(explosionFactory);
explosion.transform.position = transform.position;
```

그 뒤 기존처럼 충돌한 상대와 Enemy 자신을 제거한다.

```csharp
Destroy(collision.gameObject);
Destroy(gameObject);
```

## Unity Editor 설정

작업 후 Unity Editor에서 Enemy 프리팹을 열고 아래 내용을 확인한다.

- Enemy 스크립트에 `Explosion Factory` 필드가 보이는지 확인
- `Explosion Factory`에 CFXR 폭발 이펙트 프리팹을 연결한다
- 예시 후보:
  - `CFXR Explosion 1`
  - `CFXR2 WW Explosion`
  - `CFXR3 Fire Explosion B`

수업에서는 눈에 잘 보이고 너무 복잡하지 않은 이펙트 하나를 선택한다.

## Play 모드 확인

Play 모드에서 아래 내용을 확인한다.

- Bullet이 Enemy와 충돌하면 폭발 이펙트가 생성되는지 확인
- 폭발 이펙트가 Enemy가 있던 위치에서 나타나는지 확인
- 충돌한 Bullet이 사라지는지 확인
- Enemy도 사라지는지 확인
- Console에 오류가 없는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- 파일 목록

주요 변경:
- Enemy 충돌 시 폭발 이펙트 생성
- Enemy 프리팹에 폭발 이펙트 프리팹 연결
- 기존 충돌 제거 로직 유지

Unity Editor에서 확인할 내용:
- Enemy 프리팹의 Explosion Factory 연결 확인
- Play 모드에서 Enemy 충돌 시 폭발 이펙트 생성 확인

아직 구현하지 않은 내용:
- 점수
- 게임 오버
- UI
- 사운드
- 이펙트 자동 삭제 보정
```
