using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.AnimationUtility;

public class CreateAnimationCurve : MonoBehaviour
{
    [SerializeField] private AnimationCurve curveA;
    [SerializeField] private AnimationCurve curveI;
    [SerializeField] private AnimationCurve curveU;
    [SerializeField] private AnimationCurve curveE;
    [SerializeField] private AnimationCurve curveO;
    [SerializeField] private List<Keyframe> keyframeListA = new List<Keyframe>();
    [SerializeField] private List<Keyframe> keyframeListI = new List<Keyframe>();
    [SerializeField] private List<Keyframe> keyframeListU = new List<Keyframe>();
    [SerializeField] private List<Keyframe> keyframeListE = new List<Keyframe>();
    [SerializeField] private List<Keyframe> keyframeListO = new List<Keyframe>();

    public AnimationClip CreateAnimation(List<PhonemeData> phonemeData, SOVisemeGroups visemeGroup)
    {
        //Clear all keyframes
        keyframeListA.Clear();
        keyframeListI.Clear();
        keyframeListU.Clear();
        keyframeListE.Clear();
        keyframeListO.Clear();

        //Add each phoneme one by one
        int animLength = phonemeData.Count;
        float timeStart = 0;

        //Add a keyframe zero at the start
        keyframeListA.Add(new Keyframe(0, 0));
        keyframeListI.Add(new Keyframe(0, 0));
        keyframeListU.Add(new Keyframe(0, 0));
        keyframeListE.Add(new Keyframe(0, 0));
        keyframeListO.Add(new Keyframe(0, 0));

        //For every item in PhonemeData list 
        for (int i = 0; i < animLength; i++)
        {
            //Find phoneme group, the start timing, and the end timing
            int groupIndex = visemeGroup.FindVisemeGroupIndex(phonemeData[i].phoneme);
            timeStart = (float)phonemeData[i].start / (float)1000;

            //Add a keyframe, 3 frames before the first phoneme
            if (i == 0)
            {
                keyframeListA.Add(new Keyframe(timeStart - 0.05f, 0));
                keyframeListI.Add(new Keyframe(timeStart - 0.05f, 0));
                keyframeListU.Add(new Keyframe(timeStart - 0.05f, 0));
                keyframeListE.Add(new Keyframe(timeStart - 0.05f, 0));
                keyframeListO.Add(new Keyframe(timeStart - 0.05f, 0));
            }

            //Create a keyframe based on the Start timestamp and value inside the visemeGroup
            keyframeListA.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.a));
            keyframeListI.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.i));
            keyframeListU.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.u));
            keyframeListE.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.e));
            keyframeListO.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.o));
        }

        //Remove overlapping keys with the same timing, if it exists
        TakeMaxKeyframe(keyframeListA);
        TakeMaxKeyframe(keyframeListI);
        TakeMaxKeyframe(keyframeListU);
        TakeMaxKeyframe(keyframeListE);
        TakeMaxKeyframe(keyframeListO);

        //Create a curve for each mouth blendshape
        curveA = new AnimationCurve(keyframeListA.ToArray());
        curveI = new AnimationCurve(keyframeListI.ToArray());
        curveU = new AnimationCurve(keyframeListU.ToArray());
        curveE = new AnimationCurve(keyframeListE.ToArray());
        curveO = new AnimationCurve(keyframeListO.ToArray());

        //Force all curves to be linear. This is to prevent weird values.
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
    }

    public void TakeMaxKeyframe(List<Keyframe> keyframeList)
    {
        //Sometimes there are start and end keyframe that has the exact same timing.
        //This will result in the latest overriding the last.
        //To prevent that, we need to take only the max value keyframe

        for (int i = 0, j = 1; j < keyframeList.Count; i++, j++)
        {
            if (keyframeList[i].time == keyframeList[j].time)
            {
                //Take the max value
                if (keyframeList[i].value > keyframeList[j].value)
                {
                    keyframeList.RemoveAt(j);
                }
                else
                {
                    keyframeList.RemoveAt(i);
                }
            }
        }
    }
}
