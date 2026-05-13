# Ch01_Setup_21_BulletFireSound_REVIEW

## 리뷰 대상

이번 리뷰 대상은 사운드 리소스 추가와 Bullet 발사 사운드 적용 패치다.

패치 요약:

- `Assets/CP03_Sound` 폴더 추가
- `Audio_BGM.wav` 추가
- `Audio_Bullet.wav` 추가
- `Audio_Explosion.wav` 추가
- `Bullet.prefab`에 `AudioSource` 추가
- `AudioSource`에 `Audio_Bullet.wav` 연결
- `Mat_Background.mat`의 텍스처 오프셋 값 변경

## 전체 평가

이번 작업은 게임에 청각 피드백을 추가하는 첫 단계로 적절하다.

지금까지는 Player가 총알을 발사해도 시각적으로만 확인할 수 있었다.  
이번 변경으로 총알이 생성될 때 발사 사운드가 들리게 되어 조작감이 좋아진다.

특히 C# 코드를 수정하지 않고 `Bullet` 프리팹에 `AudioSource`를 붙여 처리한 방식은 초급 수업에서 이해하기 쉽다.

## 좋은 점

### 1. 사운드 리소스를 별도 폴더로 분리했다

사운드 파일이 다음 폴더에 모여 있다.

```text
Assets/CP03_Sound
```

이미지, 모델, 스크립트와 섞이지 않아 관리하기 쉽다.

### 2. 앞으로 사용할 사운드도 함께 준비했다

이번 단계에서 실제 연결된 것은 `Audio_Bullet.wav`지만, 다음 리소스도 함께 추가되었다.

```text
Audio_BGM.wav
Audio_Explosion.wav
```

다음 단계에서 BGM과 폭발 사운드를 추가할 수 있는 준비가 되었다.

### 3. 기존 코드 수정 없이 발사 사운드를 적용했다

`Bullet.prefab`에 `AudioSource`를 붙이고 `Play On Awake`를 켜는 방식으로 구현했다.

덕분에 기존 `PlayerFire.cs`와 `Bullet.cs`를 수정하지 않아도 된다.

구조가 단순해서 학생들이 이해하기 좋다.

### 4. 기존 발사 흐름을 유지했다

기존 흐름은 그대로 유지된다.

```text
PlayerFire가 Bullet 생성
→ Bullet이 이동
```

사운드는 Bullet 생성 시 자동으로 재생된다.

```text
PlayerFire가 Bullet 생성
→ Bullet AudioSource 자동 재생
→ Bullet이 이동
```

## 확인이 필요한 점

### 1. AudioSource 설정 확인

`Bullet` 프리팹에서 다음을 확인해야 한다.

- Audio Clip: `Audio_Bullet.wav`
- Play On Awake: On
- Loop: Off
- Volume: 적절한지 확인

### 2. Play 모드 발사 확인

Play 모드에서 다음을 확인한다.

- Fire 입력으로 Bullet이 생성되는지
- Bullet 생성 시 발사 사운드가 들리는지
- 연속 발사 시 소리가 너무 크거나 지저분하지 않은지
- Console에 오류가 없는지

### 3. 총알 삭제와 사운드 재생 관계 확인

AudioSource가 Bullet에 붙어 있으므로 Bullet이 삭제되면 사운드도 함께 사라진다.

총알이 발사 직후 바로 충돌하거나 삭제되는 상황에서 소리가 끊기지 않는지 확인하면 좋다.

### 4. Mat_Background 변경 확인

이번 커밋에는 `Mat_Background.mat` 변경도 포함되어 있다.

변경 내용은 배경 텍스처 오프셋이다.

```text
m_Offset.y: 0 → 2.1780388
```

이 변경은 사운드 작업과 직접 관련이 없을 가능성이 높다.

Play 모드에서 배경 스크롤을 확인한 뒤 머티리얼이 저장되었을 수 있으므로, 의도한 변경인지 확인하는 것이 좋다.

## 개선하면 좋은 점

### 1. 발사 사운드 볼륨 조정

총알을 빠르게 발사하면 사운드가 여러 번 겹친다.

소리가 너무 크면 `AudioSource`의 Volume을 낮추면 좋다.

예:

```text
Volume: 0.3 ~ 0.6
```

### 2. 사운드를 2D로 들리게 조정

현재 wav meta 기준으로 오디오가 3D 사운드로 import된 상태다.

총알 발사음은 화면 전체에서 동일하게 들리는 효과음에 가까우므로, 필요하면 `AudioSource`의 Spatial Blend를 2D 쪽으로 조정하면 좋다.

### 3. 사운드 매니저는 아직 미루기

나중에 사운드가 많아지면 사운드 매니저를 만들 수 있다.

하지만 지금은 초급 수업 단계이므로, `AudioSource + Play On Awake` 방식이 더 단순하고 적절하다.

### 4. BGM과 폭발 사운드는 다음 단계에서 분리

이번 커밋에 BGM과 Explosion 사운드가 추가되었지만 아직 연결하지 않았다.

다음 작업을 나눠서 진행하면 좋다.

```text
Ch01_Setup_22_ExplosionSound
Ch01_Setup_23_BackgroundMusic
```

## 다음 단계 제안

다음 단계는 폭발 사운드를 추가하는 것이 자연스럽다.

추천 다음 작업:

```text
Ch01_Setup_22_ExplosionSound
```

내용:

- Enemy가 충돌할 때 폭발 사운드 재생
- `Audio_Explosion.wav` 사용
- 기존 폭발 이펙트 생성 흐름과 연결
- Enemy 충돌 시 이펙트와 사운드가 같이 나오도록 구성

그 다음 단계에서는 BGM을 추가할 수 있다.

```text
Ch01_Setup_23_BackgroundMusic
```

내용:

- 씬에 BGM용 AudioSource 추가
- `Audio_BGM.wav` 연결
- Loop On
- Play On Awake On

## 최종 결론

이번 패치는 총알 발사 사운드를 추가하는 좋은 청각 피드백 단계다.

기존 코드를 수정하지 않고 Bullet 프리팹 설정만으로 구현했기 때문에 수업용으로 단순하고 안전하다.

다음 단계에서는 `Audio_Explosion.wav`를 사용해 Enemy 폭발 사운드를 추가하면 자연스럽다.
