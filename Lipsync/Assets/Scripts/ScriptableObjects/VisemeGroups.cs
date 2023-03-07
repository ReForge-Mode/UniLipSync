using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Viseme Group Info", menuName = "ScriptableObjects/Viseme Group Info")]
public class VisemeGroupInfo : ScriptableObject
{
    [FormerlySerializedAs("visemeGroups")]
    public List<VisemeGroup> visemeGroupList;

    /// <summary>
    /// Find a VisemeGroup that contains this phoneme.
    /// Return the index of the phoneme.
    /// </summary>
    /// <param name="phoneme"></param>
    /// <returns></returns>
    public int FindVisemeGroupIndex(string phoneme)
    {
        for (int i = 0; i < visemeGroupList.Count; i++)
        {
            for (int j = 0; j < visemeGroupList[i].ipa.Count; j++)
            {
                //Debug.Log("Compare : " + visemeGroup[i].ipa[j] + " and " + phoneme + ". Doesn't match");

                if (visemeGroupList[i].ipa[j] == phoneme)
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
    public BlendshapeValues blendshapeValues;
}

[System.Serializable]
public struct BlendshapeValues
{
    //NOTE: If you have more Blendshapes for the mouth movement,
    //you can add them here.

    [Range(0f, 100f)] public float a;
    [Range(0f, 100f)] public float i;
    [Range(0f, 100f)] public float u;
    [Range(0f, 100f)] public float e;
    [Range(0f, 100f)] public float o;
}