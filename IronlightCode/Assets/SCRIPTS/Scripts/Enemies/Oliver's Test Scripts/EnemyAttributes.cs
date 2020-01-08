using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttributes : MonoBehaviour, IAttributes
{
    [SerializeField] private int startHealth = 100;
    private int _health;
    
    [SerializeField] private float healthDisplayLength = 2f;

    private Slider healthSlider;
    [SerializeField] private GameObject enemyHealthBar;

    private bool isDead;

    private Decision _decision;
    private IState _deadState;
    private IState _stunnedState;

    private Summon _mySummoner;

    public bool IsDead() { return isDead; }

    void Start()
    {
        _health = startHealth;
        
        if (enemyHealthBar)
        {
            healthSlider = enemyHealthBar.GetComponentInChildren<Slider>();
            enemyHealthBar.SetActive(false);
        }

        _decision = GetComponent<Decision>();
        _deadState = GetComponent<Dead>();
        _stunnedState = GetComponent<Stunned>();
    }

    public bool TakeDamage(int pAmount, bool pReact)
    {
        _health -= pAmount;

        if (healthSlider)
            healthSlider.value = (float)_health / startHealth;

        if (_health <= 0 && !isDead)
            Death();

        StopCoroutine("ShowHealthbar");
        StartCoroutine("ShowHealthbar");

        //Return If Dead or Not
        if (_health <= 0)
        {
            return true;
        }

        if (pReact && _decision != null)
            _decision.ForceStateSwitch(_stunnedState);

        return false;
    }

    IEnumerator ShowHealthbar()
    {
        enemyHealthBar.SetActive(true);
        yield return new WaitForSeconds(healthDisplayLength);
        enemyHealthBar.SetActive(false);
    }

    void Death()
    {
        isDead = true;
        if (_decision != null)
            _decision.ForceStateSwitch(_deadState);

        // If I was summoned
        if (_mySummoner != null)
            _mySummoner.GruntDied();
    }

    public void SetSummonedGrunt(Summon pSummon)
    {
        _mySummoner = pSummon;
    }

    public void Respawn()
    {
        isDead = false;
        _health = startHealth;

        if (enemyHealthBar)
            enemyHealthBar.SetActive(false);

        // If I was summoned
        if (_mySummoner != null)
            _mySummoner.GruntDied();
    }
}
