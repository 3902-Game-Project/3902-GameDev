using GameProject.GlobalInterfaces;

namespace GameProject.Misc;

internal class GPTimer : ITemporalUpdatable {
  internal double Time { get; private set; }

  internal void Update(double deltaTime) {
    Time += deltaTime;
  }

  internal void OffsetTime(double offset) {
    Time += offset;
  }

  internal void SetTime(double time) {
    Time = time;
  }
}
