using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class UnderscoreReplace : EditorWindow
{

    private GameObject[] renameObjects;
    private SerializedObject so;
    private SerializedProperty goProp;
    private EditorWindow window;

    [MenuItem("Tools/UnderscoreReplace")]

    static void CreateUnderscoreReplace()
    {
      EditorWindow.GetWindow<UnderscoreReplace>();
    }

    private void OnEnable()
    {
      
       
        //goProp = so.FindProperty("renameObjects");
    }
    // Start is called before the first frame update

    private void OnGUI()
    {
        EditorGUILayout.LabelField("This tool will change the first found underscore '_' to a colon ':' ");
        EditorGUILayout.LabelField("for each object selected in the your scene.");
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("This window will close when the operation is complete.");
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        renameObjects = Selection.gameObjects;
        EditorGUILayout.LabelField("Selection count: " + Selection.objects.Length);
       // EditorGUILayout.TextArea("Number of Objects Selected: " + selection);
       // so.ApplyModifiedProperties();


            if (GUILayout.Button("Rename"))
        {
            Rename();
        }

       
        
    }

    

    void Rename()
    {
        foreach (GameObject obj in renameObjects)
        {
            var newName = obj.name.ToString();
            var stringarray = newName.ToCharArray();
            for (int i = 0; i <stringarray.Length; i++)
            {
                
                if (stringarray[i].Equals('_'))
                {

                    stringarray[i] = ':' ;
                    newName = new string (stringarray);
                    Debug.Log(newName);
                    obj.name = newName.ToString();
                   
                    break;
                }


            }
            PrefabUtility.RecordPrefabInstancePropertyModifications(obj.transform);
            EditorUtility.SetDirty(obj.transform);
        }
        this.Close();
    }

}

