using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text m_playerHP;
    [SerializeField] private TMP_Text m_playerAttack;
    [SerializeField] private TMP_Text m_playerDefense;
    [SerializeField] private TMP_Text m_playerSpeed;
    [SerializeField] private TMP_Text m_enemyHP;
    [SerializeField] private TMP_Text m_enemyAttack;
    [SerializeField] private TMP_Text m_enemyDefense;
    [SerializeField] private TMP_Text m_enemySpeed;
    [SerializeField] private Button m_startBtn;

    public static UIManager Instance { get; private set; }
    private void Awake() 
    { 
        Instance = this; 
    }

    private void Start()
    {
        m_startBtn.onClick.AddListener(OnClickStart);
    }

    private void OnClickStart()
    {
        m_startBtn.gameObject.SetActive(false);
        GameRunner.Instance.StartGame();
    }

    public void SetPlayerHp(int hpValue)
    {
        m_playerHP.text = hpValue.ToString();
    }

    public void SetPlayerAttack(int playerAttack)
    {
        m_playerAttack.text = playerAttack.ToString();
    }

    public void SetPlayerDefense(int playerDefense)
    {
        m_playerDefense.text = playerDefense.ToString();
    }

    public void SetPlayerSpeed(int playerSpeed)
    {
        m_playerSpeed.text = playerSpeed.ToString();
    }

    public void SetEnemyHP(int enemyHP)
    {
        m_enemyHP.text = enemyHP.ToString();
    }

    public void SetEnemyAttack(int enemyAttack)
    {
        m_enemyAttack.text = enemyAttack.ToString();
    }

    public void SetEnemyDefense(int enemyDefense)
    {
        m_enemyDefense.text = enemyDefense.ToString();
    }

    public void SetEnemySpeed(int enemySpeed)
    {
        m_enemySpeed.text = enemySpeed.ToString();
    }
}
