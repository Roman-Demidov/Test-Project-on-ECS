using codeBase.components;
using codeBase.factories;
using codeBase.unityComponents;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class ButtonInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var buttons = new ObjectFactory().getObjectsFromScene<UnityButtonComponent>();
            var buttonPool = world.GetPool<ButtonComponent>();
            var tracsformPool = world.GetPool<TransformComponent>();
            var colorIdPool = world.GetPool<ColorIdComponent>();

            foreach (var button in buttons)
            {
                var buttonEntity = world.NewEntity();
                buttonPool.Add(buttonEntity);
                tracsformPool.Add(buttonEntity).transform = button.transform;
                colorIdPool.Add(buttonEntity).colorType = button.colorType;
            }
        }
    }
}