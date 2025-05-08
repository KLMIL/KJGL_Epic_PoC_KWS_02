/**********************************************************
 * Script Name: NPCController
 * Author: 김우성
 * Date Created: 2025-05-08
 * Last Modified: 2025-05-08
 * Description: 
 * - 적 패턴에 맞는 플레이어 행동을 텍스트로 표시
 *********************************************************/

using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _briefingText;
    [SerializeField] EnemyController _enemyController;

    private void Update()
    {
        /* 적에 행동에 맞는 행동 텍스트 표시 */
    }
}
