/**********************************************************
 * Script Name: PlayerController
 * Author: 김우성
 * Date Created: 2025-05-08
 * Last Modified: 2025-05-08
 * Description: 
 * - 플레이어의 입력, 체력 상태 관리
 * - 모든 행위의 딜레이 정보 관리
 *********************************************************/

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _maxHealth = 100f;
    [SerializeField] float _currHealth;
    public float CurrHealth => _currHealth;


    /* 각 행동 딜레이 필드 추가? 타이밍 재는 방법 */


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
        /* 사용자 키 입력받는 부분 */
    }

    private void ProcessAction()
    {
        /* 키 입력에 따라 결과 처리하는 부분 */
    }

    public void TakeDamage(float damage)
    {
        _currHealth -= damage;
        // UI 갱신
        Debug.Log($"Player Health: {_currHealth}");
    }

    public void ChangeDelay()
    {
        /* 함수 파라미터에 액션 종류와 딜레이 변화값 추가 */
        /* 선택지 뽑으면 딜레이 변경 부분 */
    }
}
