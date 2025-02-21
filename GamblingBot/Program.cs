using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

class Program
{
    private DiscordSocketClient _client;
    private Dictionary<ulong, int> _balances = new();
    private Random _rand = new();
    private const string TOKEN = "MTM0MjU3NDAxMTY0MDU3ODA5MA.GF1KGi.PSSTldm2uSVZhOH56Jcr1kxZlMPSO035rsbKlQ";
    private readonly ulong allowedChannelId = 1342577313510068235; // Erstat med din kanal-ID
    private readonly ulong ownerId = 354681165568933888; // Erstat med dit eget Discord bruger-ID
    private const string BalanceFile = "balances.json"; // Fil til at gemme balancer

    static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

    public async Task RunBotAsync()
    {
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.GuildMessages | GatewayIntents.MessageContent | GatewayIntents.Guilds
        });

        _client.Log += Log;
        _client.MessageReceived += HandleCommandAsync;

        LoadBalances(); // Indlæser balance fra fil

        await _client.LoginAsync(TokenType.Bot, TOKEN);
        await _client.StartAsync();
        await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine($"[Discord Log] {msg}");
        return Task.CompletedTask;
    }

    private async Task HandleCommandAsync(SocketMessage message)
    {
        if (message.Author.IsBot || message.Channel.Id != allowedChannelId) return;

        Console.WriteLine($"Modtaget besked: {message.Content} fra {message.Author.Username}");

        string[] args = message.Content.Split(' ');
        string command = args[0].ToLower();
        ulong userId = message.Author.Id;

        if (!_balances.ContainsKey(userId))
            _balances[userId] = 100; // Startbalance

        if (command == "!balance")
        {
            await message.Channel.SendMessageAsync($"{message.Author.Mention}, din saldo er {_balances[userId]} mønter.");
        }
        else if (command == "!bet" && args.Length == 2 && int.TryParse(args[1], out int betAmount))
        {
            if (_balances[userId] < betAmount)
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention}, du har ikke nok mønter!");
                return;
            }

            bool win = _rand.Next(2) == 0;
            if (win)
            {
                _balances[userId] += betAmount;
                await message.Channel.SendMessageAsync($"{message.Author.Mention}, du vandt {betAmount} mønter! 🎉");
            }
            else
            {
                _balances[userId] -= betAmount;
                await message.Channel.SendMessageAsync($"{message.Author.Mention}, du tabte {betAmount} mønter. 😢");
            }

            SaveBalances();
        }
        else if (command == "!daily")
        {
            _balances[userId] += 50;
            SaveBalances();
            await message.Channel.SendMessageAsync($"{message.Author.Mention}, du har modtaget dine daglige 50 mønter!");
        }
        else if (command == "!leaderboard")
        {
            var topPlayers = _balances.OrderByDescending(x => x.Value).Take(3);
            string leaderboard = "**Top 3 rigeste spillere:**\n" +
                string.Join("\n", topPlayers.Select(x => $"<@{x.Key}>: {x.Value} mønter"));
            await message.Channel.SendMessageAsync(leaderboard);
        }
        else if (command == "!give" && args.Length == 3 && message.MentionedUsers.Count > 0 && int.TryParse(args[2], out int amount))
        {
            if (userId != ownerId)
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention}, du har ikke tilladelse til at bruge denne kommando.");
                return;
            }

            var recipient = message.MentionedUsers.First();
            ulong recipientId = recipient.Id;

            if (_balances[userId] < amount || amount <= 0)
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention}, du har ikke nok mønter eller prøver at sende et ugyldigt beløb.");
                return;
            }

            _balances[userId] -= amount;
            if (!_balances.ContainsKey(recipientId))
                _balances[recipientId] = 100;

            _balances[recipientId] += amount;
            SaveBalances();

            await message.Channel.SendMessageAsync($"{message.Author.Mention} gav {amount} mønter til {recipient.Mention}!");
        }
        else if (command == "!reset" && message.MentionedUsers.Count > 0)
        {
            if (userId != ownerId)
            {
                await message.Channel.SendMessageAsync($"{message.Author.Mention}, du har ikke tilladelse til at bruge denne kommando.");
                return;
            }

            var targetUser = message.MentionedUsers.First();
            ulong targetId = targetUser.Id;

            _balances[targetId] = 100;
            SaveBalances();
            await message.Channel.SendMessageAsync($"{targetUser.Mention}s saldo er blevet nulstillet til 100 mønter.");
        }
    }

    private void SaveBalances()
    {
        try
        {
            string json = JsonSerializer.Serialize(_balances, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(BalanceFile, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved gemning af balances: {ex.Message}");
        }
    }

    private void LoadBalances()
    {
        if (File.Exists(BalanceFile))
        {
            try
            {
                string json = File.ReadAllText(BalanceFile);
                _balances = JsonSerializer.Deserialize<Dictionary<ulong, int>>(json) ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved indlæsning af balances: {ex.Message}");
            }
        }
    }
}
