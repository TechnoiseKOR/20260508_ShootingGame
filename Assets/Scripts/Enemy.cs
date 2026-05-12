using UnityEngine;

// 목표 : 아래로 계속 이동하고 싶다.
// 필요 속성 : 이동 속도
// 순서 : 1. 방향 구하기
//       2. 이동하기


public class Enemy : MonoBehaviour
{
    // 필요 속성 : 이동 속도
    public float speed = 5.0f;

    // 방향을 전역 변수로 만들어 Start와 Update에서 사용
    Vector3 dir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Vector3 dir; -> 삭제

        // 0부터 9까지 10개의 값 중에 하나를 랜덤으로 가져온다.
        int randValue = UnityEngine.Random.Range(0, 10);
        // 만약 3보다 작으면 플레이어 방향
        if( randValue < 3 )
        {
            // 플레이어를 찾아 target으로 하고 싶다.
            GameObject target = GameObject.Find("Player");
            // 플레이어가 있다면
            if( target != null )
            {
                // 방향을 구하고 싶다. target - maxTime
                dir = target.transform.position - transform.position;
                // 방향의 크기를 1로 하고 싶다.
                dir.Normalize();                
                return;
            }            
        }
        // 그렇지 않으면 아래 방향으로 정하고 싶다.
        //else
        {
            dir = Vector3.down;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 방향을 구한다.
        // Vector3 dir = Vector3.down; -> 삭제
        // 2. 이동하고 싶다. P = P0 + vt
        transform.position += dir * speed * Time.deltaTime;
    }

    // 충돌 시작
    private void OnCollisionEnter(Collision collision)
    {
       // 충돌 시작 
       // 너 죽고
       Destroy(collision.gameObject);
       // 나 죽자
       Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        // 충돌 중
    }

    private void OnCollisionExit(Collision collision)
    {
        // 충돌 끝
    }
}
