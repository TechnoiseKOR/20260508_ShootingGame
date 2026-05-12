# Ch01_Setup_07_EnemyCollision_REVIEW

## 리뷰 대상

이번 리뷰 대상은 적 충돌 처리 구현 패치다.

패치 요약:

- Enemy 오브젝트에 Rigidbody 추가
- Rigidbody의 Use Gravity 비활성화
- `Enemy.cs`에 `OnCollisionEnter` 추가
- 충돌한 상대 오브젝트와 Enemy 자신을 제거
- `OnCollisionStay`, `OnCollisionExit` 함수 추가

## 전체 평가

이번 작업은 총알과 적이 화면에 존재하는 상태에서 다음 단계로 적절하다.

이제 단순히 움직이는 오브젝트가 아니라,
서로 닿았을 때 사라지는 기본 충돌 흐름을 확인할 수 있다.

아직 점수나 체력 같은 결과 처리는 없지만,
처음 충돌을 배우는 단계로는 충분하다.

## 좋은 점

### 1. Unity 3D 충돌 흐름을 사용했다

이번 프로젝트는 3D 공간을 기준으로 만드는 슈팅 게임이다.

따라서 `OnCollisionEnter(Collision collision)`을 사용한 점이 프로젝트 방향과 맞다.

### 2. Enemy에 Rigidbody를 추가했다

Unity의 3D 충돌 이벤트를 받기 위해 Enemy에 Rigidbody를 추가했다.

또한 Use Gravity를 꺼서 Enemy가 물리 중력에 의해 떨어지지 않고,
기존 Enemy 이동 코드로 아래로 이동하게 유지한 점이 적절하다.

### 3. 코드가 단순하다

충돌 시 상대와 자신을 제거하는 코드가 매우 단순하다.

```csharp
Destroy(collision.gameObject);
Destroy(gameObject);
```

초급자는 충돌 이벤트가 언제 호출되는지 확인하기 쉽다.

### 4. 충돌 함수 종류를 설명하기 좋다

`OnCollisionEnter`, `OnCollisionStay`, `OnCollisionExit`가 함께 들어가 있어
충돌 시작, 충돌 중, 충돌 끝의 차이를 설명하기 좋다.

## 확인이 필요한 점

### 1. 충돌 확인

Play 모드에서 다음을 확인한다.

- Bullet과 Enemy가 닿으면 둘 다 사라지는지
- Enemy와 Player가 닿으면 둘 다 사라지는지
- Console에 오류가 없는지

### 2. Rigidbody 설정 확인

Enemy의 Rigidbody에서 다음을 확인한다.

- Use Gravity가 꺼져 있는지
- Enemy가 중력으로 떨어지지 않는지
- Enemy가 기존처럼 아래로 이동하는지

### 3. Collider 설정 확인

Enemy와 충돌 대상 모두 Collider가 있어야 한다.

확인할 대상:

- Enemy의 BoxCollider
- Bullet 프리팹의 BoxCollider
- Player의 BoxCollider

## 개선하면 좋은 점

### 1. 충돌 대상을 구분하기

현재 Enemy는 어떤 오브젝트와 충돌해도 상대를 제거한다.

처음에는 단순해서 좋지만,
곧 다음과 같이 나누는 것이 필요해진다.

- Bullet과 충돌하면 Enemy와 Bullet 제거
- Player와 충돌하면 Player 피해 또는 게임 오버
- 다른 오브젝트와 충돌하면 무시

이를 위해 나중에 Tag 또는 Layer를 사용할 수 있다.

### 2. 점수 연결

총알이 적을 맞췄을 때 점수를 올리려면
충돌 대상이 Bullet인지 확인한 뒤 점수 처리와 연결해야 한다.

지금은 점수 없이 제거만 하는 상태다.

### 3. 게임 오버 연결

Enemy가 Player와 충돌했을 때는
단순히 Player를 Destroy하는 것보다 게임 오버 상태로 전환하는 것이 좋다.

이 기능은 나중에 GameManager를 만들 때 연결하면 된다.

### 4. Rigidbody 이동 방식 검토

Enemy는 Rigidbody를 갖고 있지만 이동은 Transform으로 처리하고 있다.

초반 수업에서는 괜찮지만,
나중에 물리 충돌이 어색해지면 Rigidbody 이동 방식으로 정리할 수 있다.

## 다음 단계 제안

다음 단계는 충돌 대상을 구분하는 작업이 적절하다.

추천 다음 작업:

```text
Ch01_Setup_08_CollisionTargetCheck
```

내용:

- Bullet과 Enemy 충돌 구분
- Player와 Enemy 충돌 구분
- 태그 또는 이름을 이용한 기본 구분

또는 수업 흐름에 따라 점수 표시로 넘어갈 수도 있다.

```text
Ch01_Setup_08_ScoreBasic
```

다만 점수를 넣기 전에
Bullet과 Player 충돌 처리를 구분하는 것이 더 안전하다.

## 최종 결론

이번 패치는 적 충돌 처리의 첫 단계로 적절하다.

Enemy가 충돌 이벤트를 받도록 Rigidbody를 추가했고,
충돌 시 상대와 자신을 제거하는 기본 동작을 구현했다.

다음 단계에서는 충돌 대상을 구분해서
총알 충돌, 플레이어 충돌, 점수, 게임 오버로 나누어 가면 좋다.
