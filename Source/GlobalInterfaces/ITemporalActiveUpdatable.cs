namespace GameProject.GlobalInterfaces;

internal interface ITemporalActiveUpdatable {
  void Update(double deltaTime, bool isActive);
}
