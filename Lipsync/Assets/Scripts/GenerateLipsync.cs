using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[RequireComponent(typeof(ReadRadish))]
[RequireComponent(typeof(CreateAnimationCurve))]
public class GenerateLipsync : MonoBehaviour
{
    //Choose where to save your file WITHIN Assets folder
    [SerializeField] public SOVisemeGroups visemeGroup;
    [SerializeField] public string savePath = "Assets/Animation/Lipsync/";

    //Debug things
    [SerializeField] private List<string> nameList;             //Contain the name of the files for naming purposes
    [SerializeField] private List<string> dirList;              //Contain the directory of the files
    [SerializeField] private List<AnimationClip> clipList;      //Contain the final clip, ready to be created as an asset
    private ReadRadish readRadish;
    private CreateAnimationCurve addKeyFrame;

    public void Generate()
    {
        //Since the code is run in the editor, it can't be assigned at Awake()
        readRadish = GetComponent<ReadRadish>();
        addKeyFrame = GetComponent<CreateAnimationCurve>();

        //Clear all List so we can prepare for the next operation
        nameList.Clear();
        dirList.Clear();
        clipList.Clear();

        //The three process to generate it
        OpenPhonemeFolder();
        CreateLipsyncCurve();
        CreateAnimationAsset();
    }


    /// <summary>
    /// Open file explorer to locate a folder which contains the .phoneme files.
    /// Then, load it to the nameList and dirList
    /// </summary>
    private void OpenPhonemeFolder()
    {
        string tempPath = OpenFileExplorer();
        if (tempPath != "")
        {
            DirectoryInfo dir = new DirectoryInfo(tempPath);
            FileInfo[] info = dir.GetFiles("*.phonemes");

            if (info.Length > 0)
            {
                string name = "";
                foreach (FileInfo f in info)
                {
                    dirList.Add(f.FullName);

                    name = (f.Name).Replace(".phonemes", "");
                    nameList.Add(name);
                }
            }
        }
    }

    /// <summary>
    /// Using the files loaded in the dirList, create its keyframe animation curve and load it to clipList
    /// </summary>
    private void CreateLipsyncCurve()
    {
        foreach (var dir in dirList)
        {
            var phonemeData = readRadish.StartRead(dir);
            var animationClip = addKeyFrame.CreateAnimation(phonemeData, visemeGroup);

            clipList.Add(animationClip);
        }
    }

    /// <summary>
    /// Using the clips in the clipList, create an asset and store it in savePath directory
    /// </summary>
    private void CreateAnimationAsset()
    {
        for (int i = 0; i < clipList.Count; i++)
        {
            AssetDatabase.CreateAsset(clipList[i], savePath + nameList[i] + ".anim");
        }
    }

    /// <summary>
    /// This is the function that open the windows dialogue to find the folder
    /// </summary>
    /// <returns></returns>
    private string OpenFileExplorer()
    {
        return EditorUtility.OpenFolderPanel("Find Phonemes Folder", "", "Phonemes");
    }
}
