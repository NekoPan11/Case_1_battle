using UnityEngine;
public class Player : Character
{
    public static Player Instance { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public override void TakeDamage(float damage)
    {
        float totalDamage = damage;
        totalDamage -= (totalDamage * (incomingDamageReduction / 100));
        currentHealth -= (int)totalDamage;
        if (currentHealth < 0) currentHealth = 0;
        Debug.Log($"{characterName} ������� {damage} �����. �������� ��������: {currentHealth}");
    }
    public override bool IsAlive()
    {
        return currentHealth > 0;
    }
    public override bool CanUseSuper()
    {
        return superGauge >= superThreshold;
    }
    public void AddToSuperGauge(float amount)
    {
        superGauge += amount;
        superGauge = Mathf.Clamp(superGauge, 0, superThreshold);
    }
    public void MeleeAttack(Character target)
    {
        float damage = AttackPower;
        Debug.Log($"����� ������� � ������: {damage}");
        target.TakeDamage(damage);
        BattleManager.Instance.Log($"����� ����� {damage} ����������� �����!");
    }
    public void RangedAttack(Character target)
    {
        float damage = RangedAttackPower;
        Debug.Log($"����� �������� � ������: {damage}");
        target.TakeDamage(damage);
        BattleManager.Instance.Log($"����� ����� {damage} ����� ������� ������!");
    }
    public void MagicAttack(Character target)
    {
        float damage = MagicAttackPower;
        Debug.Log($"����� ������� � ������: {damage}");
        target.TakeDamage(damage);
        BattleManager.Instance.Log($"����� ����� {damage} ����������� �����!");
    }
    public void UseSuper(Character target)
    {
        superGauge = 0;
        float damage = 25f;
        Debug.Log($"����� ���������� �����-���� � ������: {damage}");
        target.TakeDamage(damage);
        BattleManager.Instance.Log($"����� ����� {damage} �����-������!");
    }
    public void SkipTurn()
    {
        Debug.Log("����� ���������� ���.");
    }
}