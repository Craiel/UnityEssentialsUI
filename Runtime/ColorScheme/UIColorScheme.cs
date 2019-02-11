namespace Craiel.UnityEssentialsUI.Runtime.ColorScheme
{
    using Enums;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Craiel/UI/ColorScheme")]
    public class UIColorScheme : ScriptableObject
    {
        [SerializeField]
        public string DisplayName;
        
        [Header("Image Colors")]
        [SerializeField] 
        public Color ImagePrimary = Color.white;
        
        [SerializeField] 
        public Color ImageSecondary = Color.white;
        
        [SerializeField] 
        public Color ImageLight = Color.white;
        
        [SerializeField] 
        public Color ImageDark = Color.white;
        
        [SerializeField] 
        public Color ImageEffect = Color.white;
        
        [SerializeField] 
        public Color ImageBorders = Color.white;

        [Header("Button Colors")]
        [SerializeField] 
        public Color ButtonForeground = Color.white;

        [Header("Window Colors")]
        [SerializeField] 
        public Color WindowHeader = Color.white;
        
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void ApplyColorScheme()
        {
            UIColorSchemeElement[] elements = FindObjectsOfType<UIColorSchemeElement>();

            foreach (UIColorSchemeElement element in elements)
            {
                this.ApplyToElement(element);
            }

            if (UIColorSchemeSystem.IsInstanceActive)
            {
                UIColorSchemeSystem.Instance.SetScheme(this);
            }
            else
            {
                UIColorSchemeSystem system = FindObjectOfType<UIColorSchemeSystem>();
                if (system != null)
                {
                    system.SetScheme(this);
                }
            }
        }
        
        public Color GetColorShade(ColorSchemeShade shade)
        {
            switch (shade)
            {
                case ColorSchemeShade.Primary:
                {
                    return this.ImagePrimary;
                }
                
                case ColorSchemeShade.Secondary:
                {
                    return  this.ImageSecondary;
                }
                
                case ColorSchemeShade.Light:
                {
                    return  this.ImageLight;
                }
                
                case ColorSchemeShade.Dark:
                {
                    return  this.ImageDark;
                }
                
                case ColorSchemeShade.Effect:
                {
                    return  this.ImageEffect;
                }
                
                case ColorSchemeShade.Borders:
                {
                    return  this.ImageBorders;
                }
                
                case ColorSchemeShade.Button:
                {
                    return  this.ButtonForeground;
                }
                
                case ColorSchemeShade.WindowHeader:
                {
                    return this.WindowHeader;
                }

                default:
                {
                    return Color.white;
                }
            }
        }

        public void ApplyToElement(IUIColorSchemeElement element)
        {
            if (element == null)
            {
                return;
            }
            
            element.Apply(this.GetColorShade(element.Shade));
        }
    }
}