using System;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

namespace Yarvis.SlashCommands
{
	public class FunSL : ApplicationCommandModule
	{
		[SlashCommand("test", "This is our Slash Command")]
		public async Task TestSlashCommand(InteractionContext ctx, [Choice("Pre-Defined Text","Selam Usta")]
																	[Option("string","Type in anything you want")] string text)
		{
			await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
																							.WithContent("Starting Slash Command ......"));

			var embedMessage = new DiscordEmbedBuilder()
			{
				Title = text
			};

			await ctx.Channel.SendMessageAsync(embed: embedMessage);
		}

		[SlashCommand("poll", "Create your own poll")]
		public async Task PollCommand(InteractionContext ctx, [Option("Question","The main poll subject/question")] string Question,
                                                              [Option("TimeLimit", "The time set on this poll")] long TimeLimit,
                                                              [Option("option1", "Option 1")] long Option1,
                                                              [Option("option2", "Option 2")] long Option2,
                                                              [Option("option3", "Option 3")] long Option3,
                                                              [Option("option4", "Option 4")] long Option4)
		{
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                                                                            .WithContent("Starting Slash Command ......"));

            try
            {
                var interactvity = ctx.Client.GetInteractivity();
                TimeSpan timer = TimeSpan.FromSeconds(TimeLimit);
                DiscordEmoji[] optionEmojis = { DiscordEmoji.FromName(ctx.Client, ":one:", false),
                                             DiscordEmoji.FromName(ctx.Client, ":two:", false),
                                             DiscordEmoji.FromName(ctx.Client, ":three:", false),
                                             DiscordEmoji.FromName(ctx.Client, ":four:", false)};

                string optionsString = optionEmojis[0] + "|" + Option1 + "\n" +
                                       optionEmojis[1] + "|" + Option2 + "\n" +
                                       optionEmojis[2] + "|" + Option3 + "\n" +
                                       optionEmojis[3] + "|" + Option4;

                var pollMessage = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle(string.Join(" ", Question))
                    .WithDescription(optionsString)
                    );
                var putReactOn = await ctx.Channel.SendMessageAsync(pollMessage);

                foreach (var emoji in optionEmojis)
                {
                    await putReactOn.CreateReactionAsync(emoji);
                }

                var result = await interactvity.CollectReactionsAsync(putReactOn, timer);

                int count1 = 0;
                int count2 = 0;
                int count3 = 0;
                int count4 = 0;

                foreach (var emoji in result)
                {
                    if (emoji.Emoji == optionEmojis[0])
                    {
                        count1++;
                    }
                    if (emoji.Emoji == optionEmojis[1])
                    {
                        count2++;
                    }
                    if (emoji.Emoji == optionEmojis[2])
                    {
                        count3++;
                    }
                    if (emoji.Emoji == optionEmojis[3])
                    {
                        count4++;
                    }
                }



                int totalVotes = count1 + count2 + count3 + count4;

                string resultsString = optionEmojis[0] + "|" + count1 + " Votes \n" +
                                      optionEmojis[1] + "|" + count2 + "Votes \n" +
                                      optionEmojis[2] + "|" + count3 + "Votes \n" +
                                      optionEmojis[3] + "|" + count4 + "Votes \n\n" +
                                      "The total number of votes is " + totalVotes;

                var resultsMesage = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Green)
                    .WithTitle("Results of Poll")
                    .WithDescription(resultsString)
                );
                await ctx.Channel.SendMessageAsync(resultsMesage);
            }
            catch (Exception ex)
            {
                var errorMsg = new DiscordEmbedBuilder()
                {
                    Title = "Something Went Wrong",
                    Description = ex.Message,
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: errorMsg);
            }
        }

        [SlashCommand("caption","Give any image a Caption")]
        public async Task CaptionCommand(InteractionContext ctx, [Option("caption","The caption you want the image")] string caption,
                                                                 [Option("image", "The image you want to upload")] DiscordAttachment picture)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                                                                                            .WithContent("......"));

            var captionMessage = new DiscordEmbedBuilder()
            {
                Title = caption,
                ImageUrl = picture.Url,
                Color = DiscordColor.Azure
            };

            await ctx.Channel.SendMessageAsync(embed: captionMessage);
        }
    }
}