/**********************************************************
 * Script Name: PlayerController
 * Author: 김우성
 * Date Created: 2025-05-08
 * Last Modified: 2025-05-08
 * Description: 
 * - 플레이어의 입력, 체력 상태 관리
 * - 모든 행위의 딜레이 정보 관리
 *********************************************************/

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _maxHealth = 100f;
    [SerializeField] float _currHealth;
    public float CurrHealth => _currHealth;

    // 행동별 딜레이
    Dictionary<string, float> _actionDelays = new Dictionary<string, float>
    {
        { "Move", 0.0f },
        { "Dash", 0.0f },
        { "Attack", 0.0f },
        { "Guard", 0.0f },
    };

    // 마지막 행동 입력 시간
    Dictionary<string, float> _lastActionTimes = new Dictionary<string, float>
    {
        { "Move", 0.0f },
        { "Dash", 0.0f },
        { "Attack", 0.0f },
        { "Guard", 0.0f }
    };

    [SerializeField] EnemyController _enemyController;

    [SerializeField] TextMeshProUGUI _playerHealthText;


    private void Start()
    {
        _currHealth = _maxHealth;
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        // 이동 입력 (WASD)
        if (Input.GetKeyDown(KeyCode.W) && CanPerformAction("Move"))
        {
            ProcessAction("W");
            _lastActionTimes["Move"] = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.A) && CanPerformAction("Move"))
        {
            ProcessAction("A");
            _lastActionTimes["Move"] = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.S) && CanPerformAction("Move"))
        {
            ProcessAction("S");
            _lastActionTimes["Move"] = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.D) && CanPerformAction("Move"))
        {
            ProcessAction("D");
            _lastActionTimes["Move"] = Time.time;
        }

        // 회피 입력 (Shift)
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanPerformAction("Defense"))
        {
            ProcessAction("Shift");
            _lastActionTimes["Dash"] = Time.time;
        }

        // 공격 입력 (좌클릭)
        if (Input.GetMouseButtonDown(0) && CanPerformAction("Attack"))
        {
            ProcessAction("LeftClick");
            _lastActionTimes["Attack"] = Time.time;
        }

        // 방어 입력 (우클릭)
        if (Input.GetMouseButtonDown(1) && CanPerformAction("Attack"))
        {
            ProcessAction("RightClick");
            _lastActionTimes["Guard"] = Time.time;
        }
    }

    private bool CanPerformAction(string actionType)
    {
        return Time.time >= _lastActionTimes[actionType] + _actionDelays[actionType];
    }

    private void ProcessAction(string action)
    {
        _enemyController.CheckPlayerInput(action);
    }

    public void TakeDamage(float damage)
    {
        _currHealth = Mathf.Max(0, _currHealth - damage);
        _playerHealthText.text = $"Player HP\n{_currHealth}";
        Debug.Log($"Player Health: {_currHealth}");
    }

    // 선택지를 뽑았을 때 딜레이를 변경하는 함수
    public void ChangeDelay(string actionType, float change)
    {
        if (_actionDelays.ContainsKey(actionType))
        {
            _actionDelays[actionType] = Mathf.Clamp(_actionDelays[actionType] + change, -1f, 1f);
            Debug.Log($"{actionType} Delay: {_actionDelays[actionType]}");
        }
    }
}
