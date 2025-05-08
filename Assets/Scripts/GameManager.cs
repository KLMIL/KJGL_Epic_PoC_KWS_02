/**********************************************************
 * Script Name: GameManager
 * Author: 김우성
 * Date Created: 2025-05-08
 * Last Modified: 2025-05-08
 * Description: 
 * - 게임 시작과 종료 및 게임 흐름 관리
 * - 스테이지 클리어시 플레이어에게 선택지 제공
 *********************************************************/

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] EnemyController _enemyController;

    bool _isStageCleared;

    private void Start()
    {
        Debug.Log("Game Started");
        _isStageCleared = false;
    }

    private void Update()
    {
        // 스테이지 클리어

        // 게임 오버
        if (_playerController.CurrHealth <= 0)
        {
            Debug.Log("Game Over");
            /* 게임 종료 처리 추가 */
        }
    }


    // 스테이지 클리어 했을 때, 선택지 보여주는 함수
    private void ShowRandomChoices()
    {
        
    }
}
