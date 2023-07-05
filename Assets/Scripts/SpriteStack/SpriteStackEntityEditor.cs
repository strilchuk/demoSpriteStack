#if UNITY_EDITOR
using UnityEngine;
using UnityEditorInternal;
using UnityEngine.Rendering;
using System;
using System.Collections;
using System.Reflection;
using UnityEditor;

[CustomEditor(typeof(SpriteStackEntity))]
public class SpriteStackEntityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpriteStackEntity spriteStackEntity = (SpriteStackEntity)target;

        // Настройки разворота
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Rotation Settings");

        if (spriteStackEntity.rotationStyle == SpriteStackEntity.RotationStyle.ByValue)
        {
            spriteStackEntity.rotation = new Vector3(
                0,
                0,
                EditorGUILayout.Slider("Rotation value", spriteStackEntity.rotation.z, 0, 359)
            );
            // Кнопка "Повернуть"
            if (GUILayout.Button("Rotate"))
            {
                spriteStackEntity.DrawStack();
            }
        }

        if (spriteStackEntity.rotationStyle == SpriteStackEntity.RotationStyle.Targeted)
        {
            spriteStackEntity.rotationTarget = (GameObject)EditorGUILayout.ObjectField("Target",
                spriteStackEntity.rotationTarget, typeof(GameObject), true);
        }

        // Слайдер скорости разворота
        if (spriteStackEntity.rotationStyle == SpriteStackEntity.RotationStyle.Constant)
        {
            spriteStackEntity.rotationSpeed =
                EditorGUILayout.Slider("Rotation speed", spriteStackEntity.rotationSpeed, 0, 100);
        }
        


        // Кнопка "Сгенерировать стек"
        if (GUILayout.Button("GenerateStack"))
        {
            spriteStackEntity.GenerateStack();
        }
        
        // Настройки рендера
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Render Settings");
        
        
        // Заполняем слои сортировки
        var options = GetSortingLayerNames();
        var picks = new int[options.Length];
        var name = spriteStackEntity.sortingLayerName;
        var choice = -1;
        for (int i = 0; i < options.Length; i++)
        {
            picks[i] = i;
            if (name == options[i]) choice = i;
        }
        
        choice = EditorGUILayout.IntPopup("Sorting Layer", choice, options, picks);
        spriteStackEntity.sortingLayerName = options[choice];
        // spriteStackEntity.sortingOrder = EditorGUILayout.IntField("Sorting Order", renderer.sortingOrder);
        
        // if (EditorGUI.EndChangeCheck())
        //     SceneView.RepaintAll();
        

    }
    
    public string[] GetSortingLayerNames()
    {
        Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        return (string[])sortingLayersProperty.GetValue(null, new object[0]);
    }
}
#endif