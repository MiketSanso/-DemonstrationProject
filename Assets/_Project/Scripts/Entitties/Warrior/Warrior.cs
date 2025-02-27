using Cysharp.Threading.Tasks;
<<<<<<< HEAD
using System;

namespace GameScene.Characters.Warrior
{
    public class Warrior : AttackEnemy
    {
        protected override async UniTask Perk(AttackEnemy enemy, AttackEnemy character)
        {
            enemy.StopTaskAttack();

            await UniTask.Delay(TimeSpan.FromSeconds(character.Character.DurationPerk));

            if (enemy != null)
                enemy.StartTaskAttack(character, enemy);
=======

namespace GameScene.Character.Warrior
{
    public class Warrior : AttackEnemy
    {
        protected override async UniTask Perk(CharacterUI enemy, CharacterUI character)
        {
            enemy.AttackEnemy.StopTaskAttack();

            await UniTask.Delay(character.Character.DurationPerk.Get() * 1000);

            if (enemy != null)
                enemy.AttackEnemy.StartTaskAttack(character, enemy);
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
        }
    }
}