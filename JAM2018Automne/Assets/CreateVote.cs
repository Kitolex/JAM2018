#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateVote  {

    [MenuItem("Assets/Create/Vote")]
    public static Vote Create()
    {
        Vote asset = ScriptableObject.CreateInstance<Vote>();

        AssetDatabase.CreateAsset(asset, "Assets/DONNEES/newVote.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}

#endif
