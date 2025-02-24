using Cysharp.Threading.Tasks;

namespace GameScene.Character.Warrior
{
    public class Warrior : TakeDamageEnemy
    {
        protected override async UniTask Perk(CharacterUI enemy, CharacterUI character)
        {
            enemy.TakeDamageEnemy.StopTaskAttack();

            await UniTask.Delay(character.Character.DurationPerk.Get() * 1000);

            if (enemy != null)
                enemy.TakeDamageEnemy.StartTaskAttack(character, enemy);
        }
    }
}