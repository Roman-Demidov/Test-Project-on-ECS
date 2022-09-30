using codeBase.components;
using codeBase.monoBehaivours;
using codeBase.scriptableObjects;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private Animator _animator;
        private Transform _transform;

        public PlayerInitSystem(Animator animator, Transform transform)
        {
            _animator = animator;
            _transform = transform;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var characterData = systems.GetShared<PlayerSettings>();
            var playerEntity = world.NewEntity();
            var playerPool = world.GetPool<PlayerComponent>();
            var animatorPool = world.GetPool<AnimatorComponent>();
            var trnasformPool = world.GetPool<TransformComponent>();
            _transform.GetComponentInChildren<CollisionCheckerView>().world = world;

            playerPool.Add(playerEntity).moveSpeed = characterData.moveSpeed;
            animatorPool.Add(playerEntity).animator = _animator;
            trnasformPool.Add(playerEntity).transform = _transform;
        }
    }
}