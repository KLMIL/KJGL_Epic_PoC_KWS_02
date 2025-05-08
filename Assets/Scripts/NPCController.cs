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
using UnityEngine.Scripting.APIUpdating;

public class NPCController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _briefingText;

    public void UpdateBriefingText(string action)
    {
        string displayText = action switch
        {
            "W" => "Move Up",
            "A" => "Move Left",
            "S" => "Move Down",
            "D" => "Move Right",
            "LeftClick" => "Attack (Left Click)",
            "Right Click" => "Guard (Right Click)",
            "Shift" => "Dash (Shift)",
            _ => "Unknown Action"
        };
        _briefingText.text = $"Enemy Action:\n{displayText}";
    }
}
