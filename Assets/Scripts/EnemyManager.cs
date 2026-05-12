using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // 현재 시간
    float currentTime;
    // 일정 시간
    public float createTime = 1.0f;
    // 적 공장
    public GameObject enemyFactory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 시간이 흐르다가
        currentTime += Time.deltaTime;

        // 2. 만약 현재 시간이 일정 시간이 되면
        if( currentTime > createTime )
        {
            // 3. 적 공장에서 적을 생성해
            GameObject enemy = Instantiate(enemyFactory);
            // 내 위치에 갖다 놓고 싶다.
            enemy.transform.position = transform.position;
            // 현재 시간을 0으로 초기화
            currentTime = 0.0f;
        }
    }
}
