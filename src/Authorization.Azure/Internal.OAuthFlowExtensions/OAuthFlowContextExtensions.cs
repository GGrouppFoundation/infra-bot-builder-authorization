using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;

namespace GGroupp.Infra.Bot.Builder;

internal static partial class OAuthFlowExtensions
{
    private static readonly IReadOnlyCollection<string> notSupportedChannles;

    static OAuthFlowExtensions()
        =>
        notSupportedChannles = new[]
        {
            Channels.Cortana,
            Channels.Skype,
            Channels.Skypeforbusiness
        };

    private static bool IsChannelNotSupported(this ITurnContext turnContext)
        =>
        notSupportedChannles.Contains(turnContext.Activity.ChannelId, StringComparer.InvariantCultureIgnoreCase);

    private static Result<UserTokenClient, BotFlowFailure> GetUserTokenClientOrFailure(this IOAuthFlowContext context, BotAuthorizationOption option)
    {
        var userTokenClient = context.TurnState.Get<UserTokenClient>();
        if (userTokenClient != null)
        {
            return Result.Success(userTokenClient);
        }

        return new BotFlowFailure(
            userMessage: option.UnexpectedFailureMessage,
            logMessage: "UserTokenClient must be specified in the turn state");
    }

    private static IStatePropertyAccessor<ConversationReference?> CreateConversationReferenceAccessor(this IBotContext botContext)
        =>
        botContext.UserState.CreateProperty<ConversationReference?>("__conversationReference");
}