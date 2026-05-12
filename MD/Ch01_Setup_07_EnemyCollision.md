# Ch01_Setup_07_EnemyCollision

## 목표

적이 다른 오브젝트와 충돌했을 때 서로 사라지게 만든다.

이번 단계에서는 적 충돌 처리의 가장 기본 구조만 만든다.  
점수, 체력, 게임 오버, 이펙트, 사운드는 아직 만들지 않는다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `Enemy` 오브젝트에 3D 물리 충돌을 위한 `Rigidbody`를 추가한다.
2. `Enemy`의 Rigidbody는 중력의 영향을 받지 않도록 설정한다.
3. `Enemy.cs`에 충돌 처리 함수를 추가한다.
4. Enemy가 다른 오브젝트와 충돌하면 상대 오브젝트를 제거한다.
5. Enemy 자신도 제거한다.

## 중요

- 이번 작업은 적 충돌 처리만 만든다.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- 체력 기능은 만들지 마.
- 이펙트나 사운드는 만들지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### Enemy 오브젝트 설정

`Enemy` 오브젝트는 충돌 이벤트를 받을 수 있어야 한다.

필요한 컴포넌트:

- BoxCollider
- Rigidbody
- Enemy 스크립트

Rigidbody 설정:

- Use Gravity는 꺼둔다.
- 적이 중력으로 떨어지는 것이 아니라, Enemy 스크립트의 이동 코드로 아래로 내려오게 한다.

### Enemy.cs 충돌 처리

`Enemy.cs`에는 충돌이 시작될 때 호출되는 함수를 추가한다.

예상 코드 흐름:

```csharp
private void OnCollisionEnter(Collision collision)
{
    Destroy(collision.gameObject);
    Destroy(gameObject);
}
```

이 코드는 Enemy가 어떤 오브젝트와 충돌하면,
충돌한 상대 오브젝트와 Enemy 자신을 모두 제거한다.

## Unity Editor 설정

작업 후 Unity Editor에서 다음을 확인한다.

- Enemy 오브젝트에 BoxCollider가 있는지 확인
- Enemy 오브젝트에 Rigidbody가 있는지 확인
- Rigidbody의 Use Gravity가 꺼져 있는지 확인
- Enemy 오브젝트에 Enemy 스크립트가 붙어 있는지 확인

## Play 모드 확인

Play 모드에서 다음을 확인한다.

- Enemy가 아래로 이동하는지 확인
- Enemy가 다른 오브젝트와 충돌했을 때 사라지는지 확인
- 충돌한 상대 오브젝트도 사라지는지 확인
- Console에 오류가 없는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- 파일 목록

수정한 오브젝트:
- 오브젝트 이름

Unity Editor에서 확인할 내용:
- Enemy에 Rigidbody가 있는지 확인
- Use Gravity가 꺼져 있는지 확인
- Play 모드에서 충돌 시 Enemy와 상대 오브젝트가 사라지는지 확인

아직 구현하지 않은 내용:
- 점수
- 체력
- 게임 오버
- 이펙트
- 사운드
```
