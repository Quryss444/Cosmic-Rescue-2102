using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetGenerator3))]//<- editor for this
public class PlanetEditor : Editor
{
    PlanetGenerator3 planet;
    Editor shapeEditor;
    Editor colorEditor;
    
    public override void OnInspectorGUI()//gui magic here
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingsFoldOut, ref shapeEditor);
            DrawSettingsEditor(planet.colorSettings, planet.OnColorSettingsUpdated, ref planet.colorSettingsFoldOut, ref colorEditor);

            base.OnInspectorGUI();

            if (check.changed) 
            { 
                planet.GeneratePlanet();
            }

        }
            if(GUILayout.Button("Generate Planet"))
            {
                planet.GeneratePlanet();
            }
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated,ref bool foldOut,ref Editor editor) 
    {
        

        if (settings != null)
        {
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settings);
            EditorGUILayout.Space(10);
            using (var check = new EditorGUI.ChangeCheckScope())
            {

                if (foldOut)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                    if (check.changed && onSettingsUpdated != null)
                    {
                        onSettingsUpdated();
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        planet = (PlanetGenerator3)target;
    }
}
