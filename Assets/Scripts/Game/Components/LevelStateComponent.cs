namespace Game.Components {
    public struct LevelStateComponent {
        public LevelState LevelState;
    }

    public enum LevelState {
        GameInProgress,
        GameEnded,
        Restarting
    }
}