# Ch01_Setup_08_EnemyManager_REVIEW

## 리뷰 대상

이번 리뷰 대상은 Enemy 프리팹 생성 및 EnemyManager 구현 패치다.

패치 요약:

- `Assets/Prefabs/Enemy.prefab` 추가
- 씬에 직접 배치되어 있던 Enemy 제거
- `Assets/Scripts/EnemyManager.cs` 추가
- 씬에 여러 EnemyManager 오브젝트 추가
- 각 EnemyManager가 일정 시간마다 Enemy 프리팹 생성

## 전체 평가

이번 작업은 적 이동과 충돌 처리 이후 다음 단계로 적절하다.

이전까지는 씬에 직접 배치된 Enemy 하나만 움직였다.  
이번 단계에서는 Enemy를 프리팹으로 만들고, EnemyManager가 일정 시간마다 생성하도록 바뀌었다.

이제 게임 화면에 적이 계속 등장하는 구조가 생겼기 때문에, 슈팅 게임의 기본 흐름에 더 가까워졌다.

## 좋은 점

### 1. Enemy를 프리팹으로 분리했다

Enemy는 앞으로 계속 생성될 오브젝트다.

따라서 씬에 직접 놓는 방식보다 프리팹으로 분리하는 것이 맞다.

이번 패치에서 `Assets/Prefabs/Enemy.prefab`을 만든 것은 적절하다.

### 2. 기존 Enemy 기능을 재사용했다

Enemy 프리팹은 기존 Enemy 스크립트와 Rigidbody, Collider를 유지한다.

따라서 생성된 Enemy도 아래로 이동하고, 충돌 시 사라지는 기존 동작을 그대로 사용할 수 있다.

### 3. EnemyManager 코드가 단순하다

`currentTime`을 누적하고, `createTime`이 지나면 Enemy를 생성하는 구조다.

초급자에게 시간 누적과 반복 생성 개념을 설명하기 좋다.

### 4. 여러 위치에서 적을 생성한다

EnemyManager를 여러 개 배치해서 다양한 위치에서 적이 나오도록 만들었다.

이 방식은 랜덤 생성보다 단순하고, 학생들이 씬에서 생성 위치를 눈으로 확인하기 쉽다.

## 확인이 필요한 점

### 1. EnemyManager 연결 확인

각 EnemyManager의 Inspector에서 다음을 확인해야 한다.

- `enemyFactory`에 `Enemy.prefab`이 연결되어 있는지
- `createTime` 값이 적절한지

연결이 빠져 있으면 Play 모드에서 적 생성이 실패할 수 있다.

### 2. Play 모드 적 생성 확인

Play 모드에서 다음을 확인한다.

- 일정 시간마다 Enemy가 생성되는지
- Enemy가 여러 위치에서 생성되는지
- 생성된 Enemy가 아래로 이동하는지
- 생성된 Enemy가 충돌 처리를 유지하는지

### 3. FirePosition 참조 확인

씬 diff에서 FirePosition 관련 내용도 다시 정리되어 있다.

PlayerFire가 참조하는 `firePosition`이 끊어지지 않았는지 확인해야 한다.

## 개선하면 좋은 점

### 1. EnemyManager 이름 정리

현재 씬에는 `EnemyManager`, `EnemyManager (1)`, `EnemyManager (2)`처럼 자동 복제 이름이 남아 있다.

수업용 프로젝트에서는 나중에 이름을 더 이해하기 쉽게 바꿔도 좋다.

예:

```text
EnemyManager_Center
EnemyManager_Left
EnemyManager_Right
```

다만 지금 단계에서는 기능 확인이 우선이므로 필수 수정은 아니다.

### 2. 적 자동 삭제 추가

EnemyManager가 적을 계속 생성하기 때문에, 시간이 지나면 생성된 Enemy가 계속 쌓일 수 있다.

다음 단계에서는 화면 밖으로 나간 Enemy를 삭제하는 기능을 추가하는 것이 좋다.

### 3. 생성 간격 조정

모든 EnemyManager의 `createTime`이 `1`이면 여러 적이 동시에 많이 생성될 수 있다.

나중에 난이도를 조절할 때는 생성 간격을 위치별로 다르게 하거나, 랜덤 요소를 추가할 수 있다.

### 4. 오브젝트 풀링은 아직 미루기

Enemy가 계속 생성되므로 나중에는 오브젝트 풀링이 필요할 수 있다.

하지만 지금은 초반 수업 단계이므로 `Instantiate`를 사용한 단순 구조가 적절하다.

## 다음 단계 제안

다음 단계는 생성된 오브젝트 정리 기능이 적절하다.

추천 다음 작업:

```text
Ch01_Setup_09_DestroyZone
```

내용:

- 화면 밖으로 나간 Bullet과 Enemy를 삭제하는 영역 만들기
- DestroyZone 오브젝트 추가
- 충돌 또는 트리거로 오브젝트 제거

또는 더 단순하게는 각 스크립트에서 위치를 보고 삭제하는 방식도 가능하다.

```text
Ch01_Setup_09_RemoveOutOfScreen
```

내용:

- Bullet이 위쪽 경계를 넘으면 삭제
- Enemy가 아래쪽 경계를 넘으면 삭제

수업 흐름상 DestroyZone을 만들면 Unity의 Trigger 개념을 설명하기 좋다.

## 최종 결론

이번 패치는 적 프리팹과 EnemyManager를 이용해 적을 반복 생성하는 단계로 적절하다.

이제 플레이어가 총알을 발사하고, 적이 여러 위치에서 내려오는 기본 슈팅 게임 흐름이 만들어졌다.

다음 단계에서는 화면 밖으로 나간 오브젝트를 삭제해서 게임 오브젝트가 계속 쌓이지 않도록 정리하는 것이 좋다.
