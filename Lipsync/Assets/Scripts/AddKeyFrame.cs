using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using static UnityEditor.AnimationUtility;

public class AddKeyFrame : MonoBehaviour
{
    public ReadRadish readRadish;
    public SOVisemeGroups visemeGroup;
    public bool forcedLinear = false;
    public bool useLegacy = false;
    public bool noEnd = false;

    [Header("Debug")]
    public AnimationCurve curveA;
    public AnimationCurve curveI;
    public AnimationCurve curveU;
    public AnimationCurve curveE;
    public AnimationCurve curveO;
    public List<Vector2> keyframesA;
    public List<Vector2> keyframesE;
    public List<Vector2> keyframesI;
    public List<Vector2> keyframesU;
    public List<Vector2> keyframesO;

    public void CreateAnimation()
    {
        List<Keyframe> keyframeListA = new List<Keyframe>();
        List<Keyframe> keyframeListI = new List<Keyframe>();
        List<Keyframe> keyframeListU = new List<Keyframe>();
        List<Keyframe> keyframeListE = new List<Keyframe>();
        List<Keyframe> keyframeListO = new List<Keyframe>();

        

        //Add each phoneme one by one
        int animLength = readRadish.phonemeDataList.Count;
        float time = 0;
        float timeStart = 0;
        float timeEnd = 0;

        //Debug
        keyframesA.Clear();
        keyframesI.Clear();
        keyframesU.Clear();
        keyframesE.Clear();
        keyframesO.Clear();


        //Add zero frame
        keyframeListA.Add(new Keyframe(0, 0));
        keyframeListI.Add(new Keyframe(0, 0));
        keyframeListU.Add(new Keyframe(0, 0));
        keyframeListE.Add(new Keyframe(0, 0));
        keyframeListO.Add(new Keyframe(0, 0));



        for (int i = 0; i < animLength; i++)
        {
            //Find phoneme
            int groupIndex = visemeGroup.FindVisemeGroupIndex(readRadish.phonemeDataList[i].phoneme);
            timeStart = (float)readRadish.phonemeDataList[i].start / (float)1000;
            timeEnd = (float)readRadish.phonemeDataList[i].end / (float)1000;

            //Debug.Log(timeStart + " " + timeEnd);

            if(i == 0)
            {
                keyframeListA.Add(new Keyframe(timeStart - 0.05f, 0));
                keyframeListI.Add(new Keyframe(timeStart - 0.05f, 0));
                keyframeListU.Add(new Keyframe(timeStart - 0.05f, 0));
                keyframeListE.Add(new Keyframe(timeStart - 0.05f, 0));
                keyframeListO.Add(new Keyframe(timeStart - 0.05f, 0));
            }

            keyframeListA.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.a));
            keyframeListI.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.i));
            keyframeListU.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.u));
            keyframeListE.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.e));
            keyframeListO.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.o));

            if(noEnd)
            { 
                keyframeListA.Add(new Keyframe(timeEnd, visemeGroup.visemeGroup[groupIndex].end.a));
                keyframeListI.Add(new Keyframe(timeEnd, visemeGroup.visemeGroup[groupIndex].end.i));
                keyframeListU.Add(new Keyframe(timeEnd, visemeGroup.visemeGroup[groupIndex].end.u));
                keyframeListE.Add(new Keyframe(timeEnd, visemeGroup.visemeGroup[groupIndex].end.e));
                keyframeListO.Add(new Keyframe(timeEnd, visemeGroup.visemeGroup[groupIndex].end.o));
            }

            //var tempgroup = visemeGroup.visemeGroup[groupIndex];
            //Debug.Log("Set keyframe: " + timeStart + ": " + tempgroup.start.a + "," +
            //                                              tempgroup.start.i + "," +
            //                                              tempgroup.start.u + "," +
            //                                              tempgroup.start.e + "," +
            //                                              tempgroup.start.o + "," + " | " +
            //                                        tempgroup.end.a + "," +
            //                                        tempgroup.end.i + "," +
            //                                        tempgroup.end.u + "," +
            //                                        tempgroup.end.e + "," +
            //                                        tempgroup.end.o + ",");


        }

        ////Add an ending neutral mouth position
        //time = (float)readRadish.phonemeDataList[animLength - 1].end / (float)1000;
        //keyframeListA.Add(new Keyframe(time, 0));
        //keyframeListI.Add(new Keyframe(time, 0));
        //keyframeListU.Add(new Keyframe(time, 0));
        //keyframeListE.Add(new Keyframe(time, 0));
        //keyframeListO.Add(new Keyframe(time, 0));


        //Remove overlapping keys
        TakeMaxKeyframe(keyframeListA);
        TakeMaxKeyframe(keyframeListI);
        TakeMaxKeyframe(keyframeListU);
        TakeMaxKeyframe(keyframeListE);
        TakeMaxKeyframe(keyframeListO);

        //Debug
        for (int i = 0; i < keyframeListA.Count; i++)
        {
            keyframesA.Add(new Vector2(keyframeListA[i].time, keyframeListA[i].value));
            keyframesI.Add(new Vector2(keyframeListI[i].time, keyframeListI[i].value));
            keyframesU.Add(new Vector2(keyframeListU[i].time, keyframeListU[i].value));
            keyframesE.Add(new Vector2(keyframeListE[i].time, keyframeListE[i].value));
            keyframesO.Add(new Vector2(keyframeListO[i].time, keyframeListO[i].value));
        }

        //Create a curve for each mouth blendshape
        curveA = new AnimationCurve(keyframeListA.ToArray());
        curveI = new AnimationCurve(keyframeListI.ToArray());
        curveU = new AnimationCurve(keyframeListU.ToArray());
        curveE = new AnimationCurve(keyframeListE.ToArray());
        curveO = new AnimationCurve(keyframeListO.ToArray());

        

        if (forcedLinear == true)
        {
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

            //SetCurveLinear(curveA);
            //SetCurveLinear(curveI);
            //SetCurveLinear(curveU);
            //SetCurveLinear(curveE);
            //SetCurveLinear(curveO);
        }

        AnimationClip clip = new AnimationClip();
        clip.legacy = false;
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_A", curveA);
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_I", curveI);
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_U", curveU);
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_E", curveE);
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_O", curveO);

        //clip.legacy = true;
        //Create animation file
        AssetDatabase.CreateAsset(clip, "Assets/Animation/Lipsync/" + "Lipsync " + "1" + ".anim");
    }

    public void CreateAnimation2()
    {
        List<Keyframe> keyframeListA = new List<Keyframe>();
        List<Keyframe> keyframeListI = new List<Keyframe>();
        List<Keyframe> keyframeListU = new List<Keyframe>();
        List<Keyframe> keyframeListE = new List<Keyframe>();
        List<Keyframe> keyframeListO = new List<Keyframe>();

        keyframeListA.Add(new Keyframe(0, 0));
        keyframeListI.Add(new Keyframe(0, 0));
        keyframeListU.Add(new Keyframe(0, 0));
        keyframeListE.Add(new Keyframe(0, 0));
        keyframeListO.Add(new Keyframe(0, 0));

        //Add each phoneme one by one
        int animLength = readRadish.phonemeDataList.Count;
        float time = 0;
        float timeStart = 0;
        float timeEnd = 0;

        for (int i = 0; i < animLength; i++)
        {
            //Find phoneme
            int groupIndex = visemeGroup.FindVisemeGroupIndex(readRadish.phonemeDataList[i].phoneme);
            timeStart = (float)readRadish.phonemeDataList[i].start / (float)1000;
            timeEnd = (float)readRadish.phonemeDataList[i].start / (float)1000;

            keyframeListA.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.a));
            keyframeListI.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.i));
            keyframeListU.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.u));
            keyframeListE.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.e));
            keyframeListO.Add(new Keyframe(timeStart, visemeGroup.visemeGroup[groupIndex].start.o));
            keyframeListA.Add(new Keyframe(timeEnd  , visemeGroup.visemeGroup[groupIndex].end.a));
            keyframeListI.Add(new Keyframe(timeEnd  , visemeGroup.visemeGroup[groupIndex].end.i));
            keyframeListU.Add(new Keyframe(timeEnd  , visemeGroup.visemeGroup[groupIndex].end.u));
            keyframeListE.Add(new Keyframe(timeEnd  , visemeGroup.visemeGroup[groupIndex].end.e));
            keyframeListO.Add(new Keyframe(timeEnd  , visemeGroup.visemeGroup[groupIndex].end.o));

            var tempgroup = visemeGroup.visemeGroup[groupIndex];
            //Debug.Log("Set keyframe: " + timeStart+": " + tempgroup.start.a + "," +
            //                                              tempgroup.start.i + "," +
            //                                              tempgroup.start.u + "," +
            //                                              tempgroup.start.e + "," +
            //                                              tempgroup.start.o + "," + " | " + 
            //                                        tempgroup.end.a + "," +
            //                                        tempgroup.end.i + "," +
            //                                        tempgroup.end.u + "," +
            //                                        tempgroup.end.e + "," +
            //                                        tempgroup.end.o + ",");

            
        }

        //Add an ending neutral mouth position
        time = (float)readRadish.phonemeDataList[animLength - 1].end / (float)1000;
        keyframeListA.Add(new Keyframe(time, 0));
        keyframeListI.Add(new Keyframe(time, 0));
        keyframeListU.Add(new Keyframe(time, 0));
        keyframeListE.Add(new Keyframe(time, 0));
        keyframeListO.Add(new Keyframe(time, 0));

        //Create a curve for each mouth blendshape
        curveA = new AnimationCurve(keyframeListA.ToArray());
        curveI = new AnimationCurve(keyframeListI.ToArray());
        curveU = new AnimationCurve(keyframeListU.ToArray());
        curveE = new AnimationCurve(keyframeListE.ToArray());
        curveO = new AnimationCurve(keyframeListO.ToArray());



        //for (int i = 0; i < curveA.keys.Length; i++)
        //{
        //    SetKeyLeftTangentMode(curveA, i, AnimationUtility.TangentMode.Linear);
        //    SetKeyLeftTangentMode(curveI, i, AnimationUtility.TangentMode.Linear);
        //    SetKeyLeftTangentMode(curveU, i, AnimationUtility.TangentMode.Linear);
        //    SetKeyLeftTangentMode(curveE, i, AnimationUtility.TangentMode.Linear);
        //    SetKeyLeftTangentMode(curveO, i, AnimationUtility.TangentMode.Linear);
        //    SetKeyRightTangentMode(curveA, i, AnimationUtility.TangentMode.Linear);
        //    SetKeyRightTangentMode(curveI, i, AnimationUtility.TangentMode.Linear);
        //    SetKeyRightTangentMode(curveU, i, AnimationUtility.TangentMode.Linear);
        //    SetKeyRightTangentMode(curveE, i, AnimationUtility.TangentMode.Linear);
        //    SetKeyRightTangentMode(curveO, i, AnimationUtility.TangentMode.Linear);
        //}

        //SetCurveLinear(curveA);
        //SetCurveLinear(curveI);
        //SetCurveLinear(curveU);
        //SetCurveLinear(curveE);
        //SetCurveLinear(curveO);

        AnimationClip clip = new AnimationClip();
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_A", curveA);
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_I", curveI);
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_U", curveU);
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_E", curveE);
        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + "Face.M_F00_000_00_Fcl_MTH_O", curveO);

        //Create animation file
        AssetDatabase.CreateAsset(clip, "Assets/Animation/Lipsync/" + "Lipsync " + "1" + ".anim");
    }

    public void SetCurveLinear(AnimationCurve curve)
    {
        for (int i = 0; i < curve.keys.Length; ++i)
        {
            float intangent = 0;
            float outtangent = 0;
            bool intangent_set = false;
            bool outtangent_set = false;
            Vector2 point1;
            Vector2 point2;
            Vector2 deltapoint;
            Keyframe key = curve[i];

            if (i == 0)
            {
                intangent = 0; intangent_set = true;
            }

            if (i == curve.keys.Length - 1)
            {
                outtangent = 0; outtangent_set = true;
            }

            if (!intangent_set)
            {
                point1.x = curve.keys[i - 1].time;
                point1.y = curve.keys[i - 1].value;
                point2.x = curve.keys[i].time;
                point2.y = curve.keys[i].value;

                deltapoint = point2 - point1;

                intangent = deltapoint.y / deltapoint.x;
            }
            if (!outtangent_set)
            {
                point1.x = curve.keys[i].time;
                point1.y = curve.keys[i].value;
                point2.x = curve.keys[i + 1].time;
                point2.y = curve.keys[i + 1].value;

                deltapoint = point2 - point1;

                outtangent = deltapoint.y / deltapoint.x;
            }

            key.inTangent = intangent;
            key.outTangent = outtangent;
            curve.MoveKey(i, key);
        }
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
