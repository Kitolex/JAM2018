using UnityEngine;
#if UNITY_EDITOR
using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
/*
public class VoteWindowEditor : EditorWindow
{

    public Vote vote;
    private int viewIndex = 1;
    private int viewSubIndex = 1;
    private string name;
    private string nameActu;

    [MenuItem("Window/Vote Editor %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(VoteWindowEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            vote = AssetDatabase.LoadAssetAtPath(objectPath, typeof(Vote)) as Vote;
        }

    }

    void OnGUI()
    {

        if (GUI.changed)
        {
            EditorUtility.SetDirty(vote);
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Vote Item Editor : " + nameActu, EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(10);
        if (GUILayout.Button("Create New Item List", GUILayout.ExpandWidth(false)))
        {
            CreateNewItemList();
        }
        name = EditorGUILayout.TextField(name);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false)))
        {
            OpenItemList();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (vote != null)
        {
            if (GUILayout.Button("Show Item List"))
            {
                //EditorUtility.FocusProjectWindow ();
                Selection.activeObject = vote;
            }
            
           
        }
        else
        {
            nameActu = "";
        }
        GUILayout.EndHorizontal();

        if (vote != null)
        {
            GUILayout.BeginHorizontal();

            vote.enonce = EditorGUILayout.TextField(vote.enonce);
            GUILayout.BeginHorizontal();
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }
            //viewIndex = Mathf.Clamp (EditorGUILayout.IntField (viewIndex, GUILayout.ExpandWidth(false)), 1, cinematiqueItemList.itemList.Count);
            EditorGUILayout.LabelField("" + viewIndex + "", GUILayout.ExpandWidth(false));

            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < vote.nomProposition.Count)
                {
                    viewIndex++;
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false)))
            {
                DeleteItem();
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (vote.nomProposition.Count > 0)
            {
                vote.nomProposition[viewIndex - 1] = EditorGUILayout.TextField(vote.nomProposition[viewIndex - 1]);
            }
            GUILayout.EndHorizontal();


            if (vote.listEffects!=null && vote.nomProposition.Count>0)
            {

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("PrevSub", GUILayout.ExpandWidth(false)))
                {
                    if (viewSubIndex > 1)
                        viewSubIndex--;
                }
               
                EditorGUILayout.LabelField("" + viewSubIndex + "", GUILayout.ExpandWidth(false));

                if (GUILayout.Button("NextSub", GUILayout.ExpandWidth(false)))
                {
                    if (viewSubIndex < vote.listEffects[viewIndex-1].effects.Count)
                    {
                        viewSubIndex++;
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add SubItem", GUILayout.ExpandWidth(false)))
                {
                    AddSubItem();
                }
                if (GUILayout.Button("Delete SubItem", GUILayout.ExpandWidth(false)))
                {
                    DeleteSubItem();
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();

                Debug.Log(vote.listEffects[viewIndex - 1].effects);
                if (vote.listEffects[viewIndex - 1].effects.Count > 0 )
                {
                    vote.listEffects[viewIndex - 1].effects[viewSubIndex - 1] = (Effects) EditorGUILayout.EnumPopup(vote.listEffects[viewIndex - 1].effects[viewSubIndex - 1]);
                }
                GUILayout.EndHorizontal();
            }


        }

    }

    private void DeleteSubItem()
    {
        throw new NotImplementedException();
    }

    private void AddSubItem()
    {

        vote.listEffects[viewIndex-1].effects.Add(Effects.a);
        viewSubIndex = vote.listEffects[viewIndex-1].effects.Count;

    }

    private void DeleteItem()
    {
       vote.proposition.Remove()
        if (index != 0)
        {
            viewIndex--;
        }
        if (cinematiqueItemList.itemList.Count == 0)
        {
            viewIndex = 0;
        }
    }

    private void AddItem()
    {
        vote.listEffects.Add(new ListEffet());
        vote.listEffects[viewIndex].effects = new List<Effects>();
        vote.nomProposition.Add("");

        viewSubIndex = 0;
        viewIndex = vote.nomProposition.Count;
    }

    private void OpenItemList()
    {

    }

    public void CreateNewItemList()
    {
        viewIndex = 0;
        viewSubIndex = 0;
        vote = CreateVote.Create(name);
        vote.nomProposition = new List<string>();
        vote.listEffects = new List<ListEffet>();
        string relPath = AssetDatabase.GetAssetPath(vote);
        EditorPrefs.SetString("ObjectPath", relPath);
        nameActu = relPath;
    }

}*/
#endif