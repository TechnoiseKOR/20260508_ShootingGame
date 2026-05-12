# Ch01_Setup_13_ImportPlaneAssets_REVIEW

## 리뷰 대상

이번 리뷰 대상은 비행기 에셋 import 커밋이다.

커밋 요약:

- `Assets/AwesomeCartoonPlanes` 폴더 추가
- 비행기 이미지, 머티리얼, 모델, 프리팹 추가
- 에셋 예제 씬과 예제 스크립트 추가
- 기존 게임 씬과 게임 스크립트 수정 없음

## 전체 평가

이번 작업은 외형 리소스를 추가하는 준비 단계로 적절하다.

지금까지 프로젝트는 기본 큐브 형태의 Player, Bullet, Enemy를 사용하고 있었다.  
이제 비행기 프리팹을 가져왔으므로, 다음 단계에서 Player나 Enemy의 외형을 더 게임처럼 바꿀 수 있다.

## 좋은 점

### 1. 기존 게임 로직을 건드리지 않았다

이번 단계에서는 에셋만 추가했고, 기존 게임 씬이나 스크립트를 수정하지 않았다.

이 방식은 안전하다.

에셋 import와 게임 오브젝트 교체를 한 번에 처리하면 문제가 생겼을 때 원인을 찾기 어렵다.  
이번처럼 에셋 추가만 먼저 하는 것이 좋다.

### 2. 에셋 구조가 분리되어 있다

에셋은 다음 폴더 아래에 모여 있다.

```text
Assets/AwesomeCartoonPlanes
```

기존 수업용 `Assets/Scripts`, `Assets/Prefabs`와 섞이지 않아서 관리하기 쉽다.

### 3. 사용할 후보 프리팹이 여러 개 있다

추가된 프리팹은 다음과 같다.

```text
Plane1.prefab
Plane2.prefab
Plane3.prefab
Plane4.prefab
```

플레이어용, 적용, 배경 장식용 등으로 나누어 사용할 수 있다.

### 4. 다음 수업 단계로 이어가기 좋다

이제 다음 단계에서 아래 작업을 진행할 수 있다.

- Player 외형을 Plane 프리팹으로 교체
- Enemy 외형을 다른 Plane 프리팹으로 교체
- 기존 충돌 박스와 이동 스크립트 유지
- 모델 방향과 크기 조정

## 확인이 필요한 점

### 1. Unity import 오류 확인

Unity Editor에서 Console을 확인해야 한다.

확인할 내용:

- PSD import 오류가 없는지
- FBX import 오류가 없는지
- 머티리얼 연결이 깨지지 않았는지
- 예제 스크립트 컴파일 오류가 없는지

### 2. 프리팹 미리보기 확인

`Assets/AwesomeCartoonPlanes/Prefabs` 안의 프리팹을 클릭해서 정상적으로 보이는지 확인한다.

확인할 프리팹:

```text
Plane1.prefab
Plane2.prefab
Plane3.prefab
Plane4.prefab
```

### 3. 기존 게임 씬 유지 확인

이번 커밋에서는 기존 게임 씬을 수정하지 않는 것이 맞다.

따라서 `SampleScene`에서 기존 Player, Bullet, Enemy, EnemyManager, DestroyZone이 그대로 동작하는지 확인하면 좋다.

### 4. 예제 씬과 작업 씬 구분

에셋에는 `Planes.unity` 예제 씬이 포함되어 있다.

이 씬은 에셋 확인용이고, 현재 수업 프로젝트의 작업 씬은 기존 `SampleScene`이다.

학생들이 예제 씬에서 실수로 작업하지 않도록 안내하면 좋다.

## 주의할 점

### 1. 에셋 예제 스크립트와 수업용 스크립트 구분

에셋에는 다음 스크립트가 포함되어 있다.

```text
Plane.cs
RotateCamera.cs
```

이 스크립트들은 에셋 예제용일 가능성이 높다.

현재 수업용 이동/발사/충돌 로직과 섞지 않는 것이 좋다.

### 2. 모델 방향 확인 필요

3D 모델은 가져온 방향이 현재 게임 화면 방향과 다를 수 있다.

다음 단계에서 Player나 Enemy 외형으로 사용할 때는 다음을 확인해야 한다.

- 모델이 카메라를 향해 보이는지
- 비행기 앞쪽 방향이 총알 발사 방향과 맞는지
- 크기가 화면에 적절한지
- 회전값을 조정해야 하는지

### 3. Collider는 별도 확인 필요

비행기 프리팹에 Collider가 포함되어 있더라도, 현재 게임 충돌 구조와 맞지 않을 수 있다.

외형 교체 단계에서는 기존 Player/Enemy의 Collider 구조를 유지할지, 모델의 Collider를 사용할지 정해야 한다.

초급 수업에서는 기존 충돌 박스는 유지하고 외형만 바꾸는 방식이 더 안전하다.

### 4. 에셋 용량 관리

PSD와 FBX 파일이 포함되어 있으므로 커밋 용량이 커질 수 있다.

이번처럼 에셋 import 커밋은 patch 전체를 공유하기보다 파일 목록 중심으로 기록하는 방식이 적절하다.

## 다음 단계 제안

다음 단계는 비행기 에셋을 실제 게임 오브젝트 외형에 적용하는 작업이 자연스럽다.

추천 다음 작업:

```text
Ch01_Setup_14_PlayerVisualPlane
```

내용:

- Player의 외형을 비행기 프리팹 또는 모델로 교체
- 기존 PlayerMove, PlayerFire는 유지
- Collider와 Rigidbody 설정 확인
- 크기와 회전 조정

그다음 단계에서는 Enemy 외형을 교체할 수 있다.

```text
Ch01_Setup_15_EnemyVisualPlane
```

내용:

- Enemy 프리팹 외형을 비행기 모델로 교체
- 기존 Enemy 이동/충돌 스크립트 유지
- Enemy 크기와 회전 조정

## 최종 결론

이번 커밋은 비행기 외형 리소스를 프로젝트에 추가하는 단계로 적절하다.

기존 게임 기능을 수정하지 않고 에셋만 import했기 때문에 안전하고,
다음 단계에서 Player나 Enemy 외형을 교체하기 위한 준비가 되었다.
