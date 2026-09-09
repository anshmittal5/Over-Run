## Playtest Notes

### What felt good?
-

### What felt bad?
-

### One thing to improve tomorrow
-

# Development Log

## Date: 4 August 2026

### Completed

- Implemented the Shockwave weapon.
- Added Q key input using the new Input System.
- Implemented cooldown timer.
- Ability starts on cooldown at the beginning of the game.
- Created ShockWave prefab.
- Added damage using Trigger Collider.
- Added expanding wave effect using localScale.
- Made the ShockWave follow the player by parenting it.
- Adjusted initial scale to improve gameplay feel.
- Debugged scaling issue and confirmed the problem was lifetime/visual size rather than code.

### Design Decisions

- Shockwave should feel like an instant magical blast instead of slowly growing from a tiny circle.
- Future versions will replace the placeholder circle with a fantasy lightning ring.
- Version 2 will include multiple charge stages with stronger effects.

### Problems Found

- Small starting scale made the shockwave appear ineffective.
- Camera zoom affected perceived size.
- Shockwave needed to stay attached to the player while expanding.

### Next Tasks

- Finish environment generation.
- Add grass/tile background.
- Add obstacles with collisions.
- Playtest combat balance.