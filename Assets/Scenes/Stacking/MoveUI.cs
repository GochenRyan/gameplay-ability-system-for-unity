using Assets.GAS.Runtime.Core;
using GAS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveUI : MonoBehaviour
{
    public static MoveUI Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_startBtn.onClick.AddListener(OnClickStart);
        m_moveNextBtn.onClick.AddListener(OnMoveNext);
        m_attackUpBtn.onClick.AddListener(OnAttackUp);
    }

    private void OnClickStart()
    {
        m_startBtn.gameObject.SetActive(false);
        MoveRunner.Instance.StartGame();
    }

    private void OnMoveNext()
    {
        MoveRunner.Instance.MoveNext();
        m_currentMove.text = GasHost.CurrentMove.ToString();
    }

    private void OnAttackUp()
    {
        MoveRunner.Instance.AddAttackUpBuff();
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

    [SerializeField] private TMP_Text m_playerHP;
    [SerializeField] private TMP_Text m_playerAttack;
    [SerializeField] private TMP_Text m_playerDefense;
    [SerializeField] private TMP_Text m_playerSpeed;
    [SerializeField] private TMP_Text m_currentMove;
    [SerializeField] private Button m_startBtn;
    [SerializeField] private Button m_moveNextBtn;
    [SerializeField] private Button m_attackUpBtn;
}
