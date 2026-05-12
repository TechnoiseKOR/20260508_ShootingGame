# Ch01_Setup_12_LayerCollisionMatrix_CHANGE_ANALYSIS

## 변경 개요

이번 패치에서는 게임 오브젝트에 레이어를 지정하고, Unity Physics의 Layer Collision Matrix를 조정했다.

주요 변경은 다음과 같다.

1. `DestroyZone`, `Player`, `Bullet`, `Enemy` 레이어 추가
2. Bullet 프리팹을 Bullet 레이어로 변경
3. Enemy 프리팹을 Enemy 레이어로 변경
4. 씬의 Player와 FirePosition을 Player 레이어로 변경
5. 씬의 DestroyZone 오브젝트들을 DestroyZone 레이어로 변경
6. `ProjectSettings/DynamicsManager.asset`의 Layer Collision Matrix 변경

## 변경 파일

| 파일 | 변경 내용 |
|---|---|
| `Assets/Prefabs/Bullet.prefab` | Bullet 프리팹 레이어를 8번으로 변경 |
| `Assets/Prefabs/Enemy.prefab` | Enemy 프리팹 레이어를 9번으로 변경 |
| `Assets/Scenes/SampleScene.unity` | Player, FirePosition, DestroyZone 오브젝트 레이어 변경 |
| `ProjectSettings/DynamicsManager.asset` | Physics Layer Collision Matrix 변경 |
| `ProjectSettings/TagManager.asset` | DestroyZone, Player, Bullet, Enemy 레이어 추가 |

## 레이어 추가 분석

`ProjectSettings/TagManager.asset`에 새 레이어가 추가되었다.

추가된 레이어는 다음과 같다.

| 레이어 번호 | 이름 |
|---|---|
| 6 | DestroyZone |
| 7 | Player |
| 8 | Bullet |
| 9 | Enemy |

이제 오브젝트를 이름이나 태그만으로 구분하지 않고, 물리 충돌 단계에서 레이어로 구분할 수 있다.

## 프리팹 레이어 변경 분석

### Bullet 프리팹

`Assets/Prefabs/Bullet.prefab`의 레이어가 `Default`에서 `Bullet` 레이어로 변경되었다.

```diff
-  m_Layer: 0
+  m_Layer: 8
```

### Enemy 프리팹

`Assets/Prefabs/Enemy.prefab`의 레이어가 `Default`에서 `Enemy` 레이어로 변경되었다.

```diff
-  m_Layer: 0
+  m_Layer: 9
```

프리팹 자체의 레이어가 변경되었기 때문에, 앞으로 생성되는 Bullet과 Enemy도 해당 레이어를 기준으로 충돌한다.

## 씬 오브젝트 레이어 변경 분석

`SampleScene.unity`에서는 씬에 있는 오브젝트들의 레이어가 변경되었다.

| 오브젝트 | 변경 레이어 |
|---|---|
| DestroyZone_U | DestroyZone |
| DestroyZone_D | DestroyZone |
| DestroyZone_L | DestroyZone |
| DestroyZone_R | DestroyZone |
| Player | Player |
| FirePosition | Player |

DestroyZone들은 모두 레이어 6으로 변경되었고, Player와 FirePosition은 레이어 7로 변경되었다.

## Physics 설정 변경 분석

`ProjectSettings/DynamicsManager.asset`의 `m_LayerCollisionMatrix` 값이 변경되었다.

이 값은 Unity의 Physics 설정에서 레이어끼리 충돌할지 말지를 저장하는 값이다.

이번 변경은 다음 의도를 가진다.

- 필요한 충돌은 유지한다.
- 필요 없는 충돌은 끈다.
- Bullet, Enemy, Player, DestroyZone 사이의 물리 충돌 관계를 정리한다.

또한 Unity 버전에 따라 PhysicsManager의 serializedVersion과 일부 필드가 함께 갱신되었다.

이런 설정 파일은 Unity Editor에서 Physics 설정을 저장하면서 같이 바뀔 수 있다.

## 구현된 내용

이번 패치로 구현된 내용은 다음과 같다.

- 물리 충돌 구분을 위한 레이어 생성
- 주요 오브젝트와 프리팹에 레이어 적용
- Physics Layer Collision Matrix 조정
- 앞으로 충돌 처리 로직을 더 명확하게 나눌 준비

## 코드 변경 여부

이번 패치에서는 C# 코드가 변경되지 않았다.

변경 대상은 프리팹, 씬, 프로젝트 설정 파일이다.

아직 변경되지 않은 코드:

- `Bullet.cs`
- `Enemy.cs`
- `PlayerMove.cs`
- `PlayerFire.cs`
- `DestroyZone.cs`
- `EnemyManager.cs`

## 확인할 점

Unity Editor에서 다음을 확인하면 좋다.

1. Tags and Layers 설정에 `DestroyZone`, `Player`, `Bullet`, `Enemy` 레이어가 있는지 확인
2. Bullet 프리팹이 Bullet 레이어인지 확인
3. Enemy 프리팹이 Enemy 레이어인지 확인
4. Player와 FirePosition이 Player 레이어인지 확인
5. 모든 DestroyZone이 DestroyZone 레이어인지 확인
6. Project Settings > Physics의 Layer Collision Matrix가 의도대로 설정되어 있는지 확인
7. Play 모드에서 충돌 동작이 정상인지 확인

## 주의할 점

### 1. Layer Collision Matrix는 눈으로 확인해야 한다

패치에서는 `m_LayerCollisionMatrix`가 긴 문자열로 보인다.

이 값만 보고 어떤 레이어끼리 충돌하는지 바로 이해하기 어렵다.

따라서 Unity Editor의 Project Settings > Physics에서 직접 체크 상태를 확인하는 것이 좋다.

### 2. TagManager.asset 변경

`TagManager.asset`은 프로젝트 전체의 태그와 레이어 설정이다.

잘못 수정하면 다른 오브젝트의 레이어 의미가 달라질 수 있다.

이번 변경은 새 레이어 4개를 추가하는 목적이므로 적절하지만, 이후에도 레이어 이름과 번호를 유지하는 것이 좋다.

### 3. FirePosition 레이어

FirePosition은 Player의 자식 오브젝트이고 Player 레이어로 설정되었다.

FirePosition 자체에는 Collider가 없기 때문에 충돌에는 직접 영향을 주지 않을 수 있다.

하지만 Player 계열 오브젝트로 묶는 의미에서는 자연스럽다.

### 4. DestroyZone 삭제 대상

DestroyZone은 여전히 Trigger에 들어온 오브젝트를 모두 삭제한다.

레이어 설정으로 충돌 대상을 어느 정도 줄일 수 있지만, 더 안전하게 하려면 나중에 코드에서 삭제 대상을 구분할 수 있다.

## 결론

이번 패치는 게임 오브젝트 간 충돌을 더 명확하게 관리하기 위한 레이어 설정 단계다.

이제 Bullet, Enemy, Player, DestroyZone이 각각 다른 레이어를 가지므로, 이후 점수 처리나 게임 오버 처리를 만들 때 충돌 대상을 더 쉽게 구분할 수 있다.
