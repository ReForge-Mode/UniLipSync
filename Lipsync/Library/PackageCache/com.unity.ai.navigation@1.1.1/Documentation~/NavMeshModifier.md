# NavMeshModifier

NavMesh Modifiers adjust how a specific GameObject behaves during NavMesh baking at runtime.

To use the NavMesh Modifier component, navigate to __GameObject__ > __AI__ > __NavMesh Modifier__.

In the image below, the platform in the bottom right has a modifier attached to it that sets its __Area Type__ to __Lava__.

![NavMeshModifier example](Images/NavMeshModifier-Example.png "A NavMesh Modifier component open in the Inspector window")

The NavMesh Modifier optionally affects GameObjects hierarchically, meaning the GameObject that the component is attached to as well as all its children are affected. Additionally, if another NavMesh Modifier is found further down the transform hierarchy, the new NavMesh Modifier overrides the one further up the hierarchy.<br/>
To enable this behaviour use the __Apply To Children__ option.

The NavMesh Modifier affects the NavMesh generation process, meaning the NavMesh has to be updated to reflect any changes to NavMesh Modifiers.

--

Note: This component is a replacement for the legacy setting which could be enabled from the Navigation window Objects tab as well as the static flags dropdown on the GameObject. This component is available for baking at runtime, whereas the static flags are available in the editor only.

## Parameters
| Property                    | Function                                                                                                                                                                                                                                                         |
|:----------------------------|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| __Mode__                    | Depending on this value the affected object(s) will be removed or considered during the build process.<br/> - __Add or Modify Object__ : objects are considered during the build process<br/> - __Remove Object__ : objects are discarded from the build process |
| __Affected Agents__         | A selection of Agents the Modifier affects. For example, you may choose to exclude certain obstacles from specific Agents.                                                                                                                                       |
| __Apply To Children__       | Check this tickbox to apply the configuration to the GameObject children hierarchy.<br/>You can still override this component's influence down the hierarchy line by adding another NavMesh Modifier component.                                                  |
| __Override Area Type__      | Check this tickbox to change the area type for the affected GameObject(s).                                                                                                                                                                                       |
| __Area Type__               | Select the new area type to apply from the drop-down menu.                                                                                                                                                                                                       |
| __Override Generate Links__ | Check this tickbox to force the way the affected GameObject(s) will be considered by the NavMesh baking links generation.                                                                                                                                        |
| __Generate Links__          | Check this tickbox to consider the GameObject(s) during links generation, uncheck it to remove them from the link generation.                                                                                                                                    |

