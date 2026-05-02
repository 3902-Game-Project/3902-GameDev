using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;

namespace GameProject.Controllers;

internal abstract class AController<ButtonsEnum, ButtonStateReference>(
  Dictionary<ButtonsEnum, IGPCommand> pressedMappings = null,
  Dictionary<ButtonsEnum, IGPCommand> downMappings = null,
  Dictionary<ButtonsEnum, IGPCommand> releasedMappings = null
) : IController<ButtonsEnum> {
  public Dictionary<ButtonsEnum, IGPCommand> PressedMappings { get; } = pressedMappings ?? [];
  public Dictionary<ButtonsEnum, IGPCommand> DownMappings { get; } = downMappings ?? [];
  public Dictionary<ButtonsEnum, IGPCommand> ReleasedMappings { get; } = releasedMappings ?? [];

  protected abstract IButtonDiffTracker<ButtonsEnum, ButtonStateReference> ButtonTracker { get; }

  protected abstract ButtonStateReference GetButtonState();

  public void Update() {
    ButtonStateReference buttonState = GetButtonState();

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
