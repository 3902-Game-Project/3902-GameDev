Controls:
  Main Menu:
    Key                      Action
    Enter/GamePadB           Go from main menu to game.
    Q/GamePadA               Quit the game.

  Game:
    Key                      Action
    W/Up/DPadUp              Moves player upwards.
    S/Down/DPadDown          Moves player downwards.
    A/Left/DPadLeft          Moves player leftwards.
    D/Right/DPadRight        Moves player rightwards.
    Z/N/GamePadB             Cause the player to use item / attack.
    D0                       Switch to sidearm item.
    D1                       Switch to primary item.
    D2                       Switch to consumable item.
    D3                       Switch to melee item.
    E/GamePadX               Instantly kills the player.
    Q/GamePadY               Quit the game.
    R/GamePadA               Reset to main menu.
    Y/MouseLeft/RightBumper  Debug: switch to next level.
    T/MouseRight/LeftBumper  Debug: switch to previous level.

Code Metrics:
  Raw data stored at "Info/Grader Info/Code Metrics/Code Metrics.txt"
  Graph at "Info/Grader Info/Code Metrics/Sprint 3/TODO"

Code Reviews:
  Found in the "Info/Grader Info/Code Review/Sprint 3" folder.

Known bugs:
  Enemies can be killed after they are dead and replay their death animation.
  Can collide with and get killed by dead or dying enemies.
  Cannot move to escape block if just barely collided into it.
  Gamepad support not currently complete. We ran out of time.

Sprint 2 Report:
The burndown chart shows almost zero progress being made on the sprint since the start of the sprint. However, this is because the tasks on Jira do not properly correspond to the work being done on the project and because Jira was not updated to reflect the work on the project. In terms of actual functionality, by the afternoon of 2026-02-22 most of the functionality of the sprint was present. The only things missing were player and enemy weapon firing, and the ability to switch between the enemies to render. Additionally, functionality was still being added after week 2 of the sprint. This made code reviews less complete and provided less time for refactoring. In the future, it would be preferable to complete the sprint early to give time for code reviews, but this might not be feasable.
Member specific:
  Santosh:
  At the end of the sprint I feel I should have taken on a bit more work, as I feel my role didn't take much work to achieve. Thus I could try a bigger role for sprint 3.

Sprint 3 Report:
The burndown chart shows almost no progress throughout the 3 weeks of the sprint, but then a sharp spike downward near the very end of the sprint. While this is partly because Jira might not have been updated as frequently as it could have, it was also because the tasks on Jira were mainly large-scope tasks that would take the entire sprint to finish, so they couldn't really be marked as "finished" until basically the end of the sprint. In the future, we could try to break up tasks into smaller chunks on Jira so that we can measure our progress better. We also had trouble finishing all features this sprint, but the timeline for the sprint was shorter than usual and spring break was nearing so this is likely a sprint 3 issue that won't appear in future sprints. In general, everyone seemed to be a bit more on top of this sprint than the previous sprint, contributing where they could and taking the initiative. In terms of actual functionality, sprint 2 code was removed (rendering of 1 enemy, block, and item), level rendering was added with support for fading between levels, and collision handling was created. The only functionality missing is the ability for the player to pick up keys and use the keys to unlock doors (a form of collision). Additionally, gamepad support was added but the functionality to switch between held items is missing. Additionally, some collisions are a bit buggy and might need to be fixed now or at the start of sprint 4. For example, enemies pushing the player back on contact was intentional behavior but it feels jank and can cause strange results like ping-ponging around many enemies and getting pushed across half the level in a few frames.

Credits:
  See "Info/Credits.txt" for attributions on this project.
