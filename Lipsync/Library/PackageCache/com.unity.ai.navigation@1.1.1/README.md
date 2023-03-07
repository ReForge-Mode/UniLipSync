![ReleaseBadge](https://badges.cds.internal.unity3d.com/packages/com.unity.ai.navigation/release-badge.svg)
![CandidatesBadge](https://badges.cds.internal.unity3d.com/packages/com.unity.ai.navigation/candidates-badge.svg)

# Components for Runtime NavMesh Building

Here we introduce four components for the navigation system:

* __NavMeshSurface__ – for building and enabling a NavMesh surface for one agent type.
* __NavMeshModifier__ – affects the NavMesh generation of NavMesh area types, based on the transform hierarchy.
* __NavMeshModifierVolume__ – affects the NavMesh generation of NavMesh area types, based on volume.
* __NavMeshLink__ – connects same or different NavMesh surfaces for one agent type.

These components comprise the high level controls for building and using NavMeshes at runtime as well as edit time.

Detailed information can be found in the [Documentation](Documentation~).

# FAQ

Q: Can I bake a NavMesh at runtime?  
A: Yes.

Q: Can I use NavMesh'es for more than one agent size?  
A: Yes.

Q: Can I put a NavMesh in a prefab?  
A: Yes - with some limitations.

Q: How do I connect two NavMesh surfaces?  
A: Use the NavMeshLink to connect the two sides.

Q: How do I query the NavMesh for one specific size of agent?  
A: Use the NavMeshQuery filter when querying the NavMesh.

Q: What's the deal with the 'DefaultExecutionOrder' attribute?  
A: It gives a way of controlling the order of execution of scripts - specifically it allows us to build a NavMesh before the
(native) NavMeshAgent component is enabled.

Q: What's the use of the new delegate 'NavMesh.onPreUpdate'?  
A: It allows you to hook in to controlling the NavMesh data and links set up before the navigation update loop is called on the native side.

Q: Can I do moving NavMesh platforms?  
A: No - new API is required for consistently moving platforms carrying agents.

Q: Is OffMeshLink now obsolete?  
A: No - you can still use OffMeshLink - however you'll find that NavMeshLink is more flexible and have less overhead.

Q: What happened to HeightMesh and Auto Generated OffMeshLinks?  
A: They're not supported in the new NavMesh building feature. HeightMesh will be added at some point. Auto OffMeshLink generation will possibly be replaced with a solution that allows better control of placement.

# Notice on the Change of License

With effect from 8th December 2020 this package is [licensed](LICENSE.md) under the [Unity Companion License](https://unity3d.com/legal/licenses/unity_companion_license) for Unity-dependent projects. Prior to the 8th December 2020 this package was licensed under MIT and all use of the package content prior to 8th December 2020 is licensed under MIT.
