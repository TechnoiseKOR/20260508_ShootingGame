# Ch01_Setup_22_ExplosionSoundAndBGM_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 적 폭발 사운드와 배경음악이 추가되었다.

커밋 메시지:

```text
적 폭발 사운드 추가 BGM 추가
```

주요 변경은 다음과 같다.

1. `CFXR3 Fire Explosion B.prefab`에 `AudioSource` 추가
2. 폭발 이펙트 프리팹의 AudioSource에 `Audio_Explosion.wav` 연결
3. `SampleScene`의 `Background` 오브젝트에 `AudioSource` 추가
4. Background AudioSource에 `Audio_BGM.wav` 연결
5. Background AudioSource를 반복 재생으로 설정
6. `Mat_Background.mat` 텍스처 오프셋 변경
7. CFXR 폭발 프리팹의 Unity 직렬화 포맷 일부 갱신

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Explosions/CFXR3 Fire Explosion B.prefab` | 폭발 이펙트 프리팹에 AudioSource 추가, Unity 직렬화 값 갱신 |
| `Assets/Scenes/SampleScene.unity` | Background 오브젝트에 AudioSource 추가, BGM 연결 |
| `Assets/Materials/Mat_Background.mat` | 배경 텍스처 오프셋 값 변경 |

## 폭발 이펙트 프리팹 변경 분석

대상 프리팹은 다음 파일이다.

```text
Assets/JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Explosions/CFXR3 Fire Explosion B.prefab
```

이 프리팹에 `AudioSource` 컴포넌트가 추가되었다.

추가된 AudioSource는 다음 오디오 리소스를 참조한다.

```text
Audio_Explosion.wav
```

주요 설정은 다음과 같이 볼 수 있다.

```text
Play On Awake: On
Loop: Off
Volume: 1
Pitch: 1
```

이 구조에서는 `Enemy.cs`가 폭발 이펙트 프리팹을 `Instantiate`하는 순간, 프리팹에 붙은 AudioSource가 자동으로 폭발 사운드를 재생한다.

## 기존 Enemy.cs와의 연결

이전 단계에서 `Enemy.cs`는 충돌 시 다음 흐름으로 동작했다.

```text
Enemy 충돌
→ explosionFactory 생성
→ 생성된 이펙트를 Enemy 위치로 이동
→ 충돌 대상 제거
→ Enemy 제거
```

이번 패치에서는 `Enemy.cs`를 수정하지 않았다.

대신 `explosionFactory`로 연결된 폭발 이펙트 프리팹 자체에 AudioSource를 추가했다.

따라서 기존 코드 흐름은 그대로 유지하면서 폭발 사운드만 추가된 구조다.

## SampleScene 변경 분석

`SampleScene.unity`의 `Background` 오브젝트에 `AudioSource`가 추가되었다.

Background 오브젝트는 이전 단계에서 배경 머티리얼과 `Background.cs`를 통해 스크롤 배경 역할을 하던 오브젝트다.

이번에는 같은 오브젝트에 BGM용 AudioSource가 추가되었다.

연결된 오디오 리소스는 다음 파일로 볼 수 있다.

```text
Audio_BGM.wav
```

주요 설정은 다음과 같다.

```text
Play On Awake: On
Loop: On
Volume: 1
Pitch: 1
```

즉, 씬이 시작되면 BGM이 자동 재생되고 계속 반복된다.

## 코드 변경 여부

이번 패치에서는 C# 스크립트가 수정되지 않았다.

수정되지 않은 주요 스크립트:

```text
Assets/Scripts/Enemy.cs
Assets/Scripts/Bullet.cs
Assets/Scripts/PlayerFire.cs
Assets/Scripts/Background.cs
```

사운드 재생은 코드가 아니라 `AudioSource` 컴포넌트 설정으로 처리한다.

이 방식은 초급 수업에서 이해하기 쉽다.

## CFXR 프리팹 직렬화 갱신 분석

`CFXR3 Fire Explosion B.prefab`에는 AudioSource 추가 외에도 많은 직렬화 값 변경이 포함되어 있다.

예시:

- `Transform`의 `serializedVersion` 추가
- `ParticleSystem`의 `m_ColorSpace` 추가
- `ParticleSystemRenderer`의 `serializedVersion` 변경
- Renderer 관련 필드 추가
- Point Light에 `UniversalAdditionalLightData` 추가

이런 변경은 Unity 6.4 또는 URP 환경에서 에셋 프리팹을 열고 저장하면서 Unity가 최신 직렬화 형식으로 갱신한 결과로 볼 수 있다.

이번 기능의 핵심은 AudioSource 추가지만, 에셋 프리팹 저장 과정에서 주변 Unity 직렬화 값도 함께 바뀐 것이다.

## Mat_Background 변경 분석

`Assets/Materials/Mat_Background.mat`도 함께 수정되었다.

변경된 부분은 텍스처 오프셋이다.

```text
_BaseMap m_Offset.y: 0 → 1.174033
_MainTex m_Offset.y: 0 → 1.174033
```

이 값은 배경 스크롤 기능을 Play 모드에서 확인한 뒤 머티리얼 상태가 저장되었을 가능성이 있다.

이번 패치의 핵심은 폭발 사운드와 BGM 추가이므로, 배경 머티리얼 오프셋 변경이 의도한 것인지 확인하는 것이 좋다.

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- 적 폭발 이펙트 생성 시 폭발 사운드 자동 재생
- Background 오브젝트에서 BGM 자동 재생
- BGM 반복 재생
- 기존 C# 코드 수정 없이 AudioSource 설정으로 사운드 추가

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 사운드 볼륨 조절 UI
- AudioMixer
- 사운드 매니저
- BGM On/Off
- 효과음 On/Off
- 점수
- 게임 오버
- 플레이어 피격 사운드

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. `CFXR3 Fire Explosion B` 프리팹에 AudioSource가 붙어 있는지 확인
2. 폭발 이펙트 AudioSource의 Audio Clip이 `Audio_Explosion.wav`인지 확인
3. 폭발 이펙트 AudioSource의 Play On Awake가 켜져 있는지 확인
4. 폭발 이펙트 AudioSource의 Loop가 꺼져 있는지 확인
5. `SampleScene`의 Background 오브젝트에 AudioSource가 붙어 있는지 확인
6. Background AudioSource의 Audio Clip이 `Audio_BGM.wav`인지 확인
7. Background AudioSource의 Play On Awake가 켜져 있는지 확인
8. Background AudioSource의 Loop가 켜져 있는지 확인
9. Play 모드에서 BGM이 들리는지 확인
10. Enemy가 폭발할 때 폭발 사운드가 들리는지 확인
11. BGM과 효과음 볼륨이 서로 너무 크지 않은지 확인
12. `Mat_Background.mat` 오프셋 변경이 의도된 것인지 확인

## 주의할 점

### 1. 에셋 원본 프리팹 직접 수정

이번 작업은 `CFXR3 Fire Explosion B.prefab` 원본 에셋 프리팹을 직접 수정했다.

수업 흐름에서는 단순해서 괜찮지만, 나중에는 프로젝트 전용 폭발 프리팹으로 복제해서 수정하는 방법이 더 안전할 수 있다.

예:

```text
Assets/Prefabs/FX/EnemyExplosion.prefab
```

### 2. 폭발 이펙트가 바로 삭제되면 사운드가 끊길 수 있다

폭발 사운드는 이펙트 프리팹에 붙은 AudioSource가 재생한다.

만약 이펙트 오브젝트가 사운드 길이보다 빨리 Destroy되면 소리가 중간에 끊길 수 있다.

현재 CFXR 이펙트가 자동으로 충분히 유지되는지 확인해야 한다.

### 3. BGM 위치

현재 BGM용 AudioSource는 Background 오브젝트에 붙어 있다.

초급 단계에서는 이해하기 쉽고 괜찮다.

나중에 구조를 정리할 때는 `AudioManager` 또는 `BGM` 오브젝트를 따로 만들 수도 있다.

### 4. Mat_Background 변경

배경 스크롤 때문에 머티리얼 오프셋이 저장된 경우, 게임을 시작할 때 배경 시작 위치가 매번 달라질 수 있다.

필요하면 다음 정리 단계에서 오프셋을 0으로 되돌릴 수 있다.

## 결론

이번 패치는 기존 사운드 리소스를 게임에 실제로 연결한 단계다.

폭발 이펙트 프리팹에 AudioSource를 추가해 적 폭발 사운드를 만들고, Background 오브젝트에 AudioSource를 추가해 BGM을 반복 재생하도록 구성했다.

기존 C# 코드를 수정하지 않고 Unity Editor 설정 중심으로 구현했기 때문에 초급 수업 흐름에도 잘 맞는다.
