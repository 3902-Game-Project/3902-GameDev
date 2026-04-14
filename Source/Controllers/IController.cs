using GameProject.GlobalInterfaces;

namespace GameProject.Controllers;

internal enum UseType {
  Pressed,
  Held,
  Released,
}

internal interface IController : IGPUpdatable { }
