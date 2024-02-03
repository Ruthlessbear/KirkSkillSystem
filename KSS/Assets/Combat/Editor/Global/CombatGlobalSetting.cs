using UnityEngine;

[CreateAssetMenu(fileName = "CombatSetting_Default", menuName = "Combat/Setting")]
public class CombatGlobalSetting : ScriptableObject
{
    public string RootPath = "Combat";

    public string ProcessRootPath = "Assets";
}
