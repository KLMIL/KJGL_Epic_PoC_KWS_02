/**********************************************************
 * Script Name: PlayerController
 * Author: 김우성
 * Date Created: 2025-05-08
 * Last Modified: 2025-05-08
 * Description: 
 * - 플레이어의 입력, 체력 상태 관리
 * - 모든 행위의 딜레이 정보 관리
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _maxHealth = 100f;
    [SerializeField] float _currHealth;
    public float CurrHealth => _currHealth;
    public float CurrentTiming => _currentTiming;
    public float PatternStartTime => _patternStartTime;
    public float InputWindow => _inputWindow;

    [SerializeField] List<TextMeshProUGUI> _delayTexts = new List<TextMeshProUGUI>();

    [SerializeField] GameObject _damageEffect;

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
    [SerializeField] NPCController _npcController;

    [SerializeField] TextMeshProUGUI _playerHealthText;

    // 타이밍 바
    [SerializeField] Image _timingBar;
    [SerializeField] Image _successTimingBar;
    float _currentTiming;
    float _successRange = 0.15f;
    bool _canInput = true;
    float _inputWindow = 2f;
    float _patternStartTime;

    private void Start()
    {
        // 주석 해제해서 시작할 때 랜덤 딜레이 부여
        //_actionDelays["Move"] = Mathf.Round(Random.Range(-1f, 1f) * 10) / 10f;
        //_actionDelays["Dash"] = Mathf.Round(Random.Range(-1f, 1f) * 10) / 10f;
        //_actionDelays["Attack"] = Mathf.Round(Random.Range(-1f, 1f) * 10) / 10f;
        //_actionDelays["Guard"] = Mathf.Round(Random.Range(-1f, 1f) * 10) / 10f;

        UpdateDelayUI();

        // 체력 초기화
        _currHealth = _maxHealth;
        _playerHealthText.text = $"Player HP\n{_currHealth}";
        _patternStartTime = Time.time;

        UpdateSuccessTimingBar("Move"); // 초기 행동으로 타이밍 바 설정
    }

    private void Update()
    {
        UpdateTimingBar();
        GetInput();
    }

    private void UpdateTimingBar()
    {
        float timeSincePattern = Time.time - _patternStartTime;
        if (timeSincePattern <= _inputWindow)
        {
            // 2초 동안 -1초에서 1초로 타이밍 바 이동
            float t = Mathf.Clamp01(timeSincePattern / _inputWindow);
            _currentTiming = Mathf.Lerp(-1f, 1f, t);
        }
        else
        {
            // 2초 이후 1초 대기. 타이밍 바 멈춤
            _currentTiming = 1f;
            _canInput = false;
        }

        // 타이밍바 크기 조정
        if (_timingBar != null)
        {
            float normalizedPos = (_currentTiming + 1f) / 2f;
            _timingBar.rectTransform.anchorMin = new Vector2(normalizedPos, 0.4f);
            _timingBar.rectTransform.anchorMax = new Vector2(normalizedPos, 0.6f);
            _timingBar.rectTransform.sizeDelta = new Vector2(10f, _timingBar.rectTransform.sizeDelta.y);
        }
    }

    public void UpdateSuccessTimingBar(string pattern)
    {
        string actionType = GetActionTypeFromPattern(pattern);
        if (_successTimingBar != null && _actionDelays.ContainsKey(actionType))
        {
            float desiredTiming = _actionDelays[actionType];
            float normalizedPos = (desiredTiming + 1f) / 2f;
            _successTimingBar.rectTransform.anchorMin = new Vector2(normalizedPos, 0.45f);
            _successTimingBar.rectTransform.anchorMax = new Vector2(normalizedPos, 0.55f);
            _successTimingBar.rectTransform.sizeDelta = new Vector2(10f, _successTimingBar.rectTransform.sizeDelta.y);
            Debug.Log($"UpdateSuccessTimingBar: Pattern={pattern}, ActionType={actionType}, NormalizedPos={normalizedPos:F3}");
        }
    }

    private string GetActionTypeFromPattern(string pattern)
    {
        return pattern switch
        {
            "W" => "Move",
            "A" => "Move",
            "S" => "Move",
            "D" => "Move",
            "Shift" => "Dash",
            "LeftClick" => "Attack",
            "RightClick" => "Guard",
            _ => "Move" // 기본값
        };
    }

    private void GetInput()
    {
        if (!_canInput) return; // 입력은 1회로 제한

        // 이동 입력 (WASD)
        if (Input.GetKeyDown(KeyCode.W) && CanPerformAction("Move"))
        {
            ProcessAction("W");
            _lastActionTimes["Move"] = Time.time;
            _canInput = false;
        }
        else if (Input.GetKeyDown(KeyCode.A) && CanPerformAction("Move"))
        {
            ProcessAction("A");
            _lastActionTimes["Move"] = Time.time;
            _canInput = false;
        }
        else if (Input.GetKeyDown(KeyCode.S) && CanPerformAction("Move"))
        {
            ProcessAction("S");
            _lastActionTimes["Move"] = Time.time;
            _canInput = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) && CanPerformAction("Move"))
        {
            ProcessAction("D");
            _lastActionTimes["Move"] = Time.time;
            _canInput = false;
        }

        // 회피 입력 (Shift)
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanPerformAction("Dash"))
        {
            ProcessAction("Shift");
            _lastActionTimes["Dash"] = Time.time;
            _canInput = false;
        }

        // 공격 입력 (좌클릭)
        if (Input.GetMouseButtonDown(0) && CanPerformAction("Attack"))
        {
            ProcessAction("LeftClick");
            _lastActionTimes["Attack"] = Time.time;
            _canInput = false;
        }

        // 방어 입력 (우클릭)
        if (Input.GetMouseButtonDown(1) && CanPerformAction("Guard"))
        {
            ProcessAction("RightClick");
            _lastActionTimes["Guard"] = Time.time;
            _canInput = false;
        }
    }

    private bool CanPerformAction(string actionType)
    {
        float delay = _actionDelays[actionType];
        float nextAvailableTime = _lastActionTimes[actionType] + delay;
        bool canPerform = Time.time >= nextAvailableTime;
        Debug.Log($"CanPerformAction: {actionType}, Delay={delay:F1}, LastActionTime={_lastActionTimes[actionType]:F1}, NextAvailableTime={nextAvailableTime:F1}, CurrentTime={Time.time:F1}, CanPerform={canPerform}");
        return canPerform;
    }

    private void ProcessAction(string action)
    {
        string actionType = GetActionTypeFromPattern(action);
        float desiredTiming = _actionDelays[actionType];
        //bool isTimingSuccess = Mathf.Abs(_currentTiming) <= _successRange;
        bool isTimingSuccess = Mathf.Abs(_currentTiming - desiredTiming) <= _successRange;
        _npcController.ShowTimingResult(isTimingSuccess, isTimingSuccess ? null : "Bad Timing!");
        _enemyController.CheckPlayerInput(action, isTimingSuccess);
    }

    public void TakeDamage(float damage)
    {
        _currHealth = Mathf.Max(0, _currHealth - damage);
        _playerHealthText.text = $"Player HP\n{_currHealth}";
        Debug.Log($"Player Health: {_currHealth}");

        _damageEffect.SetActive(true);
        StartCoroutine(DamageEffectCoroutine(0.2f));
    }

    private IEnumerator DamageEffectCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        _damageEffect.SetActive(false);
    }

    // 선택지를 뽑았을 때 딜레이를 변경하는 함수
    public void ChangeDelay(string actionType, float change)
    {
        if (_actionDelays.ContainsKey(actionType))
        {
            _actionDelays[actionType] = Mathf.Clamp(_actionDelays[actionType] + change, -1f, 1f);
            Debug.Log($"{actionType} Delay: {_actionDelays[actionType]}");
            UpdateDelayUI();
        }
    }

    public void ResetInput(float patternStartTime)
    {
        _canInput = true;
        _patternStartTime = patternStartTime;
        _currentTiming = -1f;
    }

    private void UpdateDelayUI()
    {
        _delayTexts[0].text = "Move: " + 
            (_actionDelays["Move"] >= 0 
            ? $"+{_actionDelays["Move"].ToString("F1")}" 
            : $"{_actionDelays["Move"].ToString("F1")}") + "s";
        _delayTexts[1].text = "Dash: " + 
            (_actionDelays["Dash"] >= 0 
            ? $"+{_actionDelays["Dash"].ToString("F1")}" 
            : $"{_actionDelays["Dash"].ToString("F1")}") + "s";
        _delayTexts[2].text = "Attack: " + 
            (_actionDelays["Attack"] >= 0 
            ? $"+{_actionDelays["Attack"].ToString("F1")}" 
            : $"{_actionDelays["Attack"].ToString("F1")}") + "s";
        _delayTexts[3].text = "Guard: " + 
            (_actionDelays["Guard"] >= 0 
            ? $"+{_actionDelays["Guard"].ToString("F1")}" 
            : $"{_actionDelays["Guard"].ToString("F1")}") + "s";
    }
}
