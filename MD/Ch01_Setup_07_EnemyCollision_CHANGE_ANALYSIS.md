# Ch01_Setup_07_EnemyCollision_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 적 오브젝트에 충돌 처리가 추가되었다.

주요 변경은 다음과 같다.

1. `Enemy` 오브젝트에 `Rigidbody` 컴포넌트 추가
2. `Rigidbody`의 중력 사용 비활성화
3. `Enemy.cs`에 `OnCollisionEnter` 함수 추가
4. 충돌한 상대 오브젝트 제거
5. Enemy 자신 제거
6. `OnCollisionStay`, `OnCollisionExit` 함수 추가

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Scenes/SampleScene.unity` | Enemy 오브젝트에 Rigidbody 컴포넌트 추가 |
| `Assets/Scripts/Enemy.cs` | 충돌 처리 함수 추가 |

## 씬 변경 분석

`SampleScene.unity`에서 `Enemy` 오브젝트에 `Rigidbody` 컴포넌트가 추가되었다.

패치에서 확인되는 주요 Rigidbody 설정은 다음과 같다.

```text
Use Gravity: 0
Is Kinematic: 0
Mass: 1
```

`Use Gravity`가 꺼져 있으므로 Enemy는 중력 때문에 떨어지지 않는다.

Enemy는 기존처럼 `Enemy.cs`의 이동 코드에 의해 아래쪽으로 이동한다.

```csharp
Vector3 dir = Vector3.down;
transform.position += dir * speed * Time.deltaTime;
```

## Enemy.cs 변경 분석

`Enemy.cs`에 충돌 함수가 추가되었다.

핵심 코드는 다음과 같다.

```csharp
private void OnCollisionEnter(Collision collision)
{
   Destroy(collision.gameObject);
   Destroy(gameObject);
}
```

이 코드는 Enemy가 다른 오브젝트와 충돌을 시작했을 때 실행된다.

실행 순서는 다음과 같다.

1. 충돌한 상대 오브젝트를 제거한다.
2. Enemy 자기 자신도 제거한다.

즉, Enemy가 총알이나 플레이어 등 다른 오브젝트와 닿으면
둘 다 사라지는 구조다.

## 추가된 충돌 함수

이번 패치에는 아래 함수도 함께 추가되었다.

```csharp
private void OnCollisionStay(Collision collision)
{
    // 충돌 중
}

private void OnCollisionExit(Collision collision)
{
    // 충돌 끝
}
```

현재 두 함수에는 실제 동작이 없다.

수업에서는 충돌 이벤트의 종류를 설명하기 위한 용도로 사용할 수 있다.

| 함수 | 의미 |
|---|---|
| `OnCollisionEnter` | 충돌이 시작될 때 |
| `OnCollisionStay` | 충돌이 계속되는 동안 |
| `OnCollisionExit` | 충돌이 끝날 때 |

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- Enemy가 충돌 이벤트를 받을 수 있게 Rigidbody 추가
- Enemy가 다른 오브젝트와 충돌하면 상대 오브젝트 제거
- Enemy 자신도 제거
- Unity 3D 물리 충돌 함수 사용

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 충돌 대상 구분
- 총알과 적 충돌만 따로 처리
- 플레이어와 적 충돌만 따로 처리
- 점수 증가
- 체력 감소
- 게임 오버
- 충돌 이펙트
- 충돌 사운드

현재 단계에서는 충돌의 기본 동작을 확인하는 것이 목적이다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. Enemy 오브젝트에 `Rigidbody`가 추가되었는지 확인
2. Rigidbody의 `Use Gravity`가 꺼져 있는지 확인
3. Enemy 오브젝트에 `BoxCollider`가 있는지 확인
4. Play 모드에서 Enemy가 다른 오브젝트와 충돌했을 때 사라지는지 확인
5. 충돌한 상대 오브젝트도 같이 사라지는지 확인
6. Console에 오류가 없는지 확인

## 주의할 점

### 1. 충돌 대상을 구분하지 않는다

현재 코드는 Enemy가 충돌한 상대가 누구인지 확인하지 않는다.

```csharp
Destroy(collision.gameObject);
```

따라서 Enemy가 Bullet과 부딪혀도 상대를 제거하고,
Player와 부딪혀도 상대를 제거한다.

초반 수업에서는 단순해서 좋지만,
나중에는 태그나 레이어를 사용해 충돌 대상을 구분하는 것이 좋다.

예:

```csharp
if (collision.gameObject.CompareTag("Bullet"))
{
    Destroy(collision.gameObject);
    Destroy(gameObject);
}
```

### 2. Bullet 쪽 Rigidbody 여부 확인 필요

Unity의 3D 충돌 이벤트는 일반적으로 충돌하는 오브젝트 중 하나 이상에 Rigidbody가 필요하다.

이번 패치에서는 Enemy에 Rigidbody가 추가되었기 때문에
Enemy 쪽에서 충돌 이벤트를 받을 수 있다.

다만 이후 프리팹이나 충돌 설정을 바꾸면
Rigidbody, Collider 설정을 다시 확인해야 한다.

### 3. Transform 이동과 Rigidbody

현재 Enemy는 Rigidbody를 가지고 있지만,
이동은 여전히 `transform.position`으로 처리한다.

초반 수업에서는 이해하기 쉬운 방식이다.

다만 나중에 물리 기반 움직임을 더 정확하게 만들려면
Rigidbody 이동 방식으로 정리할 수 있다.

### 4. 빈 OnCollisionStay / OnCollisionExit

현재 `OnCollisionStay`, `OnCollisionExit`는 설명용으로만 추가되어 있고 실제 기능은 없다.

이후 사용하지 않는다면 코드 정리 단계에서 제거할 수 있다.

## 결론

이번 패치는 적 충돌 처리의 첫 단계로 적절하다.

Enemy에 Rigidbody를 추가하고,
`OnCollisionEnter`에서 충돌한 상대와 자기 자신을 제거하면서
Unity 3D 충돌 이벤트의 기본 흐름을 확인할 수 있다.

다음 단계에서는 충돌 대상을 구분하거나,
점수와 게임 오버 같은 결과 처리를 연결하면 좋다.
