using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Oliver
    Save:

    Powers Player Has
    Player Upgrades
    ////////////////////Cells Collected
    Current Checkpoint

    Time played

    Load:

    Everything in save and
    Respawn all enemies
    Respawn Player
    Set UpgradeUI

 */

public class SaveAndLoad : MonoBehaviour
{
    public static event Action<SaveAndLoad> RespawnEnemies;
    public static SaveAndLoad _Instance;

    private float lastSaveTime = 0;
    private void Start()
    {
        if (_Instance != null) 
            Destroy(gameObject);
        else
            _Instance = this;

        lastSaveTime = Time.time;
    }

    //[SerializeField] private PlayerUpgrades playerUpgrades;
    //[SerializeField] private PlayerRespawn playerRespawn;
    //[SerializeField] private PlayerAttributes playerAttributes;
    //[SerializeField] private StatUpgrades upgradesUI;
    //[SerializeField] private PlayerPowerHandler playerPowers;

    //[Header("Order Matches order of Powers Enum in PlayerPowerHandler")]
    //[SerializeField] private SOPowers[] allSOPowers;

    public void ClearSave(int pSlot = 0)
    {
        Debug.Log("SaveAndLoad: <color=Orange>ClearSave</color> " + pSlot);
        PlayerPrefs.SetInt(pSlot + "-SlotTaken", 0);
        PlayerPrefs.SetFloat(pSlot + "-TimePlayed", 0);
    }

    public void Save(int pSlot = 0)
    {
        Debug.Log("SaveAndLoad: <color=Orange>Save</color> " + pSlot);
        PlayerPrefs.SetInt(pSlot + "-SlotTaken", 1);
        
        float deltaTimeBetweenSaves = Time.time - lastSaveTime;
        PlayerPrefs.SetFloat(pSlot + "-TimePlayed", deltaTimeBetweenSaves + PlayerPrefs.GetFloat(pSlot + "-TimePlayed"));
        lastSaveTime = Time.time;
        Debug.Log("Timeplayed: " + PlayerPrefs.GetFloat(pSlot + "-TimePlayed"));

        SaveLevels(pSlot);
        SaveSpawnTransform(pSlot);
        SavePowersCollected(pSlot);
        PlayerPrefs.Save();
    }

    public bool LoadSave(int pSlot = 0)
    {
        Debug.Log("SaveAndLoad: <color=Orange>LoadSave</color> " + pSlot);
        //If Slot has not save don't load
        if (PlayerPrefs.GetInt(pSlot + "-SlotTaken") == 0) return false;
        lastSaveTime = Time.time;
        Debug.Log("Timeplayed: " + PlayerPrefs.GetFloat(pSlot + "-TimePlayed"));

        LoadLevels();
        LoadSpawnTransform(pSlot);
        LoadPowersCollected(pSlot);
        QuickLoadSave(pSlot);
        return true;
    }

    public void QuickLoadSave(int pSlot = 0)
    {
        Debug.Log("SaveAndLoad: <color=Orange>QuickLoadSave</color> " + pSlot);
        //Loads Save after dieing. Only resets enemies and player.
        //playerRespawn.Respawn();
        //playerAttributes.Respawn();
        RespawnEnemies(null);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            Save(0);
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            LoadSave(0);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            QuickLoadSave(0);
        }
    }

    // Player Position
    private void SaveSpawnTransform(int pSlot = 0)
    {
        ////Vector3 respawnPoint = playerRespawn.getRespawnPoint();
        //PlayerPrefs.SetFloat(pSlot + "-SpawnPos-x", respawnPoint.x);
        //PlayerPrefs.SetFloat(pSlot + "-SpawnPos-y", respawnPoint.y);
        //PlayerPrefs.SetFloat(pSlot + "-SpawnPos-z", respawnPoint.z);

        ////float respawnRotationY = playerRespawn.getRespawnRotation();
        //PlayerPrefs.SetFloat(pSlot + "-SpawnRot-y", respawnRotationY);
    }

    private void LoadSpawnTransform(int pSlot = 0)
    {
        Vector3 respawnPoint = new Vector3(
            PlayerPrefs.GetFloat(pSlot + "-SpawnPos-x"),
            PlayerPrefs.GetFloat(pSlot + "-SpawnPos-y"),
            PlayerPrefs.GetFloat(pSlot + "-SpawnPos-z"));

        float respawnRotation =
            PlayerPrefs.GetFloat(pSlot + "-SpawnRot-y");

        //playerRespawn.setRespawnTransform(respawnPoint, respawnRotation);
    }

    // Levels
    private void SaveLevels(int pSlot = 0)
    {
        //int[] levels = playerUpgrades.GetStatLevels();
        //for (int i = 0; i < levels.Length; i++)
        //    PlayerPrefs.SetInt(pSlot + "-Stats-" + i, levels[i]);

        //levels = playerUpgrades.GetPowerLevels();
        //for (int i = 0; i < levels.Length; i++)
        //    PlayerPrefs.SetInt(pSlot + "-Powers-" + i, levels[i]);
    }

    private void LoadLevels(int pSlot = 0)
    {
        ////Clear Current Levels
        //playerUpgrades.Setup();
        //upgradesUI.RespawnAllButtons();

        ////Levelup to match save stats
        //for (int i = 0; i < playerUpgrades.GetStatLevels().Length; i++)
        //{
        //    for (int z = 1; z < PlayerPrefs.GetInt(pSlot + "-Stats-" + i); z++)
        //    {
        //        playerUpgrades.LevelUp(i, z);
        //        upgradesUI.RespawnStatsUI(i, z);
        //    }
        //}

        //for (int i = 0; i < playerUpgrades.GetPowerLevels().Length; i++)
        //{
        //    for (int z = 1; z < PlayerPrefs.GetInt(pSlot + "-Powers-" + i); z++)
        //    {
        //        playerUpgrades.PowerUpgrade(i, z);
        //        upgradesUI.RespawnPowersUI(i, z);
        //    }
        //}
    }

    // Powers Collected
    private void SavePowersCollected(int pSlot = 0)
    {
        //List<SOPowers> powers = playerPowers.GetCollectedPowers();

        //for (int i = 0; i < 3; i++)
        //{
        //    //Save -1 if no power
        //    if (powers.Count <= i)
        //        PlayerPrefs.SetInt(pSlot + "-CollectPowers-" + i, -1);
        //    else
        //        PlayerPrefs.SetInt(pSlot + "-CollectPowers-" + i, (int)powers[i].WhichPower);

        //}
    }

    private void LoadPowersCollected(int pSlot = 0)
    {
        //List<SOPowers> powers = new List<SOPowers>();

        //for (int i = 0; i < 3; i++)
        //{
        //    //Get Saved Power
        //    int value = PlayerPrefs.GetInt(pSlot + "-CollectPowers-" + i);

        //    //If -1 no more powers
        //    if (value == -1)
        //        break;

        //    //Add Power
        //    powers.Add(allSOPowers[value]);
        //}

        //playerPowers.Respawn(powers);
    }
}
