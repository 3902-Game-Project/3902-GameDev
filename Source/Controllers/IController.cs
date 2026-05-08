using System.Collections.Generic;
using GameProject.Commands;

namespace GameProject.Controllers;

internal enum UseType {
  Pressed,
  Held,
  Released,
}

internal interface IController<ButtonsEnum, ButtonStateReference> {
  internal Dictionary<ButtonsEnum, IGPCommand> PressedMappings { get; }
  internal Dictionary<ButtonsEnum, IGPCommand> DownMappings { get; }
  internal Dictionary<ButtonsEnum, IGPCommand> ReleasedMappings { get; }
  internal IEnumerable<ButtonsEnum> GetDown();
  internal IEnumerable<ButtonsEnum> GetPressed();
  internal IEnumerable<ButtonsEnum> GetReleased();
  internal void Update(ButtonStateReference buttonState);
}
