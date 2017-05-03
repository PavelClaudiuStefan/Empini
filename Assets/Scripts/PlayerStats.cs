using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Only commands/rpc's are allowed to use direct var's for stats
/// Use the getter/setter if not in Command/Rpc
/// </summary>
public class PlayerStats : NetworkBehaviour {

    public TextMesh hpBar;

    public int _healthIncreas = 10; // 10 hp per point
    public int _maxPoints = 1000; // 6 points per stat

    private int currentPoints = 1000; // 10 momentan

    private int playerHp = 100;
    private float bulletSpeed = 5;
    private int bulletDamage = 5;
    private int maxHealth = 100;
    private int healthRegen = 1; // 1 regen per second
    private int movementSpeed = 4;

    private int[] pointsPerStats;
    private Dictionary<string, int> pointsPerStat;

    void Start()
    {
        if (!isLocalPlayer)
            return;

        StartCoroutine(HealthTick());

        pointsPerStat = new Dictionary<string, int>();

        pointsPerStat.Add("BulletSpeed", 0);
        pointsPerStat.Add("BulletDamage", 0);
        pointsPerStat.Add("MaxHealth", 0);
        pointsPerStat.Add("HealthRegen", 0);
        pointsPerStat.Add("MovementSpeed", 0);

    }
    
    #region Getters/Setters
    public int CurrentPoints
    {
        get { return currentPoints; }
        set
        {
            currentPoints = value;

            if (isServer)
                CmdCurrentPoints(value);
            else
                RpcCurrentPoints(value);
        }
    }
    public int PlayerHp
    {
        get { return playerHp; }
        set
        {
            playerHp = value;

            if (playerHp > maxHealth)
                playerHp = maxHealth;

            hpBar.text = playerHp.ToString();
            if (isLocalPlayer)
                EventManager.instance.Raise(new SetHpUiEvent(playerHp));

            if (isServer)
                RpcPlayerHp(value);
            else
                CmdPlayerHp(value);
        }
    }
    public float BulletSpeed
    {
        get { return bulletSpeed; }
        set {
            if (currentPoints > 0 && pointsPerStat["BulletSpeed"] < _maxPoints)
            {

                CurrentPoints--;
                pointsPerStat["BulletSpeed"]++;

                bulletSpeed = value;

                if (isServer)
                    RpcBulletSpeed(value);
                else
                    CmdBulletSpeed(value);
            }
        }
    }
    public int BulletDamage
    {
        get { return bulletDamage; }
        set {
            if (currentPoints > 0 && pointsPerStat["BulletDamage"] < _maxPoints)
            {
                CurrentPoints--;
                pointsPerStat["BulletDamage"]++;

                bulletDamage = value;

                if (isServer)
                    RpcBulletDamage(value);
                else
                    CmdBulletDamage(value);
            }
        }
    }
    public void MaxHealthIncreas() {

        if (currentPoints > 0 && pointsPerStat["MaxHealth"] < _maxPoints)
        {
            CurrentPoints--;
            pointsPerStat["MaxHealth"]++;

            maxHealth += _healthIncreas;

            if (isServer)
                RpcMaxHealthIncreas();
            else
                CmdMaxHealthIncreas();
        }
    }
    public int HealthRegen {
        get { return healthRegen; }
        set
        {
            if (currentPoints > 0 && pointsPerStat["HealthRegen"] < _maxPoints)
            {
                CurrentPoints--;
                pointsPerStat["HealthRegen"]++;

                healthRegen = value;

                if (isServer)
                    RpcHealthRegen(value);
                else
                    CmdHealthRegen(value);
            }
        }
    }
    public int MovementSpeed
    {

        get{ return movementSpeed; }
        set
        {

            if (currentPoints > 0 && pointsPerStat["MovementSpeed"] < _maxPoints)
            {
                CurrentPoints--;
                pointsPerStat["MovementSpeed"]++;

                movementSpeed = value;

                if (isServer)
                    RpcMovementSpeed(value);
                else
                    CmdMovementSpeed(value);
            }
        }
    }
    #endregion

    #region Commands/Rpc's per getter/setter
    [Command]
    void CmdCurrentPoints(int value)
    {
        currentPoints = value;

        RpcCurrentPoints(value);
    }
    [ClientRpc]
    void RpcCurrentPoints(int value)
    {
        if (!isServer)
            currentPoints = value;
    }
    
    [Command]
    void CmdPlayerHp(int value)
    {
        playerHp = value;
        hpBar.text = playerHp.ToString();
        if (isLocalPlayer)
            EventManager.instance.Raise(new SetHpUiEvent(playerHp));

        RpcPlayerHp(value);
    }
    [ClientRpc]
    void RpcPlayerHp(int value)
    {
        if(!isServer)
        {
            playerHp = value;
            hpBar.text = playerHp.ToString();
            if (isLocalPlayer)
                EventManager.instance.Raise(new SetHpUiEvent(playerHp));
        }
    }

    [Command]
    void CmdBulletSpeed(float value)
    {
        bulletSpeed = value;

        RpcBulletSpeed(value);
    }
    [ClientRpc]
    void RpcBulletSpeed(float value)
    {
        if (!isServer)
        {
            bulletSpeed = value;
        }
    }

    [Command]
    void CmdBulletDamage(int value)
    {
        bulletDamage = value;

        RpcBulletDamage(value);
    }
    [ClientRpc]
    void RpcBulletDamage(int value)
    {
        if (!isServer)
        {
            bulletDamage = value;
        }
    }

    [Command]
    void CmdMaxHealthIncreas()
    {
        maxHealth += _healthIncreas ;

        RpcMaxHealthIncreas();
    }
    [ClientRpc]
    void RpcMaxHealthIncreas()
    {
        if (!isServer)
        {
            maxHealth += _healthIncreas;
        }
    }

    [Command]
    void CmdHealthRegen(int value)
    {
        healthRegen = value;

        RpcHealthRegen(value);
    }
    [ClientRpc]
    void RpcHealthRegen(int value)
    {
        if (!isServer)
        {
            healthRegen = value;
        }
    }

    [Command]
    void CmdMovementSpeed(int value)
    {
        movementSpeed = value;

        RpcMovementSpeed(value);
    }
    [ClientRpc]
    void RpcMovementSpeed(int value)
    {
        if (!isServer)
            movementSpeed = value;
    }
    #endregion

    IEnumerator HealthTick()
    {
        while(true) // momentan
        {
            yield return new WaitForSeconds(1);
            PlayerHp += HealthRegen;
        }
    }
    
}
