# GitHub Copilot Instructions for RimWorld Modding Project

## Mod Overview and Purpose
This RimWorld mod project enhances the gameplay by adding various new traits, mental states, and interactions. The mod aims to diversify pawn behavior and interactions, providing a richer and more dynamic colony management experience for players. It includes features like new mental states, additional trait mechanics, specialized job drivers, and unique interactions based on personality traits.

## Key Features and Systems
- **Traits and Mental States**: Introduces new traits like "Juggernaut" and "Drunken Master," alongside mental states such as "Berserking" and "Bored To Sleep."
- **Custom Helpers**: Functions within helper classes to manage trait behaviors and effects, such as `GMT_Animal_Friend_Helper` and `GMT_Teacher_Helper`.
- **Job Drivers**: Specialized job drivers like `GMT_JobDriver_AttackMeleeDowned` allow for unique combat-related actions.
- **Harmony Patching**: Utilizes Harmony to patch existing game methods, modifying or extending RimWorld's default functionality.
- **XML Integration**: Although primarily in C#, XML is used for DefModExtensions and other similar integration points.

## Coding Patterns and Conventions
- **Class Structure**: Each class is designated for a specific system or feature, following a clear naming convention (e.g., `Building_Door_Tick_Patch` for patches related to building doors).
- **Internal Static Classes**: Used for utility and helper methods to ensure encapsulation and easy access.
- **Public Class Usage**: Used for components that need to interact with the broader game environment or other mods.

## XML Integration
XML is used minimally but plays a crucial role in integrating new traits and behaviors. DefModExtensions, such as `GMT_Trait_ModExtension`, aid in defining additional attributes and behaviors directly through XML, complementing the C# backend logic.

## Harmony Patching
- **Patch Methods**: Implemented using static classes with methods designed to target specific game functions. For example, `StunHandler_StunFor_Patch` modifies how stunning mechanics work.
- **Conditional Patching**: Classes like `PatchIfModAttribute` check for the presence of other mods before applying patches, ensuring compatibility and modularity.
- **Granular Control**: Patches are organized by their functional area, like `Verb_ValidateTarget_Patch` for target validation in combat.

## Suggestions for Copilot
- **Pattern Recognition**: When writing new patches, follow existing patterns seen in classes like `StaggerHandler_StaggerFor_Patch`.
- **Method Suggestions**: Leverage existing helper and utility methods like those found in `GMT_Slob_Helper` for consistent implementation of new features.
- **XML Code-Gen**: When XML integration is needed, auto-generate XML nodes based on class properties, especially for DefModExtensions.

These instructions provide a comprehensive guide for maintaining and expanding the RimWorld mod project using GitHub Copilot. They establish best practices and patterns for mod developers to follow and suggest how CoPilot can assist in enhancing productivity within this context.
