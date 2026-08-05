# Customizable Special Rounds

A plugin for **SCP: Secret Laboratory** servers running the [EXILED](https://github.com/ExMod-Team/EXILED) framework. Each round, a special game mode is randomly selected and put to a player vote — adding variety and chaos to your server without changing the base game.

> **Latest version:** 1.1.1  
> **Author:** zaza  
> **Framework:** EXILED

---

## How It Works

1. When the **first player connects** to the lobby, a Special Round is randomly chosen (it will never be the same as the previous round).
2. If voting is enabled, a **vote** is broadcast to all players in the lobby. Players have a configurable amount of time to vote **yes** or **no**.
3. Players who don't vote count as **no** votes.
4. If **yes** votes outnumber **no + absent** votes, the Special Round is enabled for that round.
5. At round start, the Special Round's effects are applied automatically.

Voting can be disabled in the config, in which case the Special Round is applied automatically without a vote.

---

## Special Rounds

| Name | Description |
|---|---|
| **Payday** | All non-SCP players receive a configurable number of coins every time they spawn. |
| **Vitality Shift** | All players spawn with multiplied HP. Human and SCP multipliers are configured separately. |
| **Sweet Tooth** | All non-SCP players receive an SCP-330 bag filled with a configurable number of pink candies at round start. *Let chaos reign!* |
| **Forest Gump** | All players spawn with a speed boost effect at a configurable intensity. |
| **Super Balling** | All non-SCP players receive a configurable number of SCP-018s at round start. |
| **Chill** | All non-SCP players receive a configurable number of SCP-244s at round start. |
| **Zerg Rush** | All non-SCP players are forced into a single configurable role at round start. |
| **Drug Testing** | Every player spawns with a random status effect — could be a buff, a debuff, or something in between. If the player has spawn protection, the effect is applied after it expires. |
| **One Man Army** | One random player becomes the **Chosen One**: they spawn in a configurable role with massive HP and a full arsenal (MicroHID, FRMG-0, Logicer, E-11 SR, and more). Every other player spawns as a configurable SCP role. No respawns. The Chosen One cannot pick up SCP-500, but is able to have 2 special weapons. |
| **Lights Out** | All lights on the map are permanently disabled. All SCP-player's health is multiplied by a configurable multiplier. All non-SCP players recieve a flashlight. |
| **Late For Shift** | Default inventory of all non-SCP players is randomized on spawn. MTF, CI and Guards recieve maximal carryable amount of every ammo type. |

---

## Installation

1. Make sure [EXILED](https://github.com/ExMod-Team/EXILED) is installed on your server.
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

Available to all players. Voting commands only work during the voting window.

| Subcommand | Aliases | Description |
|---|---|---|
| `specialrounds yes` | `y`, `1`, `+` | Vote **yes** for the current Special Round. |
| `specialrounds no` | `n`, `0`, `-` | Vote **no** for the current Special Round. |

### Remote Admin Subcommands

Available to players with Remote Admin access only.

| Subcommand | Aliases | Description |
|---|---|---|
| `specialrounds info` | `i`, `information` | Display plugin info: current/previous Special Round, voting status, vote counts, and all current round parameters. |
| `specialrounds pause` | `p`, `stop`, `unpause`, `up`, `resume` | Pause or unpause the current Special Round's effects. Cannot be used during voting. |
| `specialrounds reroll` | `rr`, `roll`, `change` | Reroll the Special Round that's currently being voted on. Only works during voting. |
| `specialrounds setparameter <key> <value>` | `setparam`, `sp`, `setp`, `set`, `setpar` | Override a parameter of the current Special Round at runtime. See the parameter key list below. |

### Parameter Keys (for `setparameter`)

These keys can be used with `specialrounds setparameter` to tweak a round's values mid-game.

| Special Round | Key | Description |
|---|---|---|
| Payday | `payday.coinsAtStart` | Number of coins given per spawn. |
| Vitality Shift | `vitalityShift.humanRoleHealthMultiplier` | HP multiplier for human roles. |
| Vitality Shift | `vitalityShift.scpHealthMultiplier` | HP multiplier for SCPs. |
| Sweet Tooth | `sweetTooth.pinkCandiesAtStart` | Number of pink candies in the bag. |
| Forest Gump | `forestGump.speedEffectIntensity` | Intensity of the speed effect. |
| Super Balling | `superBalling.scp018AtStart` | Number of SCP-018s given at round start. |
| Chill | `chill.scp244AtStart` | Number of SCP-244s given at round start. |
| Zerg Rush | `zergRush.roleId` | Role ID all players spawn as. |
| One Man Army | `oneManArmy.chosenOneRoleId` | Role ID the Chosen One spawns as. |
| One Man Army | `oneManArmy.chosenOneHealth` | Starting HP of the Chosen One. |
| One Man Army | `oneManArmy.scpRoleId` | Role ID all other players spawn as. |
| One Man Army | `oneManArmy.scpHealth` | Starting HP of all other players. |
| Lights Out | `lightsOut.scpHealthMultiplier` | HP multiplier for SCPs. |
| Late For Shift | `lateForShift.shouldGiveAmmo` | Whether a non-SCP player that recieves a weapon should also get max ammo for that weapon. |

> For role IDs, refer to the full list [here](https://steamcommunity.com/sharedfiles/filedetails/?id=2630379740).

---

## Configuration

The config file is generated at `EXILED/Configs/` on first run.

### General

| Key | Default | Description |
|---|---|---|
| `is_enabled` | `true` | Enable or disable the plugin. |
| `debug` | `false` | Enable debug mode. In debug mode, Tutorial players are treated as regular players. Moreover, debug mode enables print of useful debugging information in the server logs. |
| `should_affect_force_spawned_players` | `true` | Whether players who are force-spawned (via RA, commands, or other plugins) are affected by on-spawn Special Round effects. |
| `is_voting_enabled` | `true` | Whether players can vote before each round. If `false`, the Special Round is applied automatically. |
| `voting_duration` | `20` | How long the voting window lasts, in seconds. |

### Special Round Parameters

| Key | Default | Description |
|---|---|---|
| `payday_coins_at_start` | `1` | Number of coins given per spawn in **Payday**. |
| `vitality_shift_human_role_health_multiplier` | `2.0` | HP multiplier for human roles in **Vitality Shift**. |
| `vitality_shift_scp_health_multiplier` | `2.0` | HP multiplier for SCPs in **Vitality Shift**. |
| `sweet_tooth_pink_candies_at_start` | `1` | Number of pink candies in the SCP-330 bag in **Sweet Tooth**. |
| `forest_gump_speed_effect_intensity` | `1` | Intensity of the speed effect in **Forest Gump**. |
| `super_balling_scp018_at_start` | `1` | Number of SCP-018s given per player in **Super Balling**. |
| `chill_scp244_at_start` | `1` | Number of SCP-244s given per player in **Chill**. |
| `zerg_rush_role_id` | `1` | The role ID all players spawn as in **Zerg Rush**. |
| `one_man_army_scp_role_id` | `16` | The role ID all non-chosen players spawn as in **One Man Army**. |
| `one_man_army_chosen_one_role_id` | `15` | The role ID the Chosen One spawns as in **One Man Army**. |
| `one_man_army_chosen_one_health` | `10000` | Starting HP of the Chosen One in **One Man Army**. |
| `one_man_army_scp_health` | `1800` | Starting HP of each other player in **One Man Army**. |
| `one_man_army_chosen_one_special_weapons_limit` | `2` | How many special weapons can the Chosen One carry in **One Man Army**. |
| `lights_out_scp_health_multiplier` | `0.7` | HP multiplier for SCPs in **Lights Out**. |
| `late_for_shift_should_give_ammo` | `false` | Whether a non-SCP player that recieves a weapon should also get max ammo for that weapon in **Late For Shift**. |

### Custom Round Names

Every Special Round has a configurable display name that appears in broadcasts and the voting screen.

| Key | Default |
|---|---|
| `payday_name` | `Payday` |
| `vitality_shift_name` | `Vitality Shift` |
| `sweet_tooth_name` | `Sweet Tooth` |
| `forest_gump_name` | `Forest Gump` |
| `super_balling_name` | `Super Balling` |
| `chill_name` | `Chill` |
| `zerg_rush_name` | `Zerg Rush` |
| `drug_testing_name` | `Drug Testing` |
| `one_man_army_name` | `One Man Army` |
| `lights_out_name` | `Lights Out` |
| `late_for_shift_name` | `Late For Shift` |

### Broadcasts

Almost all broadcast strings are fully customizable. Several support **placeholders** — special tags that are automatically replaced with live values at the time the broadcast is sent. See the Placeholders section below.

#### Voting Broadcasts

| Key | Default |
|---|---|
| `voting_progress_broadcast` | `Voting for Special Round:\n{round_in_voting}\nYes: {votes_yes}, No: {votes_no}\nTime left: {time_left} seconds.` |
| `round_won_voting_broadcast` | `Vote passed!\nSpecial Round enabled for this round.\n{round_in_voting}\nYes: {votes_yes}, No: {votes_no}, Absent: {votes_absent}` |
| `round_lost_voting_broadcast` | `Vote failed!\nSpecial Round disabled for this round.\n{round_in_voting}\nYes: {votes_yes}, No: {votes_no}, Absent: {votes_absent}` |

#### Round Broadcasts

| Key | Default | Sent when... |
|---|---|---|
| `payday_item_given_broadcast` | `You suddenly feel a little bit richer...` | Player successfully receives coins. |
| `payday_item_not_given_broadcast` | `Seems like you don't have enough place for your paycheck...` | Player's inventory is full. |
| `sweet_tooth_item_given_broadcast` | `Let chaos reign!` | Player successfully receives the candy bag. |
| `sweet_tooth_item_not_given_broadcast` | `Seems like you don't have enough place for sweets...` | Player's inventory is full. |
| `super_balling_item_given_broadcast` | `Time to play catch!` | Player successfully receives SCP-018s. |
| `super_balling_item_not_given_broadcast` | `Seems like you don't have enough place for the ball...` | Player's inventory is full. |
| `chill_item_given_broadcast` | `Freeze 'em all!` | Player successfully receives SCP-244s. |
| `chill_item_not_given_broadcast` | `Seems like you don't have enough place for the chill ghost...` | Player's inventory is full. |
| `vitality_shift_broadcast` | `You feel {health_multiplier} times healthier!!` | Player spawns. |
| `forest_gump_broadcast` | `Run! Run {speed_intensity} times faster than ever before!` | Player spawns. |
| `zerg_rush_broadcast` | `ZERG RUSH!!!` | Player spawns. |
| `drug_testing_broadcast` | `You got: {effect}!` | Player receives their random effect. |
| `one_man_army_chosen_one_broadcast` | `<color=red><b>YOU'RE THE STAR OF THE SHOW NOW, BABY!</b></color>` | Sent to the Chosen One at round start. |
| `one_man_army_scp_broadcast` | `<color=red><b>BRING {chosen_name} DOWN!</b></color>` | Sent to all other players at round start. |
| `lights_out_flashlight_given_broadcast` | `<i>Darkness constricts you...</i>` | On spawn if flashlight was given OR if the player already has a flashlight. |
| `one_man_army_scp_broadcast` | `<i>Darkness constricts you...</i>` | On spawn if flashlight wasn't given. |
| `late_for_shift_on_spawned_broadcast` | `You probably shouldn''t have overslept...` | Player spawns. |

---

## Placeholders

Broadcast strings support placeholders — tags that are replaced with live values when the broadcast is sent. Using an unsupported placeholder in a broadcast string will simply leave it as plain text.

### Voting Placeholders

Available in `voting_progress_broadcast`, `round_won_voting_broadcast`, and `round_lost_voting_broadcast`.

| Placeholder | Replaced with |
|---|---|
| `{round_in_voting}` | Display name of the Special Round being voted on. |
| `{votes_yes}` | Current number of "yes" votes. |
| `{votes_no}` | Current number of "no" votes. |
| `{votes_absent}` | Number of players who haven't voted. |
| `{votes}` | Total number of players who have voted. |
| `{time_left}` | Seconds remaining in the voting window. |

### Round-Specific Placeholders

| Placeholder | Available in | Replaced with |
|---|---|---|
| `{health_multiplier}` | `vitality_shift_broadcast` | The HP multiplier applied to the player. |
| `{speed_intensity}` | `forest_gump_broadcast` | The speed effect intensity. |
| `{zerg_role}` | `zerg_rush_broadcast` | The name of the role all players are set to. |
| `{effect}` | `drug_testing_broadcast` | The name of the random effect the player received. |
| `{chosen_name}` | `one_man_army_scp_broadcast` | The nickname of the Chosen One. |

> Broadcasts support SCP:SL's rich text tags, so you can use `<color=red>`, `<b>`, `<i>`, and other Unity rich text formatting in your broadcast strings.

---

## License

To quote Freddie Mercury:

> *"Do whatever you want with my music, just don't make it boring."*

The same principle also applies to this plugin: everyone is free to share, modify, distribute and use this plugin in any way they wish without notifying me.

However, if you plan on using Customizable Special Rounds on a public server or modify it in a public manner (creating your own repository of it, for example), I'd like to kindly ask you to let me know so that I can add your usage/contribution to my portfolio. You can contact me via discord:

@24th_was_thursday

## Requirements

- SCP: Secret Laboratory (compatible server version)
- [EXILED](https://github.com/ExMod-Team/EXILED) framework
