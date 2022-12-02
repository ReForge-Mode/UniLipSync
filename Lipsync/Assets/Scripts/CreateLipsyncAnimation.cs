using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateLipsyncAnimation : MonoBehaviour
{
    public GameObject character;
    public int indexCounter = 0;

    [Header("Blendshapes")]
    public string a = "Face.M_F00_000_00_Fcl_MTH_A";
    public string i = "Face.M_F00_000_00_Fcl_MTH_I";
    public string u = "Face.M_F00_000_00_Fcl_MTH_U";
    public string e = "Face.M_F00_000_00_Fcl_MTH_E";
    public string o = "Face.M_F00_000_00_Fcl_MTH_O";

    public void CreateAnimation()
    {
        AnimationClip clip = new AnimationClip();

        // Create a curve to move the GameObject and assign to the clip
        AnimationCurve curve;
        Keyframe[] keys;
        keys = new Keyframe[3];
        keys[0] = new Keyframe(0.0f, 0.0f);
        keys[1] = new Keyframe(2.0f, 100.0f);
        keys[2] = new Keyframe(4.0f, 0.0f);
        curve = new AnimationCurve(keys);

        clip.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + a, curve);

        //Create folder, if it doesn't exist
        if (!AssetDatabase.IsValidFolder($"Assets/Animation/Lipsync"))
        {
            AssetDatabase.CreateFolder("Assets/Animation", "Lipsync");
        }

        //Create animation file
        AssetDatabase.CreateAsset(clip, "Assets/Animation/Lipsync/" + "Lipsync " + indexCounter + ".anim");
        indexCounter++;
    }
}
