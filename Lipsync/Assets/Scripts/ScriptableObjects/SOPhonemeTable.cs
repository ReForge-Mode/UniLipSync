using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhonemeTable", menuName = "ScriptableObjects/PhonemeTable")]
public class SOPhonemeTable : ScriptableObject
{
    public List<PhonemePair> phonemeGroups;

    public string FindIPAGroup(string ipa)
    {
        for (int i = 0; i < phonemeGroups.Count; i++)
        {
            if(ipa == phonemeGroups[i].ipa)
            {
                return phonemeGroups[i].group;
            }
        }
        Debug.LogWarning("Error Find IPA Group. " + ipa + " doesn't match!");
        return null;
    }
}

[System.Serializable]
public struct PhonemePair
{
    public string ipa;
    public string group;
}