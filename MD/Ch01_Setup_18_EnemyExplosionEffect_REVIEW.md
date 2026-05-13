# Ch01_Setup_18_EnemyExplosionEffect_REVIEW

## 리뷰 대상

이번 리뷰 대상은 Enemy 충돌 시 폭발 이펙트 생성 패치다.

패치 요약:

- `Enemy.cs`에 `explosionFactory` 필드 추가
- `OnCollisionEnter`에서 폭발 이펙트 프리팹 생성
- 생성된 폭발 이펙트를 Enemy 위치에 배치
- Enemy 프리팹에 폭발 이펙트 프리팹 연결
- 기존 충돌 제거 로직 유지

## 전체 평가

이번 작업은 이펙트 에셋 import 이후 자연스러운 기능 연결 단계다.

이전까지는 Bullet과 Enemy가 충돌하면 둘 다 사라지기만 했다.  
이번 변경으로 충돌 위치에 폭발 이펙트가 나타나므로, 적이 파괴되는 느낌이 훨씬 좋아진다.

코드도 기존 `OnCollisionEnter` 흐름에 이펙트 생성만 추가한 형태라 초급자가 이해하기 좋다.

## 좋은 점

### 1. 기존 충돌 흐름을 유지했다

이번 패치는 기존 충돌 처리 구조를 크게 바꾸지 않았다.

기존 흐름:

```text
충돌 발생 → 상대 제거 → Enemy 제거
```

변경 후 흐름:

```text
충돌 발생 → 폭발 생성 → 상대 제거 → Enemy 제거
```

기존 동작을 유지하면서 시각적 연출만 추가한 점이 좋다.

### 2. 프리팹 참조를 Inspector에서 받는다

`explosionFactory`를 public 필드로 두어 Unity Editor에서 이펙트 프리팹을 연결할 수 있게 했다.

이 방식은 초급자가 이해하기 쉽다.

- 코드에서는 생성만 담당
- 어떤 이펙트를 쓸지는 Inspector에서 연결

이 구조는 이전의 `PlayerFire`에서 `bulletFactory`를 연결했던 방식과도 비슷하다.

### 3. Enemy 프리팹에 연결했다

EnemyManager는 Enemy 프리팹을 계속 생성한다.

따라서 씬에 있는 개별 Enemy가 아니라 Enemy 프리팹에 `explosionFactory`를 연결한 것이 적절하다.

앞으로 생성되는 Enemy들도 모두 폭발 이펙트 참조를 가진다.

### 4. 시각적 피드백이 좋아졌다

이제 Bullet이 Enemy를 맞췄을 때 단순히 사라지는 것이 아니라 폭발 이펙트가 발생한다.

게임 느낌이 확실히 좋아지는 단계다.

## 확인이 필요한 점

### 1. Explosion Factory 연결 확인

Enemy 프리팹의 Inspector에서 `Explosion Factory`가 비어 있지 않은지 확인해야 한다.

비어 있으면 `Instantiate(explosionFactory)`에서 오류가 날 수 있다.

### 2. Play 모드 폭발 확인

Play 모드에서 다음을 확인한다.

- Bullet이 Enemy와 충돌하면 폭발이 나타나는지
- 폭발 위치가 Enemy 위치와 맞는지
- Bullet과 Enemy가 기존처럼 사라지는지
- Console에 오류가 없는지

### 3. 이펙트 자동 삭제 확인

CFXR 이펙트가 재생 후 자동으로 사라지는지 확인해야 한다.

만약 사라지지 않으면 게임 중 이펙트 오브젝트가 계속 쌓일 수 있다.

### 4. Player와 충돌했을 때도 폭발하는지 확인

현재 Enemy는 충돌 대상을 구분하지 않는다.

따라서 Player와 충돌해도 폭발 이펙트가 생성될 수 있다.

이 동작이 현재 수업 단계에서 의도된 것인지 확인하면 좋다.

## 개선하면 좋은 점

### 1. Null 체크 추가

현재는 `explosionFactory` 연결이 반드시 되어 있다고 가정한다.

나중에 더 안전하게 만들려면 null 체크를 추가할 수 있다.

```csharp
if (explosionFactory != null)
{
    GameObject explosion = Instantiate(explosionFactory);
    explosion.transform.position = transform.position;
}
```

다만 초급 수업에서는 Inspector 연결을 확인하는 방식으로 진행해도 괜찮다.

### 2. 충돌 대상 구분

현재는 어떤 오브젝트와 충돌해도 상대와 Enemy를 제거하고 폭발한다.

이후에는 다음처럼 나누면 좋다.

- Bullet과 충돌하면 폭발 + 점수 증가
- Player와 충돌하면 폭발 + 게임 오버
- DestroyZone과 관련된 삭제는 DestroyZone에서 처리

### 3. Instantiate 위치 지정 방식 개선

현재는 생성한 뒤 위치를 옮긴다.

```csharp
GameObject explosion = Instantiate(explosionFactory);
explosion.transform.position = transform.position;
```

나중에는 생성할 때 위치를 바로 넣을 수 있다.

```csharp
Instantiate(explosionFactory, transform.position, Quaternion.identity);
```

다만 현재 방식은 단계별로 이해하기 쉬워서 수업용으로 적절하다.

### 4. 이펙트 크기 조정

선택한 CFXR 이펙트가 너무 크거나 작을 수 있다.

필요하면 이펙트 프리팹의 Scale을 조정하거나, 별도 게임용 이펙트 프리팹으로 복제해서 사용하는 것이 좋다.

## 다음 단계 제안

다음 단계는 점수 기능으로 넘어가는 것이 자연스럽다.

추천 다음 작업:

```text
Ch01_Setup_19_ScoreBasic
```

내용:

- Bullet이 Enemy를 맞추면 점수 증가
- Console 또는 간단한 UI로 점수 확인
- 충돌 대상이 Bullet인지 구분하는 기본 조건 추가

또는 먼저 충돌 대상을 구분하는 단계로 나눌 수도 있다.

```text
Ch01_Setup_19_CollisionTargetCheck
```

내용:

- Bullet과 Enemy 충돌 처리 구분
- Player와 Enemy 충돌 처리 구분
- 이후 점수와 게임 오버로 연결

수업 흐름상 점수를 넣기 전에 충돌 대상 구분을 먼저 다루는 것이 더 안전하다.

## 최종 결론

이번 패치는 Enemy 충돌에 폭발 이펙트를 추가한 좋은 시각적 개선 단계다.

기존 충돌 구조를 유지하면서 이펙트만 추가했기 때문에 이해하기 쉽고, 에셋 import 단계와 자연스럽게 연결된다.

다음 단계에서는 충돌 대상을 구분한 뒤 점수 기능으로 이어가면 좋다.
