using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

// Lead Programmer: Oliver Loescher
// Additional Programmers: null
// Description: Stores player health & Spirit and handles the changing of these variables and death of the player

public class PLY_Attributes : MonoBehaviour, IAttributes
{
    //private AnimHandler _anim;

    private int _iMaxHealth = 100;
    private int _iMaxSpirit = 10;

    private int _iHealth;
    private int _iSpirit;

    //[Space]
    //[SerializeField] private float _powerLossStartDelaySeconds = 8f;
    //[SerializeField] private float _powerLossDelaySeconds = 0.3f;
    //[SerializeField] private int _powerLossAmount = 1;

    public bool IsDead() { return true; }

    public void Init(int pMaxHealth, int pMaxSpirit)
    {
        //_anim = GetComponentInChildren<AnimHandler>();

        setMaxHealth(pMaxHealth);
        setMaxSpirit(pMaxSpirit);

        _iHealth = _iMaxHealth;
        _iSpirit = _iMaxSpirit;
    }

    public void Respawn()
    {
        setHealth(_iMaxHealth);
        setSpirit(_iMaxSpirit);
    }

    //GET
    public int getHealth() { return _iHealth; }
    public int getSpirit() { return _iSpirit; }

    //SET
    public void setHealth(int pHealth) { modifyHealth(pHealth - _iHealth); }
    public void setSpirit(int pSpirit) { modifySpirit(pSpirit - _iSpirit); }

    //MODIFY VARS ///////////////////////////////////////////////////////////////////////////////////////////
    public void modifyHealth(int x)
    {
        //Changing Value
        _iHealth += x;
        _iHealth = Mathf.Clamp(_iHealth, 0, _iMaxHealth);

        // TODO Changing Visuals
    }

    public void modifySpirit(int x)
    {
        //Changing Value
        _iSpirit += x;
        _iSpirit = Mathf.Clamp(_iSpirit, 0, _iMaxSpirit);

        // TODO Changing Visuals
    }

    //MODIFY MAXES
    public void setMaxHealth(int pMaxHealth)
    {
        //Change Value
        _iMaxHealth = pMaxHealth;

        // TODO Change respective bar length

        modifyHealth(_iMaxHealth);
    }

    public void setMaxSpirit(int pMaxPowerGuage)
    {
        //Change Value
        _iMaxSpirit = pMaxPowerGuage;

        // TODO Change respective bar length
    }

    //GENERAL FUNCTIONS ///////////////////////////////////////////////////////////////////////////////////////////
    public bool TakeDamage(int pAmount, bool pReact)
    {
        // Return if already dead
        if (_iHealth <= 0)
            return true;

        Debug.Log("Damaging Player " + pAmount);

        //Changing Health by remainder
        modifyHealth(-pAmount);

        //Restarting Spirit Loss over time
        //if (_spirit > 0)
        //{
        //    StopCoroutine("spiritLoss");
        //    StopCoroutine("spiritLossStartDelay");
        //    StartCoroutine("spiritLossStartDelay");
        //}

        //if (pReact)
        //    _anim.Stunned(Random.value < 0.5f);

        //Return If Dead or Not
        if (_iHealth <= 0)
        {
            //Camera Shake Dead
            CameraShaker.Instance.ShakeOnce(20, 4, 0.4f, 0.3f);
            return true;
        }

        //Camera Shake Hurt
        CameraShaker.Instance.ShakeOnce(1, 2, 0.2f, 0.1f);
        return false;
    }

    public void ReciveSpirit(int pSpirit)
    {
        modifySpirit(pSpirit);
        //Debug.Log("Power Recieved: " + pPower + ", " + _power);

        //Restarting Power Loss over time
        if (_iSpirit > 0)
        {
            StopCoroutine("spiritLoss");
            StopCoroutine("spiritLossStartDelay");
            StartCoroutine("spiritLossStartDelay");
        }
    }

    //COROUTINES ///////////////////////////////////////////////////////////////////////////////////////////

    ////Spirit
    //private IEnumerator spiritLossStartDelay()
    //{
    //    yield return new WaitForSeconds(_powerLossStartDelaySeconds);
    //    StartCoroutine("powerLoss");
    //}

    //private IEnumerator spiritLoss()
    //{
    //    while (_iSpirit > 0)
    //    {
    //        //Debug.Log("Power lost: " + _power);
    //        modifySpirit(-_powerLossAmount);
    //        yield return new WaitForSeconds(_powerLossDelaySeconds);
    //    }
    //}
}
