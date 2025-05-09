/**********************************************************
 * Script Name: EnemyController
 * Author: 김우성
 * Date Created: 2025-05-08
 * Last Modified: 2025-05-08
 * Description: 
 * - 적 생성 함수
 * - 고유 패턴 실행
 * - 패턴에 따른 정보 제공
 *********************************************************/

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class EnemyController : MonoBehaviour
{
    [SerializeField] NPCController _npcController;
    [SerializeField] GameManager _gameManager;
    [SerializeField] PlayerController _playerController;

    [SerializeField] float _attackInteraval = 3f;
    [SerializeField] float _enemyHealth = 30f;
    [SerializeField] float _damageToPlayer = 10f;
    [SerializeField] float _damageToEnemy = 10f;   
    
    [SerializeField] float _nextActionTime;
    [SerializeField] string _requiredAction;

    readonly string[] _possibleActions =
    {
        "W", "A", "S", "D", "LeftClick", "RightClick", "Shift"
    };

    [SerializeField] TextMeshProUGUI _enemyHealthText;
    [SerializeField] TextMeshProUGUI _enemyIntervalText;

    public float AttackInterval => _attackInteraval;
    public float NextActionTime => _nextActionTime;

    [SerializeField] GameObject _damageEffect;


    private void Start()
    {
        CreateEnemy();
    }

    private void Update()
    {
        UpdateIntervalText();
        if (Time.time >= _nextActionTime)
        {
            ExecutePattern();
            _nextActionTime = Time.time + _attackInteraval;
        }
    }

    public void CreateEnemy()
    {
        _enemyHealth = 30f;
        _enemyHealthText.text = $"Enemy HP\n{_enemyHealth}";
        _nextActionTime = Time.time; // 즉시 패턴 시작
        GenerateRandomPattern();
    }

    private void GenerateRandomPattern()
    {
        _requiredAction = _possibleActions[Random.Range(0, _possibleActions.Length)];
        _npcController.UpdateBriefingText(_requiredAction);
        _playerController.UpdateSuccessTimingBar(_requiredAction); // 패턴 변경시 타이밍 변경
        _playerController.ResetInput(Time.time); // 새 패턴 시 입력 가능
        Debug.Log($"Enemy Pattern: {_requiredAction}");
    }

    private void ExecutePattern()
    {
        GenerateRandomPattern();
    }

    // 패턴에 따라 플레이어에게 대미지 부여
    public void CheckPlayerInput(string playerAction, bool isTimingSuccess)
    {
        bool isActionCorrect = playerAction == _requiredAction;
        
        if (isActionCorrect && isTimingSuccess) // 행동에 성공했을 때
        {
            _enemyHealth -= _damageToEnemy;
            _npcController.ShowTimingResult(true);
            _enemyHealthText.text = $"Enemy HP\n{_enemyHealth}";
            Debug.Log($"Enemy Hit! Enemy Health: {_enemyHealth}");
            _damageEffect.SetActive(true);
            StartCoroutine(DamageEffectCoroutine(0.2f));
            if (_enemyHealth <= 0)
            {
                _gameManager.OnEnemyDefeated();
            }
        }
        else
        {
            string message = !isActionCorrect ? "Wrong Action!" : "Bad Timing!";
            _playerController.TakeDamage(_damageToPlayer);
            _npcController.ShowTimingResult(false, message);
            Debug.Log($"Player Missed: {message}");
        }
    }

    private IEnumerator DamageEffectCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        _damageEffect.SetActive(false);
    }

    private void UpdateIntervalText()
    {
        float timeLeft = Mathf.Max(0, _nextActionTime - Time.time);
        _enemyIntervalText.text = $"Next Pattern: {timeLeft:F1}s";
    }
}
