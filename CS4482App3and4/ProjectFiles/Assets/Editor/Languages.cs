using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class Languages : EditorWindow
{
    //To make different tabs for each language
    string[] toolbarStrings = {"English", "French", "Spanish", "German", "Italian"};
    int _toolbar_sel = 0;

    //The ScriptableObjects
    public English english;
    public French french;
    public Italian italian;
    public Spanish spanish;
    public German german;
    public KeyValues keyValues;
    

    private SerializedObject obj;
    private SerializedObject objK;
    private SerializedObject objD;
    private SerializedProperty x;
    private SerializedProperty message;
    private SerializedProperty key;
    private SerializedProperty keys;
    private SerializedProperty value;
    private SerializedProperty values;
    private SerializedProperty Dkeys;
    private SerializedProperty active;
    private SerializedProperty tog;

    public int index = 0;
    bool r = false;
  

    [MenuItem("Custom Editor/LanguageEditor")]
    public static void Init(){
        Languages window = (Languages)EditorWindow.GetWindow(typeof(Languages));
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
                obj = new SerializedObject(english);
                break;
            case 1:
                obj = new SerializedObject(french);
                break;
            case 2:
                obj = new SerializedObject(spanish);
                break;
            case 3:
                obj = new SerializedObject(german);
                break;
            case 4:
                obj = new SerializedObject(italian);
                break;

        }

        if (EditorGUI.EndChangeCheck()){
            obj.ApplyModifiedProperties();
            GUI.FocusControl(null);
        }
        
        EditorGUI.BeginChangeCheck();

        objK = new SerializedObject(keyValues);
        keys = objK.FindProperty("dialogueKeys");
        x = objK.FindProperty("x");
        //Dkeys = objK.FindProperty("dialogueKeys");

        r = EditorGUILayout.Toggle("Responses", r);
        if(r){
           
            string [] temp = new string[x.intValue];
            for (int i = 0; i < x.intValue; i++ ){
                temp[i] = keys.GetArrayElementAtIndex(i).stringValue;     
            }
            index = EditorGUILayout.Popup(index, temp);
            for (int i =1; i<6; i++){
                string y = "response" + i.ToString();
                values = obj.FindProperty(y);
                message = values.GetArrayElementAtIndex(index);
                value = objK.FindProperty(y).GetArrayElementAtIndex(index);
                active = objK.FindProperty("active" + i.ToString());
                tog = active.GetArrayElementAtIndex(index);
                if (tog.boolValue){
                    //EditorGUILayout.PropertyField(message, new GUIContent(value.stringValue));
                    GUILayout.Label(value.stringValue);
                    message.stringValue = EditorGUILayout.TextArea(message.stringValue,GUILayout.ExpandWidth(true), GUILayout.Height(40));
                }
            }
        }
        else{
            values = obj.FindProperty("Values");

            for (int i = 0; i<= x.intValue; i++){
                
                key = keys.GetArrayElementAtIndex(i);
                value = values.GetArrayElementAtIndex(i);
                
                if (i !=  x.intValue){
                    GUILayout.Label(key.stringValue);
                    value.stringValue = EditorGUILayout.TextArea(value.stringValue,GUILayout.ExpandWidth(true), GUILayout.Height(40));
                    
                }        
            }
        }
        
        obj.ApplyModifiedProperties();
        objK.ApplyModifiedProperties();
        
        
        
        if (EditorGUI.EndChangeCheck()){
            obj.ApplyModifiedProperties();
        }

    }
}