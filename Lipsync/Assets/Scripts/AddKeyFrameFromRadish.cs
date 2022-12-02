using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class AddKeyFrameFromRadish : MonoBehaviour
{
    public ReadRadish readRadish;
    public SOPhonemeTable phonemeTable;

    public void CreateAnimation()
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

        //Loop one by one
        int animLength = readRadish.phonemeDataList.Count;
        float time = 0;
        float timeStart = 0;
        float timeEnd = 0;


        for (int i = 0; i < animLength; i++)
        {
            //Find phoneme
            string group = phonemeTable.FindIPAGroup(readRadish.phonemeDataList[i].phoneme);
            timeStart = (float)readRadish.phonemeDataList[i].start / (float)1000;
            timeEnd = (float)readRadish.phonemeDataList[i].start / (float)1000;

            switch (group)
            {
                case "h":
                    keyframeListA.Add(new Keyframe(timeStart, 60));
                    keyframeListI.Add(new Keyframe(timeStart, 0));
                    keyframeListU.Add(new Keyframe(timeStart, 0));
                    keyframeListE.Add(new Keyframe(timeStart, 0));
                    keyframeListO.Add(new Keyframe(timeStart, 0));
                    break;

                case "a":
                    keyframeListA.Add(new Keyframe(timeStart, 100));
                    keyframeListI.Add(new Keyframe(timeStart, 0));
                    keyframeListU.Add(new Keyframe(timeStart, 0));
                    keyframeListE.Add(new Keyframe(timeStart, 0));
                    keyframeListO.Add(new Keyframe(timeStart, 0));
                    break;

                case "i":
                    keyframeListA.Add(new Keyframe(timeStart, 20));
                    keyframeListI.Add(new Keyframe(timeStart, 100));
                    keyframeListU.Add(new Keyframe(timeStart, 0));
                    keyframeListE.Add(new Keyframe(timeStart, 0));
                    keyframeListO.Add(new Keyframe(timeStart, 0));
                    break;

                case "u":
                    keyframeListA.Add(new Keyframe(timeStart, 30));
                    keyframeListI.Add(new Keyframe(timeStart, 0));
                    keyframeListU.Add(new Keyframe(timeStart, 100));
                    keyframeListE.Add(new Keyframe(timeStart, 0));
                    keyframeListO.Add(new Keyframe(timeStart, 0));
                    break;

                case "e":
                    keyframeListA.Add(new Keyframe(timeStart, 30));
                    keyframeListI.Add(new Keyframe(timeStart, 0));
                    keyframeListU.Add(new Keyframe(timeStart, 0));
                    keyframeListE.Add(new Keyframe(timeStart, 100));
                    keyframeListO.Add(new Keyframe(timeStart, 0));
                    break;

                case "o":
                    keyframeListA.Add(new Keyframe(timeStart, 0));
                    keyframeListI.Add(new Keyframe(timeStart, 0));
                    keyframeListU.Add(new Keyframe(timeStart, 80));
                    keyframeListE.Add(new Keyframe(timeStart, 0));
                    keyframeListO.Add(new Keyframe(timeStart, 100));
                    break;

                case "sh":
                    keyframeListA.Add(new Keyframe(timeStart, 0));
                    keyframeListI.Add(new Keyframe(timeStart, 0));
                    keyframeListU.Add(new Keyframe(timeStart, 100));
                    keyframeListE.Add(new Keyframe(timeStart, 100));
                    keyframeListO.Add(new Keyframe(timeStart, 10));
                    break;

                case "p":
                    keyframeListA.Add(new Keyframe(timeStart, 0));
                    keyframeListI.Add(new Keyframe(timeStart, 0));
                    keyframeListU.Add(new Keyframe(timeStart, 0));
                    keyframeListE.Add(new Keyframe(timeStart, 0));
                    keyframeListO.Add(new Keyframe(timeStart, 0));
                    break;

                case "ai":
                    keyframeListA.Add(new Keyframe(timeStart, 100));
                    keyframeListI.Add(new Keyframe(timeStart, 0));
                    keyframeListU.Add(new Keyframe(timeStart, 0));
                    keyframeListE.Add(new Keyframe(timeStart, 0));
                    keyframeListO.Add(new Keyframe(timeStart, 0));
                    keyframeListA.Add(new Keyframe(timeEnd, 20));
                    keyframeListI.Add(new Keyframe(timeEnd, 100));
                    keyframeListU.Add(new Keyframe(timeEnd, 0));
                    keyframeListE.Add(new Keyframe(timeEnd, 0));
                    keyframeListO.Add(new Keyframe(timeEnd, 0));
                    break;

                case "aw":
                    keyframeListA.Add(new Keyframe(timeStart, 100));
                    keyframeListI.Add(new Keyframe(timeStart, 0));
                    keyframeListU.Add(new Keyframe(timeStart, 0));
                    keyframeListE.Add(new Keyframe(timeStart, 0));
                    keyframeListO.Add(new Keyframe(timeStart, 0));
                    keyframeListA.Add(new Keyframe(timeEnd, 0));
                    keyframeListI.Add(new Keyframe(timeEnd, 0));
                    keyframeListU.Add(new Keyframe(timeEnd, 80));
                    keyframeListE.Add(new Keyframe(timeEnd, 0));
                    keyframeListO.Add(new Keyframe(timeEnd, 80));
                    break;

                case "ei":
                    keyframeListA.Add(new Keyframe(timeStart, 30));
                    keyframeListI.Add(new Keyframe(timeStart, 0));
                    keyframeListU.Add(new Keyframe(timeStart, 0));
                    keyframeListE.Add(new Keyframe(timeStart, 100));
                    keyframeListO.Add(new Keyframe(timeStart, 0));
                    keyframeListA.Add(new Keyframe(timeEnd, 20));
                    keyframeListI.Add(new Keyframe(timeEnd, 100));
                    keyframeListU.Add(new Keyframe(timeEnd, 0));
                    keyframeListE.Add(new Keyframe(timeEnd, 0));
                    keyframeListO.Add(new Keyframe(timeEnd, 0));
                    break;

                case "":
                    break;

            }
            //keyframeListU.Add(new Keyframe((float)readRadish.phonemeDataList[i].start/(float)1000, 100));
            //keyframeListO.Add(new Keyframe((float)readRadish.phonemeDataList[i].start/(float)1000, 100));
        }

        time = (float)readRadish.phonemeDataList[animLength - 1].end / (float)1000;
        keyframeListA.Add(new Keyframe(time, 0));
        keyframeListI.Add(new Keyframe(time, 0));
        keyframeListU.Add(new Keyframe(time, 0));
        keyframeListE.Add(new Keyframe(time, 0));
        keyframeListO.Add(new Keyframe(time, 0));


        AnimationCurve curveA = new AnimationCurve(keyframeListA.ToArray());
        AnimationCurve curveI = new AnimationCurve(keyframeListI.ToArray());
        AnimationCurve curveU = new AnimationCurve(keyframeListU.ToArray());
        AnimationCurve curveE = new AnimationCurve(keyframeListE.ToArray());
        AnimationCurve curveO = new AnimationCurve(keyframeListO.ToArray());

        SetCurveLinear(curveA);
        SetCurveLinear(curveI);
        SetCurveLinear(curveU);
        SetCurveLinear(curveE);
        SetCurveLinear(curveO);

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
}
