/*
 * 用于给选中的对象修改名称
 * @caihua
 * https://caihua.tech
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Rename Utility - Written by Ryan Miller ryan@reptoidgames.com
/// </summary>
public class RenameTool : EditorWindow
{

    //public enum NameOperation
    //{
    //    NewName,
    //    AddPrefix,
    //    AddSuffix
    //}
    public enum NameOperation
    {
        新名字,
        添加前缀,
        添加后缀
    }
    public NameOperation nameOperation;

    [MenuItem("工具/重命名")]
    public static void Init()
    {
        RenameTool window = (RenameTool)EditorWindow.GetWindow(typeof(RenameTool));
        window.titleContent = new GUIContent("Rename");
        window.Show();
    }

    string rename = "";
    int numberPadding = 4;
    bool numberItems = true;
    bool sortInHierarchy = true;
    int startNumber = 0;
    int incrementBy = 1;

    void OnGUI()
    {
        GUILayout.Label("重命名多个文件", EditorStyles.boldLabel);
        GUILayout.Label("注意: 目前不支持回退功能，请先保存你的场景！");
        rename = EditorGUILayout.TextField("新名字: ", rename);
        nameOperation = (NameOperation)EditorGUILayout.EnumPopup("选择: ", nameOperation);
        numberItems = EditorGUILayout.Toggle("数字项: ", numberItems);
        if (numberItems)
        {
            numberPadding = Mathf.Clamp(EditorGUILayout.IntField("数字填充: ", numberPadding), 0, 100);
            startNumber = EditorGUILayout.IntField("开始数字: ", startNumber);
            incrementBy = Mathf.Clamp(EditorGUILayout.IntField("数字每次增加的数值: ", incrementBy), 1, 100000);
            sortInHierarchy = EditorGUILayout.Toggle("Hierarchy中排序: ", sortInHierarchy);
        }
        if (Selection.transforms.Length > 0)
        {
            if (GUILayout.Button("你选择了 " + Selection.transforms.Length + " 个对象来进行重命名"))
            {
                RenameNames(rename, nameOperation == NameOperation.添加后缀, nameOperation == NameOperation.添加前缀, numberItems, sortInHierarchy, numberPadding, startNumber, incrementBy);
            }
        }
        string sample = "";
        if (nameOperation == NameOperation.添加前缀)
        {
            sample = rename + "[name]";
        }
        else if (nameOperation == NameOperation.添加后缀)
        {
            sample = "[name]" + rename;
        }
        else if (nameOperation == NameOperation.新名字)
        {
            sample = rename;
        }
        if (numberItems)
        {
            string padFormat = "";
            for (int padNum = 0; padNum < numberPadding; padNum++)
            {
                padFormat += "0";
            }
            sample += startNumber.ToString(padFormat);
        }
        GUILayout.Space(10);
        GUILayout.Label("预览: " + sample);
    }

    public static void RenameNames(string newName, bool isSuffix, bool isPrefix, bool numberItems, bool sortInHierarchy, int padding, int startNum, int incrementBy)
    {
        int indexCount = startNum;
        string padFormat = "";
        for (int padNum = 0; padNum < padding; padNum++)
        {
            padFormat += "0";
        }
        for (int itemIndex = 0; itemIndex < Selection.transforms.Length; itemIndex++)
        {
            string thisItemName = Selection.transforms[itemIndex].name;
            // add name 
            if (!isPrefix && !isSuffix)
            {
                Debug.Log("Rename without suffix or prefix");
                Selection.transforms[itemIndex].name = newName;
            }
            else
            {
                if (isPrefix)
                {
                    Debug.Log("rename with Prefix");
                    Selection.transforms[itemIndex].name = newName + thisItemName;
                }
                if (isSuffix)
                {
                    Debug.Log("rename with Suffix");
                    Selection.transforms[itemIndex].name = thisItemName + newName;
                }
            }
            if (numberItems)
            {
                // add trailing number
                Selection.transforms[itemIndex].name += indexCount.ToString(padFormat);
                indexCount += incrementBy;
            }
        }
        if (sortInHierarchy)
        {
            for (int itemIndex = 0; itemIndex < Selection.transforms.Length; itemIndex++)
            {
                Selection.transforms[itemIndex].SetSiblingIndex(itemIndex);
            }
        }
    }
}