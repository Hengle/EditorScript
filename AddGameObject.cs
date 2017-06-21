/*
 * 用于给选中的对象添加子对象或者父对象
 * @caihua
 * https://caihua.tech
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddGameObject : EditorWindow
{
    [MenuItem("工具/修改对象结构")]
    public static void Init()
    {
        AddGameObject window = EditorWindow.GetWindow<AddGameObject>();
        window.titleContent = new GUIContent("修改对象结构");
        window.ShowPopup();
    }

    public enum OperationType
    {
        添加空的父对象,
        添加空的子对象
    }
    public OperationType operation;

    static string newGoName;

    private void OnGUI()
    {
        GUILayout.Space(10);

        newGoName = EditorGUILayout.TextField("新对象名称", newGoName);
        GUILayout.Space(10);

        operation = (OperationType)EditorGUILayout.EnumPopup("操作类型", operation);
        if (Selection.gameObjects.Length > 0)
        {
            GUILayout.Space(10);
            if (GUILayout.Button("你选择了 " + Selection.gameObjects.Length + " 个对象来进行重命名"))
            {
                ChangeGameObjectStructure(operation);
            }
        }
    }

    public static void ChangeGameObjectStructure(OperationType type)
    {
        switch (type)
        {
            case OperationType.添加空的父对象:
                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    GameObject go = new GameObject(newGoName);
                    Selection.gameObjects[i].transform.parent = go.transform;
                }
                break;
            case OperationType.添加空的子对象:
                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    GameObject go = new GameObject(newGoName);
                    go.transform.parent = Selection.gameObjects[i].transform;
                }
                break;
            default:
                break;
        }
    }
}
