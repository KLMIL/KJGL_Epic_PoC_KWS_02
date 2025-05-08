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
    [SerializeField] NPCController _npcController;

    bool _isStageCleared;
    bool _isShowingChoices;

    [SerializeField] GameObject _choicePanel;

    private void Start()
    {
        Debug.Log("Game Started");
        _isStageCleared = false;
        _isShowingChoices = false;
        _choicePanel.SetActive(false);
        _enemyController.CreateEnemy();
    }

    private void Update()
    {
        // 게임 오버
        if (_playerController.CurrHealth <= 0)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0f;
            /* 게임 종료 처리 추가 */
        }

        // 선택지 입력 처리
        if (_isShowingChoices)
        {
            HandleChoiceInput();
        }
    }

    // 적 제거시 호출
    public void OnEnemyDefeated()
    {
        _isStageCleared = true;
        _isShowingChoices = true;
        ShowRandomChoices();
    }

    // 스테이지 클리어 했을 때, 선택지 보여주는 함수
    private void ShowRandomChoices()
    {
        _choicePanel.SetActive(true);
    }

    private void HandleChoiceInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            /* 랜덤 선택지 생성 */
            ResetChoice();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            /* 랜덤 선택지 생성 */
            ResetChoice();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            /* 랜덤 선택지 생성 */
            ResetChoice();
        }
        else
        {
            /* Do Nothing */
        }
    }

    private void ResetChoice()
    {
        _isShowingChoices = false;
        _isStageCleared = false;
        _enemyController.CreateEnemy();
        _choicePanel.SetActive(false);
    }
}
