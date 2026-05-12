# Ch01_Setup_10_EnemyRandomTargetMove_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 Enemy의 이동 방향이 고정된 아래 방향에서 랜덤 조건에 따른 방향으로 변경되었다.

이전 Enemy는 항상 `Vector3.down` 방향으로 이동했다.  
이번 변경 후 Enemy는 생성될 때 랜덤 값을 뽑고, 일정 확률로 Player 방향을 향해 이동한다.

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Scripts/Enemy.cs` | Enemy 이동 방향 선택 로직 추가 |

## Enemy.cs 변경 분석

### 1. 이동 방향 필드 추가

기존에는 `Update()` 안에서 매번 지역 변수로 방향을 만들었다.

```csharp
Vector3 dir = Vector3.down;
```

변경 후에는 이동 방향을 클래스 필드로 분리했다.

```csharp
Vector3 dir;
```

이렇게 바꾸면 `Start()`에서 한 번 정한 방향을 `Update()`에서 계속 사용할 수 있다.

## Start 변경 분석

기존 `Start()`는 비어 있었다.

이번 패치에서는 `Start()`에서 Enemy의 이동 방향을 정한다.

### 랜덤 값 생성

```csharp
int randValue = UnityEngine.Random.Range(0, 10);
```

`0`부터 `9`까지의 정수 중 하나를 뽑는다.

### Player 방향 선택

```csharp
if( randValue < 3 )
{
    GameObject target = GameObject.Find("Player");

    if( target != null )
    {
        dir = target.transform.position - transform.position;
        dir.Normalize();                
        return;
    }            
}
```

랜덤 값이 `3`보다 작으면 Player를 찾고, Player가 존재하면 Enemy 위치에서 Player 위치를 향하는 방향을 계산한다.

계산된 방향은 `Normalize()`를 통해 길이가 1인 방향 벡터로 바뀐다.

즉, 일부 Enemy는 생성된 순간의 Player 위치를 향해 이동한다.

### 기본 방향 설정

```csharp
dir = Vector3.down;
```

랜덤 조건에 맞지 않거나 Player를 찾지 못하면 기존처럼 아래 방향으로 이동한다.

## Update 변경 분석

기존 `Update()`에서는 매 프레임 아래 방향을 새로 만들었다.

```csharp
Vector3 dir = Vector3.down;
transform.position += dir * speed * Time.deltaTime;
```

변경 후에는 `Start()`에서 정한 `dir` 필드를 사용한다.

```csharp
transform.position += dir * speed * Time.deltaTime;
```

이 구조는 Enemy가 생성될 때 이동 방향을 한 번 결정하고, 그 방향으로 계속 움직이게 만든다.

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- Enemy 이동 방향을 필드로 저장
- Enemy 생성 시 랜덤 값 생성
- 일정 확률로 Player 방향 계산
- Player가 없으면 아래 방향으로 이동
- Enemy마다 이동 방향이 달라질 수 있음

## 확률 해석

```csharp
int randValue = UnityEngine.Random.Range(0, 10);

if (randValue < 3)
```

`Random.Range(0, 10)`의 정수 버전은 `0`부터 `9`까지 값을 뽑는다.

그중 `0`, `1`, `2`일 때만 Player 방향을 선택하므로,
대략 30% 확률로 Player 방향을 향한다고 볼 수 있다.

나머지 약 70%는 기존처럼 아래 방향으로 이동한다.

## 중요한 동작 특징

이번 구현은 Player를 계속 추적하는 기능이 아니다.

Enemy는 생성될 때 한 번 Player 위치를 보고 방향을 정한다.  
그 이후에는 Player가 움직여도 방향을 다시 계산하지 않는다.

즉, 이번 기능은 다음에 가깝다.

```text
생성 순간의 Player 방향으로 날아오는 Enemy
```

아직 다음 기능은 아니다.

```text
계속 Player를 따라오는 추적 Enemy
```

초급 단계에서는 한 번 방향을 정하는 방식이 더 이해하기 쉽고 적절하다.

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- Player를 계속 따라가는 추적
- Enemy 종류 구분
- Player와 충돌 시 게임 오버
- 총알과 충돌 시 점수 증가
- Enemy 이동 패턴 다양화
- 난이도에 따른 확률 변경
- UI
- 사운드
- 이펙트

현재 단계에서는 이동 방향에 랜덤성과 Player 방향성을 추가한 것이 핵심이다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. Console에 컴파일 오류가 없는지 확인
2. Play 모드에서 Enemy가 계속 생성되는지 확인
3. 일부 Enemy가 Player 방향으로 이동하는지 확인
4. 나머지 Enemy는 아래쪽으로 이동하는지 확인
5. Player 오브젝트 이름이 정확히 `Player`인지 확인

## 주의할 점

### 1. GameObject.Find 사용

이번 패치에서는 Player를 찾기 위해 다음 코드를 사용한다.

```csharp
GameObject target = GameObject.Find("Player");
```

초급 단계에서는 이해하기 쉽고 사용해도 괜찮다.

다만 `GameObject.Find`는 이름에 의존한다.

따라서 씬의 Player 오브젝트 이름이 반드시 `Player`여야 한다.

### 2. Player를 한 번만 찾는다

Player는 `Start()`에서 한 번만 찾는다.

Enemy가 생성된 뒤 Player가 움직여도 방향은 바뀌지 않는다.

이 동작은 현재 단계에서는 적절하지만, 나중에 추적 Enemy를 만들 때는 Update에서 방향을 다시 계산해야 한다.

### 3. 주석 오타 확인

패치 안에는 다음 주석이 있다.

```csharp
// 방향을 구하고 싶다. target - maxTime
```

코드는 정상적으로 Player 위치와 Enemy 위치의 차이를 사용하고 있다.

```csharp
dir = target.transform.position - transform.position;
```

다만 주석의 `maxTime`은 의미상 맞지 않으므로, 나중에 정리한다면 `target - transform.position` 또는 `목표 위치 - 내 위치`처럼 고치는 것이 좋다.

### 4. dir 기본값

현재 구조에서는 Player를 찾지 못하거나 랜덤 조건에 맞지 않을 때 `dir = Vector3.down;`으로 설정된다.

따라서 `dir`이 비어 있는 상태로 이동할 가능성은 낮다.

## 결론

이번 패치는 Enemy 이동에 랜덤성을 추가하는 좋은 단계다.

적이 항상 아래로만 내려오지 않고, 일부는 Player 방향으로 움직이기 때문에 게임이 더 다양해진다.

코드 변경은 `Enemy.cs` 하나에만 집중되어 있고, 기존 EnemyManager 구조를 건드리지 않아 변경 범위도 적절하다.
