# Ch01_Setup_15_ImportMissileAssets_REVIEW

## 리뷰 대상

이번 리뷰 대상은 미사일/로켓 에셋 import 커밋이다.

커밋 요약:

- `Assets/BTM_Assets/BTM_Rockets_Missiles_Bombs` 폴더 추가
- 로켓/미사일 문서, 머티리얼, 모델, 프리팹, 텍스처 추가
- 에셋 예제 씬과 예제 스크립트 추가
- 기존 게임 씬과 게임 스크립트 수정 없음

## 전체 평가

이번 작업은 Bullet 외형 변경을 위한 준비 단계로 적절하다.

현재 게임의 Bullet은 기본 오브젝트 형태로 동작하고 있다.  
이번 미사일 에셋을 추가했으므로, 다음 단계에서 Bullet을 더 슈팅 게임다운 미사일 외형으로 바꿀 수 있다.

중요한 점은 이번 단계에서 기존 Bullet 프리팹이나 게임 로직을 건드리지 않았다는 것이다.

에셋 import와 외형 교체를 분리했기 때문에 문제가 생겼을 때 원인을 찾기 쉽다.

## 좋은 점

### 1. 기존 게임 로직을 수정하지 않았다

이번 단계에서는 에셋만 추가했고, 기존 게임 씬이나 스크립트를 수정하지 않았다.

이 방식은 안전하다.

에셋 import 후 Unity가 정상적으로 import 하는지 먼저 확인하고,
그 다음 단계에서 실제 게임 오브젝트에 적용하는 흐름이 좋다.

### 2. Bullet 외형 후보가 많다

`RMB_01`부터 `RMB_41`까지 다양한 모델이 포함되어 있다.

또한 색상별 프리팹 폴더가 있기 때문에,
게임 분위기와 구분성에 맞는 미사일을 선택할 수 있다.

예:

```text
Blue/RMB_01.prefab
Red/RMB_01.prefab
Yellow/RMB_01.prefab
```

### 3. 색상 팔레트가 분리되어 있다

Blue, Green, Grey, Orange, Pink, Red, Yellow 계열의 머티리얼과 텍스처가 포함되어 있다.

나중에 플레이어 총알과 적 총알을 색상으로 구분할 때 활용할 수 있다.

### 4. 다음 단계로 이어가기 좋다

이번 에셋은 다음 단계에서 바로 Bullet 외형 교체에 사용할 수 있다.

예상 흐름:

```text
기존 Bullet 부모 오브젝트는 유지
└─ 미사일 프리팹을 자식 외형으로 추가
```

이렇게 하면 기존 `Bullet.cs`, Collider, Layer는 유지하면서 외형만 바꿀 수 있다.

## 확인이 필요한 점

### 1. Unity import 오류 확인

Unity Editor에서 Console을 확인해야 한다.

확인할 내용:

- FBX import 오류가 없는지
- 프리팹 Missing reference가 없는지
- 머티리얼과 텍스처 연결이 깨지지 않았는지
- `RocketRotator.cs`에서 컴파일 오류가 없는지

### 2. 프리팹 미리보기 확인

색상별 `Prefabs` 폴더에서 몇 개의 RMB 프리팹을 클릭해 정상적으로 보이는지 확인한다.

추천 확인 대상:

```text
Prefabs/Blue/RMB_01.prefab
Prefabs/Red/RMB_01.prefab
Prefabs/Yellow/RMB_01.prefab
```

### 3. 기존 게임 씬 유지 확인

이번 커밋에서는 기존 게임 씬을 수정하지 않는 것이 맞다.

따라서 `SampleScene`에서 기존 Player, Bullet, Enemy, EnemyManager, DestroyZone이 그대로 동작하는지 확인하면 좋다.

### 4. 예제 씬과 작업 씬 구분

에셋에는 `DemoScene_RMB.unity` 예제 씬이 포함되어 있다.

이 씬은 에셋 확인용이고, 현재 수업 프로젝트의 작업 씬은 기존 `SampleScene`이다.

학생들이 예제 씬에서 실수로 작업하지 않도록 안내하면 좋다.

## 주의할 점

### 1. 에셋 예제 스크립트와 수업용 스크립트 구분

에셋에는 다음 스크립트가 포함되어 있다.

```text
RocketRotator.cs
```

이 스크립트는 에셋 예제용일 가능성이 높다.

현재 수업용 Bullet 이동 로직과 섞지 않는 것이 좋다.

### 2. 모델 방향 확인 필요

미사일 모델은 기본 방향이 현재 총알 이동 방향과 다를 수 있다.

다음 단계에서 Bullet 외형으로 사용할 때는 다음을 확인해야 한다.

- 미사일 앞쪽이 위쪽 이동 방향을 향하는지
- 카메라에서 잘 보이는 회전값인지
- 크기가 현재 Bullet Collider와 어울리는지
- 너무 크거나 작지 않은지

### 3. Collider는 기존 Bullet 기준 유지 권장

미사일 프리팹에 자체 Collider가 있을 수도 있지만,
초급 수업에서는 기존 Bullet 프리팹의 Collider를 유지하고 외형만 자식으로 붙이는 방식이 더 안전하다.

이 방식이면 기존 충돌, 레이어, DestroyZone 동작이 유지된다.

### 4. 에셋 용량 관리

FBX와 색상별 프리팹이 많이 포함되어 있으므로 파일 수와 용량이 커질 수 있다.

이번처럼 에셋 import 커밋은 patch 전체를 공유하지 않고 파일 목록 중심으로 기록하는 방식이 적절하다.

## 다음 단계 제안

다음 단계는 미사일 에셋을 Bullet 외형으로 적용하는 작업이 자연스럽다.

추천 다음 작업:

```text
Ch01_Setup_16_BulletVisualMissile
```

내용:

- Bullet 프리팹의 기본 큐브 외형 제거
- 선택한 RMB 프리팹을 Bullet의 자식 외형으로 추가
- Bullet Collider와 Layer 유지
- 미사일 크기와 회전 조정
- Play 모드에서 발사, 이동, 충돌 확인

그 다음 단계에서는 점수 기능으로 넘어갈 수 있다.

```text
Ch01_Setup_17_ScoreBasic
```

## 최종 결론

이번 커밋은 Bullet 외형을 미사일로 바꾸기 위한 리소스 준비 단계로 적절하다.

기존 게임 기능을 수정하지 않고 에셋만 import했기 때문에 안전하고,
다음 단계에서 Bullet 프리팹 외형을 교체하기 위한 준비가 되었다.
