# Ch01_Setup_21_BulletFireSound

## 목표

총알이 발사될 때 발사 사운드가 들리도록 만든다.

이번 단계에서는 사운드 리소스를 프로젝트에 추가하고, `Bullet` 프리팹에 `AudioSource`를 붙여 총알이 생성될 때 소리가 나도록 설정한다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `Assets/CP03_Sound` 폴더를 추가한다.
2. 사운드 리소스 3개를 추가한다.
3. `Bullet` 프리팹에 `AudioSource` 컴포넌트를 추가한다.
4. `AudioSource`에 총알 발사 사운드인 `Audio_Bullet.wav`를 연결한다.
5. 총알이 생성될 때 사운드가 자동으로 재생되도록 설정한다.
6. 기존 `Bullet.cs`, `PlayerFire.cs` 코드는 수정하지 않는다.

## 중요

- 이번 작업은 총알 발사 사운드만 적용한다.
- BGM 재생 기능은 아직 만들지 마.
- 폭발 사운드 재생 기능은 아직 만들지 마.
- 점수 기능은 만들지 마.
- 게임 오버 기능은 만들지 마.
- UI는 만들지 마.
- 기존 스크립트는 수정하지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 추가할 사운드 리소스

아래 폴더에 사운드 파일을 추가한다.

```text
Assets/CP03_Sound
```

추가되는 사운드 파일은 다음과 같다.

```text
Audio_BGM.wav
Audio_Bullet.wav
Audio_Explosion.wav
```

이번 단계에서 실제로 Bullet에 연결하는 파일은 `Audio_Bullet.wav`다.

`Audio_BGM.wav`, `Audio_Explosion.wav`는 이후 단계에서 사용할 준비 리소스로만 추가한다.

## Bullet 프리팹 설정

`Assets/Prefabs/Bullet.prefab`에 `AudioSource`를 추가한다.

AudioSource 설정 기준:

```text
Audio Clip: Audio_Bullet.wav
Play On Awake: On
Loop: Off
Volume: 1
Pitch: 1
```

이렇게 하면 `PlayerFire`가 Bullet 프리팹을 생성할 때, Bullet에 붙어 있는 AudioSource가 자동으로 재생된다.

## 구현 방식

이번 단계에서는 코드로 사운드를 재생하지 않는다.

기존 구조:

```text
PlayerFire
└─ Bullet 프리팹 생성
```

변경 후 구조:

```text
PlayerFire
└─ Bullet 프리팹 생성
   └─ Bullet에 붙은 AudioSource가 Play On Awake로 자동 재생
```

즉, `PlayerFire.cs`와 `Bullet.cs`를 수정하지 않고도 총알 생성 시 소리가 나게 만든다.

## Unity Editor에서 확인할 내용

작업 후 Unity Editor에서 아래 내용을 확인한다.

- `Assets/CP03_Sound` 폴더가 생성되어 있는지 확인
- `Audio_BGM.wav`, `Audio_Bullet.wav`, `Audio_Explosion.wav`가 import 되었는지 확인
- `Bullet` 프리팹에 `AudioSource`가 추가되어 있는지 확인
- `AudioSource`의 Audio Clip이 `Audio_Bullet.wav`인지 확인
- `Play On Awake`가 켜져 있는지 확인
- `Loop`가 꺼져 있는지 확인
- Play 모드에서 총알을 발사할 때 소리가 나는지 확인

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- 파일 목록

새로 만든 파일:
- 파일 목록

주요 변경:
- 사운드 리소스 추가
- Bullet 프리팹에 AudioSource 추가
- Bullet 발사 사운드 연결

Unity Editor에서 확인할 내용:
- Bullet 프리팹의 AudioSource 확인
- Audio_Bullet.wav 연결 확인
- Play 모드에서 발사 사운드 확인

아직 구현하지 않은 내용:
- BGM 재생
- 폭발 사운드 재생
- 사운드 볼륨 조절 UI
- 점수
- 게임 오버
```
