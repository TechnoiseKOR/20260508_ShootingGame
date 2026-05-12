# Ch01_Setup_05_PlayerFire_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 총알을 프리팹으로 만들고, 플레이어가 버튼을 눌렀을 때 총알을 발사하는 기능이 추가되었다.

주요 변경은 다음과 같다.

1. `Assets/Prefabs` 폴더 추가
2. `Bullet` 오브젝트를 `Assets/Prefabs/Bullet.prefab`으로 프리팹화
3. 씬에 직접 배치되어 있던 `Bullet` 오브젝트 제거
4. `PlayerFire.cs` 스크립트 추가
5. `Player` 오브젝트에 `PlayerFire` 컴포넌트 추가
6. `Player` 자식으로 `FirePosition` 오브젝트 추가
7. `PlayerFire`에 `Bullet.prefab`과 `FirePosition` 참조 연결

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Prefabs.meta` | `Assets/Prefabs` 폴더 메타 파일 추가 |
| `Assets/Prefabs/Bullet.prefab` | 총알 프리팹 추가 |
| `Assets/Prefabs/Bullet.prefab.meta` | 총알 프리팹 메타 파일 추가 |
| `Assets/Scenes/SampleScene.unity` | 씬 Bullet 제거, PlayerFire 추가, FirePosition 추가 |
| `Assets/Scripts/PlayerFire.cs` | 플레이어 총알 발사 스크립트 추가 |
| `Assets/Scripts/PlayerFire.cs.meta` | `PlayerFire.cs` 메타 파일 추가 |

## 프리팹 변경 분석

`Assets/Prefabs/Bullet.prefab`이 새로 추가되었다.

프리팹에는 다음 컴포넌트가 포함되어 있다.

- `Transform`
- `MeshFilter`
- `MeshRenderer`
- `BoxCollider`
- `Bullet`

이전 단계에서 씬에 직접 배치했던 Bullet 오브젝트 구성을 프리팹으로 분리한 것이다.

프리팹의 주요 설정은 다음과 같다.

- 위치: `{x: 0, y: 1.5, z: 0}`
- 크기: `{x: 0.25, y: 0.6, z: 1}`
- `Bullet` 스크립트의 `speed`: `5`

이제 총알은 씬에 미리 놓는 방식이 아니라, 필요할 때 생성하는 방식으로 바뀌었다.

## 씬 변경 분석

`SampleScene.unity`에서 기존 `Bullet` 루트 오브젝트가 제거되었다.

대신 `Player` 오브젝트에 다음 변경이 추가되었다.

### 1. PlayerFire 컴포넌트 추가

`Player` 오브젝트에 `PlayerFire` 컴포넌트가 추가되었다.

`PlayerFire`에는 다음 참조가 연결되어 있다.

- `bulletFactory`: `Assets/Prefabs/Bullet.prefab`
- `firePosition`: `FirePosition` 오브젝트

### 2. FirePosition 오브젝트 추가

`Player` 자식 오브젝트로 `FirePosition`이 추가되었다.

`FirePosition`은 총알이 생성될 위치를 나타내는 기준점이다.

현재 로컬 위치는 다음과 같다.

```text
{x: 0, y: 0, z: 0}
```

즉, 현재는 Player 중심에서 총알이 생성된다.

## 스크립트 변경 분석

새 파일 `Assets/Scripts/PlayerFire.cs`가 추가되었다.

핵심 코드는 다음 흐름이다.

```csharp
if (Input.GetButtonDown("Fire1"))
{
    GameObject bullet = Instantiate(bulletFactory);
    bullet.transform.position = firePosition.transform.position;
}
```

이 코드는 발사 버튼이 눌렸을 때 총알 프리팹을 생성하고, 생성된 총알을 FirePosition 위치로 옮긴다.

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- 총알 프리팹 생성
- 플레이어 발사 스크립트 추가
- 발사 버튼 입력 처리
- 발사 위치 오브젝트 추가
- 발사 시 총알 생성
- 생성된 총알이 기존 Bullet 스크립트에 의해 위로 이동

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 총알 자동 삭제
- 발사 간격 제한
- 총알과 적 충돌
- 적 생성
- 점수
- 게임 오버
- 사운드
- 이펙트
- 오브젝트 풀링

현재 단계에서는 총알 발사의 가장 기본 구조만 만든 것이 적절하다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. `Assets/Prefabs/Bullet.prefab`이 있는지 확인
2. `Player` 오브젝트에 `PlayerFire` 스크립트가 붙어 있는지 확인
3. `PlayerFire`의 `bulletFactory`에 `Bullet.prefab`이 연결되어 있는지 확인
4. `PlayerFire`의 `firePosition`에 `FirePosition`이 연결되어 있는지 확인
5. `FirePosition`이 Player의 자식 오브젝트인지 확인
6. Play 모드에서 발사 버튼을 누르면 총알이 생성되는지 확인
7. 생성된 총알이 위쪽으로 이동하는지 확인

## 주의할 점

### 1. FirePosition 위치가 Player 중심이다

현재 `FirePosition`의 로컬 위치는 `{x: 0, y: 0, z: 0}`이다.

즉, 총알이 Player 중심에서 생성된다.

나중에 총알이 플레이어 앞쪽에서 나가게 보이도록 하려면, `FirePosition`의 위치를 위쪽으로 조금 올리는 것이 좋다.

예:

```text
{x: 0, y: 0.7, z: 0}
```

### 2. 총알 자동 삭제가 없다

현재 총알은 발사 후 계속 위쪽으로 이동한다.

화면 밖으로 나간 총알이 계속 남아 있을 수 있으므로, 다음 단계에서 총알 삭제 기능을 추가해야 한다.

### 3. 발사 간격 제한이 없다

현재 `Input.GetButtonDown("Fire1")`을 사용하므로 버튼을 누른 순간 한 발씩 생성된다.

기본 테스트에는 충분하지만, 나중에 연사나 발사 간격을 만들려면 별도 처리가 필요하다.

### 4. public 필드 사용

현재 `bulletFactory`와 `firePosition`은 public 필드다.

초급 단계에서는 Inspector 연결을 이해하기 쉽기 때문에 괜찮다.

이후 코드 정리 단계에서는 `[SerializeField] private`로 바꿀 수 있다.

## 결론

이번 패치는 총알을 프리팹으로 만들고, 플레이어가 입력을 통해 총알을 생성하는 첫 발사 기능을 추가한 단계다.

이전 단계의 Bullet 이동 기능을 재사용하면서, 씬에 직접 놓인 오브젝트를 프리팹으로 바꾸는 중요한 흐름을 잘 보여준다.

다음 단계에서는 총알이 화면 밖으로 나갔을 때 삭제하는 기능을 추가하면 좋다.
