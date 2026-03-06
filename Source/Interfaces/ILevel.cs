namespace GameProject.Interfaces;

internal interface ILevel : IInitable, IUpdatable, IGPDrawable {
  void AddPickup(IWorldPickup pickup);
}
