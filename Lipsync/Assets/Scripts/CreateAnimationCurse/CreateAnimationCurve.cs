using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.AnimationUtility;

public class CreateAnimationCurve : MonoBehaviour
{
    //The duration where the mouth should start opening and closing. Keep this low
    public float attackTime = 0.025f;
    public float releaseTime = 0.1f;

    //The duration where the mouth should stay open.
    //If the duration between phoneme is bigger than this range,
    //close the mouth
    public float holdTime = 0.2f; //seconds

    //If there are two phonemes that are too close together,
    //combine them and average them
    public float smoothingTime = 0.010f;

    //Keyframe list contains all keyframes that will be generated as curves later
    [SerializeField] private List<Keyframe> keyframeListA = new List<Keyframe>();
    [SerializeField] private List<Keyframe> keyframeListI = new List<Keyframe>();
    [SerializeField] private List<Keyframe> keyframeListU = new List<Keyframe>();
    [SerializeField] private List<Keyframe> keyframeListE = new List<Keyframe>();
    [SerializeField] private List<Keyframe> keyframeListO = new List<Keyframe>();

    //Curves are the graphs in an animation.
    //Each curves correspond to one Blendshape values.
    //We'll convert keyframe list as curves in the animation.
    [SerializeField] private AnimationCurve curveA;
    [SerializeField] private AnimationCurve curveI;
    [SerializeField] private AnimationCurve curveU;
    [SerializeField] private AnimationCurve curveE;
    [SerializeField] private AnimationCurve curveO;

    public AnimationClip CreateAnimation(List<PhonemeData> phonemeData, VisemeGroupInfo visemeGroup)
    {
        #region Create Keyframe List

        //Clear all keyframes
        keyframeListA.Clear();
        keyframeListI.Clear();
        keyframeListU.Clear();
        keyframeListE.Clear();
        keyframeListO.Clear();

        //For every item in PhonemeData list 
        int phonemeCount = phonemeData.Count;
        for (int i = 0; i < phonemeCount; i++)
        {
            //Find phoneme group, the start timing
            int visemeGroupIndex = visemeGroup.FindVisemeGroupIndex(phonemeData[i].phoneme);

            #region Create a Fade In transition before phoneme is played
            //If the mouth is fully closed,
            //create a 0,0 keyframe as a transition to slowly open it up.
            
            float prevPhonemeEndTime = (i > 0) ? (float)phonemeData[i - 1].end / (float)1000 : 0;
            float currentPhonemeStartTime = (float)phonemeData[i].start / (float)1000;
            float timeBetweenPhoneme = currentPhonemeStartTime - prevPhonemeEndTime;

            if (i == 0 || timeBetweenPhoneme > holdTime)
            {
                keyframeListA.Add(new Keyframe(currentPhonemeStartTime - attackTime, 0));
                keyframeListI.Add(new Keyframe(currentPhonemeStartTime - attackTime, 0));
                keyframeListU.Add(new Keyframe(currentPhonemeStartTime - attackTime, 0));
                keyframeListE.Add(new Keyframe(currentPhonemeStartTime - attackTime, 0));
                keyframeListO.Add(new Keyframe(currentPhonemeStartTime - attackTime, 0));
            }
            #endregion

            #region Create the keyframe for this phoneme
            //Create a keyframe based on the Start timestamp and value inside the visemeGroup
            keyframeListA.Add(new Keyframe(currentPhonemeStartTime, visemeGroup.visemeGroupList[visemeGroupIndex].blendshapeValues.a));
            keyframeListI.Add(new Keyframe(currentPhonemeStartTime, visemeGroup.visemeGroupList[visemeGroupIndex].blendshapeValues.i));
            keyframeListU.Add(new Keyframe(currentPhonemeStartTime, visemeGroup.visemeGroupList[visemeGroupIndex].blendshapeValues.u));
            keyframeListE.Add(new Keyframe(currentPhonemeStartTime, visemeGroup.visemeGroupList[visemeGroupIndex].blendshapeValues.e));
            keyframeListO.Add(new Keyframe(currentPhonemeStartTime, visemeGroup.visemeGroupList[visemeGroupIndex].blendshapeValues.o));

            //Also create keyframe for the End of the phoneme
            keyframeListA.Add(new Keyframe(currentPhonemeStartTime, visemeGroup.visemeGroupList[visemeGroupIndex].blendshapeValues.a));
            keyframeListI.Add(new Keyframe(currentPhonemeStartTime, visemeGroup.visemeGroupList[visemeGroupIndex].blendshapeValues.i));
            keyframeListU.Add(new Keyframe(currentPhonemeStartTime, visemeGroup.visemeGroupList[visemeGroupIndex].blendshapeValues.u));
            keyframeListE.Add(new Keyframe(currentPhonemeStartTime, visemeGroup.visemeGroupList[visemeGroupIndex].blendshapeValues.e));
            keyframeListO.Add(new Keyframe(currentPhonemeStartTime, visemeGroup.visemeGroupList[visemeGroupIndex].blendshapeValues.o));
            #endregion

            #region Create a Fade Out transition after the phoneme is played
            //If the duration to the next phoneme is longer than the hold time,
            //create a 0,0 keyframe transition to close the mouth

            float nextPhonemeStartTime = ((i + 1) < phonemeCount) ? (float)phonemeData[i+1].start / (float)1000 : 0;
            float currentPhonemeEndTime = (float)phonemeData[i].end / (float)1000;
            timeBetweenPhoneme = nextPhonemeStartTime - currentPhonemeEndTime;

            if (i == phonemeCount - 1 || timeBetweenPhoneme > holdTime)
            {
                keyframeListA.Add(new Keyframe(currentPhonemeEndTime + releaseTime, 0));
                keyframeListI.Add(new Keyframe(currentPhonemeEndTime + releaseTime, 0));
                keyframeListU.Add(new Keyframe(currentPhonemeEndTime + releaseTime, 0));
                keyframeListE.Add(new Keyframe(currentPhonemeEndTime + releaseTime, 0));
                keyframeListO.Add(new Keyframe(currentPhonemeEndTime + releaseTime, 0));
            }
            #endregion
        }

        #endregion

        #region Optimization: Remove Phonemes that are too close to each other
        //This is to make the mouth movement smoother,
        //so the mouth doesn't move too quickly
        for (int i = keyframeListA.Count - 2; i > 0; i--)
        {
            if(i > 0 && (i+1) < keyframeListA.Count)
            {
                //Skip keyframes where all keyframes are zero
                if ((keyframeListA[i].value == 0 &&
                     keyframeListI[i].value == 0 &&
                     keyframeListU[i].value == 0 &&
                     keyframeListE[i].value == 0 &&
                     keyframeListO[i].value == 0) ||
                    (keyframeListA[i+1].value == 0 &&
                     keyframeListI[i+1].value == 0 &&
                     keyframeListU[i+1].value == 0 &&
                     keyframeListE[i+1].value == 0 &&
                     keyframeListO[i+1].value == 0))
                {
                    continue;
                }

                //Find two keyframes that are less than smoothingTime apart.
                float timeGap = keyframeListA[i + 1].time - keyframeListA[i].time;
                if (timeGap < smoothingTime)
                {
                    float newTime = keyframeListA[i].time + (timeGap / 2);

                    //Combine two keyframe into one, average the result
                    keyframeListA[i] = new Keyframe(newTime, (keyframeListA[i].value + keyframeListA[i + 1].value) / 2);
                    keyframeListI[i] = new Keyframe(newTime, (keyframeListI[i].value + keyframeListI[i + 1].value) / 2);
                    keyframeListU[i] = new Keyframe(newTime, (keyframeListU[i].value + keyframeListU[i + 1].value) / 2);
                    keyframeListE[i] = new Keyframe(newTime, (keyframeListE[i].value + keyframeListE[i + 1].value) / 2);
                    keyframeListO[i] = new Keyframe(newTime, (keyframeListO[i].value + keyframeListO[i + 1].value) / 2);

                    //Remove the i+1 keyframe
                    keyframeListA.RemoveAt(i + 1);
                    keyframeListI.RemoveAt(i + 1);
                    keyframeListU.RemoveAt(i + 1);
                    keyframeListE.RemoveAt(i + 1);
                    keyframeListO.RemoveAt(i + 1);
                }
            }
        }
        #endregion

        #region Create Animation Curves from KeyframeList

        //Create a curve for each mouth blendshape
        curveA = new AnimationCurve(keyframeListA.ToArray());
        curveI = new AnimationCurve(keyframeListI.ToArray());
        curveU = new AnimationCurve(keyframeListU.ToArray());
        curveE = new AnimationCurve(keyframeListE.ToArray());
        curveO = new AnimationCurve(keyframeListO.ToArray());

        //Force all curves to be linear to prevent weird values.
        //Sometimes, the curve could go below zero.
        for (int i = 0; i < curveA.keys.Length; i++)
        {
            SetKeyLeftTangentMode(curveA, i, AnimationUtility.TangentMode.Linear);
            SetKeyLeftTangentMode(curveI, i, AnimationUtility.TangentMode.Linear);
            SetKeyLeftTangentMode(curveU, i, AnimationUtility.TangentMode.Linear);
            SetKeyLeftTangentMode(curveE, i, AnimationUtility.TangentMode.Linear);
            SetKeyLeftTangentMode(curveO, i, AnimationUtility.TangentMode.Linear);
            SetKeyRightTangentMode(curveA, i, AnimationUtility.TangentMode.Linear);
            SetKeyRightTangentMode(curveI, i, AnimationUtility.TangentMode.Linear);
            SetKeyRightTangentMode(curveU, i, AnimationUtility.TangentMode.Linear);
            SetKeyRightTangentMode(curveE, i, AnimationUtility.TangentMode.Linear);
            SetKeyRightTangentMode(curveO, i, AnimationUtility.TangentMode.Linear);
        }

        //Make the curve into an animationClip, ready to be made into an asset
        AnimationClip clip = new AnimationClip();
        clip.legacy = false;
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_A", curveA);
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_I", curveI);
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_U", curveU);
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_E", curveE);
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_O", curveO);

        return clip;

        #endregion
    }
}
