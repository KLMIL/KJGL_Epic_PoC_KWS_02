/**********************************************************
 * Script Name: GameManager
 * Author: 김우성
 * Date Created: 2025-05-08
 * Last Modified: 2025-05-08
 * Description: 
 * - 게임 시작과 종료 및 게임 흐름 관리
 * - 스테이지 클리어시 플레이어에게 선택지 제공
 *********************************************************/

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] EnemyController _enemyController;
    [SerializeField] NPCController _npcController;

    bool _isShowingChoices;

    [SerializeField] GameObject _choicePanel;
    [SerializeField] List<TextMeshProUGUI> _choiceTexts;

    List<(string action, float delay)> _currentChoices; // 선택지 데이터


    [SerializeField] TextMeshProUGUI _stageText;
    int _currStage = 1;


    private void Start()
    {
        Debug.Log("Game Started");
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
            /* 게임 종료 처리 추가. 그냥 재시작 버튼 띄우기? */
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
        _isShowingChoices = true;
        Time.timeScale = 0f;
        ShowRandomChoices();
    }

    // 스테이지 클리어 했을 때, 선택지 보여주는 함수
    private void ShowRandomChoices()
    {
        // 4가지 행동 중 3개 랜덤 선택
        string[] actions = { "Move", "Attack", "Guard", "Dash" };
        _currentChoices = new List<(string action, float delay)>();
        var shuffledActions = actions.OrderBy(_ => Random.value).Take(3).ToList();

        // 각 행동에 랜덤 딜레이 부여
        for (int i = 0; i < shuffledActions.Count; i++)
        {
            float delay = Mathf.Round((Random.Range(-1f, 1f) * 10f)) / 10f;
            _currentChoices.Add((shuffledActions[i], delay));
            Debug.Log($"Choice {i}: Action={shuffledActions[i]}, Delay={delay}");
        }

        // 선택지 텍스트 표시
        for (int i = 0; i < _choiceTexts.Count; i++)
        {
            _choiceTexts[i].text = $"{i + 1}. {_currentChoices[i].action} " +
            (_currentChoices[i].delay >= 0
            ? $"{_currentChoices[i].delay.ToString("F1")}"
            : $"{_currentChoices[i].delay.ToString("F1")}") + "s";
        }


        _choicePanel.SetActive(true);
        _npcController.ShowChoicePrompt();
    }

    private void HandleChoiceInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playerController.ChangeDelay(_currentChoices[0].action, _currentChoices[0].delay);
            ResetChoice();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerController.ChangeDelay(_currentChoices[1].action, _currentChoices[1].delay);
            ResetChoice();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _playerController.ChangeDelay(_currentChoices[2].action, _currentChoices[2].delay);
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
        _choicePanel.SetActive(false);
        Time.timeScale = 1f;
        _enemyController.CreateEnemy();
        _playerController.ResetInput(Time.time); // 타이밍 초기화

        _currentChoices.Clear(); // 선택지 초기화

        _stageText.text = $"Stage {++_currStage}";
    }
}
