using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviourPunCallbacks
{
    [SerializeField] private float maxHealth = 100f;

    private PhotonView pv;
        
    private float currentHealth;
    private string color;

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = Mathf.Clamp(value, 0f, maxHealth);
            if (currentHealth <= 0f)
            {
                Die();
            }
        }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    private void Awake()
    {   
        pv = GetComponent<PhotonView>();
        CurrentHealth = MaxHealth;
    }
    private void Start()
    {
        if (pv.IsMine)
        {   
            pv.RPC("UpdateHealthStart", RpcTarget.All, MaxHealth, pv.ViewID);
        }
    }
    [PunRPC]
    public void MasterClientTakeDamage(float amount, int viewHealthID)
    {
        pv.RPC("TakeDamageAndSync", RpcTarget.All, amount, viewHealthID);
    }
    [PunRPC]
    public void TakeDamageAndSync(float amount, int viewHealthID)
    {
        CurrentHealth -= amount;
        float filledAmount = CurrentHealth / MaxHealth;
        UpdateUIHealth(currentHealth, viewHealthID, filledAmount);
    }
    private HealthBar FindHealthBar(int viewHealthID)
    {
        HealthBar[] healthBars = FindObjectsOfType<HealthBar>();
        foreach (HealthBar healthBar in healthBars)
        {
            if (healthBar.photonView.ViewID == viewHealthID)
            {
                return healthBar;
            }
        }
        return null;
    }
    private HealthAmount FindHealthAmount(int viewHealthAmount)
    {
        HealthAmount[] healthAmounts = FindObjectsOfType<HealthAmount>(); 
        foreach(HealthAmount healthAmount in healthAmounts )
        {
            if(healthAmount.photonView.ViewID == viewHealthAmount)
            {
                return healthAmount;
            }
        }
        return null;
    }
    [PunRPC]
    public void UpdateHealthStart(float currentHealth,int viewHealthID)
    {
        UpdateUIHealth(currentHealth,viewHealthID,1f);
    }
    private void UpdateUIHealth(float currentHealth,int viewHealthID, float filledAmount)
    {
        HealthBar healthBar = FindHealthBar(viewHealthID);
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(filledAmount, viewHealthID);

        }
        HealthAmount healthAmount = FindHealthAmount(viewHealthID);
        if (healthAmount != null)
        {
            healthAmount.UpdateHealthAmount(currentHealth, viewHealthID);

        }

    }
    private void Die()
    {
        ScoreUI();
        pv.RPC("MasterRevival", RpcTarget.MasterClient);
        EventDie();
    }
    void EventDie()
    {
        DeadExplosionPool.Instance.SpawnExplosion(transform.position);
        //RevivalPlayer.instance.ResetPlayerPosition(pv.Owner.ActorNumber, color);
        CurrentHealth = MaxHealth;
    }
    [PunRPC]
    public void MasterRevival()
    {
        pv.RPC("RevivalPlayerRPC", RpcTarget.All);
       // RevivalPlayer.instance.ResetPlayerPosition(pv.Owner.ActorNumber, color);
    }
    [PunRPC]
    public void RevivalPlayerRPC()
    {
        RevivalPlayer.instance.ResetPlayerPosition(pv.Owner.ActorNumber, color);
    }
    void ScoreUI()
    {
        UpdatePropertiesPlayer.Instance.GetColor(ref color, pv.Owner.ActorNumber);
        UpdateUiScore(color);

    }
    public void UpdateHealthRevival()
    {
        if (pv.IsMine)
        {
            pv.RPC("UpdateHealthStart", RpcTarget.All, MaxHealth, pv.ViewID);
        }
    }
    [PunRPC]
    public void UpdateUiScore(string color)
    {
        UiGameManager.Instance.ScorebarUI(color);
    }
    public void IncreaseHealth(float amount)
    {
        CurrentHealth += amount;
    }
    public void DoubleHealth(float duration)
    {
        StartCoroutine(DoubleHealthCoroutine(duration));
    }
    private IEnumerator DoubleHealthCoroutine(float duration)
    {
        float originalMaxHealth = MaxHealth;
        MaxHealth *= 2f;
        yield return new WaitForSeconds(duration);
        MaxHealth = originalMaxHealth;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            RevivalPlayer.instance.ResetPlayerPosition(pv.Owner.ActorNumber, color);
        }
    }
}
