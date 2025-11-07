using System;

namespace OnlineServices.API
{
	// Token: 0x020004B3 RID: 1203
	public readonly struct ApiResponse<T>
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x000105CB File Offset: 0x0000E7CB
		public bool isError
		{
			get
			{
				return this.response > Response.Success;
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x000105D6 File Offset: 0x0000E7D6
		public ApiResponse(Response response, T responseDto)
		{
			this.response = response;
			this.responseDto = responseDto;
		}

		// Token: 0x04001669 RID: 5737
		public readonly Response response;

		// Token: 0x0400166A RID: 5738
		public readonly T responseDto;
	}
}
