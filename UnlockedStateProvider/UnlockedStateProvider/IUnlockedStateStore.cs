using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UnlockedStateProvider {
	public interface IUnlockedStateStore : IDisposable
	{
		///// <summary>
		///// Public constructor.
		///// </summary>
		///// <returns></returns>
		//IUnlockedStateStore UnlockedStore();

		IUnlockedStateStore GetStoreFromContext(HttpContextBase controllerContext);

		//Dictionary<string, object> GetContextItems(HttpContextBase controllerContext);

		void SetContextItems(HttpContextBase controllerContext, Dictionary<string, object> items);

		/// <summary>
		/// Supports Auto sliding expiration or not.
		/// </summary>
		bool AutoSlidingSupport { get; }


		/// <summary>
		/// Supports asynchronous operations or not.
		/// </summary>
		bool AsyncSupport { get; }

		///// <summary>
		/////  Update context object with <see cref="Items" />
		///// </summary>
		//void UpdateContext();

		// Context shared items
		Dictionary<string, object> Items { get; set; }

		// Context shared items
		object this[string name] { get; set; }

		/// <summary>
		/// Clear item collection not the custom state items
		/// </summary>
		void ClearItems();

		/// <summary>
		/// Returns object from store with specified key.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="slide"></param>
		/// <param name="slideAsync"></param>
		/// <returns></returns>
		object Get(string key, bool slide = true, bool slideAsync = true);

		/// <summary>
		/// Set or add object to store with specified key and expireTime.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="expireTime"></param>
		/// <param name="async"></param>
		/// <returns></returns>
		bool Set(string key, object value, TimeSpan expireTime, bool async = false);

		/// <summary>
		/// Delete object from store with specified key.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="async"></param>
		/// <returns></returns>
		bool Delete(string key, bool async = false);

		/// <summary>
		/// Evaluate script on store.
		/// </summary>
		/// <param name="script"></param>
		/// <param name="async"></param>
		/// <returns></returns>
		object Eval(string script, bool async = false);

		/// <summary>
		/// Slide forward expire date as given expireTime for all object which has given prefix.
		/// </summary>
		/// <param name="prefix"></param>
		/// <param name="expireTime"></param>
		/// <param name="async"></param>
		/// <returns></returns>
		void Slide(string prefix, TimeSpan expireTime, bool async = false);

		/// <summary>
		/// Delete all objects from store which has given prefix.
		/// </summary>
		/// <param name="prefix"></param>
		/// <param name="async"></param>
		void DeleteStartsWith(string prefix, bool async = false);


	}
}
