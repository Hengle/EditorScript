/*
 * 用于设置EditorWindow的UI style
 * @caihua
 * https://caihua.tech
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CustomStyle : Editor
{

    private static GUIStyle labelStyleFont20;
    public static UnityEngine.GUIStyle LabelStyleFont20
    {
        get
        {
            labelStyleFont20 = new GUIStyle();
            labelStyleFont20.fontSize = 20;
            labelStyleFont20.fontStyle = FontStyle.Normal;
            labelStyleFont20.fixedWidth = 100;
            return labelStyleFont20;
        }

    }
}
