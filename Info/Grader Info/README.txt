Controls:
  Main Menu:
    Key                               Action
    Q/GamePadY                        Quit the game.
    Enter/GamePadB                    Go from main menu to game.
    S/GamePadX                        Toggle slow reaction time mode (game runs at 0.5x speed).

  Main Game Screen:
    Key                               Action
    Q/GamePadY                        Quit the game.
    Backspace/GamePadA                Reset to main menu.
    P/GamePadX                        Pause the game.
    I/RightTrigger                    Open item screen.
    J/GamePadB                        Cause the player to use item / attack.
    K/LeftTrigger                     Use key item.
    F/LeftStick                       Pick up a nearby item (interact).
    Space/RightStickUp                Swap to other weapon.
    C/RightStickDown                  Drop the current item.
    R/Start                           Force reload weapon (auto reloads when empty anyways).
    L/RightStick                      Instantly kills the player.
    Tab/Back                          Toggle music.
    N                                 Save the game 
    M                                 Load the game

    W/Up/DPadUp/LeftStickUp           Moves player upwards.
    S/Down/DPadDown/LeftStickDown     Moves player downwards.
    A/Left/DPadLeft/LeftStickLeft     Moves player leftwards.
    D/Right/DPadRight/LeftStickRight  Moves player rightwards.

    Y/MouseLeft/RightBumper           Debug: switch to next level.
    T/MouseRight/LeftBumper           Debug: switch to previous level.
    H/RightStickRight                 Debug: toggle halt game (disable almost all Update methods).

    Cheat Codes:
      U = up; D = down; L = left; R = right

      Sequence                        Action
      WWSSADAD/UUDDLRLR               Unlimited health
      WASDWASD/ULDRULDR               Unlimited ammo
      WWAADDSS/UULLRRDD               Unlimited items
      1212                            Toggle vignette
      67                              Toggle halt all enemies
      69                              Kill player
      3902                            Gameplay testing mode: Unlimited health, unlimited ammo
      123456                          Debug: Kill all enemies on level

  Pause Screen:
    Q/GamePadY                        Quit the game.
    P/GamePadB                        Return to game.

  Item Selection Screen:
    Q/GamePadY                        Quit the game.
    I/GamePadA                        Return to game.
    W/Up/DPadUp                       Move upward in the menu.
    S/Down/DPadDown                   Move downward in the menu.
    A/Left/DPadLeft                   Move leftward in the menu.
    D/Right/DPadRight                 Move rightward in the menu.
    Enter/Space/GamePadB              Equip the selected item.
    C/GamePadX                        Drop the selected item.

  Win/Loss Screen:
    Key                               Action
    Q/GamePadY                        Quit the game.
    Backspace/GamePadA                Reset to main menu.

Code Metrics:
  Raw data stored at "Info/Grader Info/Code Metrics/Code Metrics.txt"
  Graph at "Info/Grader Info/Code Metrics/Sprint 4/2026-04-13.png"

Code Reviews:
  Found in the "Info/Grader Info/Code Review/Sprint 4" folder.

Burndown Charts:
  Found in the "Info/Grader Info/Burndown Charts" folder.

Jira task screenshots:
  Found in the "Info/Grader Info/Jira Sprint Reports" folder.

Known bugs:
  -Can push crates through walls and enemies forced to move along with crate.
  -Mouse clicks are registered even if clicking outside the game window. This seems more to be a bug with MonoGame than the game itself.
  -Map at top of screen does not show what room player actually is in and is a static image.
  -Enemies sometimes can walk off screen.
  -Confusion in codebase between gunstats, ammostats, etc.

Hard limitations:
  -Gamepad support uses really strange gamepad inputs like right stick up/down for single button keybinds. This is a physical limitation that cannot be fixed as gamepads have less buttons than keyboards.

Sprint 2 Report:
  The burndown chart shows almost zero progress being made on the sprint since the start of the sprint. However, this is because the tasks on Jira do not properly correspond to the work being done on the project and because Jira was not updated to reflect the work on the project. In terms of actual functionality, by the afternoon of 2026-02-22 most of the functionality of the sprint was present. The only things missing were player and enemy weapon firing, and the ability to switch between the enemies to render. Additionally, functionality was still being added after week 2 of the sprint. This made code reviews less complete and provided less time for refactoring. In the future, it would be preferable to complete the sprint early to give time for code reviews, but this might not be feasable.

  Member specific:
    Santosh:
      At the end of the sprint I feel I should have taken on a bit more work, as I feel my role didn't take much work to achieve. Thus I could try a bigger role for sprint 3.

Sprint 3 Report:
  The burndown chart shows almost no progress throughout the 3 weeks of the sprint, but then a sharp spike downward near the very end of the sprint. While this is partly because Jira might not have been updated as frequently as it could have, it was also because the tasks on Jira were mainly large-scope tasks that would take the entire sprint to finish, so they couldn't really be marked as "finished" until basically the end of the sprint. In the future, we could try to break up tasks into smaller chunks on Jira so that we can measure our progress better. We also had trouble finishing all features this sprint, but the timeline for the sprint was shorter than usual and spring break was nearing so this is likely a sprint 3 issue that won't appear in future sprints. In general, everyone seemed to be a bit more on top of this sprint than the previous sprint, contributing where they could and taking the initiative. In terms of actual functionality, sprint 2 code was removed (rendering of 1 enemy, block, and item), level rendering was added with support for fading between levels, and collision handling was created. The only functionality missing is the ability for the player to pick up keys and use the keys to unlock doors (a form of collision). Additionally, gamepad support was added but the functionality to switch between held items is missing. Additionally, some collisions are a bit buggy and might need to be fixed now or at the start of sprint 4. For example, enemies pushing the player back on contact was intentional behavior but it feels jank and can cause strange results like ping-ponging around many enemies and getting pushed across half the level in a few frames.

  Member specific:
    Santosh:
      At the end of the sprint I feel I maybe tried to do too much in this shorter sprint as I wasn't able to finish everything (gamepad support) in time. I could try to do less next sprint but I think I can do the same amount of work next sprint as next sprint will be longer.

Sprint 4 Report:
  The burndown chart doesn't show much progress until the end of the sprint, but even then, less than half of the Jira tasks are completed. This is in part due to many of the team members prioritizing more immediate schoolwork in other classes over this project, and additionaly is due to our attempt to address refactoring suggestions of sprint 3. The refactoring suggestions ended up taking a lot of time and cut into time we would have been working on game features. Additionally the amount of Jira task items in general is huge this sprint, partly from refactoring, partly from backlog from previous sprints, partly from bugs, and partly from adding many tasks to "get the game to actually feel like a game." So completing all Jira tasks might be very difficult unless each of us had 20-30 hours more time to work on the sprint. Like sprint 2, not all code reviews were completed.

  In terms of features:
    We added sound effects and toggleable background music.
    Upon player death, a game over screen occurs and forces going back to main menu and thus resetting the game.
    A heads-up display was added, with health, ammo, dungeon layout, and keys player has, but missing player held items like weapons.
    Game states and a smooth fade transition was added for item selection screen, game over screen, win screen, and pause screen.
      Smooth fade does not occur between game and pause state and vice versa, this was intentional design decision as pause state shouldn't have fading.
      An additional game transition state was added that smoothly transitions between 2 game states.
    Removal of magic numbers / strings was not accomplished (the person who it was assigned to did not complete the task).
    No research on sprint 5 features was done as we were too busy trying to complete sprint 4 before the deadline.
  
  Member specific:
    Santosh:
      I tried to do even more work than in the last sprint, due to the large swath of refactoring changes, fixing various bugs that appeared during the course of the sprint, and doing many, many small tasks that came to mind as I was coding that wasn't originally on Jira. Parts of the codebase are much cleaner and more organized as a result. I was able to implement all features assigned to me, but didn't have time to implement all refactoring changes assigned to me. This is the first sprint where I have uncompleted tasks.

Credits:
  See "Info/Credits.txt" for attributions on this project.
