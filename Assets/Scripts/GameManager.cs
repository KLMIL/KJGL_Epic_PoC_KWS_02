/**********************************************************
 * Script Name: GameManager
 * Author: ��켺
 * Date Created: 2025-05-08
 * Last Modified: 2025-05-08
 * Description: 
 * - ���� ���۰� ���� �� ���� �帧 ����
 * - �������� Ŭ����� �÷��̾�� ������ ����
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
        // �������� Ŭ����

        // ���� ����
        if (_playerController.CurrHealth <= 0)
        {
            Debug.Log("Game Over");
            /* ���� ���� ó�� �߰� */
        }
    }


    // �������� Ŭ���� ���� ��, ������ �����ִ� �Լ�
    private void ShowRandomChoices()
    {
        
    }
}
