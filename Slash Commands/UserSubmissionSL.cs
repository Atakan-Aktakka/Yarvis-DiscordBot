using System;
using DSharpPlus;
using OpenAI_API;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

namespace Yarvis.SlashCommands
{
	public class UserSubmissionSL : ApplicationCommandModule
    {
		[SlashCommand("create-VC","Create a voice channel")]
		public async Task CreateVc(InteractionContext ctx, [Option("channle_name","The Channel name")] string channelName,
														   [Option("member-limit","Add a user limit to this voice Channel")]string channelLimit = null)
		{
			await ctx.DeferAsync();

			var channelUserParse = int.TryParse(channelLimit, out var channelCount);

			if(channelLimit != null && channelUserParse == true)
			{
				await ctx.Guild.CreateVoiceChannelAsync(channelName,null,null, channelCount);

				var successMsg = new DiscordEmbedBuilder()
				{
					Title = "Created voice channel" + channelName,
					Description = "The channel was created with a limit of " + channelCount + "users",
					Color = DiscordColor.Azure
				};

				await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(successMsg));

			}else if (channelLimit == null)
			{
				await ctx.Guild.CreateVoiceChannelAsync(channelName);

				var successMsg = new DiscordEmbedBuilder()
				{
					Title = "Created voice channel" + channelName,
					Color = DiscordColor.Azure
				};

				await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(successMsg));
			}else if (channelUserParse == false)
			{
				var failedMsg = new DiscordEmbedBuilder()
				{
                    Title = "Please provide a valid number fot the user limit",
					Color = DiscordColor.Red
                };

				await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(failedMsg));
			}
		}

		[SlashCommand("chat-gpt","Ask ChatGPT a question")]
		public async Task ChatGPT(InteractionContext ctx, [Option("query","What you want to ask ChatGPT")]string query)
		{
			await ctx.DeferAsync();

            var api = new OpenAIAPI("chat-gpt api token");

			var chat = api.Chat.CreateConversation();
			chat.AppendSystemMessage("Type in a query");

			chat.AppendUserInput(query);

			string response = await chat.GetResponseFromChatbot();

			var outputEmbed = new DiscordEmbedBuilder()
			{
				Title = "Results to: " + query,
				Description = response,
				Color = DiscordColor.Green
			};

			await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(outputEmbed));
		}
	}
}

