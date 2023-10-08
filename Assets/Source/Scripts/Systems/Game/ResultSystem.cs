using Kuhpik;

namespace Source.Scripts.Systems.Game
{
    public class ResultSystem : GameSystem
    {
        public override void OnInit()
        {
            ApplyResult();
        }

        private void ApplyResult()
        {
            var victory = game.MyMoney > game.OpponentMoney;
                    
            if (victory)
            {
                player.winsCount++;
                player.Money += game.MyMoney + game.OpponentMoney;
                player.RecordMoney += game.MyMoney + game.OpponentMoney;
                Bootstrap.Instance.SaveGame();
            }
        }
    }
}