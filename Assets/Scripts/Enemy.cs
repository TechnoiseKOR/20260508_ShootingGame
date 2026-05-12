using UnityEngine;

// 목표 : 아래로 계속 이동하고 싶다.
// 필요 속성 : 이동 속도
// 순서 : 1. 방향 구하기
//       2. 이동하기


public class Enemy : MonoBehaviour
{
    // 필요 속성 : 이동 속도
    public float speed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 방향을 구한다.
        Vector3 dir = Vector3.down;
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
