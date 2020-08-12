namespace Craiel.UnityEssentialsUI.Runtime.Theme.Targets
{
    using Enums;
    using UnityEditor;
    using UnityEngine;

    public abstract class UIThemeTarget : MonoBehaviour
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public virtual void Awake() { }

        public virtual void OnEnable()
        {
            if (UIThemeSystem.IsInstanceActive)
            {
                UIThemeSystem.Instance.RegisterTarget(this);
            }
        }

        public virtual void OnDisable()
        {
            if (UIThemeSystem.IsInstanceActive)
            {
                UIThemeSystem.Instance.UnregisterTarget(this);
            }
        }
        
#if UNITY_EDITOR
        public void OnValidate()
        {
            if (UIThemeSystem.IsInstanceActive && UIThemeSystem.Instance.ActiveTheme != null)
            {
                UIThemeSystem.Instance.ApplyThemeTo(this);
            }
        }
#endif

        public void Apply(UITheme theme)
        {
            if (!this.DoApply(theme))
            {
                return;
            }

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(this);
            }
#endif
        }

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected abstract bool DoApply(UITheme theme);
    }
}