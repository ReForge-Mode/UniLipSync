using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;

[RequireComponent(typeof(ReadRadish))]
[RequireComponent(typeof(CreateAnimationCurve))]
public class GenerateLipsync : MonoBehaviour
{
    //Choose where to save your file WITHIN Assets folder
    [SerializeField] public VisemeGroupInfo visemeGroup;
    [SerializeField] public string savePath = "Assets/Animation/Lipsync/";

    [SerializeField] public bool isAddToAnimator = false;
    [SerializeField] public AnimatorController animator;

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
            string name = nameList[i];
            string path = savePath + name + ".anim";

            AssetDatabase.CreateAsset(clipList[i], path);

            //Add this into the animator automatically
            if(isAddToAnimator == true)
            {
                AddAnimationIntoAnimator(path, name);
            }
        }
    }

    /// <summary>
    /// This function automatically adds the animation
    /// into the animator so we can test it.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    public void AddAnimationIntoAnimator(string path, string name)
    {
        //Find the Animation Clip first
        AnimationClip loadedClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);

        if (animator == null)
        {
            Debug.LogError("Animator not set!");
            return;
        }

        // Check if a state with the same name already exists
        AnimatorStateMachine stateMachine = animator.layers[1].stateMachine;
        AnimatorState stateToRemove = null;
        foreach (ChildAnimatorState childState in stateMachine.states)
        {
            if (childState.state.name == name)
            {
                stateToRemove = childState.state;
                break;
            }
        }
        if (stateToRemove != null)
        {
            // Remove the existing clip
            stateMachine.RemoveState(stateToRemove);
        }

        // Add the new animation clip to the state machine
        AnimatorState newState = stateMachine.AddState(name);
        newState.motion = loadedClip;


        #region Create Transition from this state to Empty State

        // Get the "Empty State" state from the state machine
        AnimatorState emptyState = null;
        foreach (ChildAnimatorState childState in stateMachine.states)
        {
            if (childState.state.name == "Empty State")
            {
                emptyState = childState.state;
                break;
            }
        }

        if (emptyState != null)
        {
            // Create a transition from the new state to the "Empty State"
            AnimatorStateTransition transition = newState.AddTransition(emptyState);
            transition.duration = loadedClip.length;
            transition.hasExitTime = true;
            transition.exitTime = 1.0f;
        }
        else
        {
            Debug.LogError("Empty State not found!");
        }
        #endregion
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
