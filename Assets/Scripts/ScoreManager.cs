using UnityEngine;
// 유니티 UI를 사용하기 위한 네임스페이스
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 현재 점수 UI
    public Text currentScoreUI;
    // 현재 점수
    public int currentScore;

    // 최고 점수 UI
    public Text bestScoreUI;
    // 최고 점수
    public int bestScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 목표 : 최고 점수를 불러와 bestScore 변수에 할당하고 화면에 표시한다.
        // 순서 : 1. 최고 점수를 불러와 bestScore에 넣어주기
        bestScore = PlayerPrefs.GetInt("Best Score", 0);
        // 2. 최고 점수를 화면에 표시하기
        bestScoreUI.text = "최고 점수 : " + bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
