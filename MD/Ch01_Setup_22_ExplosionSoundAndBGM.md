# Ch01_Setup_22_ExplosionSoundAndBGM

## 목표

적이 폭발할 때 폭발 사운드가 함께 재생되도록 만들고, 게임 씬에 배경음악을 추가한다.

이전 단계에서는 총알 발사 사운드를 Bullet 프리팹에 연결했다.  
이번 단계에서는 이미 추가된 사운드 리소스 중 `Audio_Explosion.wav`와 `Audio_BGM.wav`를 사용한다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. 폭발 이펙트 프리팹에 `AudioSource` 컴포넌트를 추가한다.
2. 폭발 이펙트의 `AudioSource`에 `Audio_Explosion.wav`를 연결한다.
3. 폭발 이펙트가 생성될 때 폭발 사운드가 자동으로 재생되도록 설정한다.
4. `SampleScene`의 `Background` 오브젝트에 `AudioSource` 컴포넌트를 추가한다.
5. `Background` 오브젝트의 `AudioSource`에 `Audio_BGM.wav`를 연결한다.
6. BGM은 씬이 시작되면 자동으로 재생되고 반복 재생되도록 설정한다.
7. 기존 C# 스크립트는 수정하지 않는다.

## 중요

- 이번 작업은 폭발 사운드와 BGM 추가만 진행한다.
- Enemy.cs는 수정하지 마.
- Bullet.cs는 수정하지 마.
- PlayerFire.cs는 수정하지 마.
- Background.cs는 수정하지 마.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- UI는 만들지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 사용할 사운드 리소스

이미 프로젝트에 추가된 사운드 폴더를 사용한다.

```text
Assets/CP03_Sound
```

사용할 파일:

```text
Audio_Explosion.wav
Audio_BGM.wav
```

## 폭발 사운드 설정

폭발 이펙트 프리팹을 수정한다.

대상 프리팹 예시:

```text
Assets/JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Explosions/CFXR3 Fire Explosion B.prefab
```

이 프리팹에 `AudioSource`를 추가하고 아래처럼 설정한다.

```text
Audio Clip: Audio_Explosion.wav
Play On Awake: On
Loop: Off
Volume: 1
Pitch: 1
```

이렇게 하면 Enemy가 충돌할 때 기존 `Enemy.cs`가 폭발 이펙트 프리팹을 생성하고, 생성된 이펙트에 붙은 `AudioSource`가 자동으로 폭발 사운드를 재생한다.

## BGM 설정

`SampleScene`의 `Background` 오브젝트에 `AudioSource`를 추가한다.

설정 기준:

```text
Audio Clip: Audio_BGM.wav
Play On Awake: On
Loop: On
Volume: 1
Pitch: 1
```

이렇게 하면 게임이 시작될 때 배경음악이 자동으로 재생되고 계속 반복된다.

## 구현 방식

이번 단계에서는 코드로 사운드를 재생하지 않는다.

폭발 사운드 흐름:

```text
Enemy 충돌
→ Enemy.cs가 폭발 이펙트 프리팹 생성
→ 폭발 이펙트 AudioSource가 Play On Awake로 자동 재생
```

BGM 흐름:

```text
SampleScene 시작
→ Background 오브젝트의 AudioSource가 Play On Awake로 자동 재생
→ Loop On 상태이므로 계속 반복 재생
```

## Unity Editor에서 확인할 내용

작업 후 Unity Editor에서 아래 내용을 확인한다.

- `CFXR3 Fire Explosion B` 프리팹에 `AudioSource`가 추가되어 있는지 확인
- 폭발 이펙트 `AudioSource`의 Audio Clip이 `Audio_Explosion.wav`인지 확인
- 폭발 이펙트 `AudioSource`의 `Play On Awake`가 켜져 있는지 확인
- 폭발 이펙트 `AudioSource`의 `Loop`가 꺼져 있는지 확인
- `SampleScene`의 `Background` 오브젝트에 `AudioSource`가 추가되어 있는지 확인
- Background `AudioSource`의 Audio Clip이 `Audio_BGM.wav`인지 확인
- Background `AudioSource`의 `Play On Awake`가 켜져 있는지 확인
- Background `AudioSource`의 `Loop`가 켜져 있는지 확인
- Play 모드에서 BGM이 재생되는지 확인
- Enemy가 폭발할 때 폭발 사운드가 들리는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- 파일 목록

주요 변경:
- 폭발 이펙트 프리팹에 폭발 사운드 연결
- Background 오브젝트에 BGM 연결
- 기존 스크립트 수정 없이 AudioSource 설정으로 사운드 재생

Unity Editor에서 확인할 내용:
- 폭발 이펙트 AudioSource 확인
- Background AudioSource 확인
- Play 모드에서 폭발 사운드와 BGM 확인

아직 구현하지 않은 내용:
- 점수
- 게임 오버
- 사운드 볼륨 조절
- 오디오 믹서
- 사운드 매니저
```
