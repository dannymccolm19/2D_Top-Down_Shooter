using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class DialogueEditor : EditorWindow
{

    string[] toolbarStrings = {"NPC", "Player"};
    int _toolbar_sel = 0;
    

    public KeyValues keyValues;

    private SerializedObject obj;
    private SerializedProperty x;
    private SerializedProperty keys;
    private SerializedProperty key;
    private SerializedProperty response;
    private SerializedProperty values;
    private SerializedProperty links;
    private SerializedProperty link;
    private SerializedProperty active;
    private SerializedProperty tog;
    public int index = 0;
    
    
    private bool tool = true;

    [MenuItem("Custom Editor/DialogueEditor")]
    public static void Init(){
        DialogueEditor window = (DialogueEditor)EditorWindow.GetWindow(typeof(DialogueEditor));
        window.Show();
        
    }
    void OnGUI()
    {
        
        EditorGUI.BeginChangeCheck();
        GUILayout.BeginHorizontal();
        _toolbar_sel = GUILayout.Toolbar(_toolbar_sel, toolbarStrings);
        GUILayout.EndHorizontal();

        switch(_toolbar_sel){
            case 0:
                tool = true;
                break;
            case 1:
                tool = false;
                break;
        }
        if (EditorGUI.EndChangeCheck()){
            GUI.FocusControl(null);
        }

        EditorGUI.BeginChangeCheck();


        obj = new SerializedObject(keyValues);
        x = obj.FindProperty("x");
        keys = obj.FindProperty("dialogueKeys");
     


        if (tool){
                for (int i = 0; i<= x.intValue; i++){
                    
                    key = keys.GetArrayElementAtIndex(i);
                    
                    if (i !=  x.intValue){
                        EditorGUILayout.PropertyField(key, new GUIContent("Key" + (i+ 1) +": "));
                    }
                    
                }
            obj.ApplyModifiedProperties();
            
            if(GUILayout.Button("Add new Key")){
                    x.intValue++;
                    obj.ApplyModifiedProperties();
            }

            if(GUILayout.Button("Remove most recent key")){
                
                x.intValue--;
                obj.ApplyModifiedProperties();
                
            }
        }
        else {
            
            string [] temp = new string[x.intValue];
            for (int i = 0; i < x.intValue; i++ ){
                temp[i] = keys.GetArrayElementAtIndex(i).stringValue;
                
            }
            index = EditorGUILayout.Popup(index, temp);
            for (int i =1; i<6; i++){
                values = obj.FindProperty("response" + i.ToString());
                response = values.GetArrayElementAtIndex(index);
                links = obj.FindProperty("responseLink" + i.ToString());
                link = links.GetArrayElementAtIndex(index);
                active = obj.FindProperty("active" + i.ToString());
                tog = active.GetArrayElementAtIndex(index);
                GUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(response, new GUIContent( "response" + i.ToString() + ": "));
               
                tog.boolValue = EditorGUILayout.Toggle(tog.boolValue);
                link.intValue = EditorGUILayout.Popup(link.intValue, temp);
                
                GUILayout.EndHorizontal();
            }
            

        }
        
        if (EditorGUI.EndChangeCheck()){
            obj.ApplyModifiedProperties();
        }



    }
}
