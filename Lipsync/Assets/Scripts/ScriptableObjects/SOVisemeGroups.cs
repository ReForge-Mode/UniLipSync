using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VisemeGroups", menuName = "ScriptableObjects/VisemeGroup 1")]
public class SOVisemeGroups : ScriptableObject
{
    public List<VisemeGroup> visemeGroup;

    public int FindVisemeGroupIndex(string phoneme)
    {
        for (int i = 0; i < visemeGroup.Count; i++)
        {
            for (int j = 0; j < visemeGroup[i].ipa.Count; j++)
            {
                //Debug.Log("Compare : " + visemeGroup[i].ipa[j] + " and " + phoneme + ". Doesn't match");

                if (visemeGroup[i].ipa[j] == phoneme)
                {
                    //Debug.Log("Match Found!");
                    return i;
                }
            }
        }
        Debug.LogWarning("Error find viseme Group. " + phoneme + " doesn't match!");
        return -1;
    }
}

[System.Serializable]
public struct VisemeGroup
{
    public string name;
    public List<string> ipa;
    public Vowel start;
    public Vowel end;
}

[System.Serializable]
public struct Vowel
{
    [Range(0f, 100f)] public float a;
    [Range(0f, 100f)] public float i;
    [Range(0f, 100f)] public float u;
    [Range(0f, 100f)] public float e;
    [Range(0f, 100f)] public float o;
}