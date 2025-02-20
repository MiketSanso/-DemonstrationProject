using Domain.Business.UseCases;
using Zenject;

namespace Common.Installers
{
    public class UseCasesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CreateTextFlyAnimationUnderEntityUseCase>().FromNew().AsSingle();
        }
    }
}