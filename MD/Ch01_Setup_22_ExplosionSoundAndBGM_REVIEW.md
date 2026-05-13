# Ch01_Setup_22_ExplosionSoundAndBGM_REVIEW

## 리뷰 대상

이번 리뷰 대상은 적 폭발 사운드와 BGM 추가 패치다.

패치 요약:

- `CFXR3 Fire Explosion B.prefab`에 AudioSource 추가
- 폭발 이펙트 프리팹에 `Audio_Explosion.wav` 연결
- `SampleScene`의 Background 오브젝트에 AudioSource 추가
- Background AudioSource에 `Audio_BGM.wav` 연결
- Background AudioSource Loop 설정
- `Mat_Background.mat` 텍스처 오프셋 변경
- CFXR 프리팹 직렬화 값 일부 갱신

## 전체 평가

이번 작업은 이전 총알 발사 사운드 단계에 이어, 게임 전체의 청각 피드백을 확장한 단계다.

이전까지는 총알 발사음만 들렸고, 적이 폭발할 때는 시각 효과만 있었다.  
이번 변경으로 적이 폭발할 때 폭발 사운드가 들리고, 씬이 시작되면 BGM도 재생된다.

게임 느낌이 확실히 좋아지는 단계다.

## 좋은 점

### 1. 기존 코드 수정 없이 사운드를 연결했다

이번 패치에서는 C# 스크립트가 수정되지 않았다.

대신 프리팹과 씬 오브젝트의 AudioSource 설정으로 사운드를 추가했다.

초급 수업에서는 이 방식이 매우 이해하기 쉽다.

### 2. 폭발 이펙트와 폭발 사운드를 같은 프리팹에 묶었다

`CFXR3 Fire Explosion B.prefab`에 AudioSource를 추가했다.

이 구조에서는 Enemy가 폭발 이펙트를 생성하면, 그 이펙트가 스스로 폭발 사운드도 재생한다.

즉, 다음 흐름이 자연스럽다.

```text
Enemy 충돌
→ 폭발 이펙트 생성
→ 이펙트가 보임
→ 이펙트 사운드도 들림
```

### 3. BGM이 씬 시작과 함께 자동 재생된다

Background 오브젝트에 AudioSource를 붙이고 `Play On Awake`, `Loop`를 켰다.

덕분에 별도의 코드 없이도 게임 시작 시 BGM이 반복 재생된다.

### 4. 이전 사운드 리소스 추가 단계와 잘 이어진다

이전 단계에서 `Audio_BGM.wav`, `Audio_Bullet.wav`, `Audio_Explosion.wav`를 추가했다.

이번 단계에서 그중 `Audio_BGM.wav`와 `Audio_Explosion.wav`를 실제 게임에 연결했다.

흐름이 자연스럽다.

## 확인이 필요한 점

### 1. 폭발 사운드 확인

Play 모드에서 Enemy가 Bullet과 충돌할 때 다음을 확인해야 한다.

- 폭발 이펙트가 보이는지
- 폭발 사운드가 들리는지
- 폭발 사운드가 너무 크거나 작지 않은지
- 사운드가 중간에 끊기지 않는지

### 2. BGM 확인

Play 모드에서 다음을 확인한다.

- 씬 시작 시 BGM이 재생되는지
- BGM이 반복되는지
- BGM 볼륨이 너무 크지 않은지
- 총알 발사음과 폭발음이 BGM에 묻히지 않는지

### 3. CFXR 프리팹 저장 변경 확인

이번 패치에는 AudioSource 추가 외에도 CFXR 프리팹의 직렬화 값이 많이 바뀌었다.

Unity 버전 차이 또는 URP 관련 자동 갱신으로 보인다.

기능상 문제가 없는지 Unity Editor에서 프리팹을 열어 확인하면 좋다.

### 4. Mat_Background 변경 확인

`Mat_Background.mat`의 오프셋 값이 변경되었다.

```text
Offset.y: 1.174033
```

이 변경은 사운드 작업과 직접 관련이 없을 수 있다.

배경 스크롤 테스트 후 머티리얼 값이 저장된 것이라면, 필요한 변경인지 확인하는 것이 좋다.

## 개선하면 좋은 점

### 1. BGM 볼륨 낮추기

BGM은 효과음보다 뒤에 깔리는 소리라 보통 볼륨을 낮추는 편이 좋다.

예:

```text
BGM Volume: 0.2 ~ 0.5
Effect Volume: 0.6 ~ 1
```

### 2. 폭발 사운드 볼륨 조정

Enemy가 여러 마리 동시에 폭발하면 사운드가 겹칠 수 있다.

폭발 사운드가 너무 강하면 AudioSource Volume을 낮추는 것이 좋다.

### 3. BGM 전용 오브젝트 분리

현재 BGM AudioSource는 Background 오브젝트에 붙어 있다.

초급 단계에서는 괜찮지만, 나중에는 별도 오브젝트로 나누면 역할이 더 명확하다.

예:

```text
BGM
└─ AudioSource
```

또는 나중에 다음 구조도 가능하다.

```text
AudioManager
```

### 4. 원본 에셋 프리팹 직접 수정 대신 복제본 사용

현재 `CFXR3 Fire Explosion B.prefab` 원본에 AudioSource를 추가했다.

나중에 에셋 원본을 보존하려면 프로젝트용 복제 프리팹을 만드는 것이 더 안전하다.

예:

```text
Assets/Prefabs/EnemyExplosion.prefab
```

### 5. Mat_Background 오프셋 초기화 검토

배경 머티리얼의 오프셋이 저장되면 씬 시작 시 배경 시작 위치가 이전과 달라질 수 있다.

다음 정리 단계에서 오프셋을 0으로 맞출지 결정하면 좋다.

## 다음 단계 제안

이제 기본적인 시각/청각 피드백이 꽤 갖춰졌다.

다음 단계는 게임 규칙 추가가 자연스럽다.

추천 다음 작업:

```text
Ch01_Setup_23_ScoreBasic
```

내용:

- Enemy가 Bullet에 맞으면 점수 증가
- 점수를 화면에 표시하거나 Console로 확인
- 충돌 대상이 Bullet인지 구분하는 기본 조건 추가

또는 사운드 구조를 조금 정리하는 단계도 가능하다.

```text
Ch01_Setup_23_AudioCleanup
```

내용:

- BGM 볼륨 조정
- 폭발 사운드 볼륨 조정
- Mat_Background 오프셋 초기화
- 필요하면 BGM 전용 오브젝트 생성

수업 흐름상은 점수 기능으로 넘어가는 편이 더 자연스럽다.

## 최종 결론

이번 패치는 폭발 사운드와 BGM을 추가해 게임 완성도를 높인 단계다.

기존 스크립트 수정 없이 AudioSource 설정으로 구현했기 때문에 수업용으로 단순하고 안전하다.

다음 단계에서는 점수 기능을 추가해 게임 규칙을 만들어 가면 좋다.
