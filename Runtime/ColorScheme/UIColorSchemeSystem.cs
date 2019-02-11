namespace Craiel.UnityEssentialsUI.Runtime.ColorScheme
{
    using UnityEditor;
    using UnityEngine;
    using UnityEssentials.Runtime.Singletons;

    public class UIColorSchemeSystem : UnitySingletonBehavior<UIColorSchemeSystem>
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [SerializeField]
        public UIColorScheme ActiveColorScheme;

        public void SetScheme(UIColorScheme newScheme)
        {
            this.ActiveColorScheme = newScheme;
                
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }
}