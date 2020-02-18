using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle ScriptableObject", menuName = "Puzzle/New Puzzle Preset", order = 5)]
public class PuzzleSO : ScriptableObject
{
    public string PuzzleTagName;

    public enum PuzzleType
    {
        ConnectBeam,
        ActivateCrystals,
        None
    }

    public PuzzleType CurrType = PuzzleType.None;
    public float CrystalCoolDown;
}
