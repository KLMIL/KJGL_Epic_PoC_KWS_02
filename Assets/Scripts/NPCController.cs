/**********************************************************
 * Script Name: NPCController
 * Author: 김우성
 * Date Created: 2025-05-08
 * Last Modified: 2025-05-08
 * Description: 
 * - 적 패턴에 맞는 플레이어 행동을 텍스트로 표시
 *********************************************************/

using TMPro;
using UnityEditor;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;

    [SerializeField] TextMeshProUGUI _briefingText;
    [SerializeField] TextMeshProUGUI _timingText;

    public void UpdateBriefingText(string action)
    {
        string displayText = action switch
        {
            "W" => "Move Up (W)",
            "A" => "Move Left (A)",
            "S" => "Move Down (S)",
            "D" => "Move Right (D)",
            "LeftClick" => "Attack (L Mouse)",
            "RightClick" => "Guard (R Mouse)",
            "Shift" => "Dash (Shift)",
            _ => "Unknown Action"
        };
        _briefingText.text = $"Enemy Action:\n{displayText}";
    }

    public void UpdateTimingText(float timing)
    {
        if (_timingText != null)
        {
            //_timingText.text = $"Timing: {timing:F2}s";
            _timingText.text = "Input Correct Action";
        }
    }

    public void ShowTimingResult(bool success, string message = null)
    {
        if (_timingText != null)
        {
            CancelInvoke(nameof(ResetTimingText));
            _timingText.text = success ? "Success!" : (message ?? "Failed!");
            Invoke(nameof(ResetTimingText), 1f);
        }
    }

    public void ShowChoicePrompt()
    {
        if (_briefingText != null)
        {
            CancelInvoke(nameof(ResetTimingText));
            _briefingText.text = "Choose an Option!";
        }
    }

    private void ResetTimingText()
    {
        UpdateTimingText(0f);
    }
}
