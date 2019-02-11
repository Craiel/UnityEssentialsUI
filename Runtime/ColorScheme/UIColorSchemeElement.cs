namespace Craiel.UnityEssentialsUI.Runtime.ColorScheme
{
    using Enums;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIColorSchemeElement : MonoBehaviour, IUIColorSchemeElement
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [SerializeField] 
        public ColorSchemeShade ShadeValue = ColorSchemeShade.Primary;

        public ColorSchemeShade Shade
        {
            get { return this.ShadeValue; }
        }

        public void Awake()
        {
            if (UIColorSchemeSystem.IsInstanceActive && UIColorSchemeSystem.Instance.ActiveColorScheme != null)
            {
                UIColorSchemeSystem.Instance.ActiveColorScheme.ApplyToElement(this);
            }
        }

#if UNITY_EDITOR
        public void OnValidate()
        {
            if (UIColorSchemeSystem.IsInstanceActive && UIColorSchemeSystem.Instance.ActiveColorScheme != null)
            {
                UIColorSchemeSystem.Instance.ActiveColorScheme.ApplyToElement(this);
            }
        }
#endif

        public void Apply(Color newColor)
        {
            Image image = this.gameObject.GetComponent<Image>();

            if (image == null)
            {
                return;
            }

            // Keep the image alpha
            image.color = new Color(newColor.r, newColor.g, newColor.b, image.color.a);

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(image);
            }
#endif
        }
    }
}