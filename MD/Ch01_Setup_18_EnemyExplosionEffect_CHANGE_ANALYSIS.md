# Ch01_Setup_18_EnemyExplosionEffect_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 Enemy가 다른 오브젝트와 충돌했을 때 폭발 이펙트를 생성하는 기능이 추가되었다.

주요 변경은 다음과 같다.

1. `Enemy.cs`에 폭발 이펙트 프리팹 참조 변수 추가
2. `OnCollisionEnter`에서 폭발 이펙트 생성
3. 생성된 폭발 이펙트를 Enemy 위치로 이동
4. Enemy 프리팹의 `explosionFactory`에 이펙트 프리팹 연결
5. 기존 충돌 제거 로직 유지

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Scripts/Enemy.cs` | 충돌 시 폭발 이펙트 생성 로직 추가 |
| `Assets/Prefabs/Enemy.prefab` | `explosionFactory` 필드에 폭발 이펙트 프리팹 연결 |

## Enemy.cs 변경 분석

### 1. 목표 주석 추가

파일 상단에 이번 기능의 목표와 순서가 주석으로 추가되었다.

```csharp
// 목표 : 적이 다른 물체와 충돌했을 때 폭발 효과를 발생시키고 싶다.
// 순서 : 1. 적이 다른 물체와 충돌했으니까.
//       2. 폭발 효과 공장에서 폭발 효과를 하나 만들어야 한다.
//       3. 폭발 효과를 발생(위치)시키고 싶다.
// 필요 속성 : 폭발 공장 주소(외부에서 값을 넣어준다)
```

기존 수업 흐름처럼 목표, 순서, 필요 속성을 먼저 적고 코드로 구현하는 방식이다.

### 2. explosionFactory 필드 추가

Enemy 클래스에 폭발 이펙트 프리팹을 받을 변수가 추가되었다.

```csharp
public GameObject explosionFactory;
```

이 변수는 Unity Inspector에서 폭발 이펙트 프리팹을 연결하기 위한 필드다.

즉, 코드에서는 어떤 이펙트를 쓸지 직접 경로로 적지 않고, Enemy 프리팹에서 외부 참조로 연결한다.

## OnCollisionEnter 변경 분석

기존 `OnCollisionEnter`는 충돌한 상대와 Enemy 자신을 제거하는 구조였다.

이번 패치에서는 제거하기 전에 폭발 이펙트를 생성한다.

추가된 핵심 코드는 다음과 같다.

```csharp
GameObject explosion = Instantiate(explosionFactory);
explosion.transform.position = transform.position;
```

실행 순서는 다음과 같다.

1. Enemy가 다른 오브젝트와 충돌한다.
2. `explosionFactory`에 연결된 폭발 이펙트 프리팹을 생성한다.
3. 생성된 폭발 이펙트의 위치를 Enemy 위치로 옮긴다.
4. 충돌한 상대 오브젝트를 제거한다.
5. Enemy 자신도 제거한다.

## Enemy 프리팹 변경 분석

`Assets/Prefabs/Enemy.prefab`에는 `explosionFactory` 연결이 추가되었다.

```yaml
explosionFactory: {fileID: 8194992990867289339, guid: 99bc28c0d45f51c4fbac01080b73edc7, type: 3}
```

즉, Enemy 프리팹의 Enemy 컴포넌트에 폭발 이펙트 프리팹이 연결된 상태다.

앞으로 EnemyManager가 생성하는 Enemy도 이 프리팹 설정을 그대로 사용하므로, 생성된 모든 Enemy가 충돌 시 폭발 이펙트를 만들 수 있다.

## 유지된 기능

이번 패치에서는 기존 충돌 제거 로직이 유지되었다.

```csharp
Destroy(collision.gameObject);
Destroy(gameObject);
```

따라서 충돌 시 다음 동작은 그대로 유지된다.

- 충돌한 상대 오브젝트 제거
- Enemy 제거

추가된 것은 그 전에 폭발 이펙트를 생성하는 단계다.

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- Enemy 충돌 시 폭발 이펙트 생성
- 폭발 이펙트를 Enemy 위치에 배치
- Enemy 프리팹에 이펙트 프리팹 참조 연결
- 기존 충돌 제거 동작 유지

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- 충돌 대상 구분
- Bullet과 Enemy 충돌일 때만 폭발 생성
- Player와 Enemy 충돌일 때 다른 이펙트 생성
- 점수 증가
- 게임 오버 처리
- 이펙트 자동 삭제 보정
- 사운드 재생
- UI 표시

현재 단계에서는 충돌 시 폭발을 보여주는 것이 핵심이다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. Enemy 프리팹의 Enemy 컴포넌트에 `Explosion Factory` 필드가 보이는지 확인
2. `Explosion Factory`에 이펙트 프리팹이 연결되어 있는지 확인
3. Play 모드에서 Bullet과 Enemy가 충돌할 때 폭발 이펙트가 생성되는지 확인
4. 이펙트가 Enemy가 있던 위치에서 나타나는지 확인
5. Bullet과 Enemy가 기존처럼 사라지는지 확인
6. Console에 NullReferenceException이 없는지 확인

## 주의할 점

### 1. explosionFactory가 비어 있으면 오류가 날 수 있다

현재 코드는 `explosionFactory`가 연결되어 있다는 전제로 작성되어 있다.

```csharp
GameObject explosion = Instantiate(explosionFactory);
```

만약 Enemy 프리팹에서 `explosionFactory` 연결이 빠지면 Play 모드에서 오류가 발생할 수 있다.

초급 수업에서는 Inspector 연결 확인을 강조하면 좋다.

나중에 안전하게 만들려면 다음처럼 null 체크를 추가할 수 있다.

```csharp
if (explosionFactory != null)
{
    GameObject explosion = Instantiate(explosionFactory);
    explosion.transform.position = transform.position;
}
```

### 2. 충돌 대상을 구분하지 않는다

현재 Enemy는 어떤 오브젝트와 충돌해도 폭발 이펙트를 생성한다.

따라서 Bullet과 충돌해도 폭발하고, Player와 충돌해도 폭발한다.

초반 단계에서는 단순해서 괜찮지만, 이후 점수와 게임 오버를 만들려면 충돌 대상을 구분해야 한다.

### 3. 이펙트 자동 삭제 확인 필요

CFXR 이펙트 프리팹이 재생 후 자동으로 사라지는지 확인해야 한다.

자동으로 사라지지 않는다면 Hierarchy에 이펙트 오브젝트가 계속 쌓일 수 있다.

다음 단계나 정리 단계에서 이펙트 제거 방식을 확인하는 것이 좋다.

### 4. 이펙트 크기와 위치 확인 필요

폭발 이펙트는 Enemy의 위치에 생성된다.

비행기 외형의 중심과 폭발 위치가 어색하면, 이펙트 프리팹 크기 또는 생성 위치를 조정해야 할 수 있다.

## 결론

이번 패치는 Enemy 충돌에 폭발 이펙트를 추가한 단계다.

기존 충돌 제거 로직을 유지하면서, 제거 전에 이펙트를 생성하는 구조라 이해하기 쉽고 수업 흐름에도 적절하다.

다음 단계에서는 점수 기능이나 충돌 대상 구분으로 이어가면 좋다.
