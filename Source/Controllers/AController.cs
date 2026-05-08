using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;

namespace GameProject.Controllers;

internal abstract class AController<ButtonsEnum, ButtonStateReference>(
  Dictionary<ButtonsEnum, IGPCommand> pressedMappings = null,
  Dictionary<ButtonsEnum, IGPCommand> downMappings = null,
  Dictionary<ButtonsEnum, IGPCommand> releasedMappings = null
) : IController<ButtonsEnum, ButtonStateReference> {
  internal Dictionary<ButtonsEnum, IGPCommand> PressedMappings { get; } = pressedMappings ?? [];
  internal Dictionary<ButtonsEnum, IGPCommand> DownMappings { get; } = downMappings ?? [];
  internal Dictionary<ButtonsEnum, IGPCommand> ReleasedMappings { get; } = releasedMappings ?? [];

  private protected abstract IButtonDiffTracker<ButtonsEnum, ButtonStateReference> ButtonTracker { get; }

  internal IEnumerable<ButtonsEnum> GetDown() {
    return ButtonTracker.GetDown();
  }

  internal IEnumerable<ButtonsEnum> GetPressed() {
    return ButtonTracker.GetPressed();
  }

  internal IEnumerable<ButtonsEnum> GetReleased() {
    return ButtonTracker.GetReleased();
  }

  internal void Update(ButtonStateReference buttonState) {
    ButtonTracker.Update(buttonState);

    foreach (ButtonsEnum button in ButtonTracker.GetPressed()) {
      if (PressedMappings.TryGetValue(button, out var command)) {
        command.Execute();
      }
    }

    foreach (ButtonsEnum button in ButtonTracker.GetDown()) {
      if (DownMappings.TryGetValue(button, out var command)) {
        command.Execute();
      }
    }

    foreach (ButtonsEnum button in ButtonTracker.GetReleased()) {
      if (ReleasedMappings.TryGetValue(button, out var command)) {
        command.Execute();
      }
    }
  }
}
