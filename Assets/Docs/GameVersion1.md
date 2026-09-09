# 🎮 Game Version 1 Design Document
**Project:** Untitled Top-Down Survival Game (Portfolio Project)

---

# ⚠️ Version 1 Development Rules

This document defines the complete scope of Version 1.

Once approved, **Version 1 is frozen.**

## Rules

- ❌ No new gameplay mechanics.
- ❌ No new systems.
- ❌ No new content outside this document.
- ❌ No scope creep.

The only allowed changes are:

- 🐞 Bug Fixes
- ⚖️ Gameplay Balancing
- ✨ Polish
- 🔄 Replacing an existing feature only if playtesting proves it is not fun.

Every new idea must be written inside **Ideas.md**.

> **We finish before we expand.**

---

# 🎯 Project Goal

Create a polished, complete, and enjoyable Unity game that demonstrates gameplay programming, clean architecture, and polish suitable for applying to Unity Gameplay Programmer internships.

The objective is **not** to build the biggest game.

The objective is to build the most polished game possible within Version 1's scope.

---

# 🧩 Core Gameplay

## Player

- [x] Player movement
- [x] Dash ability
- [x] Dash cooldown
- [x] Player health
- [x] Player death
- [x] Player hurt feedback

---

## Combat

### Primary Weapon

- [x] Auto-target nearest enemy
- [x] Bullet firing
- [x] Bullet movement
- [x] Bullet lifetime
- [x] Bullet collision
- [x] Bullet damage

### Secondary Weapon

Area of Effect ability

- [x] Manual activation
- [x] Circular damage around player
- [x] Long cooldown
- [x] Knockback enemies
- [x] Visual effect

---

## Enemy

- [x] Enemy AI
- [x] Enemy spawning
- [x] Enemy movement
- [x] Enemy attack
- [x] Enemy health
- [x] Enemy damage
- [x] Enemy death

---

## Gameplay Loop

Complete gameplay loop must function.

```
Start Game
↓

Survive
↓

Fight Enemies
↓

Avoid Damage
↓

Kill Enemies
↓

Player Dies
↓

Game Over
↓

Restart
```

---

# ⚖ Gameplay Polish

- [x] Camera feels smooth
- [x] Camera framing feels good
- [x] Player speed balanced
- [x] Enemy speed balanced
- [x] Bullet speed balanced
- [x] Dash distance balanced
- [x] Dash cooldown balanced
- [x] Weapon cooldown balanced
- [x] Enemy spawn rate balanced

---

## Enemy Behaviour

- [x] No enemy spawning inside player safe radius
- [x] Basic enemy separation (reduce overlapping)
- [x] Fair enemy spawning

---

# 🌍 Environment

## World

- [x] Ground
- [x] Background
- [x] Environment Spawner

---

## Environment Props

- [x] Trees
- [x] Rocks
- [x] Bushes
- [x] Small decorations

---

## Environment Polish

- [x] Random position
- [x] Random rotation
- [x] Random scale

---

## Collision

Objects that should block movement:

- [x] Rocks
- [x] Tree trunks
- [x] Walls

Objects without collision:

- [x] Grass
- [x] Bushes
- [x] Small decorations

---

# 🖥 User Interface

## Menus

- [x] Main Menu
- [x] Pause Menu
- [x] Game Over Screen

---

## Buttons

- [x] Play
- [x] Restart
- [x] Quit

---

## Gameplay UI

- [x] Player Health Bar
- [x] Dash Cooldown Indicator
- [x] Kill Counter
- [x] Survival Timer
- [x] Floating Damage Numbers

---

# 🔊 Audio

## Gameplay

- [x] Background Music
- [x] Gun Shot
- [x] Enemy Hurt
- [x] Enemy Death
- [x] Player Hurt
- [x] Player Death
- [x] Dash

---

## UI

- [x] Button Click
- [x] Menu Select

---

# ✨ Visual Polish

- [x] Enemy hit flash
- [x] Muzzle flash
- [x] Blood particles
- [x] Small enemy knockback
- [x] Death effect

---

# 💻 Code Quality

## Readability

- [x] Important functions documented
- [ ] Consistent naming
- [x] Easy to understand
- [x] Clean folder structure

---

## Expandability

Future Version 2 development should require adding new systems instead of rewriting old ones.

- [ ] Modular systems
- [x] Inspector-driven values
- [x] Avoid hardcoded gameplay values
- [x] Reusable scripts
- [x] Functions have one clear responsibility
- [ ] Low coupling between systems

---

## Performance

- [x] No unnecessary work in Update()
- [x] Avoid duplicated code
- [x] Simple and efficient logic
- [x] No Console Errors
- [ ] No major memory leaks during gameplay

---

# 🧪 Testing

## Gameplay

- [x] Multiple playtesting sessions
- [x] Balance pass
- [x] Bug fixing pass
- [ ] Final polish pass

---

## Technical

- [ ] Stable FPS
- [x] No game-breaking bugs
- [x] No missing references

---

# 📦 Portfolio Release

## Build

- [ ] Windows Build

---

## Portfolio Material

- [ ] Gameplay Trailer (30–60 seconds)
- [ ] Gameplay Screenshots
- [ ] README.md
- [ ] GitHub Repository
- [ ] itch.io Upload

---

# ✅ Version 1 Completion Criteria

Version 1 is considered complete only when:

- Every checkbox in this document is complete.
- The game is enjoyable to play.
- No major bugs remain.
- The project is suitable for showcasing to recruiters.
- The build is publicly available.

Only after completing Version 1 may development begin on Version 2.

---

# 🚫 Version 2 (Not Included)

These ideas are intentionally excluded from Version 1.

- Weapon durability
- Weapon repair kits
- Weapon upgrades
- Multiple weapon inventory
- Exploration maps
- Biomes
- Procedural world generation
- Boss rooms
- Arena gates
- NPCs
- Quests
- Story
- Save system
- Achievements
- Crafting
- Multiplayer

These ideas belong in **Ideas.md** and must not be implemented until Version 1 is complete.