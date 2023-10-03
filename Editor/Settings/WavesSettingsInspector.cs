using IronMountain.Waves.Settings;
using UnityEditor;

namespace IronMountain.Waves.Editor.Settings
{
    [CustomEditor(typeof(WavesSettings), true)]
    public class WavesSettingsInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            WavesSettings wavesSettings = (WavesSettings) target;
            DrawDefaultInspector();
            EditorGUILayout.Space();
            float amplitude = wavesSettings.MaximumAmplitude;
            EditorGUILayout.LabelField("Maximum Height = " + amplitude);
            EditorGUILayout.LabelField("Minimum Height = " + -amplitude);
        }
    }
}
