using codeBase.components;
using codeBase.factories;
using codeBase.unityComponents;
using Leopotam.EcsLite;

namespace codeBase.systems
{
    public class DoorInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var doors = new ObjectFactory().getObjectsFromScene<UnityDoorComponent>();
            var doorPool = world.GetPool<DoorComponent>();
            var colorIdPool = world.GetPool<ColorIdComponent>();

            foreach (var door in doors)
            {
                var doorEntity = world.NewEntity();
                doorPool.Add(doorEntity);

                ref var doorComponent = ref doorPool.Get(doorEntity);

                colorIdPool.Add(doorEntity).colorType = door.colorType;
                doorComponent.door = door.doorTransform;
                doorComponent.open = door.openTransform;
                doorComponent.close = door.closeTransform;
                doorComponent.moveSpeed = door.moveSpeed;
            }
        }
    }
}