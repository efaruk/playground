using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnlockedStateProvider.Redis
{
	public class RedisUnlockedStateUsageAttribute : UnlockedStateUsageAttribute
	{
		private readonly IUnlockedStateStore _unlockedStateStore = new RedisUnlockedStateStore();
		protected override IUnlockedStateStore UnlockedStateStore
		{
			get { return _unlockedStateStore; }
		}
	}
}
