using GameProject.GlobalInterfaces;

namespace GameProject.Misc;

internal class GPTimer : ITemporalUpdatable {
  public double Time { get; private set; }

  public void Update(double deltaTime) {
    Time += deltaTime;
  }

  public void OffsetTime(double offset) {
    Time += offset;
  }

  public void SetTime(double time) {
    Time = time;
  }
}
