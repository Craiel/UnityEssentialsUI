namespace Craiel.UnityEssentialsUI.Runtime.ColorScheme
{
    using Enums;
    using UnityEngine;

    public interface IUIColorSchemeElement
    {
        ColorSchemeShade Shade { get; }
        
        void Apply(Color color);
    }
}