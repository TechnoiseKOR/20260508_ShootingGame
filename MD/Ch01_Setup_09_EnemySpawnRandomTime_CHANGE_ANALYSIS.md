# Ch01_Setup_09_EnemySpawnRandomTime_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 EnemyManager의 적 생성 시간이 고정 간격에서 랜덤 간격으로 변경되었다.

이전 단계에서는 `createTime` 값에 따라 모든 EnemyManager가 일정한 시간마다 적을 생성했다.  
이번 단계에서는 최소 시간과 최대 시간 사이에서 랜덤한 값을 뽑아 적 생성 간격으로 사용한다.

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Scripts/EnemyManager.cs` | 적 생성 시간 랜덤화 로직 추가 |

## EnemyManager.cs 변경 분석

### 1. 최소 시간과 최대 시간 추가

다음 필드가 추가되었다.

```csharp
// 최소 시간
float minTime = 1.0f;
// 최대 시간
float maxTime = 5.0f;
```

이 값들은 적 생성 시간을 랜덤으로 정할 때 사용하는 범위다.

즉, EnemyManager는 적을 생성할 때마다 `1초`에서 `5초` 사이의 시간을 다음 생성 시간으로 사용한다.

## Start 변경 분석

기존 `Start()`는 비어 있었다.

변경 후에는 게임이 시작될 때 적 생성 시간을 랜덤으로 설정한다.

```csharp
void Start()
{
    // 태어날 때 적의 생성 시간을 설정하고
    createTime = UnityEngine.Random.Range(minTime, maxTime);
}
```

이렇게 하면 Play 모드가 시작될 때 각 EnemyManager가 서로 다른 생성 시간을 가질 수 있다.

## Update 변경 분석

적을 생성한 뒤 다음 코드가 추가되었다.

```csharp
// 적을 생성한 후 적의 생성 시간을 다시 설정하고 싶다.
createTime = UnityEngine.Random.Range(minTime, maxTime);
```

이 변경으로 적이 한 번 생성된 후에도 다음 생성 시간이 다시 랜덤으로 정해진다.

따라서 적 생성 간격이 계속 고정되지 않고 매번 달라진다.

## 변경 전 흐름

기존 흐름은 다음과 같았다.

1. 시간이 흐른다.
2. `currentTime`이 `createTime`보다 커진다.
3. Enemy를 생성한다.
4. `currentTime`을 0으로 초기화한다.
5. 같은 `createTime`으로 다시 기다린다.

즉, 적 생성 간격이 계속 일정했다.

## 변경 후 흐름

변경 후 흐름은 다음과 같다.

1. 게임 시작 시 `createTime`을 랜덤으로 정한다.
2. 시간이 흐른다.
3. `currentTime`이 `createTime`보다 커진다.
4. Enemy를 생성한다.
5. `currentTime`을 0으로 초기화한다.
6. 다음 `createTime`을 다시 랜덤으로 정한다.

즉, 적 생성 간격이 매번 달라진다.

## 구현된 기능

이번 패치로 구현된 기능은 다음과 같다.

- 적 생성 최소 시간 추가
- 적 생성 최대 시간 추가
- 게임 시작 시 적 생성 시간 랜덤 설정
- 적 생성 후 다음 생성 시간 랜덤 재설정

## 아직 구현하지 않은 기능

이번 단계에서는 아래 기능은 아직 구현하지 않았다.

- Inspector에서 최소/최대 시간 조절
- 생성 위치 랜덤화
- 적 종류 랜덤화
- 난이도에 따른 생성 시간 변화
- 점수
- 게임 오버
- UI
- 사운드
- 이펙트

현재 단계에서는 생성 시간만 랜덤화한 것이 적절하다.

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. Console에 컴파일 오류가 없는지 확인
2. Play 모드에서 적이 계속 생성되는지 확인
3. 적 생성 간격이 항상 같지 않고 달라지는지 확인
4. 여러 EnemyManager가 서로 다른 타이밍으로 적을 생성하는지 확인

## 주의할 점

### 1. minTime과 maxTime이 Inspector에 노출되지 않는다

현재 `minTime`과 `maxTime`은 일반 private 필드다.

```csharp
float minTime = 1.0f;
float maxTime = 5.0f;
```

따라서 Unity Inspector에서 값을 바꿀 수 없다.

초반 수업에서는 코드에서 숫자를 직접 바꿔보며 확인해도 괜찮다.

나중에 더 편하게 조절하려면 아래처럼 바꿀 수 있다.

```csharp
[SerializeField] private float minTime = 1.0f;
[SerializeField] private float maxTime = 5.0f;
```

### 2. UnityEngine.Random.Range 사용

`UnityEngine.Random.Range(minTime, maxTime)`은 최소값과 최대값 사이의 랜덤 float 값을 반환한다.

이번처럼 float 시간을 랜덤으로 정할 때 적절하다.

### 3. createTime은 여전히 public이다

현재 `createTime`은 기존처럼 public 필드다.

하지만 Start와 적 생성 후 코드에서 랜덤 값으로 다시 설정되므로,
Inspector에서 넣은 값이 실행 중에는 덮어써질 수 있다.

이 점은 이후 코드 정리 때 설명하거나 구조를 바꿀 수 있다.

## 결론

이번 패치는 EnemyManager의 적 생성 간격을 랜덤화하는 작은 개선 단계다.

기존 EnemyManager 구조를 크게 바꾸지 않고,
랜덤 시간만 추가했기 때문에 초급자가 이해하기 쉬운 난이도를 유지한다.

이제 적 생성 타이밍이 덜 규칙적이 되어 게임 느낌이 조금 더 자연스러워진다.
