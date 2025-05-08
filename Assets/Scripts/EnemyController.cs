/**********************************************************
 * Script Name: EnemyController
 * Author: ��켺
 * Date Created: 2025-05-08
 * Last Modified: 2025-05-08
 * Description: 
 * - �� ���� �Լ�
 * - ���� ���� ����
 * - ���Ͽ� ���� ���� ����
 *********************************************************/

using UnityEngine;
using UnityEngine.Android;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _attackInteraval = 3f;
    [SerializeField] float _nextActionTime;
    [SerializeField] string[] _patterns;
    [SerializeField] string _requiredAction;

    private void Start()
    {
        /* ���� ���� ���� �Լ� */
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

    private void ExecutePattern()
    {
        /* ���Ͽ� ���� �÷��̾�� ����� �ο� */
    }
}
