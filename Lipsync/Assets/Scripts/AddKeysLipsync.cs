using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class AddKeysLipsync : MonoBehaviour
{
    public AnimationClip anim;
    public GameObject characterFace;
    public int frame;

    public string blendshapeA = "Face.M_F00_000_00_Fcl_MTH_A";

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            AnimationCurve curve;

            // create a curve to move the GameObject and assign to the clip
            Keyframe[] keys;
            keys = new Keyframe[3];
            keys[0] = new Keyframe(0.0f, 0.0f);
            keys[1] = new Keyframe(2.0f, 100.0f);
            keys[2] = new Keyframe(4.0f, 0.0f);
            curve = new AnimationCurve(keys);

            anim.SetCurve("Face", typeof(SkinnedMeshRenderer), "blendShape." + blendshapeA, curve);
            

            
        }
    }
}
