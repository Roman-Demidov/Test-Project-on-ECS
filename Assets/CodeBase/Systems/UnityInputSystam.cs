using codeBase.components;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class UnityInputSystam : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            if (Input.GetMouseButtonDown(0))
            {
                var inputEntity = world.NewEntity();
                world.GetPool<EventMouseInputComponent>().Add(inputEntity);
            }
        }
    }
}