using System;
using GGroupp.Platform;

namespace GGroupp.Infra.Bot.Builder;

using IDataverseUserGetFunc = IAsyncValueFunc<DataverseUserGetIn, Result<DataverseUserGetOut, Failure<DataverseUserGetFailureCode>>>;
using IBotUserGetFunc = IAsyncValueFunc<AzureUserGetOut, Result<BotUser, BotFlowFailure>>;

internal sealed partial class BotDataverseUserGetFunc : IBotUserGetFunc
{
    private const string UserNotFoundFailureMessage
        =
        "Пользователь не найден. Возможно у вас нет прав для доступа в систему";

    private const string UnexpectedFailureMessage
        =
        "Возникла непредвиденная ошибка при попытке обращения в Dataverse. Повторите попытку позже или обратитесь к администратору";

    private readonly IDataverseUserGetFunc dataverseUserGetFunc;

    internal BotDataverseUserGetFunc(IDataverseUserGetFunc dataverseUserGetFunc)
        =>
        this.dataverseUserGetFunc = dataverseUserGetFunc;
}