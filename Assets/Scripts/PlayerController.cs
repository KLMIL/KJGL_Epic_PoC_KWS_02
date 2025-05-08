/**********************************************************
 * Script Name: PlayerController
 * Author: ��켺
 * Date Created: 2025-05-08
 * Last Modified: 2025-05-08
 * Description: 
 * - �÷��̾��� �Է�, ü�� ���� ����
 * - ��� ������ ������ ���� ����
 *********************************************************/

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _maxHealth = 100f;
    [SerializeField] float _currHealth;
    public float CurrHealth => _currHealth;


    /* �� �ൿ ������ �ʵ� �߰�? Ÿ�̹� ��� ��� */


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
        /* ����� Ű �Է¹޴� �κ� */
    }

    private void ProcessAction()
    {
        /* Ű �Է¿� ���� ��� ó���ϴ� �κ� */
    }

    public void TakeDamage(float damage)
    {
        _currHealth -= damage;
        // UI ����
        Debug.Log($"Player Health: {_currHealth}");
    }

    public void ChangeDelay()
    {
        /* �Լ� �Ķ���Ϳ� �׼� ������ ������ ��ȭ�� �߰� */
        /* ������ ������ ������ ���� �κ� */
    }
}
