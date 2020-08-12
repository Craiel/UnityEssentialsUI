namespace Craiel.UnityEssentialsUI.Runtime.Theme.Targets
{
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(TMP_Text))]
    public class UIThemeFontTargetTMP : UIThemeTarget
    {
        [SerializeField]
        public TMP_Text Text;
        
        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override bool DoApply(UITheme theme)
        {
            if (Text == null)
            {
                return false;
            }

            if (this.Text.font == theme.PrimaryFont)
            {
                return false;
            }
            
            this.Text.font = theme.PrimaryFont;
            return true;
        }
    }
}