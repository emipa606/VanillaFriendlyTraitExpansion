# Vanilla-Friendly Trait Expansion (Continued) GitHub Copilot Instructions

## Mod Overview and Purpose

The Vanilla-Friendly Trait Expansion (Continued) mod is an update to the original Vanilla-Friendly Trait Expansion mod by Garthor. This mod adds a variety of new traits that are designed to be balanced with vanilla traits while offering more interesting gameplay mechanics. The traits aim to enhance the player experience by providing unique abilities and characteristics to colonists without overwhelming them.

## Key Features and Systems

### Traits
The mod introduces the following traits:
1. **Animal Empathy**: The colonist cannot harm animals and is not harmed by them.
2. **Slob**: Produces a lot of filth and does not clean.
3. **Caravaneer**: Provides a bonus move speed to caravans.
4. **Boring**: Bores other colonists.
5. **Teacher**: Can teach skills to other pawns.
6. **Drunken Master**: Gains large bonuses to melee combat while drunk.
7. **Berserker**: Enters a berserk rage in combat, becoming significantly stronger but uncontrollable.
8. **Juggernaut**: Immune to stuns and staggers.

### Mental States
The mod includes custom mental states such as Berserker Rage and Boring, which can affect the colonists' behavior and interactions.

### XML Integration
The mod defines its traits, mental states, thoughts, and other game elements using XML files. These files are located in the `Defs` folder and are crucial for defining the properties and behaviors of the new traits and mental states.

### Harmony Patching
The mod uses Harmony to patch existing game mechanics and integrate new behaviors. Harmony is a powerful tool that allows mods to modify the game's code without altering the original files. This ensures compatibility and stability.

## Coding Patterns and Conventions

### C# Files
The mod's C# files are located in the `Source` folder and are responsible for implementing the logic behind the new traits and mental states. Key files include:
- `JecsTools.cs`: Contains utility methods for compatibility with other mods.
- `GMT_Hediff_Berserker_Ire.cs` and `GMT_Hediff_Berserker_Rage.cs`: Implement the Berserker rage mechanics.
- `GMT_JobDriver_AttackMeleeDowned.cs` and `GMT_JobGiver_FightDownedEnemies.cs`: Define combat behaviors for the colonists.

### XML Files
The mod's XML files are located in the `Defs` folder and are used to define the properties of the new traits, mental states, and thoughts. Key files include:
- `BerserkerMentalStateDefs.xml`: Defines the Berserker rage mental state.
- `BoringMentalStateDefs.xml`: Defines the Boring mental state.
- `ThoughtDefs.xml`: Defines custom thoughts.
- `TraitDefs.xml`: Defines the new traits.

## Suggestions for Copilot

To ensure that GitHub Copilot is useful for this mod, here are some suggestions:
1. **Trait Definitions**: When asked to define a new trait, Copilot should focus on creating a balanced and interesting trait that aligns with the mod's purpose.
2. **Mental State Implementations**: When asked to implement a mental state, Copilot should provide code that integrates seamlessly with the game's existing mental state system.
3. **Harmony Patches**: When asked to create a Harmony patch, Copilot should provide code that is well-commented and follows the Harmony patching conventions.
4. **XML Files**: When asked to create or modify an XML file, Copilot should provide a well-structured XML file that follows the game's naming conventions and structure.

By following these guidelines, GitHub Copilot can be a valuable tool for mod developers working on the Vanilla-Friendly Trait Expansion (Continued) mod.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

