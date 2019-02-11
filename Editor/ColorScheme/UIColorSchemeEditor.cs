namespace Craiel.UnityEssentialsUI.Editor.ColorScheme
{
    using Runtime.ColorScheme;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;

    [CustomEditor(typeof(UIColorScheme))]
    public class ColorSchemeEditor : Editor
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            if (GUILayout.Button("Apply Color Scheme"))
            {
                UIColorScheme scheme = this.target as UIColorScheme;

                if (scheme != null)
                {
                    // Apply the color scheme to the loaded scenes
                    scheme.ApplyColorScheme();

                    if (!Application.isPlaying)
                    {
                        EditorSceneManager.MarkAllScenesDirty();
                    }
                }
            }
        }
    }
}