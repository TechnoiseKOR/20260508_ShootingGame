# Ch01_Setup_03_PlayerMove

## 목표

Unity 슈팅 게임의 첫 번째 실제 기능으로 플레이어 오브젝트를 만들고 키보드로 움직이게 한다.

이번 단계에서는 플레이어 이동만 구현한다.  
총알, 적, 점수, 게임 오버 같은 기능은 아직 만들지 않는다.

## 작업 요청

AGENTS.md 기준으로 작업해줘.

이번 작업에서는 아래 내용만 진행해줘.

1. `Assets/Scripts` 폴더가 없다면 생성한다.
2. `Assets/Scripts/PlayerMove.cs` 스크립트를 만든다.
3. 기본 씬에 `Player` 오브젝트를 만든다.
4. `Player` 오브젝트에 `PlayerMove` 스크립트를 붙인다.
5. 키보드 입력으로 플레이어가 움직이게 한다.

## 중요

- 이번 작업은 플레이어 이동 기능만 만든다.
- 총알 기능은 만들지 마.
- 적 기능은 만들지 마.
- 점수나 UI는 만들지 마.
- 요청한 부분 이외의 작업은 하지 마.

## 구현 기준

### Player 오브젝트

기본 씬에 `Player`라는 이름의 오브젝트를 만든다.

- 처음에는 단순한 3D 오브젝트를 사용해도 된다.
- 위치는 화면 중앙 근처에서 확인할 수 있게 둔다.
- 플레이어에 `PlayerMove` 스크립트를 연결한다.

### PlayerMove 스크립트

`PlayerMove` 스크립트는 아래 기능만 담당한다.

- 키보드 입력을 받는다.
- 입력 방향으로 플레이어 위치를 이동시킨다.
- 이동 속도는 Inspector에서 조절할 수 있게 한다.

초반 수업에서는 이해하기 쉬운 코드를 우선한다.

예상 코드 흐름:

```csharp
float h = Input.GetAxis("Horizontal");
float v = Input.GetAxis("Vertical");

Vector3 dir = new Vector3(h, v, 0.0f);
transform.position = transform.position + dir * speed * Time.deltaTime;
```

## 완료 후 보고 형식

작업이 끝나면 아래 형식으로 짧게 보고해줘.

```text
작업 완료

수정한 파일:
- 파일 목록

새로 만든 파일:
- 파일 목록

새로 만든 오브젝트:
- 오브젝트 이름

Unity Editor에서 확인할 내용:
- Player 오브젝트가 있는지 확인
- PlayerMove 스크립트가 붙어 있는지 확인
- Play 모드에서 방향키 또는 WASD로 움직이는지 확인

아직 구현하지 않은 내용:
- 총알
- 적
- 점수
- 게임 오버
```
