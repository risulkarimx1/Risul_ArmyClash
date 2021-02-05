using UniRx;

namespace Assets.Code.Sources.GameSceneUi
{
    public class GameSceneUiModel
    {
        private ReactiveProperty<int> _guildAScore;
        private ReactiveProperty<int> _guildBScore;

        public GameSceneUiModel()
        {
            _guildAScore = new ReactiveProperty<int>(0);
            _guildBScore = new ReactiveProperty<int>(0);
        }

        public ReactiveProperty<int> GuildBScore => _guildBScore;
        public ReactiveProperty<int> GuildAScore => _guildAScore;
    }
}
