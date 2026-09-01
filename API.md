# GamblingBot API Documentation

## Overview

GamblingBot is a Discord bot that manages a gambling economy. This document outlines the bot's internal structure and key functions.

## Data Model

### User Balance
```json
{
  "user_id": 12345,
  "balance": 100,
  "last_updated": "2024-01-15T10:30:00Z"
}
```

### Balance Storage
- **File**: `balances.json`
- **Format**: Dictionary<ulong, int>
- **Key**: Discord User ID
- **Value**: Balance in coins

## Command Structure

### !balance
- **Purpose**: Check user's current balance
- **Parameters**: None
- **Response**: Display user's coin balance
- **Database Changes**: None

### !bet <amount>
- **Purpose**: Place a bet
- **Parameters**: 
  - `amount` (int): Coins to wager
- **Mechanics**:
  - 50% chance to win (double the bet)
  - 50% chance to lose (lose the bet)
  - Minimum: 1 coin
  - Maximum: User's current balance
- **Response**: Win/lose message with new balance
- **Database Changes**: Update user balance

### !leaderboard
- **Purpose**: Display top players
- **Parameters**: None
- **Response**: Top 3 users by balance
- **Database Changes**: None

### !give @user <amount>
- **Purpose**: Admin transfer coins
- **Parameters**:
  - `@user`: Mention of recipient
  - `amount` (int): Coins to transfer
- **Permissions**: Owner only
- **Response**: Confirmation message
- **Database Changes**: Update both user balances

## Key Functions

### LoadBalances()
Loads balance data from JSON file into memory dictionary.

### SaveBalances()
Persists current balance dictionary to JSON file.

### HandleCommandAsync()
Main command router - processes all Discord messages and delegates to appropriate command handlers.

## Configuration

### Required Settings (Program.cs)

```csharp
TOKEN = "your_bot_token_here"
allowedChannelId = 0000000000000000000 // Channel ID
ownerId = 0000000000000000000 // Owner Discord ID
```

## Error Handling

- Invalid amount: "You don't have enough coins!"
- Unauthorized: "You don't have permission to use this command."
- Invalid format: Command is ignored

## Performance Considerations

- JSON I/O happens after every bet (SaveBalances)
- In-memory dictionary for fast lookups
- No database backend required

## Future Enhancements

- Database backend (SQLite/PostgreSQL)
- Scheduled resets
- Daily login bonuses
- Item shop system
- Multiplayer games
- Admin dashboard

---

For more information, see [README.md](README.md)
