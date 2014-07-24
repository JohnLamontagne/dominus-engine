namespace Dominus_RPG_Core.World.Entities.Stats
{
    public interface IStat<T>
    {
        T GetPoints();

        void SetPoints(T points);

        void AddPoints(T points);

        void RemovePoints(T points);
    }
}
