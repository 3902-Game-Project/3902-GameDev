using System.Collections.Generic;
using GameProject.Commands;
using GameProject.GlobalInterfaces;

namespace GameProject.Controllers;

internal enum UseType {
  Pressed,
  Held,
  Released,
}

internal interface IController<ButtonsEnum> : IInstantaneousUpdatable {
  Dictionary<ButtonsEnum, IGPCommand> PressedMappings { get; }
  Dictionary<ButtonsEnum, IGPCommand> DownMappings { get; }
  Dictionary<ButtonsEnum, IGPCommand> ReleasedMappings { get; }
}
