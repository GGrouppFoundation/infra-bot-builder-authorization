using System;
using System.Linq;

namespace GGroupp.Infra.Bot.Builder;

partial class DataverseUserExtensions
{
    internal static Optional<Guid> GetDataverseUserIdOrAbsent(this BotUser user)
        =>
        user.Claims.GetValueOrAbsent(DataverseUserIdClaimName).Map(Guid.Parse);
}