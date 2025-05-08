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
        /* 랜덤 패턴 생성 함수 */
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
        /* 패턴에 따라 플레이어에게 대미지 부여 */
    }
}
