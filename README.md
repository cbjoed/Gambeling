# GamblingBot

A sophisticated Discord bot written in C# that enables gambling and betting mechanics on Discord servers. Users can place bets, track balances, and compete on leaderboards with persistent data storage.

## 🎮 Features

- **Balance System**: Each user starts with 100 coins and their balance persists in JSON storage
- **Betting Mechanic**: Simple 50/50 betting system - win or lose your wager
- **Leaderboard**: View the top 3 richest players with `!leaderboard`
- **Admin Controls**: Server owner can distribute coins using the `!give` command
- **Discord Integration**: Seamless integration with Discord.Net API
- **Data Persistence**: All balances automatically saved to JSON file

## 🛠️ Tech Stack

- **Language**: C# (.NET 8.0)
- **Framework**: Discord.Net 3.17.1
- **Deployment**: Docker
- **Data Storage**: JSON (local file)
- **Platform**: Discord API

## 📋 Commands

| Command | Description | Example |
|---------|-------------|---------|
| `!balance` | Check your current coin balance | `!balance` |
| `!bet <amount>` | Place a 50/50 bet | `!bet 50` |
| `!leaderboard` | View top 3 richest players | `!leaderboard` |
| `!give @user <amount>` | Admin: Give coins to a user | `!give @john 100` |

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK
- Discord.Net NuGet package (automatically restored)
- A Discord bot token and server setup

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/gambeling.git
   cd gambeling
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure your bot**
   - Open `Program.cs`
   - Replace `TOKEN` with your Discord bot token
   - Set `allowedChannelId` to your Discord channel ID
   - Set `ownerId` to your Discord user ID

4. **Build the project**
   ```bash
   dotnet build
   ```

5. **Run the bot**
   ```bash
   dotnet run
   ```

### Docker Deployment

Build and run the bot using Docker:

```bash
docker build -t gamblingbot .
docker run --rm gamblingbot
```

## 💾 Data Storage

- User balances are stored in `balances.json`
- Persists automatically after each transaction
- JSON format for easy inspection and backup

## 🎯 How It Works

1. Users join a Discord channel where the bot is active
2. Each user receives an initial balance of 100 coins
3. Users can check their balance with `!balance`
4. Users place bets using `!bet <amount>`
5. Bot randomly determines win/loss (50/50 chance)
6. Balances update and persist to JSON
7. Players compete for top positions on `!leaderboard`
8. Server owner can distribute coins as needed with `!give`

## 🔒 Security Notes

- Never commit your bot token to version control
- Use environment variables for sensitive configuration in production
- Only the server owner can use admin commands

## 📝 Project Structure

```
GamblingBot/
├── Program.cs           # Main bot logic and command handlers
├── GamblingBot.csproj   # Project configuration
├── balances.json        # Persisted user balances
└── notes.txt           # Development notes
```

## 🤝 Contributing

Feel free to fork this project and submit pull requests for any improvements.

## 📄 License

This project is open source and available under the MIT License.

## 🔗 Live Demo

Visit the bot showcase: [https://gamblingbot.cbjoed.com](https://gamblingbot.cbjoed.com)

---

**Built with ❤️ by [Your Name]** | [Back to Portfolio](https://www.cbjoed.com)
