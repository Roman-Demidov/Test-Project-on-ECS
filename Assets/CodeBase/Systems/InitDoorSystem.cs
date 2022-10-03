using codeBase.components;
using codeBase.unityComponents;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class InitDoorSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var doors = GameObject.FindObjectsOfType<UnityDoorComponent>();
            var doorPool = world.GetPool<DoorComponent>();
            var colorIdPool = world.GetPool<ColorIdComponent>();
            var transformPool = world.GetPool<TransformComponent>();
            var positionPool = world.GetPool<PositionComponent>();

            foreach (var door in doors)
            {
                var doorEntity = world.NewEntity();
                doorPool.Add(doorEntity);

                ref var doorComponent = ref doorPool.Get(doorEntity);

                colorIdPool.Add(doorEntity).colorType = door.colorType;
                transformPool.Add(doorEntity).transform = door.doorTransform;
                positionPool.Add(doorEntity).position = door.doorTransform.position;
                doorComponent.openPosition = door.openTransform.position;
                doorComponent.closePosition = door.closeTransform.position;
                doorComponent.moveSpeed = door.moveSpeed;
            }
        }
    }
}