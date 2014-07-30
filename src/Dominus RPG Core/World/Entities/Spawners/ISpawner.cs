using Dominus_Core;

namespace Dominus_RPG_Core.World.Entities.Spawners
{
    public interface ISpawner<T> : IGameObject where T : IGameObject, IEntity
    {
        int SpawnInterval { get; set; }

        void ForceSpawn();

        // T GetSpawnables
    }
}