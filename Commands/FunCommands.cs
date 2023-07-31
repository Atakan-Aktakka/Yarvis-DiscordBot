using System;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using Yarvis.LevelSystem;
using System.IO;
using System.Collections.Generic;

namespace Yarvis.Commands
{
	public class FunCommands:BaseCommandModule
	{
		[Command("message")]
        [Cooldown(5,10,CooldownBucketType.User)]
		public async Task TestCommand(CommandContext ctx)
		{
			await ctx.Channel.SendMessageAsync("Hello");
		}
   
        [Command("embedmessage")]
        public async Task SendEmbedMessage(CommandContext ctx)
        {
            var embedMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithTitle("This is a Title")
                .WithDescription("This is the description")
                .WithColor(DiscordColor.Azure)
                );
            await ctx.Channel.SendMessageAsync(embedMessage);
        }

        [Command("embedmessage2")]
        //[RequireRoles(RoleCheckMode.MatchNames, "Ekip")]
        public async Task SendEmbedMessage1(CommandContext ctx)
        {
            
                var embedMessage = new DiscordEmbedBuilder()
                {
                    Title = "This is a title",
                    Description = "This is a description",
                    Color = DiscordColor.Azure,
                };
                await ctx.Channel.SendMessageAsync(embed:embedMessage);
           
        }

        /*
        [Command("add")]
        public async Task Addition(CommandContext ctx,int number1, int number2)
        {
			int answer = number1 + number2;
			await ctx.Channel.SendMessageAsync(answer.ToString());
        }

        [Command("substract")]
        public async Task Substract(CommandContext ctx, int number1, int number2)
        {
            int answer = number1 - number2;
            await ctx.Channel.SendMessageAsync(answer.ToString());
        }*/

        [Command("restrictions")]

        [RequireRoles(RoleCheckMode.MatchNames,"Enter your roles here")]

        [RequireBotPermissions(DSharpPlus.Permissions.Administrator,true)]

        [RequireOwner] 

        public async Task CommandRestrictionExamples(CommandContext ctx)
        {
            if(ctx.Guild.Id == 711166342568345641)
            {
                //
            }

            if (ctx.Channel.Id == 711166342568345641)
            {
                //
            }

            if (ctx.Channel.IsNSFW == true)
            {
                //
            }

            if (ctx.User.Id == 711166342568345641 || ctx.User.Username=="Enter Your User Name Here")
            {
                //
            }
        }

        [Command("poll")]
        public async Task PollCommand(CommandContext ctx,int TimeLimit, string Option1, string Option2, string Option3, string Option4, params string[] Question)
        {
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
            catch(Exception ex)
            {
                var errorMsg = new DiscordEmbedBuilder()
                {
                    Title="Something Went Wrong",
                    Description=ex.Message,
                    Color=DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: errorMsg);
            }
           
        }

        [Command("button")]
        public async Task ButtonExample(CommandContext ctx)
        {
            DiscordButtonComponent button1 = new DiscordButtonComponent(ButtonStyle.Primary, "1", "Button 1");
            DiscordButtonComponent button2 = new DiscordButtonComponent(ButtonStyle.Primary, "2", "Button 2");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("This is a message with buttons")
                .WithDescription("Please select a button")
                )
                .AddComponents(button1)
                .AddComponents(button2);

            await ctx.Channel.SendMessageAsync(message);
        }

        [Command("help")]
        public async Task HelpCommand(CommandContext ctx)
        {
            var funButton = new DiscordButtonComponent(ButtonStyle.Success, "funButton", "Fun");
            var gameButton = new DiscordButtonComponent(ButtonStyle.Success, "gameButton", "Games");

            var helpMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("Help Menu")
                .WithDescription("Please pick a button for more information on the commands")
                )
                .AddComponents(funButton,gameButton);

            await ctx.Channel.SendMessageAsync(helpMessage);
        }

        [Command("profile")]
        public async Task ProfileCommand(CommandContext ctx)
        {
            string username = ctx.User.Username;
            ulong guildID = ctx.Guild.Id;

            var userDetails = new DUser()
            {
                UserName = ctx.User.Username,
                guildID = ctx.Guild.Id,
                avatarURL = ctx.User.AvatarUrl,
                Level = 1,
                XP = 0
            };

            var levelEngine = new LevelEngine();
            var doesExist = levelEngine.CheckUserExists(username, guildID);

            if(doesExist == false)
            {
                var isStored =levelEngine.StoreUserDetails(userDetails);
                if(isStored == true)
                {
                    var successMessage = new DiscordEmbedBuilder()
                    {
                        Title = "Succesfully created profile",
                        Description = "Please execute !profile again to view profile",
                        Color = DiscordColor.Green
                    };

                    await ctx.Channel.SendMessageAsync(embed: successMessage);

                    var pulledUser = levelEngine.GetUser(username, guildID);

                    var profile = new DiscordMessageBuilder()
                        .AddEmbed(new DiscordEmbedBuilder()
                        .WithColor(DiscordColor.Aquamarine)
                        .WithTitle(pulledUser.UserName + "'s Profile")
                        .WithThumbnail(pulledUser.avatarURL)
                        .AddField("Level", pulledUser.Level.ToString(),true)
                        .AddField("XP", pulledUser.XP.ToString(),true)
                        );

                    await ctx.Channel.SendMessageAsync(profile);
                }
                else
                {
                    var failedMessage = new DiscordEmbedBuilder()
                    {
                        Title = "Something went wrong when creating your profile",
                        Color = DiscordColor.Red
                    };
                    await ctx.Channel.SendMessageAsync(embed: failedMessage);
                }
            }
            else
            {
                var pulledUser = levelEngine.GetUser(username,guildID);

                var profile = new DiscordMessageBuilder()
                       .AddEmbed(new DiscordEmbedBuilder()
                       .WithColor(DiscordColor.Aquamarine)
                       .WithTitle(pulledUser.UserName + "'s Profile")
                       .WithThumbnail(pulledUser.avatarURL)
                       .AddField("Level", pulledUser.Level.ToString(), true)
                       .AddField("XP", pulledUser.XP.ToString(), true)
                       );

                await ctx.Channel.SendMessageAsync(profile);
            }
        }
    }
}