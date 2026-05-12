# Ch01_Setup_05_PlayerFire

## 목표

플레이어가 버튼을 누르면 총알을 발사하게 만든다.

이전 단계에서는 씬에 직접 놓인 `Bullet` 오브젝트가 위로 이동하는 것만 확인했다.  
이번 단계에서는 총알을 프리팹으로 만들고, 플레이어가 입력을 받으면 총알 프리팹을 생성하도록 한다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `Assets/Prefabs` 폴더가 없다면 생성한다.
2. 기존 `Bullet` 오브젝트를 프리팹으로 만든다.
3. 씬에 직접 배치되어 있던 `Bullet` 오브젝트는 제거한다.
4. `Assets/Scripts/PlayerFire.cs` 스크립트를 만든다.
5. `Player` 오브젝트에 `PlayerFire` 스크립트를 붙인다.
6. `Player` 아래에 총알이 생성될 위치를 나타내는 `FirePosition` 오브젝트를 만든다.
7. 발사 버튼을 누르면 `FirePosition` 위치에 총알 프리팹을 생성한다.

## 중요

- 이번 작업은 총알 프리팹 생성과 플레이어 발사 기능만 만든다.
- 적 기능은 만들지 마.
- 총알과 적 충돌은 만들지 마.
- 점수나 UI는 만들지 마.
- 총알 자동 삭제는 아직 만들지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### Bullet 프리팹

`Bullet` 오브젝트는 재사용할 수 있도록 프리팹으로 만든다.

- 프리팹 위치: `Assets/Prefabs/Bullet.prefab`
- 기존 `Bullet` 스크립트는 그대로 사용한다.
- 씬에 직접 놓여 있던 Bullet은 제거한다.
- 앞으로는 PlayerFire가 필요할 때 Bullet 프리팹을 생성한다.

### FirePosition 오브젝트

`Player` 오브젝트 아래에 `FirePosition` 오브젝트를 만든다.

`FirePosition`은 총알이 생성될 위치를 나타낸다.

- `FirePosition`은 Player의 자식 오브젝트로 둔다.
- 처음에는 Player 중심 또는 위쪽 위치에 둔다.
- 나중에 필요하면 위치를 조정할 수 있게 한다.

### PlayerFire 스크립트

`PlayerFire` 스크립트는 아래 기능만 담당한다.

- 발사 버튼 입력을 확인한다.
- 입력이 들어오면 Bullet 프리팹을 생성한다.
- 생성한 Bullet을 FirePosition 위치로 옮긴다.

초반 수업에서는 이해하기 쉬운 코드를 우선한다.

예상 코드 흐름:

```csharp
if (Input.GetButtonDown("Fire1"))
{
    GameObject bullet = Instantiate(bulletFactory);
    bullet.transform.position = firePosition.transform.position;
}
```

## Unity Editor 설정

작업 후 Unity Editor에서 다음 연결이 필요하다.

- `Player` 오브젝트에 `PlayerFire` 스크립트가 붙어 있어야 한다.
- `PlayerFire`의 `bulletFactory`에 `Bullet.prefab`을 연결한다.
- `PlayerFire`의 `firePosition`에 `FirePosition` 오브젝트를 연결한다.
- Play 모드에서 발사 버튼을 눌렀을 때 총알이 생성되어 위로 이동하는지 확인한다.

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- 파일 목록

새로 만든 파일:
- 파일 목록

새로 만든 프리팹:
- 프리팹 이름

새로 만든 오브젝트:
- 오브젝트 이름

Unity Editor에서 확인할 내용:
- PlayerFire 스크립트 연결 확인
- Bullet 프리팹 연결 확인
- FirePosition 연결 확인
- Play 모드에서 발사 버튼으로 총알이 생성되는지 확인

아직 구현하지 않은 내용:
- 총알 자동 삭제
- 적
- 충돌 처리
- 점수
- 게임 오버
```
