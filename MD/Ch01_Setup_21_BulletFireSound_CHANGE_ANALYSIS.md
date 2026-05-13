# Ch01_Setup_21_BulletFireSound_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 사운드 리소스를 추가하고, `Bullet` 프리팹에 발사 사운드를 연결했다.

커밋 메시지:

```text
총알 사운드 리소스 추가 총알에 발사 사운드 추가
```

주요 변경은 다음과 같다.

1. `Assets/CP03_Sound` 폴더 추가
2. `Audio_BGM.wav` 추가
3. `Audio_Bullet.wav` 추가
4. `Audio_Explosion.wav` 추가
5. 각 wav 파일의 `.meta` 추가
6. `Bullet.prefab`에 `AudioSource` 컴포넌트 추가
7. `AudioSource`에 `Audio_Bullet.wav` 연결
8. `Mat_Background.mat`의 텍스처 오프셋 값 변경

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/CP03_Sound.meta` | 사운드 폴더 메타 파일 추가 |
| `Assets/CP03_Sound/Audio_BGM.wav` | BGM 사운드 리소스 추가 |
| `Assets/CP03_Sound/Audio_BGM.wav.meta` | BGM 사운드 메타 파일 추가 |
| `Assets/CP03_Sound/Audio_Bullet.wav` | 총알 발사 사운드 리소스 추가 |
| `Assets/CP03_Sound/Audio_Bullet.wav.meta` | 총알 발사 사운드 메타 파일 추가 |
| `Assets/CP03_Sound/Audio_Explosion.wav` | 폭발 사운드 리소스 추가 |
| `Assets/CP03_Sound/Audio_Explosion.wav.meta` | 폭발 사운드 메타 파일 추가 |
| `Assets/Prefabs/Bullet.prefab` | AudioSource 추가 및 Audio_Bullet 연결 |
| `Assets/Materials/Mat_Background.mat` | 텍스처 오프셋 값 변경 |

## 사운드 리소스 추가 분석

새 폴더가 추가되었다.

```text
Assets/CP03_Sound
```

이 폴더에는 다음 사운드 파일이 추가되었다.

```text
Audio_BGM.wav
Audio_Bullet.wav
Audio_Explosion.wav
```

각 wav 파일의 `.meta`도 함께 추가되었다.

파일 역할은 다음과 같이 볼 수 있다.

| 파일 | 용도 |
|---|---|
| `Audio_BGM.wav` | 이후 배경음악으로 사용할 수 있는 리소스 |
| `Audio_Bullet.wav` | 총알 발사 사운드 |
| `Audio_Explosion.wav` | 이후 폭발 사운드로 사용할 수 있는 리소스 |

이번 단계에서 실제 게임 오브젝트에 연결된 것은 `Audio_Bullet.wav`다.

## AudioImporter 설정 분석

추가된 wav 파일들의 `.meta`에는 Unity `AudioImporter` 설정이 포함된다.

주요 설정은 다음과 같다.

```text
loadType: 0
sampleRateOverride: 44100
compressionFormat: 1
quality: 1
preloadAudioData: 0
forceToMono: 0
normalize: 1
3D: 1
```

현재 메타 기준으로 오디오는 3D 사운드로 import된 상태다.

프로젝트가 3D 공간 기반 슈팅 게임이므로 큰 문제는 없지만, 발사 사운드처럼 화면 전체에서 들리는 효과음은 나중에 2D 사운드처럼 들리도록 Spatial Blend를 조정할 수도 있다.

## Bullet.prefab 변경 분석

`Bullet.prefab`의 컴포넌트 목록에 `AudioSource`가 추가되었다.

추가된 컴포넌트:

```text
AudioSource
```

`AudioSource`에는 `Audio_Bullet.wav`가 연결되어 있다.

주요 설정은 다음과 같다.

```text
Audio Clip: Audio_Bullet.wav
Play On Awake: On
Volume: 1
Pitch: 1
Loop: Off
```

이 설정으로 인해 Bullet 프리팹이 생성되는 순간 발사 사운드가 자동으로 재생된다.

## 코드 변경 여부

이번 패치에서는 C# 스크립트가 수정되지 않았다.

수정되지 않은 주요 스크립트:

```text
Assets/Scripts/PlayerFire.cs
Assets/Scripts/Bullet.cs
Assets/Scripts/Enemy.cs
Assets/Scripts/Background.cs
```

따라서 발사 사운드는 코드에서 직접 재생하는 방식이 아니라, `AudioSource`의 `Play On Awake` 설정으로 재생되는 구조다.

## 동작 흐름

기존 흐름:

```text
PlayerFire가 Bullet 프리팹 생성
→ Bullet이 이동
```

변경 후 흐름:

```text
PlayerFire가 Bullet 프리팹 생성
→ Bullet에 붙은 AudioSource가 자동 재생
→ Bullet이 이동
```

기존 발사 코드가 바뀌지 않았기 때문에 초급 수업용으로 이해하기 쉬운 방식이다.

## Mat_Background 변경 분석

`Assets/Materials/Mat_Background.mat`도 함께 변경되었다.

변경 내용은 `_BaseMap` 텍스처 오프셋 값이다.

```text
m_Offset.y: 0 → 2.1780388
```

이 값은 이전 단계에서 `Background.cs`가 실행되면서 머티리얼의 `mainTextureOffset`이 변경된 상태가 저장된 것으로 보인다.

이번 커밋의 핵심은 사운드 추가이므로, 배경 머티리얼 오프셋 변경은 의도한 변경인지 확인하는 것이 좋다.

## 구현된 내용

이번 패치로 구현된 내용은 다음과 같다.

- 사운드 리소스 폴더 추가
- BGM, Bullet, Explosion wav 파일 추가
- Bullet 프리팹에 AudioSource 추가
- Bullet 생성 시 발사 사운드 자동 재생
- 기존 발사/이동 코드 유지

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- BGM 재생
- 폭발 사운드 재생
- 사운드 볼륨 조절
- 오디오 믹서
- 사운드 매니저
- 점수
- 게임 오버
- UI

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. `Assets/CP03_Sound` 폴더가 있는지 확인
2. `Audio_BGM.wav`, `Audio_Bullet.wav`, `Audio_Explosion.wav`가 import 되었는지 확인
3. `Bullet` 프리팹에 `AudioSource`가 붙어 있는지 확인
4. `AudioSource`에 `Audio_Bullet.wav`가 연결되어 있는지 확인
5. `Play On Awake`가 켜져 있는지 확인
6. Play 모드에서 총알 발사 시 소리가 나는지 확인
7. 총알을 빠르게 연속 발사할 때 소리가 너무 겹치거나 크지 않은지 확인
8. `Mat_Background.mat` 오프셋 변경이 의도된 것인지 확인

## 주의할 점

### 1. Play On Awake 방식

현재는 Bullet이 생성될 때 AudioSource가 자동 재생된다.

이 방식은 단순하고 수업용으로 좋지만, 나중에 사운드를 세밀하게 제어하려면 코드에서 `Play()`를 호출하는 방식이나 사운드 매니저를 사용할 수 있다.

### 2. 총알이 빨리 사라지면 소리가 끊길 수 있다

AudioSource가 Bullet에 붙어 있기 때문에, Bullet이 매우 빨리 Destroy되면 사운드가 끝까지 재생되지 못할 수 있다.

현재 총알은 위로 이동하다 충돌하거나 DestroyZone에 닿을 때 삭제되므로 대부분 문제 없을 수 있다.

나중에 충돌 직후에도 소리가 끝까지 나야 한다면 별도 AudioSource 오브젝트나 사운드 매니저가 필요할 수 있다.

### 3. 사운드 겹침

총알을 연속 발사하면 Bullet이 여러 개 생성되고, 각 Bullet의 AudioSource가 동시에 재생된다.

볼륨이 너무 크면 `AudioSource`의 Volume을 낮추는 것이 좋다.

### 4. Mat_Background 변경

이번 패치의 핵심은 사운드인데 `Mat_Background.mat`도 변경되었다.

배경 스크롤을 Play 모드에서 확인한 뒤 머티리얼 오프셋이 저장된 것일 수 있다.

필요 없는 변경이라면 다음 정리 단계에서 오프셋을 다시 0으로 맞추는 것도 좋다.

## 결론

이번 패치는 총알 발사 사운드를 추가하는 단계다.

사운드 리소스를 추가하고 Bullet 프리팹에 AudioSource를 연결했기 때문에, 기존 발사 코드를 수정하지 않고도 총알 생성 시 소리가 재생된다.

다음 단계에서는 폭발 사운드 또는 BGM 재생 기능으로 이어갈 수 있다.
