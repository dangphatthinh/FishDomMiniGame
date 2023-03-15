public static class C
{
    public static class Scenes
    {
        public const string Game = "game";
    }

    public static class Layer
    {
        public const string Loading = "Loading/LoadingUI";
        public const string Home = "Home/HomeUI";
        public const string LevelTarget = "Home/LevelTargetUI";
        public const string Setting = "Home/SettingUI";
        public const string ActionPhase = "ActionPhase/ActionPhaseUI";
        public const string Pause = "ActionPhase/PauseUI";
        public const string LevelWin = "ActionPhase/LevelWinUI";
        public const string LevelOutOfMove = "ActionPhase/OutOfMovesUI";
        public const string LevelGiveUp = "ActionPhase/QuitUI";
        public const string LevelLose = "ActionPhase/LevelLoseUI";
        public const string CheatUI = "CheatUI";
        public const string Shop = "ShopUI";
        public const string FTUE = "FTUE/FtueUI";
    }

    public static class GameConfig
    {
    }

    public static class ResourcePaths
    {
        public static readonly string LevelDataPath = "data/levels";
    }

    public static class PlayerPrefKeys
    {
        public const string IsEnableSound = "pf_is_enable_sound";
        public const string IsEnableMusic = "pf_is_enable_music";
    }

    public static class AudioIds
    {
        public static class Music
        {
            public const string Gameplay = "bg_gameplay";
        }

        public static class Sound
        {
            public const string TouchBubble = "touch_bubble";
            public const string PopBubble = "pop_bubble";
            public const string DropBubble = "drop_bubble";
            public const string PopGem = "pop_gem";
            public const string PopLog = "pop_log";
            public const string PopColorBomb = "pop_colorbomb";
            public const string PopPotionBomb = "pop_potionbomb";
            public const string UseBoosterBomb = "use_booster_bomb";
            public const string UseBoosterRainbow = "use_booster_rainbow";
            public const string UseBoosterLightning = "use_booster_lightning";
            public const string UseBoosterRocket = "use_booster_rocket";

            public const string PopupLevelWin = "popup_level_win";
            public const string PopupLevelLose = "popup_level_lose";
            public const string PopupLevelRetry = "popup_level_retry";
        }
    }
}