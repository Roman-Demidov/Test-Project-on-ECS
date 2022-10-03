using codeBase.components;
using codeBase.unityComponents;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class InitButtonSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var buttons = GameObject.FindObjectsOfType<UnityButtonComponent>();
            var buttonPool = world.GetPool<ButtonComponent>();
            var tracsformPool = world.GetPool<TransformComponent>();
            var positionPool = world.GetPool<PositionComponent>();
            var radiusPool = world.GetPool<RadiusComponent>();
            var activePool = world.GetPool<ActiveComponent>();
            var colorIdPool = world.GetPool<ColorIdComponent>();

            foreach (var button in buttons)
            {
                var buttonEntity = world.NewEntity();
                buttonPool.Add(buttonEntity);
                tracsformPool.Add(buttonEntity).transform = button.transform;
                colorIdPool.Add(buttonEntity).colorType = button.colorType;
                positionPool.Add(buttonEntity).position = button.transform.position;
                radiusPool.Add(buttonEntity).radius = button.radius;
                activePool.Add(buttonEntity);
            }
        }
    }
}