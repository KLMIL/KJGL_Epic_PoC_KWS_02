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

using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class EnemyController : MonoBehaviour
{
    [SerializeField] NPCController _npcController;
    [SerializeField] GameManager _gameManager;
    [SerializeField] PlayerController _playerController;

    [SerializeField] float _attackInteraval = 3f;
    [SerializeField] float _enemyHealth = 100f;
    [SerializeField] float _damageToPlayer = 10f;
    [SerializeField] float _damageToEnemy = 10f;   
    
    [SerializeField] float _nextActionTime;
    [SerializeField] string _requiredAction;

    readonly string[] _possibleActions =
    {
        "W", "A", "S", "D", "LeftClick", "RightClick", "Shift"
    };

    [SerializeField] TextMeshProUGUI _enemyHealthText;


    private void Start()
    {
        CreateEnemy();
        _nextActionTime = Time.time + _attackInteraval;
    }

    private void Update()
    {
        if (Time.time >= _nextActionTime)
        {
            ExecutePattern();
            _nextActionTime = Time.time + _attackInteraval;
        }
    }

    public void CreateEnemy()
    {
        _enemyHealth = 100f;
        GenerateRandomPattern();
    }

    private void GenerateRandomPattern()
    {
        _requiredAction = _possibleActions[Random.Range(0, _possibleActions.Length)];
        Debug.Log($"Enemy Pattern: {_requiredAction}");
    }

    private void ExecutePattern()
    {
        GenerateRandomPattern();
    }

    /* 패턴에 따라 플레이어에게 대미지 부여 */
    public void CheckPlayerInput(string playerAction)
    {
        if (playerAction == _requiredAction)
        {
            _enemyHealth -= _damageToEnemy;
            Debug.Log($"Enemy Hit! Enemy Health: {_enemyHealth}");
            _enemyHealthText.text = $"Enemy HP\n{_enemyHealth}";
            if (_enemyHealth <= 0)
            {
                // 적 제거 알림
                _gameManager.OnEnemyDefeated();
            }
        }
        else
        {
            _playerController.TakeDamage(_damageToPlayer);
            Debug.Log("Player Missed!");
        }
    }
}
