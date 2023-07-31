using System;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Yarvis.ExternalClasses;

namespace Yarvis.Commands
{
	public class GameCommands:BaseCommandModule
	{
		[Command("cardgame")]
		public async Task SimpleCardGame(CommandContext ctx)
		{
			var UserCard = new CardBuilder();
            var BotCard = new CardBuilder();

            var userCardMessage = new DiscordMessageBuilder()
				.AddEmbed(new DiscordEmbedBuilder()
				.WithColor(DiscordColor.Azure)
				.WithTitle("Your Card")
				.WithDescription("You drew a: " + UserCard.SelectedCard)
				);

			await ctx.Channel.SendMessageAsync(userCardMessage);

			var botCardMessage = new DiscordMessageBuilder()
				.AddEmbed(new DiscordEmbedBuilder()
				.WithColor(DiscordColor.Azure)
                .WithTitle("Bot Card")
                .WithDescription("You drew a: " + BotCard.SelectedCard)
                );

			await ctx.Channel.SendMessageAsync(botCardMessage);

            if (UserCard.SelectedNumber > BotCard.SelectedNumber)
			{
				var winningMessage = new DiscordEmbedBuilder()
				{
					Title = "** You Win The game **",
					Color = DiscordColor.Green
				};

				await ctx.Channel.SendMessageAsync(embed: winningMessage);
			}
			else 
			{
				var losingMessage = new DiscordEmbedBuilder()
				{
                    Title = "** You Lost The game **",
                    Color = DiscordColor.DarkRed
                };
                await ctx.Channel.SendMessageAsync(embed: losingMessage);

            }
        }
	}
}