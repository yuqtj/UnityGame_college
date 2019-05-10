using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class ChangeFontWindow : EditorWindow
{
    [MenuItem("WeiTools/换字体")]
    public static void Open()
    {
        EditorWindow.GetWindow(typeof(ChangeFontWindow));
    }

    public Font font;
    static Font toChangeFont;


    void OnGUI()
    {
        font = (Font)EditorGUILayout.ObjectField(font, typeof(Font), true, GUILayout.MinWidth(50f));

        toChangeFont = font;

        if (GUILayout.Button("变换字体"))
        {
            ChangeFont();
        }
    }

    public static void ChangeFont()
    {
        Object[] labels = Selection.GetFiltered(typeof(Text), SelectionMode.Deep);

        foreach(Object item in labels)
        {
            Text label = (Text)item;
            label.font = toChangeFont;
        }
    }
}
