namespace GameProject.Interfaces;

public interface ILevel : IInitable, IUpdatable, IGPDrawable {
  void AddPickup(IWorldPickup pickup);
}
