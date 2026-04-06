# Customizable Special Rounds

A plugin for **SCP: Secret Laboratory** servers running the [EXILED](https://github.com/ExMod-Team/EXILED) framework. Each round, a special game mode is randomly selected and put to a player vote — adding variety and chaos to your server without changing the base game.

> **Current version:** 0.9.1  
> **Author:** zaza  
> **Framework:** EXILED (v.9.13.3)

---

## License

To quote Freddy Mercury:

> *"Do whatever you want with my music, just don't make it boring."*

The same principle also applies to this plugin: everyone is free to share, modify, distribute and use this plugin in any way they wish without notifying me.

However, if you plan on using Customizable Special Rounds on a public server or modify it in a public manner (creating your own repository of it, for example), I'd like to kindly ask you to let me know so that I can add your usage/contribution to my portfolio. You can contact me via discord:

@24th_was_thursday

## How It Works

1. When the **first player connects** to the lobby, a special round is randomly chosen (it will never be the same as the previous round).
2. A **vote** is broadcast to all players in the lobby. Players have a configurable amount of time to vote **yes** or **no**.
3. Players who don't vote count as **no** votes.
4. If **yes** votes outnumber **no + absent** votes, the special round is enabled for that round.
5. At round start, the special round's effects are applied automatically.

Voting can be disabled in the config, in which case the special round is applied automatically without a vote.

---

## Special Rounds

| Name               | Description                                                                                                                                                                                                                                                                                                             |
| ------------------ | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Payday**         | All non-SCP players receive a configurable number of coins at round start.                                                                                                                                                                                                                                              |
| **Vitality Shift** | All players spawn with multiplied HP. Human and SCP multipliers are configured separately.                                                                                                                                                                                                                              |
| **Sweet Tooth**    | All non-SCP players receive an SCP-330 bag filled with a configurable number of pink candies.                                                                                                                                                                                                                           |
| **Forest Gump**    | All players spawn with a speed boost effect at a configurable intensity.                                                                                                                                                                                                                                                |
| **Super Balling**  | All non-SCP players receive a configurable number of SCP-018s at round start.                                                                                                                                                                                                                                           |
| **Chill**          | All non-SCP players receive a configurable number of SCP-244s at round start.                                                                                                                                                                                                                                           |
| **Zerg Rush**      | All non-SCP players are forced into a single configurable role at round start.                                                                                                                                                                                                                                          |
| **Drug Testing**   | Every player spawns with a random status effect — could be a buff, a debuff, or something in between.                                                                                                                                                                                                                   |
| **One Man Army**   | One random player becomes the **Chosen One**: they spawn in a configurable role with 10,000 HP (configurable) and a full arsenal (MicroHID, FRMG-0, Logicer, E-11 SR, and more). Every other player spawns as a configurable SCP role with 1,800 HP (configurable). No respawns. The Chosen One cannot pick up SCP-500. |

---

## Installation

1. Make sure [EXILED](https://github.com/ExMod-Team/EXILED) of a compatible version is installed on your server.
2. Download the latest `.dll` release from the [Releases](../../releases) page.
3. Place the `.dll` file in your server's `EXILED/Plugins` folder.
4. Restart your server. A config file will be generated automatically.

---

## Commands

All commands are subcommands of `specialrounds`. The command is available in the **game console** (`` ` ``), **client console**, and **Remote Admin**.

| Command | Aliases |
|---|---|
| `specialrounds` | `spr`, `csr`, `specialr`, `csrounds`, `srounds` |

### Player Subcommands

These are available to all players and work **only during the voting window**.

| Subcommand | Aliases | Description |
|---|---|---|
| `specialrounds yes` | `y`, `1`, `+` | Vote **yes** for the current special round. |
| `specialrounds no` | `n`, `0`, `-` | Vote **no** for the current special round. |

### Remote Admin Subcommands

These are available to players with Remote Admin access.

| Subcommand | Aliases | Description |
|---|---|---|
| `specialrounds info` | `i`, `information` | Display plugin info: current/previous special round, voting status, vote counts, and all configurable values. |

---

## Configuration

The config file is generated at `EXILED/Configs/` on first run.

| Key | Default | Description |
|---|---|---|
| `is_enabled` | `true` | Enable or disable the plugin. |
| `debug` | `false` | Enable debug mode. |
| `voting_is_allowed` | `true` | Whether players can vote before each round. If `false`, the special round is applied automatically. |
| `voting_duration` | `20` | How long the voting window lasts, in seconds. |
| `payday_coins_at_start` | `1` | Number of coins given per player in **Payday**. |
| `vitality_shift_human_role_health_multiplier` | `2.0` | HP multiplier for human roles in **Vitality Shift**. |
| `vitality_shift_scp_health_multiplier` | `2.0` | HP multiplier for SCPs in **Vitality Shift**. |
| `sweet_tooth_pink_candies_at_start` | `1` | Number of pink candies in the SCP-330 bag in **Sweet Tooth**. |
| `forest_gumo_speed_effect_intensity` | `1` | Intensity of the speed effect in **Forest Gump**. |
| `super_balling_scp018_at_start` | `1` | Number of SCP-018s given per player in **Super Balling**. |
| `chill_scp244_at_start` | `1` | Number of SCP-244s given per player in **Chill**. |
| `zerg_rush_role_id` | `1` | The role ID all players spawn as in **Zerg Rush**. |
| `one_man_army_scp_role_id` | `16` | The role ID all non-chosen players spawn as in **One Man Army**. |
| `one_man_army_chosen_one_role_id` | `15` | The role ID the Chosen One spawns as in **One Man Army**. |
| `one_man_army_chosen_one_health` | `10000` | Starting HP of the Chosen One in **One Man Army**. |
| `one_man_army_scp_health` | `1800` | Starting HP of each SCP player in **One Man Army**. |

> For role IDs, refer to the full list [here](https://steamcommunity.com/sharedfiles/filedetails/?id=2630379740).

---
