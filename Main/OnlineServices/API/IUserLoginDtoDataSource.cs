using System;
using System.Threading;
using System.Threading.Tasks;
using LeaderboardsDTO;

namespace OnlineServices.API
{
	// Token: 0x020004B4 RID: 1204
	public interface IUserLoginDtoDataSource
	{
		// Token: 0x06001603 RID: 5635
		Task<LoginRequestDTO> GetLoginRequestDTOAsync(CancellationToken cancellationToken);

		// Token: 0x06001604 RID: 5636
		Task<string[]> GetUserFriendsUserIds(CancellationToken cancellationToken);

		// Token: 0x06001605 RID: 5637
		Task<string> GetPlatformUserIdAsync(CancellationToken cancellationToken);
	}
}
