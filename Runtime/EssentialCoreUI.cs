namespace Craiel.UnityEssentialsUI.Runtime
{
    using NLog;

    public static class EssentialCoreUI
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public static readonly NLog.Logger Logger = LogManager.GetLogger("CRAIEL_ESSENTIALS_UI");

        public const string ComponentMenuFolder = "UI/Craiel";
    }
}